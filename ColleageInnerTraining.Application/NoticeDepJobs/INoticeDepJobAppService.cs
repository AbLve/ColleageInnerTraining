using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 服务接口
    /// </summary>
    public interface INoticeDepJobAppService : IApplicationService
    {
        #region 管理

        /// <summary>
        /// 根据查询条件获取分页列表
        /// </summary>
        PagedResultDto<NoticeDepJobListDto> GetPagedNoticeDepJobs(GetNoticeDepJobInput input);

        /// <summary>
        /// 通过Id获取信息进行编辑或修改 
        /// </summary>
        GetNoticeDepJobForEditOutput GetNoticeDepJobForEdit(NullableIdDto<long> input);

        /// <summary>
        /// 通过指定id获取ListDto信息
        /// </summary>
        NoticeDepJobListDto GetNoticeDepJobById(EntityDto<long> input);

        /// <summary>
        /// 通过公告ID获取数据
        /// </summary>
        NoticeDepJob GetCDJByNIdOrTypeId(int NId, int BId);

        /// <summary>
        /// 新增或更改
        /// </summary>
        void CreateOrUpdateNoticeDepJob(CreateOrUpdateNoticeDepJobInput input);


        /// </summary>

        /// <summary>
        /// 新增
        /// </summary>
        NoticeDepJobEditDto CreateNoticeDepJob(NoticeDepJobEditDto input);

        /// <summary>
        /// 更新
        /// </summary>
        void UpdateNoticeDepJob(NoticeDepJobEditDto input);

        /// <summary>
        /// 删除
        /// </summary>
        void DeleteNoticeDepJob(EntityDto<long> input);
        #endregion




    }
}
