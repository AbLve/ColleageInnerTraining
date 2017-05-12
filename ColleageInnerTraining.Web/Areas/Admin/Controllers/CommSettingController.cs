using Abp.Web.Security.AntiForgery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{
    public class CommSettingController : Controller
    {
        /// <summary>
        /// 安全KEY
        /// </summary>
        private string appkey = System.Configuration.ConfigurationManager.AppSettings["Appkey"].ToString();
        /// <summary>
        /// 加密串
        /// </summary>
        private string appsecret = System.Configuration.ConfigurationManager.AppSettings["AppSecret"].ToString();
        /// <summary>
        /// aip地址
        /// </summary>
        public string host = System.Configuration.ConfigurationManager.AppSettings["ApiHost"].ToString();

        [HttpGet]
        public JsonResult Index()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string timestamp = Convert.ToInt64(ts.TotalSeconds).ToString();
            string sign = MD5Encrypt(MD5Encrypt(appkey + timestamp) + appsecret);
            JavaScriptSerializer json = new JavaScriptSerializer();
            ComSetting cs = new ComSetting();
            cs.timestamp = timestamp;
            cs.appkey = appkey;
            cs.sign = sign;
            cs.apiUrl = host;
            cs.type = "courseware";
            if (Session["UserId"] != null)
            {
                cs.userId = Session["UserId"].ToString();
                if (Session["UserName"] != null)
                {
                    cs.userName = Session["UserName"].ToString();
                }
                if (Session["DepartId"] != null)
                {
                    cs.departId = Session["DepartId"].ToString();
                }
            }
            return Json(cs, JsonRequestBehavior.AllowGet);
        }

        private string MD5Encrypt(string strText)
        {
            strText = FormsAuthentication.HashPasswordForStoringInConfigFile(strText, "MD5").ToLower();
            return strText;
        }

        public class ComSetting
        {
            public string apiUrl { get; set; }
            public string timestamp { get; set; }
            public string appkey { get; set; }
            public string sign { get; set; }
            public string type { get; set; }
            public string userId { get; set; }
            public string userName { get; set; }
            public string departId { get; set; }
        }
    }
}