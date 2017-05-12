using ColleageInnerTraining.Web.Areas.Wap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Wap.Controllers
{
    public class PollController : Controller
    {
        // GET: Wap/Poll
        [Route("Wap/Poll/Index")]
        public ActionResult Index()
        {
            return View();
        }


        // GET: Wap/Polling
        [Route("Wap/Poll/Polling")]
        public ActionResult Polling(int pollId)
        {
            PollViewModel vm = new PollViewModel();
            vm.PollId = pollId;
            return View(vm);
        }
    }
}