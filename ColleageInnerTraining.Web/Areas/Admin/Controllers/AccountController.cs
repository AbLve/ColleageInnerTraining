using Abp.Application.Services.Dto;
using Abp.Runtime.Session;
using Castle.Core.Logging;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;
using ColleageInnerTraining.Web.Auth;
using ColleageInnerTraining.Web.Admin.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
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
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogInViewModel viewModel)
        {
            if (!ModelState.IsValid)
            { return View(); }
            var user = AuthorizeManager.Login(viewModel.UserName, viewModel.Password);
            if (user != null)
            {
                //获取用户所属的功能权限列表，并保持的Session中
                var permissions = AuthorizeManager.GetUserAuthKey(viewModel.UserName);

                UserAccountListDto userInfo = _userAccountAppService.GetUserAccountBySysNo(user.SysNO);
                if (userInfo != null)
                {
                    Session["DepartId"] = userInfo.DepartmentID;
                    Session["PostId"] = userInfo.PostID;
                }
                else
                {
                    return View((object)"账号未同步到商学院系统中！");
                }

                Session["UserPermissions"] = permissions;
                Session["UserId"] = user.SysNO;
                Session["UserName"] = viewModel.UserName;
                Session["DisplayName"] = user.DisplayName;


                FormsAuthentication.SetAuthCookie(viewModel.UserName, true);
                viewModel.ReturnUrl = HttpUtility.UrlDecode(viewModel.ReturnUrl);
                if (string.IsNullOrWhiteSpace(viewModel.ReturnUrl))
                    return RedirectToAction("Index", "Home");
                else
                    return Redirect(viewModel.ReturnUrl);
            }
            else
            {
                return View((object)"用户名或密码不正确！");
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["UserPermissions"] = null;
            Session["UserId"] = null;
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