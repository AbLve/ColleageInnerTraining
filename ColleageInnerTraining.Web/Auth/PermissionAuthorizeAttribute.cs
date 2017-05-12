using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Auth
{
    /// <summary>
    /// 权限授权验证
    /// </summary>
    public class PermissionAuthorizeAttribute : ActionFilterAttribute
    {
        public List<string> RequiredPermissionNames { get; private set; }

        public PermissionAuthorizeAttribute(params string[] requiredPermissions)
        {
            this.RequiredPermissionNames = new List<string>();
            this.RequiredPermissionNames.AddRange(requiredPermissions);
            this.Order = 1;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    filterContext.Result = UnAuthorizedResult();
            //}

            //List<AuthKey> permissions = filterContext.HttpContext.Session["UserPermissions"] as List<AuthKey>;
            //if (permissions == null
            //    || !permissions.Any(p => p.Key == RequiredPermissionNames.FirstOrDefault()))
            //{
            //    filterContext.Result = UnAuthorizedResult();
            //}
        }

        public ActionResult UnAuthorizedResult()
        {
            return new RedirectResult("/Account/AccessDenied");
        }
    }
}
