using Abp.Application.Navigation;
using Abp.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColleageInnerTraining.Web.Navigation
{
    public class FrontEndNavigationProvider : NavigationProvider
    {
        public const string MenuName = "Frontend";

        public override void SetNavigation(INavigationProviderContext context)
        {
           
        }


        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name,"");
        }

    }
}