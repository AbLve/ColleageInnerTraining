using Abp.Domain.Repositories;
using Abp.Domain.Services;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 用户账号信息业务管理
    /// </summary>
    public class UserAccountManage : IDomainService
    {
        private readonly IRepository<UserAccount,long> _userAccountRepository;

         /// <summary>
        /// 构造方法
        /// </summary>
        public UserAccountManage(IRepository<UserAccount,long> userAccountRepository  )
        {
            _userAccountRepository = userAccountRepository;
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
