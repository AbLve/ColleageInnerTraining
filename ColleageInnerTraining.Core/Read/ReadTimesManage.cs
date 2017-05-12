using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 阅读人数业务管理
    /// </summary>
    public class ReadTimesManage : IDomainService
    {
        private readonly IRepository<ReadTimes,long> _readTimesRepository;

         /// <summary>
        /// 构造方法
        /// </summary>
        public ReadTimesManage(IRepository<ReadTimes,long> readTimesRepository  )
        {
            _readTimesRepository = readTimesRepository;
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
