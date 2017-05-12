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
    /// 班级服务接口
    /// </summary>
    public interface IClassesInfoAppService : IApplicationService
    {
        #region 班级管理

        /// <summary>
        /// 根据查询条件获取班级分页列表
        /// </summary>
        PagedResultDto<ClassesInfoListDto> GetPagedClassesInfos(GetClassesInfoInput input);
        /// <summary>
        /// 获取所有班级
        /// </summary>
        /// <returns></returns>
        List<ClassesInfoListDto> GetAllClassesInfos();
        /// <summary>
        /// 通过Id获取班级信息进行编辑或修改 
        /// </summary>
        GetClassesInfoForEditOutput GetClassesInfoForEdit(NullableIdDto<long> input);

		  /// <summary>
        /// 通过指定id获取班级ListDto信息
        /// </summary>
		ClassesInfoListDto GetClassesInfoById(EntityDto<long> input);

        /// <summary>
        /// 通过指定id获取班级ListDto信息
        /// </summary>
        ClassesInfoEditDto GetClassesInfoEditById(EntityDto<long> input);

        /// <summary>
        /// 新增或更改班级
        /// </summary>
        Task CreateOrUpdateClassesInfoAsync(CreateOrUpdateClassesInfoInput input);

        /// <summary>
        /// 获取所有数据
        /// </summary>
        List<ClassesInfo> GetClassesInfoAll();


        /// <summary>
        /// 新增班级
        /// </summary>
        Task<ClassesInfoEditDto> CreateClassesInfoAsync(ClassesInfoEditDto input);

        /// <summary>
        /// 更新班级
        /// </summary>
        Task UpdateClassesInfoAsync(ClassesInfoEditDto input);

        /// <summary>
        /// 删除班级
        /// </summary>
        Task DeleteClassesInfoAsync(EntityDto<long> input);

        /// <summary>
        /// 批量删除班级
        /// </summary>
        Task BatchDeleteClassesInfoAsync(List<long> input);

        #endregion

    }
}
