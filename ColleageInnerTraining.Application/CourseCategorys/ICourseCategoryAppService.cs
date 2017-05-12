using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 课程分类服务接口
    /// </summary>
    public interface ICourseCategoryAppService : IApplicationService
    {
        #region 课程分类管理

        /// <summary>
        /// 根据查询条件获取课程分类分页列表
        /// </summary>
        PagedResultDto<CourseCategoryListDto> GetPagedCourseCategorys(GetCourseCategoryInput input);

        /// <summary>
        /// 通过Id获取课程分类信息进行编辑或修改 
        /// </summary>
        GetCourseCategoryForEditOutput GetCourseCategoryForEdit(NullableIdDto<long> input);

		  /// <summary>
        /// 通过指定id获取课程分类ListDto信息
        /// </summary>
		CourseCategoryListDto GetCourseCategoryById(EntityDto<long> input);
        /// <summary>
        /// 获取顶级分类
        /// </summary>
        /// <returns></returns>
        List<CourseCategoryListDto> GetTopCourseCategory();

        /// <summary>
        /// 根据父Id取下级子分类
        /// </summary>
        /// <returns></returns>
        List<CourseCategoryListDto> GetCourseCategorysByParentId(int parentId);

        /// <summary>
        /// 通过指定id获取课程分类ListDto信息
        /// </summary>
        List<CourseCategoryListDto> GetCourseCategoryList();

        /// <summary>
        /// 获取课程分类顶级分类
        /// </summary>
        List<CourseCategoryListDto> GetCourseTopCategoryList();


        /// <summary>
        /// 新增或更改课程分类
        /// </summary>
        void CreateOrUpdateCourseCategory(CreateOrUpdateCourseCategoryInput input);

        /// <summary>
        /// 新增课程分类
        /// </summary>
        CourseCategoryEditDto CreateCourseCategory(CourseCategoryEditDto input);

        /// <summary>
        /// 更新课程分类
        /// </summary>
        void UpdateCourseCategory(CourseCategoryEditDto input);

        /// <summary>
        /// 删除课程分类
        /// </summary>
        void DeleteCourseCategorybyCategoryId(int categoryId);

        /// <summary>
        /// 删除课程分类
        /// </summary>
        void DeleteCourseCategory(EntityDto<long> input);

        /// <summary>
        /// 批量删除课程分类
        /// </summary>
        void BatchDeleteCourseCategory(List<long> input);

        #endregion




    }
}
