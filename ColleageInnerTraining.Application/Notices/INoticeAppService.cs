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
    /// 公告服务接口
    /// </summary>
    public interface INoticeAppService : IApplicationService
    {
        #region 公告管理

        /// <summary>
        /// 根据查询条件获取公告分页列表
        /// </summary>
        PagedResultDto<NoticeListDto> GetPagedNotices(GetNoticeInput input);
        /// <summary>
        /// 获取所有公告
        /// </summary>
        /// <returns></returns>
        List<NoticeListDto> GetAllNotices();
        /// <summary>
        /// 通过Id获取公告信息进行编辑或修改 
        /// </summary>
        GetNoticeForEditOutput GetNoticeForEdit(NullableIdDto<long> input);

		  /// <summary>
        /// 通过指定id获取公告ListDto信息
        /// </summary>
		NoticeListDto GetNoticeById(EntityDto<long> input);



        /// <summary>
        /// 新增或更改公告
        /// </summary>
        Task CreateOrUpdateNoticeAsync(CreateOrUpdateNoticeInput input);

        /// <summary>
        /// 获取所有数据
        /// </summary>
        List<Notice> GetNoticeAll();


        /// <summary>
        /// 新增公告
        /// </summary>
        Task<NoticeEditDto> CreateNoticeAsync(NoticeEditDto input);

        /// <summary>
        /// 更新公告
        /// </summary>
        Task UpdateNoticeAsync(NoticeEditDto input);

        /// <summary>
        /// 删除公告
        /// </summary>
        Task DeleteNoticeAsync(EntityDto<long> input);

        /// <summary>
        /// 批量删除公告
        /// </summary>
        Task BatchDeleteNoticeAsync(List<long> input);

        #endregion

    }
}
