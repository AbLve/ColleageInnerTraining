using System.Web.Mvc;
using Abp.Application.Navigation;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.Threading;
using ColleageInnerTraining.Web.Models.Layout;
using Abp.Web.Mvc.Controllers;
using ColleageInnerTraining.Web.Navigation;
using System.Collections.Generic;
using ColleageInnerTraining.Core;
using ColleageInnerTraining.Application.Dtos;
using System.Web.Security;
using ColleageInnerTraining.Application;

namespace ColleageInnerTraining.Web.Controllers
{
    /// <summary>
    /// Layout for 'front end' pages.
    /// </summary>
    public class LayoutController  : AbpController
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
            if (Request.Cookies[FormsAuthentication.FormsCookieName]!=null)
            {
                var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];            
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                headerModel.UserName = ticket.Name;
            }
            return PartialView("~/Views/Layout/_Header.cshtml", headerModel);
        }

        [ChildActionOnly]
        public PartialViewResult Footer(string currentPageName = "")
        {
            return PartialView("~/Views/Layout/_Footer.cshtml");
        }

        [ChildActionOnly]
        public PartialViewResult Left(string currentPageName = "",int menuId = 0)
        {
            //左边导航菜单
            var leftModel = new LeftViewModel();
            leftModel.Menu = _menuAppService.GetMenusByParentId(menuId);
            leftModel.CurrentPageName = currentPageName;
            return PartialView("~/Views/Layout/_Left.cshtml", leftModel);
        }
    }
}