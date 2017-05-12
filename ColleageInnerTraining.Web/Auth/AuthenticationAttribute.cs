using ColleageInnerTraining.Web.Utilities;
using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ColleageInnerTraining.Web.Auth
{
    /// <summary>
    /// 验证用户是否已经登录
    /// </summary>
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.ActionName;
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var areaName = (filterContext.RouteData.DataTokens["area"] == null ? "" : filterContext.RouteData.DataTokens["area"]).ToString();
            if (actionName != "Login" && actionName != "Header" && actionName != "Left" && actionName != "Footer")
            {
                if (!actionName.Contains("Upload"))
                {
                    if (areaName.ToLower() != "wap")
                    {
                        if (filterContext.HttpContext.Session["UserId"] == null)
                        {
                            string returnUrl = "";
                            returnUrl += string.Format("/{0}/{1}/{2}?{3}", areaName, controllerName, actionName, GetParameters(filterContext));

                            filterContext.Result = UnAuthorizedResult(areaName, string.IsNullOrWhiteSpace(returnUrl.Split('?')[1]) ? returnUrl.Split('?')[0] : returnUrl);
                        }
                    }
                    else
                    {
                        if (CookieHelper.GetCookieValue("UserId") == null|| CookieHelper.GetCookieValue("UserId")==string.Empty)
                        {
                            string returnUrl = "";
                            returnUrl += string.Format("/{0}/{1}/{2}?{3}", areaName, controllerName, actionName, GetParameters(filterContext));

                            filterContext.Result = UnAuthorizedResult(areaName, string.IsNullOrWhiteSpace(returnUrl.Split('?')[1]) ? returnUrl.Split('?')[0] : returnUrl);
                        }

                    }
                }

            }
            base.OnActionExecuting(filterContext);
        }


        public ActionResult UnAuthorizedResult(string areaName, string ReturnUrl)
        {
            ReturnUrl = HttpUtility.UrlEncode(ReturnUrl);
            if (areaName != null && areaName != string.Empty)
                return new RedirectResult("/" + areaName + "/Account/Login?ReturnUrl=" + ReturnUrl);
            else
                return new RedirectResult("/Account/Login");
        }
        public string GetParameters(ActionExecutingContext filterContext)
        {
            var listParameters = filterContext.ActionParameters;
            var parameters = new StringBuilder();
            foreach (var item in listParameters)
            {
                if (listParameters.Count > 1)
                {
                    parameters.AppendFormat("{0}={1}&", item.Key, item.Value);
                }
                else
                {
                    parameters.AppendFormat("{0}={1}", item.Key, item.Value);
                }
            }
            var str = parameters.ToString();
            if (str.Contains("&"))
            {
                str.Remove(str.LastIndexOf("&"));
            }
            return str;
        }

    }
}
