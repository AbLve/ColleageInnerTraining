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
    public class CBCTController : BaseController
    {
        #region 注入服务
        private ICourseInfoAppService courseInfoService;
        private IDepartmentInfoAppService departmentService;
        private ICourseBoundConfigureTypeAppService configureTypeService;
        private IJobPostAppService jobService;
        private IClassesInfoAppService classService;
        private IUserAccountAppService userAccountService;
        private ICourseBoundPersonnelAppService cbPersonnelService;
        private IClassUserAppService classUserlService;
        private ISqlExecuter _sqlExecuter;

        public CBCTController(ICourseInfoAppService _courseInfoService, IJobPostAppService _jobService,
            IDepartmentInfoAppService _departmentService, ICourseBoundConfigureTypeAppService _configureTypeService,
            IClassesInfoAppService _classService, IUserAccountAppService _userAccountService,
            ICourseBoundPersonnelAppService _cbPersonnelService, IClassUserAppService _classUserlService,
            ISqlExecuter sqlExecuter)
        {
            courseInfoService = _courseInfoService;
            departmentService = _departmentService;
            configureTypeService = _configureTypeService;
            jobService = _jobService;
            classService = _classService;
            userAccountService = _userAccountService;
            cbPersonnelService = _cbPersonnelService;
            classUserlService = _classUserlService;
            _sqlExecuter = sqlExecuter;
        }
        #endregion
        // GET: Admin/CBCT
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.CBCT.Index")]
        public ActionResult Index(int id)
        {
            #region 部门，岗位，班级
            //岗位下拉框
            var jobPostList = jobService.GetJobPostDataList();
            var jobPostItem = new List<SelectListItem>();
            if (jobPostList != null && jobPostList.Any())
            {
                foreach (var item1 in jobPostList)
                {
                    jobPostItem.Add(new SelectListItem { Text = item1.Name, Value = item1.Id.ToString() });
                }
            }
            ViewBag.JobPostData = jobPostItem;

            //班级
            var classes = classService.GetClassesInfoAll();
            var classItem = new List<SelectListItem>();
            if (classes != null && classes.Any())
            {
                foreach (var item2 in classes)
                {
                    classItem.Add(new SelectListItem { Text = item2.ClassesName, Value = item2.Id.ToString() });
                }
            }
            ViewBag.ClassData = classItem;
            #endregion
            //课程Id
            TempData["CourseId"] = id;
            ViewBag.CourseId = id;
            return View();
        }

        public ActionResult GetCBCTManageDataList(int pIndex = 1)
        {
            ViewBag.pageName = "GetCBCTManageDataList";
            int courseId = TempData["CourseId"] != null ? (int)TempData["CourseId"] : 0;
            var input = new GetCourseBoundConfigureTypeInput()
            {
                CourseId = courseId,
                SkipCount = (pIndex - 1) * PageSize,
                MaxResultCount = PageSize
            };
            var pagedata = configureTypeService.GetPagedCourseBoundConfigureTypes(input);
            GetPageData(pagedata.TotalCount);
            return PartialView("Shared/CBCTDateList", pagedata);
        }


        //设置绑定
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.CBCT.SetBoundCourseByType")]
        public ActionResult SetBoundCourseByType(int courseId, int typeId, int typeCaseId)
        {
            var result = new resultData();
            result.status = "-1";
            var courseDate = courseInfoService.GetCourseInfoById(new EntityDto<long>() { Id = courseId });//课程数据
            if (courseDate.Status != (int)CourseStatus.Pending)
            {
                return Content(Serialize(new resultData() { status = "-2", msg = "当前课程状态为：待审核，才能添加人员" }));
            }

            if (typeId == (int)ConfigureType.Class && courseDate != null 
                && courseDate.Status != (int)CourseStatus.Completed && typeCaseId > 0)
            {
                var configure = configureTypeService.GetCTypeByClassIdOrType(typeCaseId, typeId);//班级的数据
                var configureByCourseId = configureTypeService.GetCTypeByCourseIdOrType(courseId, typeId);//课程和班级数量

                if ((configure != null && configure.Count > 0) || (configureByCourseId != null && configureByCourseId.Count > 0))
                {
                    return Content(Serialize(new resultData() { status = "-1", msg = "班级和课程只能关联一次，不允许添加" }));
                }               
            }

            if (courseDate != null && typeId >= 0 && typeCaseId > 0)
            {
                var create = new CourseBoundConfigureTypeEditDto();//实例化对象Bound对象
                var boundData = configureTypeService.GetCTypeByCourseIdOrTypeId((int)courseDate.Id, typeId, typeCaseId);//绑定的数据
                if (boundData == null)//如果没就增加
                {
                    create.CourseId = (int)courseDate.Id;
                    create.CourseName = courseDate.CourseName;
                    create.type = typeId;
                    switch (typeId)//类型
                    {
                        case (int)ConfigureType.Department:
                            #region 部门操作
                            var department = departmentService.GetAllDepartmentInfos().FirstOrDefault(v => v.DepartmentId == typeCaseId);//获取部门唯一数据
                            if (department != null)
                            {
                                var createDataDUser = cbPersonnelService.GetUserDateByDepartment((int)courseDate.Id, department);
                                foreach (var itemD in createDataDUser)//循环遍历插入
                                {
                                    CourseBoundPersonnelEditDto cbp = new CourseBoundPersonnelEditDto();
                                    cbp.CourseId = (int)courseDate.Id;
                                    cbp.CourseName = courseDate.CourseName;
                                    cbp.AccountSysNo = itemD.SysNO;
                                    cbp.AccountUserName = itemD.DisplayName;
                                    cbPersonnelService.CreateCourseBoundPersonnel(cbp);
                                }

                                create.BusinessId = department.DepartmentId;
                                create.BusinessName = department.DisplayName;
                            }
                            #endregion                            
                            break;
                        case (int)ConfigureType.Post:
                            #region 岗位操作
                            var post = jobService.GetJobPostById(new EntityDto<long>() { Id = typeCaseId });//获取唯一岗位
                            if (post != null)
                            {
                                var creatDataPUser = cbPersonnelService.GetUserDateByJobPost((int)courseDate.Id, post);
                                foreach (var itemP in creatDataPUser)//循环遍历插入
                                {
                                    CourseBoundPersonnelEditDto cbp = new CourseBoundPersonnelEditDto();
                                    cbp.CourseId = (int)courseDate.Id;
                                    cbp.CourseName = courseDate.CourseName;
                                    cbp.AccountSysNo = itemP.SysNO;
                                    cbp.AccountUserName = itemP.DisplayName;
                                    cbPersonnelService.CreateCourseBoundPersonnel(cbp);
                                }
                                create.BusinessId = (int)post.Id;
                                create.BusinessName = post.Name;
                            }
                            #endregion
                            break;
                        case (int)ConfigureType.Class:
                            #region 班级操作
                            var classes = classService.GetClassesInfoById(new EntityDto<long>() { Id = typeCaseId });//获取唯一课程数据
                            if (classes != null)
                            {
                                var creatUserDate = cbPersonnelService.GetUserDateByClasses((int)courseDate.Id, classes);
                                foreach (var itemC in creatUserDate)//循环遍历插入
                                {
                                    CourseBoundPersonnelEditDto cbp = new CourseBoundPersonnelEditDto();
                                    cbp.CourseId = (int)courseDate.Id;
                                    cbp.CourseName = courseDate.CourseName;
                                    cbp.AccountSysNo = itemC.SysNO;
                                    cbp.AccountUserName = itemC.DisplayName;
                                    cbPersonnelService.CreateCourseBoundPersonnel(cbp);
                                }
                                create.BusinessId = (int)classes.Id;
                                create.BusinessName = classes.ClassesName;
                            }
                            #endregion
                            break;
                        default:

                            break;
                    }

                    configureTypeService.CreateCourseBoundConfigureType(create);
                    result.msg = "";
                }
                else
                {
                    result.msg = boundData.BusinessName;
                }
                result.status = "0";
            }
            return Content(Serialize(result));
        }

        //取消绑定
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.CBCT.CancellBount")]
        public ActionResult CancellBount(int courseId, int typeId, int businId)
        {
            var result = new resultData();
            result.status = "-1";
            var boundDataAll = configureTypeService.GetCTypeByCId(courseId);//绑定关系的数据
            var boundData = configureTypeService.GetCTypeByCourseIdOrTypeId(courseId, typeId, businId);//绑定的唯一数据
            var courseDate = courseInfoService.GetCourseInfoById(new EntityDto<long>() { Id = courseId });//课程数据
            
            if (boundData != null)
            {
                //删除关系
                string sqlcc = "delete from px_course_bound_configure where Id=" + boundData.Id;
                var rcc = _sqlExecuter.Execute(sqlcc);
                if (rcc > 0 && typeId == (int)ConfigureType.Class)
                {
                    configureTypeService.SetCourseCountReduce(boundData.BusinessId);
                }
                result.msg = "";
                //获取课程下的所有人员
                var coursePersom = cbPersonnelService.GetCouPerByCourseAll(courseId);
                if (coursePersom.Any())
                {
                    //删除课程下的所有人员
                    string deleteIds = string.Join(",", coursePersom.Select(v => v.Id).ToList());
                    string sqlcp = "delete from px_course_bound_personnel where Id in(" + deleteIds + ")";
                    var rcp = _sqlExecuter.Execute(sqlcp);
                }
                var bound = boundDataAll.Where(c => c.BusinessId != businId).ToList();
                if (bound.Any())
                {
                    var resultDate = AddUser(bound, courseId);
                    if (resultDate.Any())
                    {
                        //插入剩余关系下的所有用户
                        foreach (var itemuser in resultDate)
                        {
                            CourseBoundPersonnelEditDto cbp = new CourseBoundPersonnelEditDto();
                            cbp.CourseId = courseId;
                            cbp.CourseName = courseDate.CourseName;
                            cbp.AccountSysNo = itemuser.SysNO;
                            cbp.AccountUserName = itemuser.DisplayName;
                            cbPersonnelService.CreateCourseBoundPersonnel(cbp);
                        }
                    }
                }
                result.status = "0";
            }                      
            return Content(Serialize(result));
        }


        /// <summary>
        /// 添加的人员
        /// </summary>
        public List<UserAccount> AddUser(List<CourseBoundConfigureType> cbType, int courseId)
        {
            List<UserAccount> userList = new List<UserAccount>();
            if (cbType.Any())
            {
                foreach (var item in cbType)
                {
                    switch (item.type)
                    {
                        case (int)ConfigureType.Department:
                            #region 部门操作
                            var department = departmentService.GetAllDepartmentInfos().FirstOrDefault(v => v.DepartmentId == item.BusinessId);//获取部门唯一数据
                            if (department != null)
                            {
                                var createDataDUser = cbPersonnelService.GetUserDateByDepartment(courseId, department).ToList();
                                userList.AddRange(createDataDUser);
                            }
                            #endregion
                            break;
                        case (int)ConfigureType.Post:
                            #region 岗位操作
                            var post = jobService.GetJobPostById(new EntityDto<long>() { Id = item.BusinessId });//获取唯一岗位
                            if (post != null)
                            {
                                var creatDataPUser = cbPersonnelService.GetUserDateByJobPost(courseId, post).ToList();
                                userList.AddRange(creatDataPUser);
                            }
                            #endregion
                            break;
                        case (int)ConfigureType.Class:
                            #region 班级操作
                            var classes = classService.GetClassesInfoById(new EntityDto<long>() { Id = item.BusinessId });//获取唯一课程数据
                            if (classes != null)
                            {
                                var creatUserDate = cbPersonnelService.GetUserDateByClasses(courseId, classes).ToList();
                                userList.AddRange(creatUserDate);
                            }
                            #endregion
                            break;
                        case (int)ConfigureType.Personal:
                            #region 班级操作
                            var creatUserDatee = cbPersonnelService.GetUserDateByPerson(courseId, (int)ConfigureType.Personal);
                            userList.AddRange(creatUserDatee);
                            #endregion
                            break;
                    }
                }
            }

            if (userList.Any())
            {
                userList = userList.Distinct().ToList();
            }
            return userList; 
        }

        public class resultData
        {
            public string status { get; set; }

            public string msg { get; set; }
        }
        public static string Serialize(object o)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
            json.Serialize(o, sb);
            return sb.ToString();
        }

        public ActionResult GetTreeDepartSelectJson()
        {
            var data = departmentService.DepartmentList();
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.DepartmentId + "";
                treeModel.text = item.DisplayName;
                treeModel.parentId = item.ParentId + "";
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }
    }
}