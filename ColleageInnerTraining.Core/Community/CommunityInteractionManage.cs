using Abp.Domain.Repositories;
using Abp.Domain.Services;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 互动社区业务管理
    /// </summary>
    public class CommunityInteractionManage : IDomainService
    {
        private readonly IRepository<CommunityInteraction,long> _communityInteractionRepository;

         /// <summary>
        /// 构造方法
        /// </summary>
        public CommunityInteractionManage(IRepository<CommunityInteraction,long> communityInteractionRepository  )
        {
            _communityInteractionRepository = communityInteractionRepository;
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
