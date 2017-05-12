using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 内训师业务管理
    /// </summary>
    public class TeachersManage : IDomainService
    {
        private readonly IRepository<Teachers,long> _teachersRepository;

         /// <summary>
        /// 构造方法
        /// </summary>
        public TeachersManage(IRepository<Teachers,long> teachersRepository  )
        {
            _teachersRepository = teachersRepository;
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
