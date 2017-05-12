using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using ColleageInnerTraining.Core;
using ColleageInnerTraining.Application.Dtos;
using System.Transactions;
using Abp.Domain.Uow;
using ColleageInnerTraining.Common;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 所属类型配置服务实现
    /// </summary>
    public class CourseBoundConfigureTypeAppService : ColleageInnerTrainingAppServiceBase, ICourseBoundConfigureTypeAppService
    {
        private readonly IRepository<CourseBoundConfigureType, long> _courseBoundConfigureTypeRepository;        
        private readonly CourseBoundConfigureTypeManage _courseBoundConfigureTypeManage;
        private readonly IClassesInfoAppService classInfolService;

        /// <summary>
        /// 构造方法
        /// </summary>
        public CourseBoundConfigureTypeAppService(IRepository<CourseBoundConfigureType, long> courseBoundConfigureTypeRepository,
        CourseBoundConfigureTypeManage courseBoundConfigureTypeManage, IClassesInfoAppService _classInfolService)
        {
            _courseBoundConfigureTypeRepository = courseBoundConfigureTypeRepository;
            _courseBoundConfigureTypeManage = courseBoundConfigureTypeManage;
            classInfolService = _classInfolService;
        }

        #region 所属类型配置管理

        /// <summary>
        /// 根据查询条件获取所属类型配置分页列表
        /// </summary>
        public PagedResultDto<CourseBoundConfigureTypeListDto> GetPagedCourseBoundConfigureTypes(GetCourseBoundConfigureTypeInput input)
        {

            var query = _courseBoundConfigureTypeRepository.GetAll();
            //TODO:根据传入的参数添加过滤条件
            if (input.CourseId > 0)
            {
                query = query.Where(c => c.CourseId == input.CourseId);
            }

            var courseBoundConfigureTypeCount = query.Count();

            var courseBoundConfigureTypes = query
            .OrderByDescending(t => t.CreationTime)
            .PageBy(input)
            .ToList();

            var courseBoundConfigureTypeListDtos = courseBoundConfigureTypes.MapTo<List<CourseBoundConfigureTypeListDto>>();
            return new PagedResultDto<CourseBoundConfigureTypeListDto>(
            courseBoundConfigureTypeCount,
            courseBoundConfigureTypeListDtos
            );
        }
        public List<CourseBoundConfigureType> GetAll()
        {
            return _courseBoundConfigureTypeRepository.GetAll().ToList() ?? new List<CourseBoundConfigureType>();
        }
        /// <summary>
        /// 通过Id获取所属类型配置信息进行编辑或修改 
        /// </summary>
        public GetCourseBoundConfigureTypeForEditOutput GetCourseBoundConfigureTypeForEdit(NullableIdDto<long> input)
        {
            var output = new GetCourseBoundConfigureTypeForEditOutput();

            CourseBoundConfigureTypeEditDto courseBoundConfigureTypeEditDto;

            if (input.Id.HasValue)
            {
                var entity = _courseBoundConfigureTypeRepository.Get(input.Id.Value);
                courseBoundConfigureTypeEditDto = entity.MapTo<CourseBoundConfigureTypeEditDto>();
            }
            else
            {
                courseBoundConfigureTypeEditDto = new CourseBoundConfigureTypeEditDto();
            }

            output.CourseBoundConfigureType = courseBoundConfigureTypeEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取所属类型配置ListDto信息
        /// </summary>
        public CourseBoundConfigureTypeListDto GetCourseBoundConfigureTypeById(EntityDto<long> input)
        {
            var entity = _courseBoundConfigureTypeRepository.Get(input.Id);

            return entity.MapTo<CourseBoundConfigureTypeListDto>();
        }

        /// <summary>
        /// 通过课程ID和类型,类性值获取数据
        /// </summary>
        public CourseBoundConfigureType GetCTypeByCourseIdOrTypeId(int CourseId, int typeId, int businId)
        {
            var entity = _courseBoundConfigureTypeRepository.GetAll()
                .FirstOrDefault(c => c.CourseId == CourseId && c.type == typeId && c.BusinessId == businId);
            return entity;
        }

        /// <summary>
        /// 通过课程ID获取数据
        /// </summary>
        public List<CourseBoundConfigureType> GetCTypeByCId(int CourseId)
        {
            var entity = _courseBoundConfigureTypeRepository.GetAll()
                .Where(c => c.CourseId == CourseId).ToList();
            return entity;
        }

        /// <summary>
        /// 通过课程ID获取所有数据
        /// </summary>
        public List<CourseBoundConfigureType> GetCTypeByCourseId(int CourseId)
        {
            var entity = _courseBoundConfigureTypeRepository.GetAll()
                .Where(c => c.CourseId == CourseId).ToList();
            return entity;
        }


        /// <summary>
        /// 通过课程ID,类型,类型值获取所有数据
        /// </summary>
        public CourseBoundConfigureType GetCTypeByCIdOrType(int CourseId, int type, int businId)
        {
            var entity = _courseBoundConfigureTypeRepository.GetAll()
                .FirstOrDefault(c => c.CourseId == CourseId && c.type == type && c.BusinessId == businId);
            return entity;
        }
        /// <summary>
        /// 根据类型type查第一个符合条件的数据
        /// </summary>
        public CourseBoundConfigureType GetCTypeByCIdOrByType(int CourseId, int type)
        {
            var entity = _courseBoundConfigureTypeRepository.GetAll()
                .FirstOrDefault(c => c.CourseId == CourseId && c.type == type);
            return entity;
        }

        /// <summary>
        /// 新增或更改所属类型配置
        /// </summary>
        public void CreateOrUpdateCourseBoundConfigureType(CreateOrUpdateCourseBoundConfigureTypeInput input)
        {
            if (input.CourseBoundConfigureTypeEditDto.Id.HasValue)
            {
                UpdateCourseBoundConfigureType(input.CourseBoundConfigureTypeEditDto);
            }
            else
            {
                CreateCourseBoundConfigureType(input.CourseBoundConfigureTypeEditDto);
            }
        }

        /// <summary>
        /// 新增所属类型配置
        /// </summary>
        public CourseBoundConfigureTypeEditDto CreateCourseBoundConfigureType(CourseBoundConfigureTypeEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增
            var entity = input.MapTo<CourseBoundConfigureType>();
            entity.Id = _courseBoundConfigureTypeRepository.InsertAndGetId(entity);
            if (input.type == (int)ConfigureType.Class)
            {
                SetCourseCount(entity.BusinessId);
            }          
            return entity.MapTo<CourseBoundConfigureTypeEditDto>();
        }

        /// <summary>
        /// 班级数量加1
        /// </summary>
        private void SetCourseCount(int classId)
        {
            var classed = classInfolService.GetClassesInfoForEdit(new NullableIdDto<long>() { Id = classId });
            if (classed != null)
            {
                var date = _courseBoundConfigureTypeRepository.GetAll()
                    .Where(c => c.type == (int)ConfigureType.Class && c.BusinessId == classed.ClassesInfo.Id);
                classed.ClassesInfo.CourseCount += date.Count();
                classInfolService.UpdateClassesInfoAsync(classed.ClassesInfo);
            }            
        }

        /// <summary>
        /// 班级数量减1
        /// </summary>
        public void SetCourseCountReduce(int classId)
        {
            var classed = classInfolService.GetClassesInfoForEdit(new NullableIdDto<long>() { Id = classId });
            if (classed != null)
            {
                classed.ClassesInfo.CourseCount -= 1;
                if (classed.ClassesInfo.CourseCount < 0)
                {
                    classed.ClassesInfo.CourseCount = 0;
                }
                classInfolService.UpdateClassesInfoAsync(classed.ClassesInfo);
            }
        }

        /// <summary>
        /// 编辑所属类型配置
        /// </summary>
        public void UpdateCourseBoundConfigureType(CourseBoundConfigureTypeEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = _courseBoundConfigureTypeRepository.Get(input.Id.Value);
            input.MapTo(entity);
            _courseBoundConfigureTypeRepository.InsertOrUpdateAndGetId(entity);
        }

        /// <summary>
        /// 删除所属类型配置
        /// </summary>
        public void DeleteCourseBoundConfigureType(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            _courseBoundConfigureTypeRepository.Delete(input.Id);
            
        }

        /// <summary>
        /// 批量删除所属类型配置
        /// </summary>
        public void BatchDeleteCourseBoundConfigureType(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            _courseBoundConfigureTypeRepository.Delete(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 通过课程ID和类型获取数据
        /// </summary>
        public List<CourseBoundConfigureType> GetCTypeByCourseIdOrType(int CourseId, int type)
        {
            var entity = _courseBoundConfigureTypeRepository.GetAll()
                .Where(c => c.CourseId == CourseId && c.type == type).ToList();
            return entity;
        }


        /// <summary>
        /// 通过班级和类型值获取数据
        /// </summary>
        public List<CourseBoundConfigureType> GetCTypeByClassIdOrType(int ClassId, int type)
        {
            var entity = _courseBoundConfigureTypeRepository.GetAll()
                .Where(c => c.BusinessId == ClassId && c.type == type).ToList();
            return entity;
        }


        #endregion












    }
}
