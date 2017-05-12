using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using ColleageInnerTraining.Core;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Application;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 知识库标签服务实现
    /// </summary>
    public class KonwledgeTagAppService : ColleageInnerTrainingAppServiceBase, IKonwledgeTagAppService
    {
        private readonly IRepository<KonwledgeTag, long> _KonwledgeTagRepository;


        private readonly IRepository<ClassUser, long> _classUserRepository;
        /// <summary>
        /// 构造方法
        /// </summary>
        public KonwledgeTagAppService(IRepository<KonwledgeTag, long> KonwledgeTagRepository, IRepository<ClassUser, long> classUserRepository

  )
        {
            _KonwledgeTagRepository = KonwledgeTagRepository;
            _classUserRepository = classUserRepository;

        }

        #region 知识库标签管理

        /// <summary>
        /// 根据查询条件获取知识库标签分页列表
        /// </summary>
        public PagedResultDto<KonwledgeTagListDto> GetPagedKonwledgeTags(GetKonwledgeTagInput input)
        {

            var query = _KonwledgeTagRepository.GetAll().
                        WhereIf(input.KId > 0, t => t.knowledgeId == input.KId).
                        WhereIf(input.TId > 0, t => t.TagId == input.TId);

            //TODO:根据传入的参数添加过滤条件
            var KonwledgeTags = query.OrderByDescending(t=>t.CreationTime).PageBy(input).ToList();
            var KonwledgeTagListDto = KonwledgeTags.MapTo<List<KonwledgeTagListDto>>();
            return new PagedResultDto<KonwledgeTagListDto>(query.Count(), KonwledgeTagListDto);
        }

        /// <summary>
        /// 获取所有知识库标签
        /// </summary>
        /// <returns></returns>
        public List<KonwledgeTagListDto> GetAllKonwledgeTags()
        {
            try
            {
                var query = _KonwledgeTagRepository.GetAll();
                //TODO:根据传入的参数添加过滤条件
                var KonwledgeTags = query.ToList();
                var KonwledgeTagListDto = KonwledgeTags.MapTo<List<KonwledgeTagListDto>>();
                return KonwledgeTagListDto;
            }
            catch (Exception e)
            {
                return new List<KonwledgeTagListDto>();
            }

        }

        /// <summary>
        /// 通过Id获取知识库标签信息进行编辑或修改 
        /// </summary>
        public GetKonwledgeTagForEditOutput GetKonwledgeTagForEdit(NullableIdDto<long> input)
        {
            var output = new GetKonwledgeTagForEditOutput();

            KonwledgeTagEditDto KonwledgeTagEditDto;

            if (input.Id.HasValue)
            {
                var entity = _KonwledgeTagRepository.Get(input.Id.Value);
                KonwledgeTagEditDto = entity.MapTo<KonwledgeTagEditDto>();
            }
            else
            {
                KonwledgeTagEditDto = new KonwledgeTagEditDto();
            }

            output.KonwledgeTag = KonwledgeTagEditDto;
            return output;
        }

        /// <summary>
        /// 通过指定id获取知识库标签ListDto信息
        /// </summary>
        public KonwledgeTagListDto GetKonwledgeTagById(EntityDto<long> input)
        {
            var entity = _KonwledgeTagRepository.Get(input.Id);

            return entity.MapTo<KonwledgeTagListDto>();
        }
        /// <summary>
        /// 获取所有知识库标签
        /// </summary>
        public List<KonwledgeTag> GetKonwledgeTagAll()
        {
            var entity = _KonwledgeTagRepository.GetAll().ToList();

            return entity;
        }

        /// <summary>
        /// 新增或更改知识库标签
        /// </summary>
        public async Task CreateOrUpdateKonwledgeTagAsync(CreateOrUpdateKonwledgeTagInput input)
        {
            if (input.KonwledgeTagEditDto.Id.HasValue)
            {
                await UpdateKonwledgeTagAsync(input.KonwledgeTagEditDto);
            }
            else
            {
                await CreateKonwledgeTagAsync(input.KonwledgeTagEditDto);
            }
        }

        /// <summary>
        /// 新增知识库标签
        /// </summary>
        public virtual async Task<KonwledgeTagEditDto> CreateKonwledgeTagAsync(KonwledgeTagEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<KonwledgeTag>();

            entity = await _KonwledgeTagRepository.InsertAsync(entity);
            return entity.MapTo<KonwledgeTagEditDto>();
        }

        /// <summary>
        /// 编辑知识库标签
        /// </summary>
        public virtual async Task UpdateKonwledgeTagAsync(KonwledgeTagEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _KonwledgeTagRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            await _KonwledgeTagRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除知识库标签
        /// </summary>
        public async Task DeleteKonwledgeTagAsync(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _KonwledgeTagRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除知识库标签
        /// </summary>
        public async Task BatchDeleteKonwledgeTagAsync(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _KonwledgeTagRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion
    }
}
