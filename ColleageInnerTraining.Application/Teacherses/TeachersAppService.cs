using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Application;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 内训师服务实现
    /// </summary>
    public class TeachersAppService : ColleageInnerTrainingAppServiceBase, ITeachersAppService
    {
        private readonly IRepository<Teachers, long> _teachersRepository;
        private readonly TeachersManage _teachersManage;

        private readonly IRepository<DepartmentInfo, long> _departmentRepository;
        private readonly DepartmentInfoManage _departmentManage;

        private readonly IRepository<CourseInfo, long> _courseInfoRepository;
        private readonly CourseInfoManage _courseInfoManage;
        /// <summary>
        /// 构造方法
        /// </summary>
        public TeachersAppService(IRepository<Teachers, long> teachersRepository, TeachersManage teachersManage,
            IRepository<DepartmentInfo, long> departmentRepository, DepartmentInfoManage departmentManage,
            IRepository<CourseInfo, long> courseInfoRepository, CourseInfoManage courseInfoManage)
        {
            _teachersRepository = teachersRepository;
            _teachersManage = teachersManage;
            _departmentRepository = departmentRepository;
            _departmentManage = departmentManage;
            _courseInfoRepository = courseInfoRepository;
            _courseInfoManage = courseInfoManage;
        }

        #region 内训师管理

        /// <summary>
        /// 根据查询条件获取内训师分页列表
        /// </summary>
        public PagedResultDto<TeachersListDto> GetPagedTeacherss(GetTeachersInput input)
        {

            var query = from tear in _teachersRepository.GetAll()
                        join depart in _departmentRepository.GetAll() on tear.DepartmentId equals depart.DepartmentId into dti
                        join course in _courseInfoRepository.GetAll() on tear.Id equals course.TeacherId into programs
                        select new TeachersListDto
                        {
                            Id = tear.Id,
                            SysNo = tear.SysNo,
                            UserName = tear.UserName,
                            UserPhone = tear.UserPhone,
                            UserEmail = tear.UserEmail,
                            DepartmentId = tear.DepartmentId,
                            DepartmentName = dti.FirstOrDefault().DisplayName,
                            JobpostId = tear.JobpostId,
                            Role = tear.Role,
                            Status = tear.Status,
                            PortraitUrl = tear.PortraitUrl,
                            SpeakerCourse = tear.SpeakerCourse,
                            CreationTime = tear.CreationTime,
                            TrainCount = programs.Count()

                        };
            //TODO:根据传入的参数添加过滤条件

            var teachersCount = query.Count();

            var teacherss = query
           .OrderByDescending(t => t.CreationTime)
            .PageBy(input)
            .ToList();

            var teachersListDtos = teacherss.MapTo<List<TeachersListDto>>();
            return new PagedResultDto<TeachersListDto>(
            teachersCount,
            teachersListDtos
            );
        }

        /// <summary>
        /// 通过Id获取内训师信息进行编辑或修改 
        /// </summary>
        public GetTeachersForEditOutput GetTeachersForEdit(NullableIdDto<long> input)
        {
            var output = new GetTeachersForEditOutput();

            TeachersEditDto teachersEditDto;

            if (input.Id.HasValue)
            {
                var entity = _teachersRepository.Get(input.Id.Value);
                teachersEditDto = entity.MapTo<TeachersEditDto>();
            }
            else
            {
                teachersEditDto = new TeachersEditDto();
            }

            output.Teachers = teachersEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取内训师ListDto信息
        /// </summary>
        public TeachersListDto GetTeachersById(EntityDto<long> input)
        {



            var query = from tear in _teachersRepository.GetAll().Where(t=>t.Id ==input.Id)
                        join depart in _departmentRepository.GetAll() on tear.DepartmentId equals depart.DepartmentId into dti
                        join course in _courseInfoRepository.GetAll() on tear.Id equals course.TeacherId into programs
                        select new TeachersListDto
                        {
                            Id = tear.Id,
                            SysNo = tear.SysNo,
                            UserName = tear.UserName,
                            UserPhone = tear.UserPhone,
                            UserEmail = tear.UserEmail,
                            DepartmentId = tear.DepartmentId,
                            DepartmentName = dti.FirstOrDefault().DisplayName,
                            JobpostId = tear.JobpostId,
                            Role = tear.Role,
                            Status = tear.Status,
                            PortraitUrl = tear.PortraitUrl,
                            SpeakerCourse = tear.SpeakerCourse,
                            CreationTime = tear.CreationTime,
                            TrainCount = programs.Count()

                        };
            var teachers = query.ToList();
            if (teachers != null)
                return teachers[0];
            else
                return new TeachersListDto();
        }


        /// <summary>
        /// 获取所有内训师
        /// </summary>
        public List<Teachers> GetAllDate()
        {
            var entity = _teachersRepository.GetAll().ToList();
            return entity;
        }



        /// <summary>
        /// 新增或更改内训师
        /// </summary>
        public void CreateOrUpdateTeachers(CreateOrUpdateTeachersInput input)
        {
            if (input.TeachersEditDto.Id.HasValue)
            {
                UpdateTeachers(input.TeachersEditDto);
            }
            else
            {
                CreateTeachers(input.TeachersEditDto);
            }
        }

        /// <summary>
        /// 新增内训师
        /// </summary>
        public TeachersEditDto CreateTeachers(TeachersEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<Teachers>();

            entity = _teachersRepository.Insert(entity);
            return entity.MapTo<TeachersEditDto>();
        }

        /// <summary>
        /// 编辑内训师
        /// </summary>
        public void UpdateTeachers(TeachersEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = _teachersRepository.Get(input.Id.Value);
            input.MapTo(entity);

            _teachersRepository.Update(entity);
        }

        /// <summary>
        /// 删除内训师
        /// </summary>
        public void DeleteTeachers(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            _teachersRepository.Delete(input.Id);
        }

        /// <summary>
        /// 批量删除内训师
        /// </summary>
        public void BatchDeleteTeachers(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            _teachersRepository.Delete(s => input.Contains(s.Id));
        }

        #endregion












    }
}
