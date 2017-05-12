using Abp.Application.Navigation;
using Abp.Configuration.Startup;
using Abp.Localization;
using ColleageInnerTraining.Core;
using ColleageInnerTraining.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ColleageInnerTraining.Application;
using Abp.Application.Services.Dto;
using ColleageInnerTraining.Web.Areas.Wap.Models;
using ColleageInnerTraining.Web.Utilities;

namespace ColleageInnerTraining.Web.Areas.Wap.Controllers
{
    public class LayoutController : Controller
    {
        private const string menuType = "menu";
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly ILanguageManager _languageManager;
        /// <summary>
        /// 菜单
        /// </summary>
        private readonly IMenuAppService _menuAppService;
        /// <summary>
        /// 用户注入对像
        /// </summary>
        IUserAccountAppService _userAccountService;



        public LayoutController(
            IUserNavigationManager userNavigationManager,
            IMultiTenancyConfig multiTenancyConfig,
            ILanguageManager languageManager,
            IMenuAppService menuAppService,
            IUserAccountAppService userAccountService
            )
        {
            _userNavigationManager = userNavigationManager;
            _multiTenancyConfig = multiTenancyConfig;
            _languageManager = languageManager;
            _menuAppService = menuAppService;
            _userAccountService = userAccountService;
        }
        /// <summary>
        /// 主菜单
        /// </summary>
        /// <param name="currentPageName"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult Header(string currentPageName = "")
        {
            var headerModel = new HeaderViewModel();
            int userId = int.Parse(CookieHelper.GetCookieValue("UserId").ToString());

            var user = _userAccountService.GetUserAccountBySysNo(userId);//查询用户数据
            headerModel.DepartMentName = user.DepartmentName;
            headerModel.JobPostName = user.PostName;
            headerModel.UserName = user.DisplayName;
            headerModel.CurrentPageName = currentPageName;
            return PartialView("~/Areas/Wap/Views/Layout/_Header.cshtml", headerModel);
        }

        [ChildActionOnly]
        public PartialViewResult Footer(string currentPageName = "")
        {
            var footerModel = new FooterViewModel();
            footerModel.CurrentPageName = currentPageName;
            return PartialView("~/Areas/Wap/Views/Layout/_Footer.cshtml", footerModel);
        }
 
    }
}