using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 课程分类服务实现
    /// </summary>
    public class CourseCategoryAppService : ColleageInnerTrainingAppServiceBase, ICourseCategoryAppService
    {
        private readonly IRepository<CourseCategory, long> _courseCategoryRepository;
        private readonly CourseCategoryManage _courseCategoryManage;

        /// <summary>
        /// 构造方法
        /// </summary>
        public CourseCategoryAppService(IRepository<CourseCategory, long> courseCategoryRepository,CourseCategoryManage courseCategoryManage)
        {
            _courseCategoryRepository = courseCategoryRepository;
            _courseCategoryManage = courseCategoryManage;

        }

        #region 课程分类管理

        /// <summary>
        /// 根据查询条件获取课程分类分页列表
        /// </summary>
        public PagedResultDto<CourseCategoryListDto> GetPagedCourseCategorys(GetCourseCategoryInput input)
        {

            var query = _courseCategoryRepository.GetAll()
            .Where(item => item.IsDeleted == false)
            .WhereIf(!input.FilterText.IsNullOrWhiteSpace(), item => item.CourseCategoryName.Contains(input.FilterText)
            || item.Description.Contains(input.FilterText));
            //TODO:根据传入的参数添加过滤条件

            var courseCategoryCount = query.Count();

            var courseCategorys = query
            .OrderByDescending(t => t.CreationTime)
            .PageBy(input)
            .ToList();

            var courseCategoryListDtos = courseCategorys.MapTo<List<CourseCategoryListDto>>();
            return new PagedResultDto<CourseCategoryListDto>(
            courseCategoryCount,
            courseCategoryListDtos
            );
        }

        /// <summary>
        /// 通过Id获取课程分类信息进行编辑或修改 
        /// </summary>
        public GetCourseCategoryForEditOutput GetCourseCategoryForEdit(NullableIdDto<long> input)
        {
            var output = new GetCourseCategoryForEditOutput();

            CourseCategoryEditDto courseCategoryEditDto;

            if (input.Id.HasValue)
            {
                var entity = _courseCategoryRepository.Get(input.Id.Value);
                courseCategoryEditDto = entity.MapTo<CourseCategoryEditDto>();
            }
            else
            {
                courseCategoryEditDto = new CourseCategoryEditDto();
            }

            output.CourseCategory = courseCategoryEditDto;
            return output;
        }

        /// <summary>
        /// 通过指定id获取课程分类ListDto信息
        /// </summary>
        public CourseCategoryListDto GetCourseCategoryById(EntityDto<long> input)
        {
            var entity = _courseCategoryRepository.Get(input.Id);

            return entity.MapTo<CourseCategoryListDto>();
        }

        /// <summary>
        /// 获取顶级分类
        /// </summary>
        public List<CourseCategoryListDto> GetTopCourseCategory()
        {
            var courseCategoryListDtos = _courseCategoryRepository.GetAll().Where(t=>t.ParentId ==0);
            return courseCategoryListDtos.MapTo<List<CourseCategoryListDto>>();
        }

        /// <summary>
        /// 根据父Id取下级子分类
        /// </summary>
        public List<CourseCategoryListDto> GetCourseCategorysByParentId(int parentId)
        {
            var courseCategoryListDtos = _courseCategoryRepository.GetAll().Where(t => t.ParentId == parentId);
            return courseCategoryListDtos.MapTo<List<CourseCategoryListDto>>();
        }


        /// <summary>
        /// 新增或更改课程分类
        /// </summary>
        public void CreateOrUpdateCourseCategory(CreateOrUpdateCourseCategoryInput input)
        {
            if (input.CourseCategoryEditDto.Id.HasValue)
            {
                UpdateCourseCategory(input.CourseCategoryEditDto);
            }
            else
            {
                CreateCourseCategory(input.CourseCategoryEditDto);
            }
        }

        /// <summary>
        /// 新增课程分类
        /// </summary>
        public CourseCategoryEditDto CreateCourseCategory(CourseCategoryEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<CourseCategory>();

            entity.Id = _courseCategoryRepository.InsertAndGetId(entity);
            return entity.MapTo<CourseCategoryEditDto>();
        }

        /// <summary>
        /// 编辑课程分类
        /// </summary>
        public void UpdateCourseCategory(CourseCategoryEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = _courseCategoryRepository.Get(input.Id.Value);
            input.MapTo(entity);

            _courseCategoryRepository.Update(entity);
        }

        /// <summary>
        /// 删除课程分类
        /// </summary>
        public void DeleteCourseCategorybyCategoryId(int categoryId)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            _courseCategoryRepository.Delete(c => c.CategoryId == categoryId);
        }

        /// <summary>
        /// 删除课程分类
        /// </summary>
        public void DeleteCourseCategory(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            _courseCategoryRepository.Delete(input.Id);
        }

        /// <summary>
        /// 批量删除课程分类
        /// </summary>
        public void BatchDeleteCourseCategory(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            _courseCategoryRepository.Delete(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 获取课程分类ListDto所有信息信息
        /// </summary>
        public List<CourseCategoryListDto> GetCourseCategoryList()
        {
            var entity = _courseCategoryRepository.GetAll();

            return entity.MapTo<List<CourseCategoryListDto>>();
        }

        /// <summary>
        /// 获取课程分类ListDto所有信息信息
        /// </summary>
        public List<CourseCategoryListDto> GetCourseTopCategoryList()
        {
            var entity = _courseCategoryRepository.GetAll().Where(t=>t.ParentId ==0);
            return entity.MapTo<List<CourseCategoryListDto>>();
        }


        #endregion


    }
}
