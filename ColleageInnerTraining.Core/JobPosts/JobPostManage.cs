using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 岗位业务管理
    /// </summary>
    public class JobPostManage : IDomainService
    {
        private readonly IRepository<JobPost,long> _jobPostRepository;

         /// <summary>
        /// 构造方法
        /// </summary>
        public JobPostManage(IRepository<JobPost,long> jobPostManageRepository)
        {
            _jobPostRepository = jobPostManageRepository;
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
