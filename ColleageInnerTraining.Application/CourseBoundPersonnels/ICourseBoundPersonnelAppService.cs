using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 课程人员绑定表服务接口
    /// </summary>
    public interface ICourseBoundPersonnelAppService : IApplicationService
    {
        #region 课程人员绑定表管理

        /// <summary>
        /// 根据查询条件获取课程人员绑定表分页列表
        /// </summary>
        PagedResultDto<CourseBoundPersonnelListDto> GetPagedCourseBoundPersonnels(GetCourseBoundPersonnelInput input);

        PagedResultDto<CourseBoundPersonnelExportDto> GetPagedCourseBoundPersonnelsForExport(GetCourseBoundPersonnelInput input);


        /// <summary>
        /// 通过Id获取课程人员绑定表信息进行编辑或修改 
        /// </summary>
        GetCourseBoundPersonnelForEditOutput GetCourseBoundPersonnelForEdit(NullableIdDto<long> input);

        /// <summary>
        /// 通过指定id获取课程人员绑定表ListDto信息
        /// </summary>
        CourseBoundPersonnelListDto GetCourseBoundPersonnelById(EntityDto<long> input);

        /// <summary>
        /// 通过课程ID和获取所有用户ID
        /// </summary>
        List<int> GetCourseBoundUserId(int courseId);

        /// <summary>
        /// 新增或更改课程人员绑定表
        /// </summary>
        void CreateOrUpdateCourseBoundPersonnel(CreateOrUpdateCourseBoundPersonnelInput input);

        /// <summary>
        /// 通过人员ID和课程Id获取绑定数据
        /// </summary
        CourseBoundPersonnelEditDto GetCourseBoundByUserIdOrCourseId(int userId, int courseId);

        /// <summary>
        /// 通过课程ID获取所有数据
        /// </summary
        List<CourseBoundPersonnel> GetCourseBoundUserByCourseId(int courseId);

        /// <summary>
        /// 新增课程人员绑定表
        /// </summary>
        CourseBoundPersonnelEditDto CreateCourseBoundPersonnel(CourseBoundPersonnelEditDto input);

        /// <summary>
        /// 更新课程人员绑定表
        /// </summary>
        void UpdateCourseBoundPersonnel(CourseBoundPersonnelEditDto input);

        /// <summary>
        /// 删除课程人员绑定表
        /// </summary>
        void DeleteCourseBoundPersonnel(EntityDto<long> input);

        /// <summary>
        /// 删除课程人员根据课程和用户Id
        /// </summary>
        void DeleteCourseBoundPersonnelBy(int courseId, int sysNo);

        /// <summary>
        /// 批量删除课程人员绑定表
        /// </summary>
        void BatchDeleteCourseBoundPersonnel(List<long> input);

        /// <summary>
        /// 根据课程获取所有用户
        /// </summary>
        List<CourseBoundPersonnel> GetCouPerByCourseAll(int courseId);

        /// <summary>
        /// 获取部门下的所有用户
        /// </summary>
        List<UserAccount> GetUserDateByDepartment(int courseId, DepartmentInfoListDto department);

        /// <summary>
        /// 获取岗位下的所有用户
        /// </summary>
        List<UserAccount> GetUserDateByJobPost(int courseId, JobPostListDto job);

        /// <summary>
        /// 获取岗位下的所有用户
        /// </summary>
        List<UserAccount> GetUserDateByClasses(int courseId, ClassesInfoListDto classed);

        /// <summary>
        /// 获取个人下的所有用户
        /// </summary>
        List<UserAccount> GetUserDateByPerson(int courseId, int type);
        #endregion
    }
}
