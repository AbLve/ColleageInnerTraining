using Abp.Application.Services.Dto;
using Abp.Web.Security.AntiForgery;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Common;
using ColleageInnerTraining.Web.Auth;
using ColleageInnerTraining.Web.Controllers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{
    public class TeacherUserController : BaseController
    {
        private ITeachersAppService teachersService;
        private IDepartmentInfoAppService departmentService;
        private IJobPostAppService jobPostService;
        private ICourseInfoAppService courseInfoService;
        private IUserAccountAppService userService;

        public TeacherUserController(ITeachersAppService _teachersService, IDepartmentInfoAppService _departmentService,
            IJobPostAppService _jobPostService, ICourseInfoAppService _courseInfoService, IUserAccountAppService _userService)
        {
            teachersService = _teachersService;
            departmentService = _departmentService;
            jobPostService = _jobPostService;
            courseInfoService = _courseInfoService;
            userService = _userService;
        }
        // GET: Admin/TeacherUser
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.TeacherUser.Index")]
        public ActionResult Index()
        {
            return View();
        }

        //列表数据
        public ActionResult GetTeacherDataList(int pIndex = 1)
        {
            ViewBag.pageName = "GetTeacherDataList";
            var input = new GetTeachersInput { SkipCount = (pIndex - 1) * PageSize, MaxResultCount = PageSize };
            var pagedata = teachersService.GetPagedTeacherss(input);

            GetPageData(pagedata.TotalCount);
            return PartialView("Shared/TeacherDataList", pagedata.Items);
        }

        //编辑
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.TeacherUser.EditTeacher")]
        public ActionResult EditTeacher(int id)
        {
            #region 下拉框
            //岗位下拉框
            var jobPostList = jobPostService.GetJobPostDataList();
            var jobPostItem = new List<SelectListItem>();
            if (jobPostList != null && jobPostList.Any())
            {
                foreach (var item1 in jobPostList)
                {
                    jobPostItem.Add(new SelectListItem { Text = item1.Name, Value = item1.Id.ToString() });
                }
            }
            ViewBag.JobPostData = jobPostItem;
            #endregion
            var resultDate = teachersService.GetTeachersForEdit(new NullableIdDto<long> { Id = id });
            return View(resultDate.Teachers);
        }

        //上传图片
        [HttpPost]
        [DisableAbpAntiForgeryTokenValidation]
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.TeacherUser.UploadfyPic")]
        public ActionResult UploadfyPic()
        {
            var result = new resultDate();
            try
            {
                Response.ContentType = "text/plain";
                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["importFile"];
                string uploadPath = Server.MapPath("/Uploads/TeacherPic/");
                if (file != null)
                {
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    //保存文件
                    file.SaveAs(uploadPath + file.FileName);
                    result.IsSuccess = "0";
                    result.filePath = "/Uploads/TeacherPic/" + file.FileName;
                }
                else
                {
                    result.IsSuccess = "-1";
                }
            }
            catch (System.Exception)
            {

                result.IsSuccess = "-1";
            }
            return Json(result);
        }


        //保存
        [HttpPost]
        [Route("/TeacherUser/Save")]
        [ValidateAntiForgeryToken]
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.TeacherUser.Save")]
        public ActionResult Save(TeachersEditDto model)
        {
            try
            {
                teachersService.UpdateTeachers(model);
                return RedirectToAction("/Index");
            }
            catch (System.Exception){ }

            return RedirectToAction("/Index");
        }


        //删除
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.TeacherUser.DeleteById")]
        public ActionResult DeleteById(int id)
        {
            teachersService.DeleteTeachers(new EntityDto<long>() { Id = id });
            return RedirectToAction("/Index");
        }


        //内训师审核
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.TeacherUser.Check")]
        public ActionResult Check(int id, int status)
        {
            var result = "-1";
            var teacherCoreInfo = teachersService.GetTeachersForEdit(new NullableIdDto<long>() { Id = id });
            if (teacherCoreInfo != null)
            {
                if (status == 1)
                {
                    teacherCoreInfo.Teachers.Status = (int)TeacherStatus.Audited;
                }
                else if (status == 0)
                {
                    teacherCoreInfo.Teachers.Status = (int)TeacherStatus.Fail;
                }
                teachersService.UpdateTeachers(teacherCoreInfo.Teachers);
                result = "0";
            }
            return Content(result);
        }

        //内训师关联的课程
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.TeacherUser.SeeCourse")]
        public ActionResult SeeCourse(int sysNo)
        {
            TempData["SysNo"] = sysNo;
            return View();
        }

        public ActionResult GetTeacherByCourseList(int pIndex = 1)
        {
            int sysNo = TempData["SysNo"] != null ? (int)TempData["SysNo"] : 0;
            int teacherId = 0;
            if (sysNo > 0)
            {
                var date = userService.GetAccountData().FirstOrDefault(c => c.SysNO == sysNo);
                if (date != null)
                {
                    teacherId = (int)date.Id;
                }               
            }
            ViewBag.pageName = "TeacherByCourseList";
            var input = new GetCourseInfoInput
            {
                SkipCount = (pIndex - 1) * PageSize,
                MaxResultCount = PageSize,
                TeacherId = teacherId
            };
            var pagedata = courseInfoService.GetPagedCourseInfos(input);

            GetPageData(pagedata.TotalCount);
            return PartialView("Shared/TeacherByCourseList", pagedata.Items);
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
    public class resultDate
    {
        public string IsSuccess { get; set; }
        public string filePath { get; set; }
    }
}