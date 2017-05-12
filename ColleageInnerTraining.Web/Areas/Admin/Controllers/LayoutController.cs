using Abp.Application.Navigation;
using Abp.Configuration.Startup;
using Abp.Localization;
using ColleageInnerTraining.Core;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Web.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ColleageInnerTraining.Application;
using Abp.Application.Services.Dto;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{
    public class LayoutController : Controller
    {
        private const string menuType = "menu";
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly ILanguageManager _languageManager;
        private readonly IMenuAppService _menuAppService;

        public LayoutController(
            IUserNavigationManager userNavigationManager,
            IMultiTenancyConfig multiTenancyConfig,
            ILanguageManager languageManager,
            IMenuAppService menuAppService
            )
        {
            _userNavigationManager = userNavigationManager;
            _multiTenancyConfig = multiTenancyConfig;
            _languageManager = languageManager;
            _menuAppService = menuAppService;
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
            headerModel.Menu = _menuAppService.GetMenusByParentId(0);
            headerModel.CurrentPageName = currentPageName;
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                headerModel.UserName = ticket.Name;
            }
            return PartialView("~/Areas/Admin/Views/Layout/_Header.cshtml", headerModel);
        }

        [ChildActionOnly]
        public PartialViewResult Footer(string currentPageName = "")
        {
            return PartialView("~/Areas/Admin/Views/Layout/_Footer.cshtml");
        }

        [ChildActionOnly]
        public PartialViewResult Left(string currentPageName = "", int menuId = 0)
        {
            //左边导航菜单
            var leftModel = new LeftViewModel();
            MenuListDto m = _menuAppService.GetMenuById(new EntityDto<long> { Id = menuId} );
            leftModel.MenuParentName = m.MenuName;
            leftModel.Menu = _menuAppService.GetMenusByParentId(menuId);
            leftModel.CurrentPageName = currentPageName;
            return PartialView("~/Areas/Admin/Views/Layout/_Left.cshtml", leftModel);
        }
    }
}