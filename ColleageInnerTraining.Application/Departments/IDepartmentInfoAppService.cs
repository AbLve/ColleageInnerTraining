using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 部门服务接口
    /// </summary>
    public interface IDepartmentInfoAppService : IApplicationService
    {
        #region 部门管理

        /// <summary>
        /// 根据查询条件获取部门分页列表
        /// </summary>
        PagedResultDto<DepartmentInfoListDto> GetPagedDepartmentInfos(GetDepartmentInfoInput input);


        List<DepartmentInfoListDto> GetAllDepartmentInfos();
        /// <summary>
        /// 通过Id获取部门信息进行编辑或修改 
        /// </summary>
        Task<GetDepartmentInfoForEditOutput> GetDepartmentInfoForEditAsync(NullableIdDto<long> input);

		  /// <summary>
        /// 通过指定id获取部门ListDto信息
        /// </summary>
		DepartmentInfoListDto GetDepartmentInfoById(EntityDto<long> input);



        /// <summary>
        /// 新增或更改部门
        /// </summary>
        Task CreateOrUpdateDepartmentInfoAsync(CreateOrUpdateDepartmentInfoInput input);

        /// <summary>
        /// 新增或更改部门
        /// </summary>
        List<DepartmentInfoEditDto> DepartmentList();        


        /// <summary>
        /// 新增部门
        /// </summary>
        DepartmentInfoEditDto CreateDepartmentInfoAsync(DepartmentInfoEditDto input);

        /// <summary>
        /// 更新部门
        /// </summary>
        Task UpdateDepartmentInfoAsync(DepartmentInfoEditDto input);

        /// <summary>
        /// 删除部门
        /// </summary>
        Task DeleteDepartmentInfoAsync(EntityDto<long> input);

        /// <summary>
        /// 批量删除部门
        /// </summary>
        Task BatchDeleteDepartmentInfoAsync(List<long> input);
        #endregion




    }
}
