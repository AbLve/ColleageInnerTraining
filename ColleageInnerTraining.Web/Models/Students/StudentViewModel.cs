using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Abp.Configuration.Startup;

namespace ColleageInnerTraining.Web.Models.Strudents
{
    public class StudentViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Gender { get; set; }
    }
}