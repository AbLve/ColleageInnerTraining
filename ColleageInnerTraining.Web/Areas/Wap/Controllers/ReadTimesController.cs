using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Wap.Controllers
{
    public class ReadTimesController : Controller
    {

        public IReadTimesAppService _readTimesAppService;
        private readonly ICourseInfoAppService _courseInfoAppService;

        public ReadTimesController(IReadTimesAppService readTimesAppService, ICourseInfoAppService courseInfoAppService)
        {
            _readTimesAppService = readTimesAppService;
            _courseInfoAppService = courseInfoAppService;
        }

        // GET: Wap/ReadTimes
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult AddReadTime(string type, string name, int bizId)
        {
            ReadTimesEditDto readtimes = new ReadTimesEditDto();
            ReadTimesListDto rlist = new ReadTimesListDto();
            try
            {
                int userId = int.Parse(CookieHelper.GetCookieValue("UserId").ToString());
                rlist = _readTimesAppService.GetReadTimesByTypeAndId(type, bizId, userId);
                if (rlist == null)
                {
                    readtimes.BizId = bizId;
                    readtimes.BizType = type;
                    readtimes.UserId = userId;
                    readtimes.BizName = name;
                    readtimes = _readTimesAppService.CreateReadTimes(readtimes);
                }


            }
            catch (Exception e)
            {

            }
            return Json(readtimes, JsonRequestBehavior.AllowGet);
        }

    }
}