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
    /// 服务实现
    /// </summary>
    public class KonwledgeInfoAppService : ColleageInnerTrainingAppServiceBase, IKonwledgeInfoAppService
    {
        private readonly IRepository<KonwledgeInfo, long> _KonwledgeInfoRepository;

        private readonly IRepository<Taget, long> tageservice;

        private readonly IRepository<UserAccount, long> userservice;

        private readonly IRepository<CourseCategory, long> cservice;
        /// <summary>
        /// 构造方法
        /// </summary>
        public KonwledgeInfoAppService(IRepository<KonwledgeInfo, long> KonwledgeInfoRepository, IRepository<UserAccount, long> _userservice, IRepository<Taget, long> _tageservice, IRepository<CourseCategory, long> _cservice)
        {
            _KonwledgeInfoRepository = KonwledgeInfoRepository;
            userservice = _userservice;
            tageservice = _tageservice;
            cservice = _cservice;
        }

        #region 管理

        /// <summary>
        /// 根据查询条件获取分页列表
        /// </summary>
        public PagedResultDto<KonwledgeInfoListDto> GetPagedKonwledgeInfos(GetKonwledgeInfoInput input)
        {

            var query = from a in _KonwledgeInfoRepository.GetAll()
                        join b in userservice.GetAll() on a.CreatorUserId equals b.Id into bc
                        from c in bc.DefaultIfEmpty()
                        join d in userservice.GetAll() on a.Auditor equals d.Id into dc
                        from e in dc.DefaultIfEmpty()
                        join f in tageservice.GetAll() on a.TagetId equals f.Id into fc
                        from g in fc.DefaultIfEmpty()
                        join h in cservice.GetAll() on a.TypeId equals h.CategoryId into hc
                        from i in hc.DefaultIfEmpty()
                        select new KonwledgeInfoListDto
                        {
                            Id = a.Id,
                            Title = a.Title,
                            Content = a.Content,
                            ImageUrl = a.ImageUrl,
                            CreationTime = a.CreationTime,
                            CreatorUserName = c.DisplayName,
                            Enabled = a.Enabled,
                            AuditorName = e.DisplayName,
                            Stauts = a.Stauts,
                            ReviewedDate = a.ReviewedDate,
                            CollectionCount = a.CollectionCount,
                            ReadingCount = a.ReadingCount,
                            TypeId = a.TypeId,
                            TagetName = g.Name,
                            TagetId = a.TagetId,
                            TypeName = i.CourseCategoryName
                        };
            query = query.WhereIf(!string.IsNullOrEmpty(input.FilterText), t => t.Title.Contains(input.FilterText)).
                          WhereIf(input.TagetId > 0, t => t.TagetId == input.TagetId).
                          WhereIf(input.TypeId > 0, t => t.TypeId == input.TypeId);

            //TODO:根据传入的参数添加过滤条件
            var KonwledgeInfos = query.OrderByDescending(t=>t.CreationTime).PageBy(input).ToList();
            var KonwledgeInfoListDto = KonwledgeInfos.MapTo<List<KonwledgeInfoListDto>>();
            return new PagedResultDto<KonwledgeInfoListDto>(query.Count(), KonwledgeInfoListDto);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<KonwledgeInfoListDto> GetAllKonwledgeInfos()
        {
            try
            {
                var query = _KonwledgeInfoRepository.GetAll();
                //TODO:根据传入的参数添加过滤条件
                var KonwledgeInfos = query.ToList();
                var KonwledgeInfoListDto = KonwledgeInfos.MapTo<List<KonwledgeInfoListDto>>();
                return KonwledgeInfoListDto;
            }
            catch (Exception e)
            {
                return new List<KonwledgeInfoListDto>();
            }

        }

        /// <summary>
        /// 通过Id获取信息进行编辑或修改 
        /// </summary>
        public GetKonwledgeInfoForEditOutput GetKonwledgeInfoForEdit(NullableIdDto<long> input)
        {
            var output = new GetKonwledgeInfoForEditOutput();

            KonwledgeInfoEditDto KonwledgeInfoEditDto;

            if (input.Id.HasValue)
            {
                var entity = _KonwledgeInfoRepository.Get(input.Id.Value);
                KonwledgeInfoEditDto = entity.MapTo<KonwledgeInfoEditDto>();
            }
            else
            {
                KonwledgeInfoEditDto = new KonwledgeInfoEditDto();
            }

            output.KonwledgeInfo = KonwledgeInfoEditDto;
            return output;
        }

        /// <summary>
        /// 通过指定id获取ListDto信息
        /// </summary>
        public KonwledgeInfoListDto GetKonwledgeInfoById(EntityDto<long> input)
        {
            var entity = _KonwledgeInfoRepository.Get(input.Id);

            return entity.MapTo<KonwledgeInfoListDto>();
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        public List<KonwledgeInfo> GetKonwledgeInfoAll()
        {
            var entity = _KonwledgeInfoRepository.GetAll().ToList();

            return entity;
        }

        /// <summary>
        /// 新增或更改
        /// </summary>
        public async Task CreateOrUpdateKonwledgeInfoAsync(CreateOrUpdateKonwledgeInfoInput input)
        {
            if (input.KonwledgeInfoEditDto.Id.HasValue)
            {
                await UpdateKonwledgeInfoAsync(input.KonwledgeInfoEditDto);
            }
            else
            {
                await CreateKonwledgeInfoAsync(input.KonwledgeInfoEditDto);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        public virtual async Task<KonwledgeInfoEditDto> CreateKonwledgeInfoAsync(KonwledgeInfoEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<KonwledgeInfo>();

            entity = await _KonwledgeInfoRepository.InsertAsync(entity);
            return entity.MapTo<KonwledgeInfoEditDto>();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public virtual async Task UpdateKonwledgeInfoAsync(KonwledgeInfoEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _KonwledgeInfoRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            await _KonwledgeInfoRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public async Task DeleteKonwledgeInfoAsync(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _KonwledgeInfoRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        public async Task BatchDeleteKonwledgeInfoAsync(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _KonwledgeInfoRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion
    }
}
