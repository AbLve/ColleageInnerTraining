using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Common;
using ColleageInnerTraining.Core;
using ColleageInnerTraining.Web.Auth;
using ColleageInnerTraining.Web.Controllers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 班级部门岗位管理
    /// </summary>
    public class ClassDepJobController : BaseController
    {
        private const string ex = "GZSXY.Pages.Manage.Classes.ClassDepJob.";

        #region 注入服务
        private IClassDepJobAppService cdjService;
        private IClassesInfoAppService classService;
        private IClassUserAppService classUserService;
        private ICourseInfoAppService courseservic;
        private ICourseBoundConfigureTypeAppService cbtservic;
        private ICourseBoundPersonnelAppService cpservic;
        public ClassDepJobController(IClassesInfoAppService _classService, IClassDepJobAppService _cdjService, IClassUserAppService _classUserService, ICourseBoundPersonnelAppService _cpservic,
                                     IJobPostAppService _jobService, IDepartmentInfoAppService _departmentService, ICourseBoundConfigureTypeAppService _cbtservic,
                                     IUserAccountAppService _userService, ISqlExecuter _sqlExecuter) : base(_departmentService, _jobService, _sqlExecuter, _userService)
        {
            cdjService = _cdjService;
            classService = _classService;
            classUserService = _classUserService;
            cbtservic = _cbtservic;
            cpservic = _cpservic;

        }
        #endregion

        #region 操作
        /// <summary>
        /// index页面
        /// </summary>
        /// <param name="cId"></param>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex + "Index")]
        [Route("/ClassDepJob/Index")]
        public ActionResult Index(int cId)
        {
            BindJSel();
            return View();
        }

        [Route("/ClassDepJob/GetDataList")]
        public ActionResult GetDataList(int pIndex = 1, int cId = 0)
        {
            ViewBag.pageName = "GetDataList";
            var pagedata = cdjService.GetPagedClassDepJobs(new GetClassDepJobInput { cId = cId });
            GetPageData(pagedata.TotalCount);
            return PartialView("Shared/DataList", pagedata.Items);
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="cId"></param>
        /// <param name="type"></param>
        /// <param name="bId"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorizeAttribute(ex + "Set")]
        [Route("/ClassDepJob/Set")]
        public ActionResult Set(int cId, int type, int bId)
        {
            var classDate = classService.GetClassesInfoById(new EntityDto<long>() { Id = cId });//班级数据
            var create = new ClassDepJobEditDto();//实例化对象Bound对象            
            create.ClassId = Convert.ToInt32(classDate.Id);
            create.ClassName = classDate.ClassesName;
            create.type = type;

            var edit = classService.GetClassesInfoForEdit(new NullableIdDto<long> { Id = create.ClassId }).ClassesInfo;//更新人数对象
            switch (type)
            {
                case (int)ConfigureType.Department:

                    if (IsChildren(cId, bId))
                    {
                        return Warn("已经有父级部门绑定了班级");
                    }
                    IsParent(cId, bId);
                    SetForDepart(edit, create, bId, cId);
                    break;
                case (int)ConfigureType.Post:
                    SetForPost(edit, create, bId, cId);
                    break;
            }

            return Success();
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorizeAttribute(ex + "Remove")]
        [Route("/ClassDepJob/Remove")]
        public ActionResult Remove(int Id)
        {
            var boundData = cdjService.GetClassDepJobById(new EntityDto<long> { Id = Id });//绑定的数据
            if (boundData != null)
            {
                cdjService.DeleteClassDepJob(new EntityDto<long>() { Id = Convert.ToInt32(boundData.Id) });

                //只能移除部门和岗位不重复的人
                var userids = UnRepeatUser(boundData);
                foreach (var item in userids)
                {
                    var sqlstr = "update px_class_user set removed=1 where class_id=@class_id and user_id=@user_id";
                    var i = sqlExecuter.Execute(sqlstr, new MySqlParameter("@class_id", boundData.ClassId), new MySqlParameter("@user_id", item));
                    //判断课程班级表有当前班级没用如果有要更新课程人员表保持一致和手机签到一样
                    var dto = cbtservic.GetAll().FirstOrDefault(t => t.BusinessId == boundData.ClassId && t.type == 2);
                    if (dto != null && dto.type != 4)
                    {
                        cpservic.DeleteCourseBoundPersonnel(new EntityDto<long> { Id = dto.Id });
                    }
                }
                //更新班级成员数量
                var edit = classService.GetClassesInfoForEdit(new NullableIdDto<long> { Id = boundData.ClassId }).ClassesInfo;
                var count = classUserService.GetAll().Where(t => t.ClassId == boundData.ClassId).Count();
                edit.MemberCount = count;
                classService.UpdateClassesInfoAsync(edit);

            }
            return Success();
        }

        /// <summary>
        /// 设置部门
        /// </summary>
        /// <param name="edit"></param>
        /// <param name="create"></param>
        /// <param name="bId"></param>
        /// <param name="cId"></param>
        protected void SetForDepart(ClassesInfoEditDto edit, ClassDepJobEditDto create, int bId, int cId)
        {
            IsParent(cId, bId);
            //不重复人数
            var cuserids = new List<int>();//班级当前人数
            var department = departmentService.GetAllDepartmentInfos().FirstOrDefault(t => t.DepartmentId == bId);
            if (department != null)
            {
                create.BusinessId = bId;
                create.BusinessName = department.DisplayName;
                //获取当前子部门及自己的部门id
                var dids = departmentService.GetAllDepartmentInfos().Where(t => t.Path.Contains(department.Path)).Select(t => t.DepartmentId);

                //获取人员表当前选中部门ids的总人数
                var userids = userService.GetAll().Where(t => dids.Contains(t.DepartmentID)).Select(t => t.SysNO).ToList();

                foreach (var item in userids)
                {
                    //班级中已绑定的人员id
                    cuserids = classUserService.GetAll().Where(t => t.ClassId == cId).Select(t => t.UserId).ToList();
                    if (!cuserids.Contains(item))
                    {
                        classUserService.CreateClassUser(new ClassUserEditDto { ClassId = cId, UserId = item });
                    }
                    //判断课程班级表有当前班级没用如果有要更新课程人员表保持一致和手机签到一样
                    var dto = cbtservic.GetAll().FirstOrDefault(t => t.BusinessId == cId && t.type == 2);
                    if (dto != null && dto.type != 4)
                    {
                        cpservic.CreateCourseBoundPersonnel(new CourseBoundPersonnelEditDto { CourseId = dto.CourseId, AccountSysNo = item, CheckIN = false, IsBound = true });
                    }
                }
                cdjService.CreateClassDepJob(create);
            }
        }
        /// <summary>
        /// 父部门是否已绑定 绑定就跳过
        /// </summary>
        /// <param name="cId"></param>
        /// <param name="bId"></param>
        /// <returns></returns>
        public bool IsChildren(int cId, int bId)
        {
            //班级所有绑定的部门数据
            var alreadylist = cdjService.GetPagedClassDepJobs(new GetClassDepJobInput { cId = cId }).Items.Where(t => t.type == 0).Select(t => t.BusinessId).ToList();
            var ischildren = false;//当前设置部门是已设置部门的子部门
            var didparnts = new List<string>();
            //获取设置部门的path从而得到所有父id
            var path = departmentService.GetAllDepartmentInfos().FirstOrDefault(t => t.DepartmentId == bId).Path;
            if (path.Contains("/"))
            {
                didparnts = path.Split('/').ToList();
                didparnts.Remove(didparnts.Last());
                var intids = new List<int>();//当前部们的父部门ids及自己
                didparnts.ForEach(t => intids.Add(int.Parse(t)));

                foreach (var item in intids)
                {
                    ischildren = alreadylist.Contains(item);
                    if (ischildren)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 子部们是否已绑定 绑定就删掉再重新绑定为父部门
        /// </summary>
        /// <param name="cId"></param>
        /// <param name="bId"></param>
        /// <returns></returns>
        public void IsParent(int cId, int bId)
        {
            var flag = false;
            //班级所有绑定的部门数据
            var alreadylist = cdjService.GetPagedClassDepJobs(new GetClassDepJobInput { cId = cId }).Items.Where(t => t.type == 0).Select(t => t.BusinessId).ToList();
            var alreadyDId = 0;
            foreach (var item in alreadylist)
            {
                var dto = departmentService.GetAllDepartmentInfos().FirstOrDefault(t => t.DepartmentId == item);
                if (dto.Path.Contains(bId + "/"))//已绑定到班级的部门中如果有一个的path完全包含当前要设置的部门那么当前部门为父部门并删掉原来绑定得子部门关系
                {
                    alreadyDId = dto.DepartmentId;
                    flag = true;
                    break;
                }
            }
            //删掉原来绑定得子部门关系
            if (flag)
            {
                var cdjdto = cdjService.GetCDJByCIdOrTypeId(cId, 0).FirstOrDefault(t => t.BusinessId == alreadyDId);
                cdjService.DeleteClassDepJob(new EntityDto<long> { Id = cdjdto.Id });
            }
        }
        /// <summary>
        /// 设置岗位
        /// </summary>
        /// <param name="edit"></param>
        /// <param name="create"></param>
        /// <param name="bId"></param>
        /// <param name="cId"></param>
        protected void SetForPost(ClassesInfoEditDto edit, ClassDepJobEditDto create, int bId, int cId)
        {
            var cuserids = new List<int>();//班级当前人数
            var post = jobPostService.GetJobPostById(new EntityDto<long>() { Id = bId });
            if (post != null)
            {
                create.BusinessId = (int)post.Id;
                create.BusinessName = post.Name;

                //获取人员表当前选中岗位id的总人数
                var userids = userService.GetAll().Where(t => t.PostID == bId).Select(t => t.SysNO).ToList();
                foreach (var item in userids)
                {
                    cuserids = classUserService.GetAll().Where(t => t.ClassId == cId).Select(t => t.UserId).ToList();
                    if (!cuserids.Contains(item))
                    {
                        classUserService.CreateClassUser(new ClassUserEditDto { ClassId = Convert.ToInt16(cId), UserId = item });
                    }
                    //判断课程班级表有当前班级没用如果有要更新课程人员表保持一致和手机签到一样
                    var dto = cbtservic.GetAll().FirstOrDefault(t => t.BusinessId == cId && t.type == 2);
                    if (dto != null && dto.type != 4)
                    {
                        cpservic.CreateCourseBoundPersonnel(new CourseBoundPersonnelEditDto { CourseId = dto.CourseId, AccountSysNo = item, CheckIN = false, IsBound = true });
                    }
                }
            }
            cdjService.CreateClassDepJob(create);
        }
        /// <summary>
        /// 移除时取到当前设置部门的人数和设置岗位的人事的部共有的人
        /// </summary>
        protected List<int> UnRepeatUser(ClassDepJobListDto boundData)
        {
            var duserids = new int[] { };
            var juserids = new int[] { };
            if (boundData.type == 0)//部门
            {
                var department = departmentService.GetAllDepartmentInfos().FirstOrDefault(t => t.DepartmentId == boundData.BusinessId);
                //获取所有子部门及自己的部门id
                var dids = departmentService.GetAllDepartmentInfos().Where(t => t.Path.Contains(department.Path)).Select(t => t.DepartmentId);
                //获取人员表所选部门ids的总人数
                duserids = userService.GetAll().Where(t => dids.Contains(t.DepartmentID)).Select(t => t.SysNO).ToArray();


                var jids = cdjService.GetClassDepJobCIdsOrJids(1);
                userService.GetAll().Where(t => jids.Contains(t.PostID)).Select(t => t.SysNO).ToArray();

            }
            else
            {
                //获取人员表当前所选岗位id的总人数
                juserids = userService.GetAll().Where(t => t.PostID == boundData.BusinessId).Select(t => t.SysNO).ToArray();

                var dids = cdjService.GetClassDepJobCIdsOrJids(0);
                duserids = userService.GetAll().Where(t => dids.Contains(t.DepartmentID)).Select(t => t.SysNO).ToArray();
            }


            List<int> result = new List<int>();
            foreach (int a in duserids)
            {
                if (!juserids.Contains(a))
                    result.Add(a);
            }
            duserids = new int[result.Count];  //重新设置长度
            result.CopyTo(duserids);//将List结果复制到数组duserids中
            return result;
        }
        #endregion
    }
}