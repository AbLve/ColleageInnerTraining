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
    /// 标签服务实现
    /// </summary>
    public class TagetAppService : ColleageInnerTrainingAppServiceBase, ITagetAppService
    {
        private readonly IRepository<Taget, long> _TagetRepository;


        private readonly IRepository<ClassUser, long> _classUserRepository;
        /// <summary>
        /// 构造方法
        /// </summary>
        public TagetAppService(IRepository<Taget, long> TagetRepository, IRepository<ClassUser, long> classUserRepository

  )
        {
            _TagetRepository = TagetRepository;
            _classUserRepository = classUserRepository;

        }

        #region 标签管理

        /// <summary>
        /// 根据查询条件获取标签分页列表
        /// </summary>
        public PagedResultDto<TagetListDto> GetPagedTagets(GetTagetInput input)
        {

            var query = _TagetRepository.GetAll().
                        WhereIf(!string.IsNullOrEmpty(input.FilterText), t => t.Name.Contains(input.FilterText));

            //TODO:根据传入的参数添加过滤条件
            var Tagets = query.OrderByDescending(t => t.CreationTime).PageBy(input).ToList();
            var TagetListDto = Tagets.MapTo<List<TagetListDto>>();
            return new PagedResultDto<TagetListDto>(query.Count(), TagetListDto);
        }

        /// <summary>
        /// 获取所有标签
        /// </summary>
        /// <returns></returns>
        public List<TagetListDto> GetAllTagets()
        {
            try
            {
                var query = _TagetRepository.GetAll();
                //TODO:根据传入的参数添加过滤条件
                var Tagets = query.ToList();
                var TagetListDto = Tagets.MapTo<List<TagetListDto>>();
                return TagetListDto;
            }
            catch (Exception e)
            {
                return new List<TagetListDto>();
            }

        }

        /// <summary>
        /// 通过Id获取标签信息进行编辑或修改 
        /// </summary>
        public GetTagetForEditOutput GetTagetForEdit(NullableIdDto<long> input)
        {
            var output = new GetTagetForEditOutput();

            TagetEditDto TagetEditDto;

            if (input.Id.HasValue)
            {
                var entity = _TagetRepository.Get(input.Id.Value);
                TagetEditDto = entity.MapTo<TagetEditDto>();
            }
            else
            {
                TagetEditDto = new TagetEditDto();
            }

            output.Taget = TagetEditDto;
            return output;
        }

        /// <summary>
        /// 通过指定id获取标签ListDto信息
        /// </summary>
        public TagetListDto GetTagetById(EntityDto<long> input)
        {
            var entity = _TagetRepository.Get(input.Id);

            return entity.MapTo<TagetListDto>();
        }
        /// <summary>
        /// 获取所有标签
        /// </summary>
        public List<Taget> GetTagetAll()
        {
            var entity = _TagetRepository.GetAll().ToList();

            return entity;
        }

        /// <summary>
        /// 新增或更改标签
        /// </summary>
        public async Task CreateOrUpdateTagetAsync(CreateOrUpdateTagetInput input)
        {
            if (input.TagetEditDto.Id.HasValue)
            {
                await UpdateTagetAsync(input.TagetEditDto);
            }
            else
            {
                await CreateTagetAsync(input.TagetEditDto);
            }
        }

        /// <summary>
        /// 新增标签
        /// </summary>
        public virtual async Task<TagetEditDto> CreateTagetAsync(TagetEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<Taget>();

            entity = await _TagetRepository.InsertAsync(entity);
            return entity.MapTo<TagetEditDto>();
        }

        /// <summary>
        /// 编辑标签
        /// </summary>
        public virtual async Task UpdateTagetAsync(TagetEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _TagetRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            await _TagetRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        public async Task DeleteTagetAsync(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _TagetRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除标签
        /// </summary>
        public async Task BatchDeleteTagetAsync(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _TagetRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion

    }
}
