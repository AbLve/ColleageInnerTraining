
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 菜单业务管理
    /// </summary>
    public class MenuManage : IDomainService
    {
        private readonly IRepository<Menu,long> _menuRepository;

         /// <summary>
        /// 构造方法
        /// </summary>
        public MenuManage(IRepository<Menu,long> menuRepository  )
        {
            _menuRepository = menuRepository;
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
