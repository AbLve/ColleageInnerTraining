using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 收藏业务管理
    /// </summary>
    public class CollectionManage : IDomainService
    {
        private readonly IRepository<Collection,long> _collectionRepository;

         /// <summary>
        /// 构造方法
        /// </summary>
        public CollectionManage(IRepository<Collection,long> collectionRepository  )
        {
            _collectionRepository = collectionRepository;
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
