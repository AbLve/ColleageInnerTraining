using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ColleageInnerTraining.Web.Areas.Wap.Models;

namespace ColleageInnerTraining.Web.Areas.Wap.Controllers
{
    public class ExamController : Controller
    {
        // GET: Wap/Exam
        [HttpGet]
        [Route("Wap/Exam/Index")]        
        public ActionResult Index()
        {
            ExamViewModel vm = new ExamViewModel();
            return View(vm);
        }


        // GET: Wap/Exam/Examing
        [HttpGet]
        [Route("Wap/Exam/Examing")]
        public ActionResult Examing()
        {
            string examId = Request.QueryString["examId"];
            ExamViewModel vm = new ExamViewModel();
            vm.ExamId = int.Parse(examId);
            return View(vm);
        }


        // GET: Wap/Exam/ExamedResult
        [HttpGet]
        [Route("Wap/Exam/ExamedResult")]
        public ActionResult ExamResult()
        {
            string examId = Request.QueryString["examId"];
            ExamViewModel vm = new ExamViewModel();
            vm.ExamId = int.Parse(examId);
            return View(vm);
        }

    }
}