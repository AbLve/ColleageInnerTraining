
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;

namespace ColleageInnerTraining.Application
{
	/// <summary>
    /// 菜单服务接口
    /// </summary>
    public interface IMenuAppService : IApplicationService
    {
        #region 菜单管理

        /// <summary>
        /// 根据查询条件获取菜单分页列表
        /// </summary>
        List<MenuListDto> GetAllMenus(string menuType);


        List<MenuListDto> GetMenusByParentId(int parentId);

        /// <summary>
        /// 通过Id获取菜单信息进行编辑或修改 
        /// </summary>
        Task<GetMenuForEditOutput> GetMenuForEditAsync(NullableIdDto<long> input);

		  /// <summary>
        /// 通过指定id获取菜单ListDto信息
        /// </summary>
		MenuListDto GetMenuById(EntityDto<long> input);



        /// <summary>
        /// 新增或更改菜单
        /// </summary>
        Task CreateOrUpdateMenuAsync(CreateOrUpdateMenuInput input);





        /// <summary>
        /// 新增菜单
        /// </summary>
        Task<MenuEditDto> CreateMenuAsync(MenuEditDto input);

        /// <summary>
        /// 更新菜单
        /// </summary>
        Task UpdateMenuAsync(MenuEditDto input);

        /// <summary>
        /// 删除菜单
        /// </summary>
        Task DeleteMenuAsync(EntityDto<long> input);

        /// <summary>
        /// 批量删除菜单
        /// </summary>
        Task BatchDeleteMenuAsync(List<long> input);

        #endregion




    }
}
