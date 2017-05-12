using ColleageInnerTraining.StudentService;
using System.Collections.Generic;
using ColleageInnerTraining.Web.Auth;
using System.Web.Mvc;
using ColleageInnerTraining.Web.Models.Strudents;
using Abp.AutoMapper;

namespace ColleageInnerTraining.Web.Controllers
{

    public class HomeController : ColleageInnerTrainingControllerBase
    {
        private IStudentAppService studentService;

        public HomeController(IStudentAppService service)
        { studentService = service; }

        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Home")]
        public ActionResult Index()
        {
            return RedirectToAction("Login", "Admin/Account");
        }
    }
}