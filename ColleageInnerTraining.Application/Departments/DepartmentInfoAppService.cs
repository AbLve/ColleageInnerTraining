using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using ColleageInnerTraining.Application.Dtos;
using System;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;
using ColleageInnerTraining.Core;
namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 部门服务实现
    /// </summary>
    public class DepartmentInfoAppService : ColleageInnerTrainingAppServiceBase, IDepartmentInfoAppService
    {
        private readonly IRepository<DepartmentInfo, long> _departmentInfoRepository;

        private readonly DepartmentInfoManage _departmentInfoManage;

        private readonly ISqlExecuter _sqlExecuter;

        /// <summary>
        /// 构造方法
        /// </summary>
        public DepartmentInfoAppService(IRepository<DepartmentInfo, long> departmentInfoRepository,DepartmentInfoManage departmentInfoManage, ISqlExecuter sqlExecuter)
        {
            _departmentInfoRepository = departmentInfoRepository;
            _departmentInfoManage = departmentInfoManage;
            _sqlExecuter = sqlExecuter;

        }

        #region 部门管理

        /// <summary>
        /// 根据查询条件获取部门分页列表
        /// </summary>
        public PagedResultDto<DepartmentInfoListDto> GetPagedDepartmentInfos(GetDepartmentInfoInput input)
        {

            var query = _departmentInfoRepository.GetAll();
            //TODO:根据传入的参数添加过滤条件

            var departmentInfoCount = query.Count();

            var departmentInfos = query
            .OrderByDescending(t => t.CreationTime)
            .PageBy(input)
            .ToList();

            var departmentInfoListDtos = departmentInfos.MapTo<List<DepartmentInfoListDto>>();
            return new PagedResultDto<DepartmentInfoListDto>(
            departmentInfoCount,
            departmentInfoListDtos
            );
        }

        public List<DepartmentInfoListDto> GetAllDepartmentInfos()
        {
            try
            {
                var query = _departmentInfoRepository.GetAll();
                //TODO:根据传入的参数添加过滤条件
                var departmentInfos = query.ToList();
                var departmentInfoListDtos = departmentInfos.MapTo<List<DepartmentInfoListDto>>();
                return departmentInfoListDtos;
            }
            catch (Exception e)
            {
                return new List<DepartmentInfoListDto>();
            }
        }

        /// <summary>
        /// 通过Id获取部门信息进行编辑或修改 
        /// </summary>
        public async Task<GetDepartmentInfoForEditOutput> GetDepartmentInfoForEditAsync(NullableIdDto<long> input)
        {
            var output = new GetDepartmentInfoForEditOutput();

            DepartmentInfoEditDto departmentInfoEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _departmentInfoRepository.GetAsync(input.Id.Value);
                departmentInfoEditDto = entity.MapTo<DepartmentInfoEditDto>();
            }
            else
            {
                departmentInfoEditDto = new DepartmentInfoEditDto();
            }

            output.DepartmentInfo = departmentInfoEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取部门ListDto信息
        /// </summary>
        public DepartmentInfoListDto GetDepartmentInfoById(EntityDto<long> input)
        {
            var entity = _departmentInfoRepository.Get(input.Id);

            return entity.MapTo<DepartmentInfoListDto>();
        }



        public List<DepartmentInfoEditDto> DepartmentList()
        {
            var entity = _departmentInfoRepository.GetAll();
            return entity.MapTo<List<DepartmentInfoEditDto>>();
        }



        /// <summary>
        /// 新增或更改部门
        /// </summary>
        public async Task CreateOrUpdateDepartmentInfoAsync(CreateOrUpdateDepartmentInfoInput input)
        {
            if (input.DepartmentInfoEditDto.Id.HasValue)
            {
                await UpdateDepartmentInfoAsync(input.DepartmentInfoEditDto);
            }
            else
            {
                CreateDepartmentInfoAsync(input.DepartmentInfoEditDto);
            }
        }

        /// <summary>
        /// 新增部门
        /// </summary>
        public virtual DepartmentInfoEditDto CreateDepartmentInfoAsync(DepartmentInfoEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<DepartmentInfo>();
            entity.Id = _departmentInfoRepository.InsertAndGetId(entity);
            return entity.MapTo<DepartmentInfoEditDto>();
        }

        /// <summary>
        /// 编辑部门
        /// </summary>
        public virtual async Task UpdateDepartmentInfoAsync(DepartmentInfoEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _departmentInfoRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            await _departmentInfoRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        public async Task DeleteDepartmentInfoAsync(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _departmentInfoRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除部门
        /// </summary>
        public async Task BatchDeleteDepartmentInfoAsync(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            if (input.Count == 0)
            {
                await _departmentInfoRepository.DeleteAsync(s => true);
            }
            await _departmentInfoRepository.DeleteAsync(s => input.Contains(s.Id));
        }
        #endregion
    }
}
