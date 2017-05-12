using Abp.Application.Services.Dto;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Controllers;
using Castle.Core.Logging;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;
using ColleageInnerTraining.Web.Auth;
using ColleageInnerTraining.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ColleageInnerTraining.Web.Controllers
{
    /// <summary>
    /// 登录及账号管理
    /// </summary>
    [Authorize]
    public class AccountController : AbpController
    {
        IUserAccountAppService _userAccountAppService;
        public AccountController( IUserAccountAppService userAccountAppService)
        { 
            _userAccountAppService = userAccountAppService; 
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return RedirectToAction("Login", "Admin/Account");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogInViewModel viewModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            { return View(); }

            Session["UserPermissions"] = null;
            Session["UserId"] = 3081;
            Session["DepartId"] = 1;
            Session["UserName"] = "管理员";
            FormsAuthentication.SetAuthCookie(viewModel.UserName, true);
            return RedirectToAction("Index", "Admin/Home");

            //var user = AuthorizeManager.Login(viewModel.UserName, viewModel.Password);
            //if (user != null)
            //{
            //    //获取用户所属的功能权限列表，并保持的Session中
            //    var permissions = AuthorizeManager.GetUserAuthKey(viewModel.UserName);
            //    Session["UserPermissions"] = permissions;
            //    Session["UserId"] = user.SysNO;
            //    UserAccountListDto userInfo = _userAccountAppService.GetUserAccountBySysNo(user.SysNO);
            //    if (userInfo != null)
            //    {
            //        Session["DepartId"] = userInfo.DepartmentID;
            //    }
            //    else
            //        return RedirectToAction("Login", "Account");
            //    Session["UserName"] = user.DisplayName;
            //    FormsAuthentication.SetAuthCookie(viewModel.UserName, true);
            //    return RedirectToAction("Index", "Admin/Home");
            //}
            //else
            //{
            //    return View((object)"用户名或密码不正确！");
            //}
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