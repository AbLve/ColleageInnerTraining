using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 所属类型配置服务接口
    /// </summary>
    public interface ICourseBoundConfigureTypeAppService : IApplicationService
    {
        #region 所属类型配置管理

        /// <summary>
        /// 根据查询条件获取所属类型配置分页列表
        /// </summary>
        PagedResultDto<CourseBoundConfigureTypeListDto> GetPagedCourseBoundConfigureTypes(GetCourseBoundConfigureTypeInput input);

        /// <summary>
        /// 通过Id获取所属类型配置信息进行编辑或修改 
        /// </summary>
        GetCourseBoundConfigureTypeForEditOutput GetCourseBoundConfigureTypeForEdit(NullableIdDto<long> input);

		  /// <summary>
        /// 通过指定id获取所属类型配置ListDto信息
        /// </summary>
		CourseBoundConfigureTypeListDto GetCourseBoundConfigureTypeById(EntityDto<long> input);

        /// <summary>
        /// 通过课程ID,类型,类型值获取所有数据
        /// </summary>
        CourseBoundConfigureType GetCTypeByCIdOrType(int CourseId, int type, int businId);
        /// <summary>
        /// 根据类型查第一个符合条件的数据
        /// </summary>
        /// <param name="CourseId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        CourseBoundConfigureType GetCTypeByCIdOrByType(int CourseId, int type);

        /// <summary>
        /// 新增或更改所属类型配置
        /// </summary>
        void CreateOrUpdateCourseBoundConfigureType(CreateOrUpdateCourseBoundConfigureTypeInput input);


        /// <summary>
        /// 通过课程ID和类型,类性值获取数据
        /// </summary>
        CourseBoundConfigureType GetCTypeByCourseIdOrTypeId(int CourseId, int typeId, int businId);

        /// <summary>
        /// 通过课程ID获取数据
        /// </summary>
        List<CourseBoundConfigureType> GetCTypeByCId(int CourseId);

        /// <summary>
        /// 通过课程ID获取所有数据
        /// </summary>
        List<CourseBoundConfigureType> GetCTypeByCourseId(int CourseId);
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<CourseBoundConfigureType> GetAll();

        /// <summary>
        /// 新增所属类型配置
        /// </summary>
        CourseBoundConfigureTypeEditDto CreateCourseBoundConfigureType(CourseBoundConfigureTypeEditDto input);

        /// <summary>
        /// 更新所属类型配置
        /// </summary>
        void UpdateCourseBoundConfigureType(CourseBoundConfigureTypeEditDto input);

        /// <summary>
        /// 删除所属类型配置
        /// </summary>      

        void DeleteCourseBoundConfigureType(EntityDto<long> input);

        /// <summary>
        /// 批量删除所属类型配置
        /// </summary>
        void BatchDeleteCourseBoundConfigureType(List<long> input);

        /// <summary>
        /// 通过课程ID和类型获取数据
        /// </summary>
        List<CourseBoundConfigureType> GetCTypeByCourseIdOrType(int CourseId, int type);

        /// <summary>
        /// 通过班级和类型值获取数据
        /// </summary>
        List<CourseBoundConfigureType> GetCTypeByClassIdOrType(int ClassId, int type);

        /// <summary>
        /// 班级数量减1
        /// </summary>
        void SetCourseCountReduce(int classId);

        #endregion




    }
}
