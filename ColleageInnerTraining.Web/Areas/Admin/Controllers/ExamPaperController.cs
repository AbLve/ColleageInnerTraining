using Abp;
using Abp.Castle.Logging.Log4Net;
using Castle.Facilities.Logging;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Core;
using ColleageInnerTraining.Web.Areas.Admin.Models;
using ColleageInnerTraining.Web.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{
    public class ExamPaperController : Controller
    {

        private readonly IDepartmentInfoAppService _departmentInfoAppService;
        public ExamPaperController(IDepartmentInfoAppService departmentInfoAppService)
        {
            _departmentInfoAppService = departmentInfoAppService;
        }
        // GET: Admin/Exam
        public ActionResult Index()
        {
            var examViewModel = new ExamViewModel();
            examViewModel.departs = _departmentInfoAppService.GetAllDepartmentInfos();
            return View(examViewModel);
        }

        [HttpGet]
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Exam.ExamPaper.EditExamPaper")]
        [Route("Admin/ExamPaper/EditExamPaper")]
        public ActionResult EditExamPaper() {
            string paperId = Request.QueryString["paperId"];            

            var examViewModel = new ExamViewModel(); 
                examViewModel.PaperId = int.Parse(paperId);
            return View("~/Areas/Admin/Views/ExamPaper/EditExamPaper.cshtml", examViewModel);
        }


        [HttpGet]
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Exam.ExamPaper.CreateExamPaper")]
        [Route("Admin/ExamPaper/CreateExamPaper")]
        public ActionResult CreateExamPaper()
        { 
            return View("~/Areas/Admin/Views/ExamPaper/CreateExamPaper.cshtml");
        }

        [HttpGet]
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Exam.ExamPaper.QuestionExamPaper")]
        [Route("Admin/ExamPaper/QuestionExamPaper")]
        public ActionResult QuestionExamPaper()
        {
            string paperId = Request.QueryString["paperId"];
            var examViewModel = new ExamViewModel();
            examViewModel.PaperId = int.Parse(paperId);
            return View("~/Areas/Admin/Views/ExamPaper/QuestionExamPaper.cshtml", examViewModel);
        }

        [HttpGet]
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Exam.ExamPaper.ApproeExamPaper")]
        [Route("Admin/ExamPaper/ApproeExamPaper")]
        public ActionResult ApproeExamPaper()
        {
            string paperId = Request.QueryString["paperId"];
            var examViewModel = new ExamViewModel();
            examViewModel.PaperId = int.Parse(paperId);
            return View("~/Areas/Admin/Views/ExamPaper/ApproeExamPaper.cshtml", examViewModel);
        }



    }
}