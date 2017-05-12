using Abp.Web.Mvc.Controllers;
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
    public class ExamListController : AbpController
    {
        private readonly IDepartmentInfoAppService _departmentInfoAppService;
        public ExamListController(IDepartmentInfoAppService departmentInfoAppService) {
            _departmentInfoAppService = departmentInfoAppService;
        }
        /// <summary>
        /// 试题管理
        /// </summary>
        /// <returns></returns>
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Exam.ExamQuestionList")]
        [Route("Admin/ExamList")]
        public ActionResult Index()
        {            
            var examViewModel = new ExamViewModel();
            examViewModel.departs = _departmentInfoAppService.GetAllDepartmentInfos();
            return View(examViewModel);
        }
    }
}