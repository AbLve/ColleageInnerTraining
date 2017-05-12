using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColleageInnerTraining.StudentService
{
    public interface IStudentAppService: IApplicationService
    {
        List<Student> GetAll();
        Student GetById(long id);
        void Save(Student entity);
        void Update(Student entity);
    }
}
