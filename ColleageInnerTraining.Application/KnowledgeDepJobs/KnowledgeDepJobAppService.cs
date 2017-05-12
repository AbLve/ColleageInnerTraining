using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using ColleageInnerTraining.Core;
using ColleageInnerTraining.Application.Dtos;
using System;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 服务实现
    /// </summary>
    public class KnowledgeDepJobAppService : ColleageInnerTrainingAppServiceBase, IKnowledgeDepJobAppService
    {
        private readonly IRepository<KnowledgeDepJob, long> _KnowledgeDepJobRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        public KnowledgeDepJobAppService(IRepository<KnowledgeDepJob, long> KnowledgeDepJobRepository)
        {
            _KnowledgeDepJobRepository = KnowledgeDepJobRepository;
        }

        #region 管理

        /// <summary>
        /// 根据查询条件获取分页列表
        /// </summary>
        public PagedResultDto<KnowledgeDepJobListDto> GetPagedKnowledgeDepJobs(GetKnowledgeDepJobInput input)
        {

            var query = _KnowledgeDepJobRepository.GetAll();
            //TODO:根据传入的参数添加过滤条件

            var KnowledgeDepJobCount = query.Count();

            var KnowledgeDepJobs = query
            .OrderByDescending(t=>t.CreationTime)
            .PageBy(input)
            .ToList();

            var KnowledgeDepJobListDtos = KnowledgeDepJobs.MapTo<List<KnowledgeDepJobListDto>>();
            return new PagedResultDto<KnowledgeDepJobListDto>(
            KnowledgeDepJobCount,
            KnowledgeDepJobListDtos
            );
        }

        /// <summary>
        /// 通过Id获取信息进行编辑或修改 
        /// </summary>
        public GetKnowledgeDepJobForEditOutput GetKnowledgeDepJobForEdit(NullableIdDto<long> input)
        {
            var output = new GetKnowledgeDepJobForEditOutput();

            KnowledgeDepJobEditDto KnowledgeDepJobEditDto;

            if (input.Id.HasValue)
            {
                var entity = _KnowledgeDepJobRepository.Get(input.Id.Value);
                KnowledgeDepJobEditDto = entity.MapTo<KnowledgeDepJobEditDto>();
            }
            else
            {
                KnowledgeDepJobEditDto = new KnowledgeDepJobEditDto();
            }

            output.KnowledgeDepJob = KnowledgeDepJobEditDto;
            return output;
        }

        /// <summary>
        /// 通过知识点ID和类型获取数据
        /// </summary>
        public KnowledgeDepJob GetCDJByKIdOrTypeId(int KId, int BId)
        {
            return _KnowledgeDepJobRepository.GetAll().Where(c => c.KnoledgeId == KId && c.BusinessId == BId).FirstOrDefault();
        }
        /// <summary>
        /// 通过指定id获取ListDto信息
        /// </summary>
        public KnowledgeDepJobListDto GetKnowledgeDepJobById(EntityDto<long> input)
        {
            var entity = _KnowledgeDepJobRepository.Get(input.Id);

            return entity.MapTo<KnowledgeDepJobListDto>();
        }

        /// <summary>
        /// 新增或更改
        /// </summary>
        public void CreateOrUpdateKnowledgeDepJob(CreateOrUpdateKnowledgeDepJobInput input)
        {
            if (input.KnowledgeDepJobEditDto.Id.HasValue)
            {
                UpdateKnowledgeDepJob(input.KnowledgeDepJobEditDto);
            }
            else
            {
                CreateKnowledgeDepJob(input.KnowledgeDepJobEditDto);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        public KnowledgeDepJobEditDto CreateKnowledgeDepJob(KnowledgeDepJobEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<KnowledgeDepJob>();

            entity = _KnowledgeDepJobRepository.Insert(entity);
            return entity.MapTo<KnowledgeDepJobEditDto>();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public void UpdateKnowledgeDepJob(KnowledgeDepJobEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = _KnowledgeDepJobRepository.Get(input.Id.Value);
            input.MapTo(entity);

            _KnowledgeDepJobRepository.Update(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void DeleteKnowledgeDepJob(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            _KnowledgeDepJobRepository.Delete(input.Id);
        }

        #endregion


    }
}
