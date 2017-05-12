
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;

namespace ColleageInnerTraining.Application
{
	/// <summary>
    /// 岗位服务接口
    /// </summary>
    public interface IJobPostAppService : IApplicationService
    {
        #region 岗位管理

        /// <summary>
        /// 根据查询条件获取岗位分页列表
        /// </summary>
        PagedResultDto<JobPostListDto> GetPagedJobPosts(GetJobPostInput input);
        /// <summary>
        /// 获取所有岗位
        /// </summary>
        /// <returns></returns>
        List<JobPostListDto> GetAllJobPosts();
        /// <summary>
        /// 通过Id获取岗位信息进行编辑或修改 
        /// </summary>
        Task<GetJobPostForEditOutput> GetJobPostForEditAsync(NullableIdDto<long> input);

		  /// <summary>
        /// 通过指定id获取岗位ListDto信息
        /// </summary>
		JobPostListDto GetJobPostById(EntityDto<long> input);



        /// <summary>
        /// 新增或更改岗位
        /// </summary>
        Task CreateOrUpdateJobPostAsync(CreateOrUpdateJobPostInput input);


        /// <summary>
        /// 获取所有数据
        /// </summary>
        List<JobPostEditDto> GetJobPostDataList();

        /// <summary>
        /// 新增岗位
        /// </summary>
        Task<JobPostEditDto> CreateJobPostAsync(JobPostEditDto input);

        /// <summary>
        /// 更新岗位
        /// </summary>
        Task UpdateJobPostAsync(JobPostEditDto input);

        /// <summary>
        /// 删除岗位
        /// </summary>
        Task DeleteJobPostAsync(EntityDto<long> input);

        /// <summary>
        /// 批量删除岗位
        /// </summary>
        Task BatchDeleteJobPostAsync(List<long> input);

        #endregion

    }
}
