using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 菜单服务实现
    /// </summary>
    public class MenuAppService : ColleageInnerTrainingAppServiceBase, IMenuAppService
    {
        private readonly IRepository<Menu, long> _menuRepository;


        private readonly MenuManage _menuManage;
        /// <summary>
        /// 构造方法
        /// </summary>
        public MenuAppService(IRepository<Menu, long> menuRepository,MenuManage menuManage )
        {
            _menuRepository = menuRepository;
            _menuManage = menuManage;

        }

        #region 菜单管理

        /// <summary>
        /// 根据查询条件获取菜单分页列表
        /// </summary>
        public  List<MenuListDto> GetAllMenus(string menuType)
        {

            var query = _menuRepository.GetAll();
            //TODO:根据传入的参数添加过滤条件            
            var menus = query.OrderBy(t=>t.Sort).ToArray(); 

            var menuListDtos = menus.MapTo<List<MenuListDto>>();
            return menuListDtos;
        }


        /// <summary>
        /// 根据查询条件获取菜单分页列表
        /// </summary>
        public List<MenuListDto> GetMenusByParentId(int parentId)
        {

            var query = _menuRepository.GetAll().Where(item => item.ParentId.Equals(parentId));
            //TODO:根据传入的参数添加过滤条件            
            var menus = query.OrderBy(t => t.Sort).ToArray();

            var menuListDtos = menus.MapTo<List<MenuListDto>>();
            return menuListDtos;
        }
        /// <summary>
        /// 通过Id获取菜单信息进行编辑或修改 
        /// </summary>
        public async Task<GetMenuForEditOutput> GetMenuForEditAsync(NullableIdDto<long> input)
        {
            var output = new GetMenuForEditOutput();

            MenuEditDto menuEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _menuRepository.GetAsync(input.Id.Value);
                menuEditDto = entity.MapTo<MenuEditDto>();
            }
            else
            {
                menuEditDto = new MenuEditDto();
            }

            output.Menu = menuEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取菜单ListDto信息
        /// </summary>
        public MenuListDto GetMenuById(EntityDto<long> input)
        {
            var entity = _menuRepository.Get(input.Id);

            return entity.MapTo<MenuListDto>();
        }







        /// <summary>
        /// 新增或更改菜单
        /// </summary>
        public async Task CreateOrUpdateMenuAsync(CreateOrUpdateMenuInput input)
        {
            if (input.MenuEditDto.Id.HasValue)
            {
                await UpdateMenuAsync(input.MenuEditDto);
            }
            else
            {
                await CreateMenuAsync(input.MenuEditDto);
            }
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        public virtual async Task<MenuEditDto> CreateMenuAsync(MenuEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<Menu>();

            entity = await _menuRepository.InsertAsync(entity);
            return entity.MapTo<MenuEditDto>();
        }

        /// <summary>
        /// 编辑菜单
        /// </summary>
        public virtual async Task UpdateMenuAsync(MenuEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _menuRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            await _menuRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        public async Task DeleteMenuAsync(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _menuRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除菜单
        /// </summary>
        public async Task BatchDeleteMenuAsync(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _menuRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion

    }
}
