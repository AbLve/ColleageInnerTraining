using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using ColleageInnerTraining.Core;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Common;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 课程基本信息服务实现
    /// </summary>
    public class CourseInfoAppService : ColleageInnerTrainingAppServiceBase, ICourseInfoAppService
    {
        private readonly IRepository<CourseInfo, long> _courseInfoRepository;       
        private readonly CourseInfoManage _courseInfoManage;

        private readonly IRepository<CourseCategory, long> _categoryRepository;

        private readonly IRepository<CourseBoundPersonnel, long> _courseBoundPersonnelRepository;

        private readonly IRepository<Teachers, long> _teacherRepository;
        

        /// <summary>
        /// 构造方法
        /// </summary>
        public CourseInfoAppService(IRepository<CourseInfo, long> courseInfoRepository,CourseInfoManage courseInfoManage,
        IRepository<CourseCategory, long> categoryRepository,IRepository<CourseBoundPersonnel, long> courseBoundPersonnelRepository,
        IRepository<Teachers, long> teacherRepository)
        {
            _courseInfoRepository = courseInfoRepository;
            _courseInfoManage = courseInfoManage;
            _categoryRepository = categoryRepository;
            _courseBoundPersonnelRepository = courseBoundPersonnelRepository;
            _teacherRepository = teacherRepository;
        }

        #region 课程基本信息管理

        /// <summary>
        /// 根据查询条件获取课程基本信息分页列表
        /// </summary>
        public PagedResultDto<CourseInfoListDto> GetPagedCourseInfos(GetCourseInfoInput input)
        {

            var query = from info in _courseInfoRepository.GetAll()
                        join cate in _categoryRepository.GetAll() on info.CategoryType equals cate.CategoryId into gc 
                        join teach in _teacherRepository.GetAll() on info.TeacherId equals teach.Id into tc

                        select new CourseInfoListDto()
                        {
                            Id = info.Id,
                            CourseName = info.CourseName,
                            Description = info.Description,
                            Sort = info.Sort,
                            Enabled = info.Enabled,
                            Content = info.Content,
                            Type = info.Type,
                            StageName = info.StageName,
                            Status = info.Status,
                            ImageUrl = info.ImageUrl,
                            TeacherId = info.TeacherId,
                            TeacherName = info.TeacherName,
                            LiveStatus = info.LiveStatus,
                            StartTime = info.StartTime,
                            EndTime = info.EndTime,
                            CourseCategoryName = gc.FirstOrDefault().CourseCategoryName,
                            DisplayPosition = info.DisplayPosition,
                            ReadTimes = info.ReadTimes,
                            CollectionTimes = info.CollectionTimes,
                            CategoryType = gc.FirstOrDefault().CategoryId,
                            DepartmentId = tc.FirstOrDefault().DepartmentId,
                            JobPostId = tc.FirstOrDefault().JobpostId,
                            CreationTime = info.CreationTime,
                            Enrollment = info.Enrollment
                        };
            //into urJoined from ur in urJoined.DefaultIfEmpty()            
            //group info by info into userGrouped select userGrouped.Key;
            //TODO:根据传入的参数添加过滤条件       
            if (!string.IsNullOrEmpty(input.FilterText))
            {
                query = query.Where(c => c.CourseName.Contains(input.FilterText));
            }
            if (!string.IsNullOrEmpty(input.TeacherName))
            {
                query = query.Where(c => c.TeacherName.Contains(input.TeacherName));
            }
            if (input.Type > 0)
            {
                query = query.Where(c => c.Type == input.Type);
            }
            if (input.categoryType > 0)
            {
                query = query.Where(c => c.CategoryType == input.categoryType);
            }
            if (input.CheckStatus > 0)
            {
                query = query.Where(c => c.Status == input.CheckStatus);
            }
            if (input.TeacherId > 0)
            {
                query = query.Where(c => c.TeacherId == input.TeacherId);
            }
            if (input.DepartmentId > 0)
            {
                query = query.Where(c => c.DepartmentId == input.DepartmentId);
            }
            if (input.JobPostId > 0)
            {
                query = query.Where(c => c.JobPostId == input.JobPostId);
            }

            var courseInfoCount = query.Count();             
            var courseInfos = query
            .OrderByDescending(t => t.CreationTime)
            .PageBy(input)
            .ToList();

            var courseInfoListDtos = courseInfos.MapTo<List<CourseInfoListDto>>();
            return new PagedResultDto<CourseInfoListDto>(
            courseInfoCount,
            courseInfoListDtos
            );
        }



        /// <summary>
        /// 根据查询条件获取课程基本信息分页列表
        /// </summary>
        public PagedResultDto<CourseInfoListDto> GetPagedCourseInfoForWap(GetCourseInfoInput input)
        {

            var query = from info in _courseInfoRepository.GetAll()
                        .Where(t=>t.Status !=1&&t.Status!=3).WhereIf(input.Type != 0, t => t.Type == input.Type).Where(t => t.DisplayPosition == 2 || t.DisplayPosition == 0)
                        join cate in _categoryRepository.GetAll().WhereIf((input.Path != string.Empty && input.Path != null && input.Path != "0"), t => t.Path.StartsWith(input.Path)) on info.CategoryType equals cate.CategoryId
                        into gc
                        from gci in gc
                        select new CourseInfoListDto()
                        {
                            Id = info.Id,
                            CourseName = info.CourseName,
                            Description = info.Description,
                            Sort = info.Sort,
                            Enabled = info.Enabled,
                            Content = info.Content,
                            Type = info.Type,
                            StageName = info.StageName,
                            Status = info.Status,
                            ImageUrl = info.ImageUrl,
                            TeacherId = info.TeacherId,
                            TeacherName = info.TeacherName,
                            LiveStatus = info.LiveStatus,
                            StartTime = info.StartTime,
                            EndTime = info.EndTime,
                            CourseCategoryName = gci.CourseCategoryName,
                            DisplayPosition = info.DisplayPosition,
                            ReadTimes = info.ReadTimes,
                            CollectionTimes = info.CollectionTimes,
                            Enrollment = info.Enrollment,
                            CreationTime = info.CreationTime,
                            PollId = info.PollId,
                            TypeName = info.TypeName
                        };
            //TODO:根据传入的参数添加过滤条件
            var courseInfoCount = query.Count();
            if (input.Sorting == null || input.Sorting == string.Empty)
                query = query.OrderByDescending(t => t.CreationTime).OrderByDescending(t=>t.Id);
            if (input.Sorting == "ReadTimes")
                query = query.OrderByDescending(t => t.ReadTimes);
            if (input.Sorting == "CreationTime")
                query = query.OrderByDescending(t => t.CreationTime);

            var courseInfos = query
            .PageBy(input)
            .ToList();

            var courseInfoListDtos = courseInfos.MapTo<List<CourseInfoListDto>>();
            return new PagedResultDto<CourseInfoListDto>(
            courseInfoCount,
            courseInfoListDtos
            );
        }

        /// <summary>
        /// 获取所有课程
        /// </summary>
        public List<CourseInfoListDto> GetAll()
        {

            var courseInfos = _courseInfoRepository.GetAll();
            var returnList = courseInfos.MapTo<List<CourseInfoListDto>>();
            return returnList;
        }


        /// <summary>
        /// 获取最新创建的3个课程
        /// </summary>
        public List<CourseInfoListDto> GetCourseInfo3News(int displayPosition)
        {

            var courseInfos = _courseInfoRepository.GetAll().Where(t => t.Status != 1 && t.Status != 3).Where(t => t.DisplayPosition == displayPosition || t.DisplayPosition ==0).OrderByDescending(t=>t.CreationTime).Skip(0).Take(3);
            var returnList = courseInfos.MapTo<List<CourseInfoListDto>>();
            return returnList;
        }


        /// <summary>
        /// 获取最新创建的3个课程
        /// </summary>
        public List<CourseInfoListDto> GetCourseInfoByCategory3News(int categoryId,int displayPosition)
        {

            var query = from info in _courseInfoRepository.GetAll().Where(t => t.Status != 1 && t.Status != 3).Where(t => t.DisplayPosition == displayPosition || t.DisplayPosition == 0)
                        join cate in _categoryRepository.GetAll().Where(t => t.Path.StartsWith(categoryId.ToString() + "/")) on info.CategoryType equals cate.CategoryId
                        into all                        
                        from t in all 
                        select new CourseInfoListDto()
                        {
                            Id = info.Id,
                            CourseName = info.CourseName,
                            Description = info.Description,
                            Sort = info.Sort,
                            Enabled = info.Enabled,
                            Content = info.Content,
                            Type = info.Type,
                            StageName = info.StageName,
                            Status = info.Status,
                            ImageUrl = info.ImageUrl,
                            TeacherId = info.TeacherId,
                            TeacherName = info.TeacherName,
                            LiveStatus = info.LiveStatus,
                            StartTime = info.StartTime,
                            EndTime = info.EndTime,
                            DisplayPosition = info.DisplayPosition,
                            ReadTimes = info.ReadTimes,
                            CollectionTimes = info.CollectionTimes,
                            CreationTime = info.CreationTime,
                            Enrollment = info.Enrollment,
                            TypeName = info.TypeName
                        };

            var courseInfos = query.OrderByDescending(t => t.CreationTime).Take(3)
            .ToList();

            var courseInfoListDtos = courseInfos.MapTo<List<CourseInfoListDto>>();
            return courseInfoListDtos;
        }


        /// <summary>
        /// 获取最新创建的3个课程
        /// </summary>
        public List<CourseInfoListDto> GetCourseInfoByUserId(int userId,int type,int status)
        {

            var query = from info in _courseInfoRepository.GetAll().Where(t => t.Status != 1 && t.Status != 3).WhereIf(type==4,t=>t.Type==type).WhereIf(type!=4,t=>t.Type!=4)
                        join cate in _categoryRepository.GetAll() on info.CategoryType equals cate.CategoryId
                        join user in _courseBoundPersonnelRepository.GetAll()
                            .WhereIf(status==1,t=>t.CheckIN==false)
                            .WhereIf(status == 2, t => t.CheckIN == true)
                            .WhereIf(status == 3, t => t.CheckIN == true)
                            .WhereIf(status == 3, t => t.IsComplete == true)
                            .Where(t => t.AccountSysNo == userId) 
                            on info.Id equals user.CourseId
                        into all
                        from t in all
                        select new CourseInfoListDto()
                        {
                            Id = info.Id,
                            CourseName = info.CourseName,
                            Description = info.Description,
                            Sort = info.Sort,
                            Enabled = info.Enabled,
                            Content = info.Content,
                            Type = info.Type,
                            StageName = info.StageName,
                            Status = info.Status,
                            ImageUrl = info.ImageUrl,
                            TeacherId = info.TeacherId,
                            TeacherName = info.TeacherName,
                            LiveStatus = info.LiveStatus,
                            StartTime = info.StartTime,
                            EndTime = info.EndTime,
                            DisplayPosition = info.DisplayPosition,
                            ReadTimes = info.ReadTimes,
                            CollectionTimes = info.CollectionTimes,
                            CreationTime = info.CreationTime,
                            Enrollment = info.Enrollment,
                            TypeName = info.TypeName
                        };

            var courseInfos = query.OrderByDescending(t => t.CreationTime).Take(3)
            .ToList();

            var courseInfoListDtos = courseInfos.MapTo<List<CourseInfoListDto>>();
            return courseInfoListDtos;
        }



        /// <summary>
        /// 通过Id获取课程基本信息信息进行编辑或修改 
        /// </summary>
        public GetCourseInfoForEditOutput GetCourseInfoForEdit(NullableIdDto<long> input)
        {
            var output = new GetCourseInfoForEditOutput();

            CourseInfoEditDto courseInfoEditDto;

            if (input.Id.HasValue)
            {
                var entity =  _courseInfoRepository.Get(input.Id.Value);
                courseInfoEditDto = entity.MapTo<CourseInfoEditDto>();
            }
            else
            {
                courseInfoEditDto = new CourseInfoEditDto();
            }

            output.CourseInfo = courseInfoEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取课程基本信息ListDto信息
        /// </summary>
        public CourseInfoListDto GetCourseInfoById(EntityDto<long> input)
        {

            var query = from info in _courseInfoRepository.GetAll().Where(t=>t.Id == input.Id)
                        join cate in _categoryRepository.GetAll() on info.CategoryType equals cate.CategoryId
                        into all
                        from t in all
                        select new CourseInfoListDto()
                        {
                            Id = info.Id,
                            CourseName = info.CourseName,
                            Description = info.Description,
                            Sort = info.Sort,
                            Enabled = info.Enabled,
                            Content = info.Content,
                            Type = info.Type,
                            StageName = info.StageName,
                            Status = info.Status,
                            ImageUrl = info.ImageUrl,
                            TeacherId = info.TeacherId,
                            TeacherName = info.TeacherName,
                            CourseCategoryName= t.CourseCategoryName,
                            LiveStatus = info.LiveStatus,
                            StartTime = info.StartTime,
                            EndTime = info.EndTime,
                            DisplayPosition = info.DisplayPosition,
                            ReadTimes = info.ReadTimes,
                            CollectionTimes = info.CollectionTimes,
                            CreationTime = info.CreationTime,
                            ExaminationId = info.ExaminationId,
                            PollId = info.PollId,
                            Enrollment = info.Enrollment,
                            TypeName = info.TypeName
                        };

            var courseInfos = query.ToList();

            if (courseInfos != null)
                return courseInfos[0];
            else
                return new CourseInfoListDto();

        }


        /// <summary>
        /// 通过指定id获取课程基本信息EditDto信息
        /// </summary>
        public CourseInfoEditDto GetCourseInfoEditById(EntityDto<long> input)
        {
            var entity = _courseInfoRepository.Get(input.Id);

            return entity.MapTo<CourseInfoEditDto>();
        }


        /// <summary>
        /// 新增或更改课程基本信息
        /// </summary>
        public void CreateOrUpdateCourseInfo(CreateOrUpdateCourseInfoInput input)
        {
            if (input.CourseInfoEditDto.Id.HasValue)
            {
                if (input.CourseInfoEditDto.Status == (int)CourseStatus.Fail)
                {
                    input.CourseInfoEditDto.Status = (int)CourseStatus.Pending;
                }                
                UpdateCourseInfo(input.CourseInfoEditDto);
            }
            else
            {
                input.CourseInfoEditDto.Status = (int)CourseStatus.Pending;
                CreateCourseInfo(input.CourseInfoEditDto);
            }
        }

        /// <summary>
        /// 新增课程基本信息
        /// </summary>
        public CourseInfoEditDto CreateCourseInfo(CourseInfoEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<CourseInfo>();

            entity = _courseInfoRepository.Insert(entity);
            return entity.MapTo<CourseInfoEditDto>();
        }

        /// <summary>
        /// 编辑课程基本信息
        /// </summary>
        public void UpdateCourseInfo(CourseInfoEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = _courseInfoRepository.Get(input.Id.Value);
            input.MapTo(entity);

            _courseInfoRepository.Update(entity);
        }

        /// <summary>
        /// 删除课程基本信息
        /// </summary>
        public void DeleteCourseInfo(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            _courseInfoRepository.Delete(input.Id);
        }

        /// <summary>
        /// 批量删除课程基本信息
        /// </summary>
        public void BatchDeleteCourseInfo(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            _courseInfoRepository.Delete(s => input.Contains(s.Id));
        }

        #endregion

    }
}
