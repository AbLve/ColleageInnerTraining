using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColleageInnerTraining.Web.Areas.Wap.Models
{
    public class ChangeViewModel
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string SuccessMessage { get; set; }
        public bool IsSuccess { get; set; }


    }
}