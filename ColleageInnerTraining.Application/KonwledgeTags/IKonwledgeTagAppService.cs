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
    /// 知识库标签服务接口
    /// </summary>
    public interface IKonwledgeTagAppService : IApplicationService
    {
        #region 知识库标签管理

        /// <summary>
        /// 根据查询条件获取知识库标签分页列表
        /// </summary>
        PagedResultDto<KonwledgeTagListDto> GetPagedKonwledgeTags(GetKonwledgeTagInput input);
        /// <summary>
        /// 获取所有知识库标签
        /// </summary>
        /// <returns></returns>
        List<KonwledgeTagListDto> GetAllKonwledgeTags();
        /// <summary>
        /// 通过Id获取知识库标签信息进行编辑或修改 
        /// </summary>
        GetKonwledgeTagForEditOutput GetKonwledgeTagForEdit(NullableIdDto<long> input);

		  /// <summary>
        /// 通过指定id获取知识库标签ListDto信息
        /// </summary>
		KonwledgeTagListDto GetKonwledgeTagById(EntityDto<long> input);



        /// <summary>
        /// 新增或更改知识库标签
        /// </summary>
        Task CreateOrUpdateKonwledgeTagAsync(CreateOrUpdateKonwledgeTagInput input);

        /// <summary>
        /// 获取所有数据
        /// </summary>
        List<KonwledgeTag> GetKonwledgeTagAll();


        /// <summary>
        /// 新增知识库标签
        /// </summary>
        Task<KonwledgeTagEditDto> CreateKonwledgeTagAsync(KonwledgeTagEditDto input);

        /// <summary>
        /// 更新知识库标签
        /// </summary>
        Task UpdateKonwledgeTagAsync(KonwledgeTagEditDto input);

        /// <summary>
        /// 删除知识库标签
        /// </summary>
        Task DeleteKonwledgeTagAsync(EntityDto<long> input);

        /// <summary>
        /// 批量删除知识库标签
        /// </summary>
        Task BatchDeleteKonwledgeTagAsync(List<long> input);

        #endregion

    }
}
