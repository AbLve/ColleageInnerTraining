using Abp.Domain.Repositories;
using ColleageInnerTraining.Core;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ColleageInnerTraining.StudentService
{
    public class StudentAppService : IStudentAppService
    {
        private IRepository<Student,long> studentRepository;

        private readonly ISqlExecuter _sqlExecuter;
        public StudentAppService(IRepository<Student,long> reposi)
        {
            this.studentRepository = reposi;
        }

        public StudentAppService(ISqlExecuter sqlExecuter)
        {
            _sqlExecuter = sqlExecuter;
        }

        public Student GetById(long id)
        {
            return this.studentRepository.Get(id);
        }
        public List<Student> GetAll()
        {
            return this.studentRepository.GetAll().ToList();
        }

        public void Save(Student entity)
        {
            this.studentRepository.Insert(entity);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Student entity)
        {
            this.studentRepository.Update(entity);
        }

        public int delete(long Id)
        {
            return this._sqlExecuter.Execute("update student set g", new SqlParameter("@Id", Id));
        }
       
    }
}
