using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 课程人员绑定表业务管理
    /// </summary>
    public class CourseBoundPersonnelManage : IDomainService
    {
        private readonly IRepository<CourseBoundPersonnel,long> _courseBoundPersonnelRepository;

         /// <summary>
        /// 构造方法
        /// </summary>
        public CourseBoundPersonnelManage(IRepository<CourseBoundPersonnel,long> courseBoundPersonnelRepository  )
        {
            _courseBoundPersonnelRepository = courseBoundPersonnelRepository;
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
