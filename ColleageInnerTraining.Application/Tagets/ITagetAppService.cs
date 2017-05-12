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
    /// 标签服务接口
    /// </summary>
    public interface ITagetAppService : IApplicationService
    {
        #region 标签管理

        /// <summary>
        /// 根据查询条件获取标签分页列表
        /// </summary>
        PagedResultDto<TagetListDto> GetPagedTagets(GetTagetInput input);
        /// <summary>
        /// 获取所有标签
        /// </summary>
        /// <returns></returns>
        List<TagetListDto> GetAllTagets();
        /// <summary>
        /// 通过Id获取标签信息进行编辑或修改 
        /// </summary>
        GetTagetForEditOutput GetTagetForEdit(NullableIdDto<long> input);

		  /// <summary>
        /// 通过指定id获取标签ListDto信息
        /// </summary>
		TagetListDto GetTagetById(EntityDto<long> input);



        /// <summary>
        /// 新增或更改标签
        /// </summary>
        Task CreateOrUpdateTagetAsync(CreateOrUpdateTagetInput input);

        /// <summary>
        /// 获取所有数据
        /// </summary>
        List<Taget> GetTagetAll();


        /// <summary>
        /// 新增标签
        /// </summary>
        Task<TagetEditDto> CreateTagetAsync(TagetEditDto input);

        /// <summary>
        /// 更新标签
        /// </summary>
        Task UpdateTagetAsync(TagetEditDto input);

        /// <summary>
        /// 删除标签
        /// </summary>
        Task DeleteTagetAsync(EntityDto<long> input);

        /// <summary>
        /// 批量删除标签
        /// </summary>
        Task BatchDeleteTagetAsync(List<long> input);

        #endregion

    }
}
