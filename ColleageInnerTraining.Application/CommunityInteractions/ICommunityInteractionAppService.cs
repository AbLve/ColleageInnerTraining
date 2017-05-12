using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 互动社区服务接口
    /// </summary>
    public interface ICommunityInteractionAppService : IApplicationService
    {
        #region 互动社区管理

        /// <summary>
        /// 根据查询条件获取互动社区分页列表
        /// </summary>
        PagedResultDto<CommunityInteractionListDto> GetPagedCommunityInteractions(GetCommunityInteractionInput input);

        /// <summary>
        /// 通过Id获取互动社区信息进行编辑或修改 
        /// </summary>
        GetCommunityInteractionForEditOutput GetCommunityInteractionForEdit(NullableIdDto<long> input);

		  /// <summary>
        /// 通过指定id获取互动社区ListDto信息
        /// </summary>
		CommunityInteractionListDto GetCommunityInteractionById(EntityDto<long> input);



        /// <summary>
        /// 新增或更改互动社区
        /// </summary>
        void CreateOrUpdateCommunityInteraction(CreateOrUpdateCommunityInteractionInput input);





        /// <summary>
        /// 新增互动社区
        /// </summary>
        CommunityInteractionEditDto CreateCommunityInteraction(CommunityInteractionEditDto input);

        /// <summary>
        /// 更新互动社区
        /// </summary>
        void UpdateCommunityInteraction(CommunityInteractionEditDto input);

        /// <summary>
        /// 删除互动社区
        /// </summary>
        void DeleteCommunityInteraction(EntityDto<long> input);

        /// <summary>
        /// 批量删除互动社区
        /// </summary>
        void BatchDeleteCommunityInteraction(List<long> input);

        #endregion




    }
}
