using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;

namespace ColleageInnerTraining.Application
{
	/// <summary>
    /// 班级成员服务接口
    /// </summary>
    public interface IClassUserAppService : IApplicationService
    {
        #region 班级成员管理

        /// <summary>
        /// 根据查询条件获取班级成员分页列表
        /// </summary>
        PagedResultDto<ClassUserListDto> GetPagedClassUsers(GetClassUserInput input);
        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        List<ClassUserListDto> GetAll();


        /// <summary>
        /// 新增或更改班级成员
        /// </summary>
        void CreateOrUpdateClassUserAsync(CreateOrUpdateClassUserInput input);


        /// <summary>
        /// 新增班级成员
        /// </summary>
        ClassUserEditDto CreateClassUser(ClassUserEditDto input);

        /// <summary>
        /// 更新班级成员
        /// </summary>
        void UpdateClassUserAsync(ClassUserEditDto input);

        /// <summary>
        /// 删除班级成员
        /// </summary>
        void DeleteClassUserAsync(EntityDto<long> input);

        /// <summary>
        /// 批量删除班级成员
        /// </summary>
        Task BatchDeleteClassUserAsync(List<long> input);

        #endregion

    }
}
