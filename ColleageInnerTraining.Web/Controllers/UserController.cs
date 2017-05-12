using System;
using System.Collections.Generic;
using ColleageInnerTraining.Web.Models.Strudents;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Domain.Uow;
using System.Threading.Tasks;
using ColleageInnerTraining.StudentService;
using ColleageInnerTraining.Web.Models;
using ColleageInnerTraining.Web.Navigation;

namespace ColleageInnerTraining.Web.Controllers
{
    public class UserController : ColleageInnerTrainingControllerBase
    {
        private string menuId = PageNames.ManagemnetKey.User.ToString();


        public ActionResult Index()
        {
           
            UserModel model = new UserModel();
            
            model.menuId = int.Parse(menuId);
            return View(model);
        }

    }
}