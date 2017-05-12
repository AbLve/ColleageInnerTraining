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

namespace ColleageInnerTraining.AbpZeroTemplate.Application
{
    /// <summary>
    /// 轮播图服务实现
    /// </summary>
    public class BannerAppService : ColleageInnerTrainingAppServiceBase, IBannerAppService
    {
        private readonly IRepository<Banner, long> _BannerRepository;


        /// <summary>
        /// 构造方法
        /// </summary>
        public BannerAppService(IRepository<Banner, long> BannerRepository)
        {
            _BannerRepository = BannerRepository;

        }

        #region 轮播图管理

        /// <summary>
        /// 获取全部
        /// </summary>
        public List<BannerListDto> GetAll()
        {
            var query = _BannerRepository.GetAll().ToList();
            if (query.Count < 1)
            {
                return new List<BannerListDto>();
            }
            return query.MapTo<List<BannerListDto>>();
        }

        /// <summary>
        /// 获取全部ByClientType
        /// </summary>
        public List<BannerListDto> GetBannerByClientType(int clientType)
        {
            var query = _BannerRepository.GetAll()
                .Where(item => item.IsDeleted == false)
                .Where(item => item.Enabled == true)
                .Where(item => item.ClientType.Equals(clientType)).ToList();
            if (query.Count < 1)
            {
                return new List<BannerListDto>();
            }
            return query.MapTo<List<BannerListDto>>();
        }

        /// <summary>
        /// 根据查询条件获取轮播图分页列表
        /// </summary>
        public PagedResultDto<BannerListDto> GetPagedBanners(GetBannerInput input)
        {

            var query = _BannerRepository.GetAll()
                        .Where(item => item.IsDeleted == false)
                       .WhereIf(input.Id > 0, item => item.Id == input.Id)
                       .WhereIf(!string.IsNullOrEmpty(input.Title), item => item.Title.Contains(input.Title))
                       .WhereIf(input.ClientType > 0, item => item.ClientType == input.ClientType);
            //TODO:根据传入的参数添加过滤条件

            var BannerCount = query.Count();
            var Banners = query
            .OrderByDescending(t => t.CreationTime)
            .PageBy(input)
            .ToList();
            var BannerListDtos = Banners.MapTo<List<BannerListDto>>();
            return new PagedResultDto<BannerListDto>(BannerCount, BannerListDtos);
        }
        /// <summary>
        /// 通过Id获取轮播图信息进行编辑或修改 
        /// </summary>
        public async Task<GetBannerForEditOutput> GetBannerForEditAsync(NullableIdDto<long> input)
        {
            var output = new GetBannerForEditOutput();

            BannerEditDto EditDto;

            if (input.Id.HasValue)
            {
                var entity = await _BannerRepository.GetAsync(input.Id.Value);
                EditDto = entity.MapTo<BannerEditDto>();
            }
            else
            {
                EditDto = new BannerEditDto();
            }

            output.BannerEditDto = EditDto;
            return output;
        }
        /// <summary>
        /// 新增或更改轮播图
        /// </summary>
        public async Task CreateOrUpdateBannerAsync(CreateOrUpdateBannerInput input)
        {
            if (input.BannerEditDto.Id.HasValue)
            {
                await UpdateBannerAsync(input.BannerEditDto);
            }
            else
            {
                await CreateBannerAsync(input.BannerEditDto);
            }
        }
        /// <summary>
        /// 新增轮播图
        /// </summary>
        public virtual async Task<BannerEditDto> CreateBannerAsync(BannerEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<Banner>();

            entity = await _BannerRepository.InsertAsync(entity);
            return entity.MapTo<BannerEditDto>();
        }

        /// <summary>
        /// 编辑轮播图
        /// </summary>
        public virtual async Task UpdateBannerAsync(BannerEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _BannerRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            await _BannerRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除轮播图
        /// </summary>
        public async Task DeleteBannerAsync(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _BannerRepository.DeleteAsync(input.Id);
        }

        #endregion

    }
}
