using Abp.Application.Services.Dto;
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
    public class PollController : Controller
    {

        private readonly IDepartmentInfoAppService _departmentInfoAppService;
        private readonly IJobPostAppService _jobPostAppService;
        private readonly IClassesInfoAppService _classesInfoAppService;
        private IUserAccountAppService _userAccountService;

        public PollController(IDepartmentInfoAppService departmentInfoAppService, IJobPostAppService jobPostAppService,
            IClassesInfoAppService classesInfoAppService, IUserAccountAppService userAccountService)
        {
            _departmentInfoAppService = departmentInfoAppService;
            _jobPostAppService = jobPostAppService;
            _classesInfoAppService = classesInfoAppService;
            _userAccountService = userAccountService;
        }

        // GET: Admin/Poll
        public ActionResult Index()
        {
            var pollViewModel = new PollViewModel();
            pollViewModel.departs = _departmentInfoAppService.GetAllDepartmentInfos();
            return View(pollViewModel);
        }


        // GET: Admin/Poll
        public ActionResult CreatePoll()
        {
            var pollViewModel = new PollViewModel();
            pollViewModel.departs = _departmentInfoAppService.GetAllDepartmentInfos();
            return View(pollViewModel);
        }


        // GET: Admin/Poll
        public ActionResult EditPoll(int pollId)
        {
            var pollViewModel = new PollViewModel();
            pollViewModel.PollId = pollId;
            pollViewModel.departs = _departmentInfoAppService.GetAllDepartmentInfos();
            return View(pollViewModel);
        }


        // GET: Admin/Poll
        public ActionResult QuestionPoll(int pollId)
        {
            var pollViewModel = new PollViewModel();
            pollViewModel.PollId = pollId;
            pollViewModel.departs = _departmentInfoAppService.GetAllDepartmentInfos();
            return View(pollViewModel);
        }


        // GET: Admin/Poll
        public ActionResult SetRetion(int pollId)
        {
            var pollViewModel = new PollViewModel();
            pollViewModel.PollId = pollId;

            var departs = _departmentInfoAppService.GetAllDepartmentInfos().OrderBy(p => p.Path).Select(p => new ComboboxItemDto(p.DepartmentId.ToString() + "," + p.DisplayName, new string('　', p.Level * 2) + " " + p.DisplayName))
                                                   .ToList();
            pollViewModel.departLists = departs;

            pollViewModel.departs = _departmentInfoAppService.GetAllDepartmentInfos();
            pollViewModel.posts = _jobPostAppService.GetAllJobPosts();
            pollViewModel.classes = _classesInfoAppService.GetAllClassesInfos();
            return View(pollViewModel);
        }

        /// <summary>
        /// 配置人员
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Poll.PollManage.SetUsers")]
        [Route("Admin/Poll/SetUsers")]
        public ActionResult SetUsers(int pollId)
        {
            var examViewModel = new PollViewModel();
            examViewModel.PollId = pollId;
            return View("~/Areas/Admin/Views/Poll/SetUsers.cshtml", examViewModel);
        }

        /// <summary>
        /// 为考试添加人员
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentId"></param>
        /// <param name="jobPostId"></param>
        /// <param name="pIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Poll.PollManage.AddUsers")]
        [Route("Admin/Poll/AddUsers")]
        public ActionResult AddUsers(int? pollId)
        {
            int pollId1 = 0;
            if (pollId == null || pollId == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            else
                pollId1 = (int)pollId;
            var pollViewModel = new PollViewModel();
            pollViewModel.PollId = pollId1;
            return View("~/Areas/Admin/Views/Poll/addUsers.cshtml", pollViewModel);
        }


    }
}