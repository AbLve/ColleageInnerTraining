using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;
using ColleageInnerTraining.Web.Auth;
using ColleageInnerTraining.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{
    public class CommunityController : BaseController
    {
        private ICommunityInteractionAppService communityService;

        public CommunityController(ICommunityInteractionAppService _communityService)
        {
            communityService = _communityService;
        }

        // GET: Admin/Community
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Community.Index")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCommunityDataList(string name, string quest, DateTime? bTime, DateTime? eTime , int pIndex = 1)
        {
            ViewBag.pageName = "GetCommunityDataList";
            var input = new GetCommunityInteractionInput() {
                IdType = 0,
                UserName = name,
                Quest = quest,
                BTIme = bTime,
                ETime = eTime
            };
            var pagedata = communityService.GetPagedCommunityInteractions(input);
            GetPageData(pagedata.TotalCount);
            return PartialView("Shared/CommunityDataList", pagedata);
        }

        //删除问题主数据
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Community.DeleteById")]
        public ActionResult DeleteById(int id)
        {
            communityService.DeleteCommunityInteraction(new EntityDto<long>() { Id = id });
            return RedirectToAction("/Index");
        }

        //详细信息
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Community.Detail")]
        public ActionResult Detail(int id)
        {
            TempData["CommId"] = id;
            return View();
        }

        public ActionResult GetCommunityChDataList(string name, string quest, DateTime? bTime, DateTime? eTime, int pIndex = 1)
        {
            ViewBag.pageName = "Community";
            var input = new GetCommunityInteractionInput()
            {
                IdType = (int)TempData["CommId"],
                UserName = name,
                Quest = quest,
                BTIme = bTime,
                ETime = eTime
            };
            var pagedata = communityService.GetPagedCommunityInteractions(input);
            GetPageData(pagedata.TotalCount);
            return PartialView("Shared/CommunityChDataList", pagedata);
        }

        //删除问题子数据
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Community.DeleteChById")]
        public ActionResult DeleteChById(int id,int parentId)
        {
            communityService.DeleteCommunityInteraction(new EntityDto<long>() { Id = id });
            return RedirectToAction("Detail", new { id = parentId });
        }
    }
}