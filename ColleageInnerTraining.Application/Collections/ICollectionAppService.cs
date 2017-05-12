using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;

namespace ColleageInnerTraining.Application
{
	/// <summary>
    /// 收藏服务接口
    /// </summary>
    public interface ICollectionAppService : IApplicationService
    {
        #region 收藏管理

        /// <summary>
        /// 根据查询条件获取收藏分页列表
        /// </summary>
        PagedResultDto<CollectionListDto> GetPagedCollections(GetCollectionInput input);

        /// <summary>
        /// 通过Id获取收藏信息进行编辑或修改 
        /// </summary>
        Task<GetCollectionForEditOutput> GetCollectionForEditAsync(NullableIdDto<long> input);

	    /// <summary>
        /// 通过指定id获取收藏ListDto信息
        /// </summary>
		Task<CollectionListDto> GetCollectionByIdAsync(EntityDto<long> input);

        /// <summary>
        /// 新增或更改收藏
        /// </summary>
        void CreateOrUpdateCollection(CreateOrUpdateCollectionInput input);

        /// <summary>
        /// 新增收藏
        /// </summary>
        CollectionEditDto CreateCollection(CollectionEditDto input);

        /// <summary>
        /// 查询用户是否已收藏对像
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="bizId">收藏对像Id</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        CollectionListDto GetCollectionByTypeAndId(string type, int bizId, int userId);
        /// <summary>
        /// 更新收藏
        /// </summary>
        void UpdateCollection(CollectionEditDto input);

        /// <summary>
        /// 删除收藏
        /// </summary>
        void DeleteCollection(EntityDto<long> input);

        /// <summary>
        /// 删除收藏
        /// </summary>
        /// <param name="type"></param>
        /// <param name="bizId"></param>
        /// <param name="userId"></param>
        void DeleteCollectionByBizId(string type, int bizId, int userId);
      
        /// <summary>
        /// 批量删除收藏
        /// </summary>
        Task BatchDeleteCollectionAsync(List<long> input);

        #endregion
    }
}
