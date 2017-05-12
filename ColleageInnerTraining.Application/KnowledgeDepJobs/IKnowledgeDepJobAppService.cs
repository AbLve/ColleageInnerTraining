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
    public interface IKnowledgeDepJobAppService : IApplicationService
    {
        #region 管理

        /// <summary>
        /// 根据查询条件获取分页列表
        /// </summary>
        PagedResultDto<KnowledgeDepJobListDto> GetPagedKnowledgeDepJobs(GetKnowledgeDepJobInput input);

        /// <summary>
        /// 通过Id获取信息进行编辑或修改 
        /// </summary>
        GetKnowledgeDepJobForEditOutput GetKnowledgeDepJobForEdit(NullableIdDto<long> input);

        /// <summary>
        /// 通过指定id获取ListDto信息
        /// </summary>
        KnowledgeDepJobListDto GetKnowledgeDepJobById(EntityDto<long> input);

        /// <summary>
        /// 通过知识点ID获取数据
        /// </summary>
        KnowledgeDepJob GetCDJByKIdOrTypeId(int KId, int BId);

        /// <summary>
        /// 新增或更改
        /// </summary>
        void CreateOrUpdateKnowledgeDepJob(CreateOrUpdateKnowledgeDepJobInput input);


        /// </summary>

        /// <summary>
        /// 新增
        /// </summary>
        KnowledgeDepJobEditDto CreateKnowledgeDepJob(KnowledgeDepJobEditDto input);

        /// <summary>
        /// 更新
        /// </summary>
        void UpdateKnowledgeDepJob(KnowledgeDepJobEditDto input);

        /// <summary>
        /// 删除
        /// </summary>
        void DeleteKnowledgeDepJob(EntityDto<long> input);
        #endregion




    }
}
