using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System.Collections.Generic;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 课程基本信息业务管理
    /// </summary>
    public class CourseInfoManage : IDomainService
    {
        private readonly IRepository<CourseInfo,long> _courseInfoRepository;

         /// <summary>
        /// 构造方法
        /// </summary>
        public CourseInfoManage(IRepository<CourseInfo,long> courseInfoRepository  )
        {
            _courseInfoRepository = courseInfoRepository;
        }

		//TODO:编写领域业务代码


		/// <summary>
        ///     初始化
        /// </summary>
        private void Init()
        {
            
        }        

    }

    
	
}
