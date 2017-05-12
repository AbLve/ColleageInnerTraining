using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;

namespace ColleageInnerTraining.Application
{
	/// <summary>
    /// 班级项目服务接口
    /// </summary>
    public interface IClassTrainingInfoAppService : IApplicationService
    {
        #region 班级项目管理

        /// <summary>
        /// 获取全部
        /// </summary>
        List<ClassTrainingInfoListDto> GetAll();

        /// <summary>
        /// 根据查询条件获取班级项目分页列表
        /// </summary>
        PagedResultDto<ClassTrainingInfoListDto> GetPagedClassTrainingInfos(GetClassTrainingInfoInput input);

        /// <summary>
        /// 通过Id获取项目信息进行编辑或修改 
        /// </summary>
        Task<GetClassTrainingInfoForEditOutput> GetClassTrainingInfoForEditAsync(NullableIdDto<long> input);



        /// <summary>
        /// 新增或更改班级项目
        /// </summary>
        Task CreateOrUpdateClassTrainingInfoAsync(CreateOrUpdateClassTrainingInfoInput input);


        /// <summary>
        /// 新增班级项目
        /// </summary>
        Task<ClassTrainingInfoEditDto> CreateClassTrainingInfoAsync(ClassTrainingInfoEditDto input);

        /// <summary>
        /// 更新班级项目
        /// </summary>
        Task UpdateClassTrainingInfoAsync(ClassTrainingInfoEditDto input);

        /// <summary>
        /// 删除班级项目
        /// </summary>
        Task DeleteClassTrainingInfoAsync(EntityDto<long> input);

        /// <summary>
        /// 批量删除班级项目
        /// </summary>
        Task BatchDeleteClassTrainingInfoAsync(List<long> input);

        #endregion

    }
}
