using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application
{
	/// <summary>
    /// 知识库服务接口
    /// </summary>
    public interface IKonwledgeInfoAppService : IApplicationService
    {
        #region 知识库管理

        /// <summary>
        /// 根据查询条件获取知识库分页列表
        /// </summary>
        PagedResultDto<KonwledgeInfoListDto> GetPagedKonwledgeInfos(GetKonwledgeInfoInput input);
        /// <summary>
        /// 获取所有知识库
        /// </summary>
        /// <returns></returns>
        List<KonwledgeInfoListDto> GetAllKonwledgeInfos();
        /// <summary>
        /// 通过Id获取知识库信息进行编辑或修改 
        /// </summary>
        GetKonwledgeInfoForEditOutput GetKonwledgeInfoForEdit(NullableIdDto<long> input);

		  /// <summary>
        /// 通过指定id获取知识库ListDto信息
        /// </summary>
		KonwledgeInfoListDto GetKonwledgeInfoById(EntityDto<long> input);



        /// <summary>
        /// 新增或更改知识库
        /// </summary>
        Task CreateOrUpdateKonwledgeInfoAsync(CreateOrUpdateKonwledgeInfoInput input);

        /// <summary>
        /// 获取所有数据
        /// </summary>
        List<KonwledgeInfo> GetKonwledgeInfoAll();


        /// <summary>
        /// 新增知识库
        /// </summary>
        Task<KonwledgeInfoEditDto> CreateKonwledgeInfoAsync(KonwledgeInfoEditDto input);

        /// <summary>
        /// 更新知识库
        /// </summary>
        Task UpdateKonwledgeInfoAsync(KonwledgeInfoEditDto input);

        /// <summary>
        /// 删除知识库
        /// </summary>
        Task DeleteKonwledgeInfoAsync(EntityDto<long> input);

        /// <summary>
        /// 批量删除知识库
        /// </summary>
        Task BatchDeleteKonwledgeInfoAsync(List<long> input);

        #endregion

    }
}
