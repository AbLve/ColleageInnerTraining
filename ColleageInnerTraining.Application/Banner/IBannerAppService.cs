using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;

namespace ColleageInnerTraining.Application
{
	/// <summary>
    /// 轮播图服务接口
    /// </summary>
    public interface IBannerAppService : IApplicationService
    {
        #region 轮播图管理

        /// <summary>
        /// 获取全部
        /// </summary>
        List<BannerListDto> GetAll();

        /// <summary>
        /// 根据类
        /// </summary>
        /// <param name="clientType"></param>
        /// <returns></returns>
        List<BannerListDto> GetBannerByClientType(int clientType);

        /// <summary>
        /// 根据查询条件获取轮播图分页列表
        /// </summary>
        PagedResultDto<BannerListDto> GetPagedBanners(GetBannerInput input);

        /// <summary>
        /// 通过Id获取项目信息进行编辑或修改 
        /// </summary>
        Task<GetBannerForEditOutput> GetBannerForEditAsync(NullableIdDto<long> input);



        /// <summary>
        /// 新增或更改轮播图
        /// </summary>
        Task CreateOrUpdateBannerAsync(CreateOrUpdateBannerInput input);


        /// <summary>
        /// 新增轮播图
        /// </summary>
        Task<BannerEditDto> CreateBannerAsync(BannerEditDto input);

        /// <summary>
        /// 更新轮播图
        /// </summary>
        Task UpdateBannerAsync(BannerEditDto input);

        /// <summary>
        /// 删除轮播图
        /// </summary>
        Task DeleteBannerAsync(EntityDto<long> input);
        #endregion

    }
}
