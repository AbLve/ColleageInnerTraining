using Abp.Application.Services.Dto;
using Abp.UI;
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
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 班级管理
    /// </summary>
    public class ClassesController : BaseController
    {
        #region 注入服务
        private IClassesInfoAppService classesinfoService;
        private IClassUserAppService classuserService;
        private IClassTrainingInfoAppService classprojectService;
        private ICourseInfoAppService courseservic;
        private ICourseBoundConfigureTypeAppService cbtservic;
        private ICourseBoundPersonnelAppService cpservic;


        public ClassesController(IClassesInfoAppService service, ICourseInfoAppService _courseservic, IClassUserAppService _classuserService, ISqlExecuter _sqlExecuter,
                                 ICourseBoundConfigureTypeAppService _cbtservic, ICourseBoundPersonnelAppService _cpservic, IClassTrainingInfoAppService _classprojectService,
                                 IDepartmentInfoAppService _departmentService, IJobPostAppService _jobPostService, IUserAccountAppService _userAccountService) :
                                 base(_departmentService, _jobPostService, _sqlExecuter, _userAccountService)
        {
            classesinfoService = service;
            classuserService = _classuserService;
            classprojectService = _classprojectService;
            courseservic = _courseservic;
            cbtservic = _cbtservic;
            cpservic = _cpservic;
        }
        #endregion

        #region 班级信息
        private const string ex1 = "GZSXY.Pages.Manage.Classes.ClassesManage.";

        /// <summary>
        /// 班级管理列表
        /// </summary>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex1 + "Index")]
        [Route("/Classes/ClassesManageIndex")]
        public ActionResult ClassesManageIndex()
        {
            return View("ClassesManage/Index");
        }

        /// <summary>
        /// 班级分布列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pIndex"></param>
        /// <returns></returns>
        [Route("/Classes/GetCManageDataList")]
        public ActionResult GetCManageDataList(string keyword, int pIndex = 1)
        {
            ViewBag.pageName = "GetCManageDataList";
            var input = new GetClassesInfoInput() { FilterText = keyword, SkipCount = (pIndex - 1) * PageSize, MaxResultCount = PageSize };
            var pagedata = classesinfoService.GetPagedClassesInfos(input);

            GetPageData(pagedata.TotalCount);
            return PartialView("Shared/CManageDataList", pagedata.Items);
        }

        /// <summary>
        /// 班级新建页
        /// </summary>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex1 + "Create")]
        [Route("/Classes/ClassesManageCreate")]
        public ActionResult ClassesManageCreate(int cId = 0)
        {
            var model = new GetClassesInfoForEditOutput();
            if (cId > 0)
            {
                model = classesinfoService.GetClassesInfoForEdit(new NullableIdDto<long> { Id = cId });
            }
            return View("ClassesManage/Create", model.ClassesInfo);
        }

        /// <summary>
        /// 班级新增或修改提交
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Classes/ClassesManageSave")]
        public ActionResult ClassesManageSave(ClassesInfoEditDto dto)
        {
            try
            {
                CheckModelState();
                classesinfoService.CreateOrUpdateClassesInfoAsync(new CreateOrUpdateClassesInfoInput { ClassesInfoEditDto = dto });
                return RedirectToAction("ClassesManageIndex");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// 班级删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex1 + "Delete")]
        [Route("/Classes/ClassesManageDelete")]
        public ActionResult ClassesManageDelete(int id)
        {
            try
            {
                var dto = new EntityDto<long>() { Id = id };
                classesinfoService.DeleteClassesInfoAsync(dto);
                return Success();
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region 班级成员部分
        private const string ex2 = "GZSXY.Pages.Manage.Classes.ClassesMember.";


        [PermissionAuthorizeAttribute(ex2 + "Index")]
        [Route("/Classes/ClassesMemberIndex")]
        public ActionResult ClassesMemberIndex()
        {
            BindJSel();
            return View("ClassesMember/Index");
        }

        /// <summary>
        ///班级成员列表页
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="cId"></param>
        /// <param name="dId"></param>
        /// <param name="pIndex"></param>
        /// <param name="isExit"></param>
        /// <returns></returns>
        [Route("/Classes/GetMemberDataList")]
        public ActionResult GetMemberDataList(string keyword, int cId = 0, int dId = 0, int jId = 0, int pIndex = 1, int isExit = 0)
        {
            ViewBag.pageName = "GetMemberDataList";
            //获取班级已选的成员
            IEnumerable<ClassUserListDto> users = classuserService.GetAll();
            IEnumerable<int> userIds = new List<int>();
            if (users != null)
            {
                userIds = users.Where(t => t.ClassId == cId).Select(t => t.UserId);
            }
            var listdata = userService.GetAll(keyword, jId, dId, isExit, userIds);
            GetPageData(listdata.Count());
            return PartialView("Shared/MemberDataList", listdata.Skip((pIndex - 1) * PageSize).Take(PageSize));
        }

        /// <summary>
        /// 设为班员
        /// </summary>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex2 + "SetUser")]
        [Route("/Classes/SetUser")]
        public ActionResult SetUser(int cid, string uids)
        {

            var idlist = uids.Split(',');
            for (int i = 0; i < idlist.Count() - 1; i++)
            {
                var input = new ClassUserEditDto { ClassId = cid, UserId = int.Parse(idlist[i]) };
                classuserService.CreateClassUser(input);
                //判断课程班级表有当前班级没用如果有要更新课程人员表保持一致和手机签到一样
                var dto = cbtservic.GetAll().FirstOrDefault(t => t.BusinessId == cid && t.type == 2);
                if (dto != null && dto.type != 4)
                {
                    cpservic.CreateCourseBoundPersonnel(new CourseBoundPersonnelEditDto { CourseId = dto.CourseId, AccountSysNo = int.Parse(idlist[i]), CheckIN = false, IsBound = true });
                }
            }
            return Success();
        }

        /// <summary>
        /// 移除班级成员
        /// </summary>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex2 + "RemoveUser")]
        [Route("/Classes/RemoveUser")]
        public ActionResult RemoveUser(int cid, int uid)
        {
            var sqlstr = "update px_class_user set removed=1 where class_id=@class_id and user_id=@user_id";
            var i = sqlExecuter.Execute(sqlstr, new MySqlParameter("@class_id", cid), new MySqlParameter("@user_id", uid));


            var edit = classesinfoService.GetClassesInfoForEdit(new NullableIdDto<long> { Id = cid }).ClassesInfo;
            var count = classuserService.GetAll().Where(t => t.ClassId == cid).Count();
            edit.MemberCount = count;
            //更新班级成员数量
            classesinfoService.UpdateClassesInfoAsync(edit);

            //判断课程班级表有当前班级没用如果有要更新课程人员表保持一致和手机签到一样
            var dto = cbtservic.GetAll().FirstOrDefault(t => t.BusinessId == cid && t.type == 2);
            if (dto != null && dto.type != 4)
            {
                cpservic.DeleteCourseBoundPersonnel(new EntityDto<long> { Id = dto.Id });
            }
            return Success();
        }
        #endregion

        #region 班级项目
        private const string ex3 = "GZSXY.Pages.Manage.Classes.ClassesProject.";

        /// <summary>
        /// 班级项目index页
        /// </summary>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex3 + "Index")]
        [Route("/Classes/ClassesManageDelete")]
        public ActionResult ClassesProjectIndex(int cId = 0)
        {
            if (cId == 0)
            {
                return RedirectToAction("/ClassesManageIndex");
            }
            return View("ClassesProject/Index", new Models.EnumViewModel());
        }

        /// <summary>
        /// 班级项目分布列表页
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="cId"></param>
        /// <param name="pIndex"></param>
        /// <returns></returns>
        [Route("/Classes/GetProjectDataList")]
        public ActionResult GetProjectDataList(string keyword, int cId = 0, int type = 0, int pIndex = 1)
        {
         
            ViewBag.pageName = "GetProjectDataList";
            var input = new GetClassTrainingInfoInput { CId = cId, Name = keyword, TrainingType = type, SkipCount = (pIndex - 1) * PageSize, MaxResultCount = PageSize };
            var pagedata = classprojectService.GetPagedClassTrainingInfos(input);

            GetPageData(pagedata.TotalCount);
            return PartialView("Shared/ProjectDataList", pagedata.Items);
        }


        /// <summary>
        /// 班级项目新建页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex3 + "Form")]
        [Route("/Classes/ClassesProjectForm")]
        public async Task<ActionResult> ClassesProjectForm(int id = 0)
        {
            var model = new GetClassTrainingInfoForEditOutput();
            if (id > 0)
            {
                model = await classprojectService.GetClassTrainingInfoForEditAsync(new NullableIdDto<long> { Id = id });

            }
            var typeId = model.ClassTrainingInfoEditDto == null ? 1 : model.ClassTrainingInfoEditDto.TypeId;
            //培训类型（1 - 课程、2 - 考试、3 - 问卷、4 - 线下培训）
            ViewBag.typeSel = new List<SelectListItem>{
                                      //new SelectListItem { Text = "课程", Value = "1" ,Selected= typeId==1},
                                      new SelectListItem { Text = "考试", Value = "2" ,Selected= typeId==2},
                                      new SelectListItem { Text = "问卷", Value = "3" ,Selected= typeId==3},
                                      //new SelectListItem { Text = "线下培训", Value = "4",Selected= typeId==4}
                                     };
            var prolist = courseservic.GetAll().Where(t => t.Type == typeId).ToList();
            var prosel = new List<SelectListItem>();
            prolist.ForEach(t => prosel.Add(new SelectListItem { Text = t.CourseName, Value = t.Id + "" }));
            ViewBag.proSel = prosel;
            return View("ClassesProject/Form", model.ClassTrainingInfoEditDto);
        }

        /// <summary>
        /// 提交班级项目
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Classes/ClassesProjectSave")]
        public ActionResult ClassesProjectSave(ClassTrainingInfoEditDto dto)
        {
            var rdto = classprojectService.GetAll().FirstOrDefault(t => t.TrainingType == dto.TrainingType && t.TypeId == dto.TypeId && t.ClassId == dto.ClassId);
            if (rdto != null)
            {
                return Warn("班级中已存在项目");
            }
            var c = classesinfoService.GetClassesInfoById(new EntityDto<long> { Id = dto.ClassId });
            classprojectService.CreateClassTrainingInfoAsync(dto);

            var edit = classesinfoService.GetClassesInfoForEdit(new NullableIdDto<long> { Id = dto.ClassId }).ClassesInfo;
            switch (dto.TrainingType)
            {
                case (int)ClassProType.KS:
                    edit.ExamCount++;
                    break;
                case (int)ClassProType.WJ:
                    edit.PollCount++;
                    break;
                default:
                    break;
            }
            //更新项目数量
            classesinfoService.UpdateClassesInfoAsync(edit);
            return Success("操作成功", string.Format("ClassesProjectIndex?cId={0}&cName={1}", dto.ClassId, c.ClassesName));
        }

        /// <summary>
        /// 获取选中类型所有项目
        /// </summary>
        /// <param name="tId"></param>
        /// <returns></returns>
        //[Route("/Classes/GetProList")]
        //public JsonResult GetProList(int tId)
        //{
        //    switch (tId)
        //    {
        //        case (int)ClassProType.KS:
        //            var returnList = new List<CourseInfoListDto>();
        //            returnList = courseservic.GetAll().Where(t => t.Type != 4).ToList();
        //            return Json(returnList, JsonRequestBehavior.AllowGet);
        //        case (int)ClassProType.WJ:
        //            return Json(null, JsonRequestBehavior.AllowGet);
        //        default:
        //            return Json(null, JsonRequestBehavior.AllowGet);
        //    }

        //}

        /// <summary>
        /// 移除班级项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex3 + "Remove")]
        [Route("/Classes/RemoveProject")]
        public ActionResult RemoveProject(int Id, int cId,int type)
        {
            try
            {
                classprojectService.DeleteClassTrainingInfoAsync(new EntityDto<long> { Id = Id });

                var edit = classesinfoService.GetClassesInfoForEdit(new NullableIdDto<long> { Id = cId }).ClassesInfo;
                switch (type)
                {
                    case (int)ClassProType.KS:
                        edit.ExamCount--;
                        break;
                    case (int)ClassProType.WJ:
                        edit.PollCount--;
                        break;
                    default:
                        break;
                }
                //更新项目数量
                classesinfoService.UpdateClassesInfoAsync(edit);
                return Success();
            }
            catch (Exception e)
            {
                return Fail(e.InnerException.Message);
            }
        }
        #endregion

    }
}
