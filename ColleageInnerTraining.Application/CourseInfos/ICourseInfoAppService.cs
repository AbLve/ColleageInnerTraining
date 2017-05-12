using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 课程基本信息服务接口
    /// </summary>
    public interface ICourseInfoAppService : IApplicationService
    {
        #region 课程基本信息管理

        /// <summary>
        /// 根据查询条件获取课程基本信息分页列表
        /// </summary>
        PagedResultDto<CourseInfoListDto> GetPagedCourseInfos(GetCourseInfoInput input);
        /// <summary>
        /// 根据查询条件获取手机端课程基本信息分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResultDto<CourseInfoListDto> GetPagedCourseInfoForWap(GetCourseInfoInput input);
        /// <summary>
        /// 通过Id获取课程基本信息信息进行编辑或修改 
        /// </summary>
        GetCourseInfoForEditOutput GetCourseInfoForEdit(NullableIdDto<long> input);

        /// <summary>
        /// 通过指定id获取课程基本信息ListDto信息
        /// </summary>
        CourseInfoListDto GetCourseInfoById(EntityDto<long> input);
        /// <summary>
        /// 通过指定id获取课程基本信息EditDto信息
        /// </summary>
        CourseInfoEditDto GetCourseInfoEditById(EntityDto<long> input);

        /// <summary>
        /// 新增或更改课程基本信息
        /// </summary>
        void CreateOrUpdateCourseInfo(CreateOrUpdateCourseInfoInput input);
        
        /// <summary>
        /// 新增课程基本信息
        /// </summary>
        CourseInfoEditDto CreateCourseInfo(CourseInfoEditDto input);
        /// <summary>
        /// 获取所有课程
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<CourseInfoListDto> GetAll();
        /// <summary>
        /// 获取最新创建的3个课程
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<CourseInfoListDto> GetCourseInfo3News(int displayPosition);
        /// <summary>
        /// 获取分类最新创建的3个课程
        /// </summary>
        /// <returns></returns>
        List<CourseInfoListDto> GetCourseInfoByCategory3News(int categoryId,int displayPosition);
        /// <summary>
        /// 获取分类最新创建的3个课程
        /// </summary>
        /// <returns></returns>
        List<CourseInfoListDto> GetCourseInfoByUserId(int userId ,int type,int status);
        /// <summary>
        /// 更新课程基本信息
        /// </summary>
        void UpdateCourseInfo(CourseInfoEditDto input);

        /// <summary>
        /// 删除课程基本信息
        /// </summary>
        void DeleteCourseInfo(EntityDto<long> input);

        /// <summary>
        /// 批量删除课程基本信息
        /// </summary>
        void BatchDeleteCourseInfo(List<long> input);

        #endregion




    }
}
