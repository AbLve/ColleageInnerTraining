using System.Collections.Generic;
using Abp.AutoMapper;
using System;

namespace ColleageInnerTraining.Web.Models.Strudents
{
    public class StudentPageViewModel
    {

        public string Name { get; set; }
        public List<StudentInfo> Students { get; set; }

        [AutoMapFrom(typeof(Student))]
        public class StudentInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public DateTime BirthDate { get; set; }

            public bool Gender { get; set; }
        }

    }
}