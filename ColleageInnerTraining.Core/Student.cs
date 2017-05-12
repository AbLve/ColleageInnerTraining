using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColleageInnerTraining
{
    public class Student : Entity<long>
    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Gender { get; set; }
    }
}
