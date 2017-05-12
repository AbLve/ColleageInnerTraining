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
    /// 验证用户是否已经登录
    /// </summary>
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["username"] == null)
            {
                filterContext.Result = UnAuthorizedResult();
            }
                
            base.OnActionExecuting(filterContext);
        }

        public ActionResult UnAuthorizedResult()
        {
            return new RedirectResult("/Account/Login");
        }
    }
}
