using ColleageInnerTraining.Web.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AuthenticationAttribute());
            //filters.Add(new HandleErrorAttribute());
            //监控引用
            //filters.Add(new StatisticsTrackerAttribute());
        }
    }
}