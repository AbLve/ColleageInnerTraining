using Abp.Application.Services.Dto;
using Abp.Runtime.Session;
using Castle.Core.Logging;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;
using ColleageInnerTraining.Web.Auth;
using ColleageInnerTraining.Web.Utilities;
using ColleageInnerTraining.Web.Wap.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ColleageInnerTraining.Web.Areas.Wap.Controllers
{
    public class AccountController : Controller
    {
        ILogger _ILogger;
        IUserAccountAppService _userAccountAppService;
        public IAbpSession AbpSession { get; set; }
        public AccountController(ILogger logger, IUserAccountAppService userAccountAppService)
        {
            _ILogger = logger;
            _userAccountAppService = userAccountAppService;
            AbpSession = NullAbpSession.Instance;
        }
        // GET: Admin/Account
        public ActionResult Login(string ReturnUrl)
        {
            LogInViewModel viewModel = new LogInViewModel();
            ViewBag.ReturnUrl = ReturnUrl;
            return View(viewModel);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogInViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = AuthorizeManager.Login(viewModel.UserName, viewModel.Password);
            if (user != null)
            {
                //获取用户所属的功能权限列表，并保持的Session中
                var permissions = AuthorizeManager.GetUserAuthKey(viewModel.UserName);

                UserAccountListDto userInfo = _userAccountAppService.GetUserAccountBySysNo(user.SysNO);
                if (userInfo != null)
                {
                    CookieHelper.SetCookie("UserId", user.SysNO.ToString());
                    CookieHelper.SetCookie("DepartId", userInfo.DepartmentID.ToString());
                    CookieHelper.SetCookie("PostId", userInfo.PostID.ToString());
                    CookieHelper.SetCookie("UserName", viewModel.UserName.ToString());
                    CookieHelper.SetCookie("DisplayUserName", charTrans(user.DisplayName.ToString()==string.Empty? viewModel.UserName.ToString() : user.DisplayName.ToString()));

                }
                FormsAuthentication.SetAuthCookie(viewModel.UserName, true);
                viewModel.ReturnUrl = HttpUtility.UrlDecode(viewModel.ReturnUrl);
                if (string.IsNullOrWhiteSpace(viewModel.ReturnUrl))
                    return RedirectToAction("Index", "Home");
                else
                    return Redirect(viewModel.ReturnUrl);
            }
            else
            {
                viewModel.SuccessMessage = "用户名或密码不正确！";
                return View(viewModel);
            }
        }
        private static string charTrans(string zh_cn)
        {
            return System.Web.HttpUtility.UrlEncode(zh_cn);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            CookieHelper.SetCookie("UserId", null);
            CookieHelper.SetCookie("UserName", null);
            CookieHelper.SetCookie("DisplayUserName", null);
            CookieHelper.SetCookie("PostId", null);
            return RedirectToAction("Login", "Account");
        }

        public ActionResult NotFoundError()
        {
            return View();
        }

        public ActionResult InternalServerError()
        {
            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}