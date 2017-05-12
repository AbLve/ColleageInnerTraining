using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColleageInnerTraining.Web.Wap.Models.Account
{
    public class LogInViewModel
    {
        public string SuccessMessage { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsSelfRegistrationEnabled { get; set; }

        public string ReturnUrl { get; set; }
    }
}