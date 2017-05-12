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
    public class NoticeDepJobAppService : ColleageInnerTrainingAppServiceBase, INoticeDepJobAppService
    {
        private readonly IRepository<NoticeDepJob, long> _NoticeDepJobRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        public NoticeDepJobAppService(IRepository<NoticeDepJob, long> NoticeDepJobRepository)
        {
            _NoticeDepJobRepository = NoticeDepJobRepository;
        }

        #region 管理

        /// <summary>
        /// 根据查询条件获取分页列表
        /// </summary>
        public PagedResultDto<NoticeDepJobListDto> GetPagedNoticeDepJobs(GetNoticeDepJobInput input)
        {

            var query = _NoticeDepJobRepository.GetAll();
            //TODO:根据传入的参数添加过滤条件

            var NoticeDepJobCount = query.Count();

            var NoticeDepJobs = query
            .OrderByDescending(t=>t.CreationTime)
            .PageBy(input)
            .ToList();

            var NoticeDepJobListDtos = NoticeDepJobs.MapTo<List<NoticeDepJobListDto>>();
            return new PagedResultDto<NoticeDepJobListDto>(
            NoticeDepJobCount,
            NoticeDepJobListDtos
            );
        }

        /// <summary>
        /// 通过Id获取信息进行编辑或修改 
        /// </summary>
        public GetNoticeDepJobForEditOutput GetNoticeDepJobForEdit(NullableIdDto<long> input)
        {
            var output = new GetNoticeDepJobForEditOutput();

            NoticeDepJobEditDto NoticeDepJobEditDto;

            if (input.Id.HasValue)
            {
                var entity = _NoticeDepJobRepository.Get(input.Id.Value);
                NoticeDepJobEditDto = entity.MapTo<NoticeDepJobEditDto>();
            }
            else
            {
                NoticeDepJobEditDto = new NoticeDepJobEditDto();
            }

            output.NoticeDepJob = NoticeDepJobEditDto;
            return output;
        }

        /// <summary>
        /// 通过知识点ID和类型获取数据
        /// </summary>
        public NoticeDepJob GetCDJByNIdOrTypeId(int NId, int BId)
        {
            return _NoticeDepJobRepository.GetAll().Where(c => c.NoticeId == NId && c.BusinessId == BId).FirstOrDefault();
        }
        /// <summary>
        /// 通过指定id获取ListDto信息
        /// </summary>
        public NoticeDepJobListDto GetNoticeDepJobById(EntityDto<long> input)
        {
            var entity = _NoticeDepJobRepository.Get(input.Id);

            return entity.MapTo<NoticeDepJobListDto>();
        }

        /// <summary>
        /// 新增或更改
        /// </summary>
        public void CreateOrUpdateNoticeDepJob(CreateOrUpdateNoticeDepJobInput input)
        {
            if (input.NoticeDepJobEditDto.Id.HasValue)
            {
                UpdateNoticeDepJob(input.NoticeDepJobEditDto);
            }
            else
            {
                CreateNoticeDepJob(input.NoticeDepJobEditDto);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        public NoticeDepJobEditDto CreateNoticeDepJob(NoticeDepJobEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<NoticeDepJob>();

            entity = _NoticeDepJobRepository.Insert(entity);
            return entity.MapTo<NoticeDepJobEditDto>();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public void UpdateNoticeDepJob(NoticeDepJobEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = _NoticeDepJobRepository.Get(input.Id.Value);
            input.MapTo(entity);

            _NoticeDepJobRepository.Update(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void DeleteNoticeDepJob(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            _NoticeDepJobRepository.Delete(input.Id);
        }

        #endregion


    }
}
