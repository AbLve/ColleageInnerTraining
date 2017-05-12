using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System.Linq;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 课程分类业务管理
    /// </summary>
    public class CourseCategoryManage : IDomainService
    {
        private readonly IRepository<CourseCategory, long> _courseCategoryRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        public CourseCategoryManage(IRepository<CourseCategory, long> courseCategoryRepository)
        {
            _courseCategoryRepository = courseCategoryRepository;
        }

        //TODO:编写领域业务代码

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {

        }

        /// <summary>
        /// 查询所有分类
        /// </summary>
        public IQueryable<CourseCategory> CourseCategorys => _courseCategoryRepository.GetAll().Where(item => item.IsDeleted == false).Where(item => item.Enabled == true);

    }
}
