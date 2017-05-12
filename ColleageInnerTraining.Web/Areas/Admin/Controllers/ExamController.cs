using Abp;
using Abp.Castle.Logging.Log4Net;
using Castle.Facilities.Logging;
using ColleageInnerTraining.Web.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 考试管理
    /// </summary>
    public class ExamController : Controller
    {
        /// <summary>
        /// 考试管理首页，考试分类管理
        /// </summary>
        /// <returns></returns>
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Exam")]
        [Route("Admin/Exam/index")]
        public ActionResult Index()
        {
            return View();
        }
    }
}