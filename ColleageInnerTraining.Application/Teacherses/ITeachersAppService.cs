using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 内训师服务接口
    /// </summary>
    public interface ITeachersAppService : IApplicationService
    {
        #region 内训师管理

        /// <summary>
        /// 根据查询条件获取内训师分页列表
        /// </summary>
        PagedResultDto<TeachersListDto> GetPagedTeacherss(GetTeachersInput input);

        /// <summary>
        /// 通过Id获取内训师信息进行编辑或修改 
        /// </summary>
        GetTeachersForEditOutput GetTeachersForEdit(NullableIdDto<long> input);

		  /// <summary>
        /// 通过指定id获取内训师ListDto信息
        /// </summary>
		TeachersListDto GetTeachersById(EntityDto<long> input);



        /// <summary>
        /// 新增或更改内训师
        /// </summary>
        void CreateOrUpdateTeachers(CreateOrUpdateTeachersInput input);


        /// <summary>
        /// 获取所有内训师
        /// </summary>
        List<Teachers> GetAllDate();


        /// <summary>
        /// 新增内训师
        /// </summary>
        TeachersEditDto CreateTeachers(TeachersEditDto input);

        /// <summary>
        /// 更新内训师
        /// </summary>
        void UpdateTeachers(TeachersEditDto input);

        /// <summary>
        /// 删除内训师
        /// </summary>
        void DeleteTeachers(EntityDto<long> input);

        /// <summary>
        /// 批量删除内训师
        /// </summary>
        void BatchDeleteTeachers(List<long> input);

        #endregion




    }
}
