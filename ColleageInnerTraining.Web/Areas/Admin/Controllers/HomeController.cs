using Abp.Web.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{
    public class HomeController : AbpController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}