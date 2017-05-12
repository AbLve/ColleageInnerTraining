using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace AuthServiceUtil
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
            var user = filterContext.HttpContext.User.Identity.Name;
           
            if (!AuthorizeManager.HasFunction(new AuthKey(RequiredPermissionNames.FirstOrDefault())))
            {
                filterContext.Result = UnAuthorizedResult();
            }
        }

        public ActionResult UnAuthorizedResult()
        {
            return new RedirectResult("/Account/AccessDenied");
        }
    }
}
