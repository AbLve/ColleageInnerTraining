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
using System;
using Abp.Domain.Uow;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 课程人员绑定表服务实现
    /// </summary>
    public class CourseBoundPersonnelAppService : ColleageInnerTrainingAppServiceBase, ICourseBoundPersonnelAppService
    {
        /// <summary>
        /// 课程人员对像
        /// </summary>
        private readonly IRepository<CourseBoundPersonnel, long> _courseBoundPersonnelRepository;
        /// <summary>
        /// 用户业务对像
        /// </summary>
        private readonly IRepository<UserAccount, long> _userAccountRepository;
        /// <summary>
        /// 部门业务对像
        /// </summary>
        private readonly IRepository<DepartmentInfo, long> _departmentInfoRepository;
        /// <summary>
        /// 课程业务对像
        /// </summary>
        private readonly IRepository<CourseInfo, long> _courseInfoRepository;
        /// <summary>
        /// 考试记录业务对像
        /// </summary>
        private readonly IRepository<ExamRecord, long> _examRecordRepository;

        /// <summary>
        /// 岗位业务对像
        /// </summary>
        private readonly IRepository<JobPost, long> _jobPostRepository;


        private readonly CourseBoundPersonnelManage _courseBoundPersonnelManage;

        private readonly ICourseInfoAppService _courseInfoAppService;
        private readonly IDepartmentInfoAppService departmentService;
        private readonly IUserAccountAppService userAccountService;
        private readonly IClassUserAppService classUserlService;
        private ICourseBoundConfigureTypeAppService cbPersonnelService;

        /// <summary>
        /// 构造方法
        /// </summary>
        public CourseBoundPersonnelAppService(IRepository<CourseBoundPersonnel, long> courseBoundPersonnelRepository,
        CourseBoundPersonnelManage courseBoundPersonnelManage, ICourseInfoAppService courseInfoAppService,IDepartmentInfoAppService _departmentService,
        IUserAccountAppService _userAccountService,IClassUserAppService _classUserlService,
        ICourseBoundConfigureTypeAppService _cbPersonnelService)
        {
            _courseBoundPersonnelRepository = courseBoundPersonnelRepository;
            _courseBoundPersonnelManage = courseBoundPersonnelManage;
            _courseInfoAppService = courseInfoAppService;
            departmentService = _departmentService;
            userAccountService = _userAccountService;
            cbPersonnelService = _cbPersonnelService;
            classUserlService = _classUserlService;
        }

        #region 课程人员绑定表管理

        /// <summary>
        /// 根据查询条件获取课程人员绑定表分页列表
        /// </summary>
        public PagedResultDto<CourseBoundPersonnelListDto> GetPagedCourseBoundPersonnels(GetCourseBoundPersonnelInput input)
        {

            var query = _courseBoundPersonnelRepository.GetAll();
            //TODO:根据传入的参数添加过滤条件

            var courseBoundPersonnelCount = query.Count();

            var courseBoundPersonnels = query
           .OrderByDescending(t => t.CreationTime)
            .PageBy(input)
            .ToList();

            var courseBoundPersonnelListDtos = courseBoundPersonnels.MapTo<List<CourseBoundPersonnelListDto>>();
            return new PagedResultDto<CourseBoundPersonnelListDto>(
            courseBoundPersonnelCount,
            courseBoundPersonnelListDtos
            );
        }

        /// <summary>
        /// 根据查询条件获取课程人员绑定表分页列表
        /// </summary>
        public PagedResultDto<CourseBoundPersonnelExportDto> GetPagedCourseBoundPersonnelsForExport(GetCourseBoundPersonnelInput input)
        {
            var query = from person in _courseBoundPersonnelRepository.GetAll()
                        join user in _userAccountRepository.GetAll() on person.AccountSysNo equals user.SysNO
                        join depart in _departmentInfoRepository.GetAll() on user.DepartmentID equals depart.DepartmentId
                        join depart1 in _departmentInfoRepository.GetAll() on depart.DepartmentId equals depart1.DepartmentId
                        join course in _courseInfoRepository.GetAll() on person.CourseId equals course.Id
                        join exam in _examRecordRepository.GetAll() on person.AccountSysNo equals exam.UserId
                        join post in _jobPostRepository.GetAll() on user.PostID equals post.Id
                        where exam.ExamId == course.ExaminationId
                        select new CourseBoundPersonnelExportDto()
                        {
                            Id = person.Id,
                            CourseName = course.CourseName,
                            TrainDocNo = "",
                            UserName = user.DisplayName,
                            DepartMentName1 = depart1.DisplayName,
                            DepartMentName2 = depart.DisplayName,
                            PostName = post.Name,
                            Gender = "",
                            StartTime = course.StartTime,
                            EndTime = course.EndTime,
                            CourseAmout = 0,
                            TypeName = course.TypeName,
                            TimeLength = course.TimeLength,
                            ExamMethod = "考试",
                            ExamResult = exam.Status == 0 ? "考试中" : exam.Status == 1 ? "通过" : exam.Status == 2 ? "未通过" : exam.Status == 3 ? "待批改" : "批改中",
                            TotalTime = course.TimeLength,
                            TotalAmount = 0,
                            TrainSore = 0,
                            TotalTrainSore = 0,
                            memo = ""
                        };

            var courseInfoCount = query.Count();

            var boundPersonnelExportDtos = query
                .PageBy(input)
                .OrderByDescending(t => t.UserName).OrderByDescending(t => t.CourseName)
                .ToList();
            var courseBoundPersonnelExportDtos = boundPersonnelExportDtos.MapTo<List<CourseBoundPersonnelExportDto>>();
            return new PagedResultDto<CourseBoundPersonnelExportDto>(
            courseInfoCount,
            courseBoundPersonnelExportDtos);
        }




        /// <summary>
        /// 通过Id获取课程人员绑定表信息进行编辑或修改 
        /// </summary>
        public GetCourseBoundPersonnelForEditOutput GetCourseBoundPersonnelForEdit(NullableIdDto<long> input)
        {
            var output = new GetCourseBoundPersonnelForEditOutput();

            CourseBoundPersonnelEditDto courseBoundPersonnelEditDto;

            if (input.Id.HasValue)
            {
                var entity = _courseBoundPersonnelRepository.Get(input.Id.Value);
                courseBoundPersonnelEditDto = entity.MapTo<CourseBoundPersonnelEditDto>();
            }
            else
            {
                courseBoundPersonnelEditDto = new CourseBoundPersonnelEditDto();
            }

            output.CourseBoundPersonnel = courseBoundPersonnelEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取课程人员绑定表ListDto信息
        /// </summary>
        public CourseBoundPersonnelListDto GetCourseBoundPersonnelById(EntityDto<long> input)
        {
            var entity = _courseBoundPersonnelRepository.Get(input.Id);

            return entity.MapTo<CourseBoundPersonnelListDto>();
        }


        /// <summary>
        /// 通过人员ID和课程Id获取绑定数据
        /// </summary>
        public CourseBoundPersonnelEditDto GetCourseBoundByUserIdOrCourseId(int sysno, int courseId)
        {
            var entity = _courseBoundPersonnelRepository.GetAll().FirstOrDefault(c => c.AccountSysNo == sysno && c.CourseId == courseId);
            return entity.MapTo<CourseBoundPersonnelEditDto>();
        }

        /// <summary>
        /// 通过课程ID和获取所有用户ID
        /// </summary>
        public List<int> GetCourseBoundUserId(int courseId)
        {
            var entityUserIds = _courseBoundPersonnelRepository.GetAll()
                .Where(c => c.CourseId == courseId).Select(v =>v.AccountSysNo).ToList();
            return entityUserIds;
        }


        /// <summary>
        /// 通过课程ID和获取所有数据
        /// </summary>
        public List<CourseBoundPersonnel> GetCourseBoundUserByCourseId(int courseId)
        {
            var entityUserIds = _courseBoundPersonnelRepository.GetAll()
                .Where(c => c.CourseId == courseId).ToList();
            return entityUserIds;
        }


        /// <summary>
        /// 新增或更改课程人员绑定表
        /// </summary>
        public void CreateOrUpdateCourseBoundPersonnel(CreateOrUpdateCourseBoundPersonnelInput input)
        {
            if (input.CourseBoundPersonnelEditDto.Id.HasValue)
            {
                UpdateCourseBoundPersonnel(input.CourseBoundPersonnelEditDto);
            }
            else
            {
                CreateCourseBoundPersonnel(input.CourseBoundPersonnelEditDto);
            }
        }

        /// <summary>
        /// 新增课程人员绑定表
        /// </summary>
        public CourseBoundPersonnelEditDto CreateCourseBoundPersonnel(CourseBoundPersonnelEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<CourseBoundPersonnel>();
            entity.Id = _courseBoundPersonnelRepository.InsertAndGetId(entity);
            SetEnrollment(input.CourseId);
            return entity.MapTo<CourseBoundPersonnelEditDto>();
        }

        private void SetEnrollment(int courseId)
        {
            var course = _courseInfoAppService.GetCourseInfoEditById(new EntityDto<long>() { Id = courseId });
            course.Enrollment = SumEnrollment(courseId);
            _courseInfoAppService.UpdateCourseInfo(course);
        }

        //获取报名人数
        private int SumEnrollment(int courseId)
        {
            var queryDate = _courseBoundPersonnelRepository.GetAll().Where(t=>t.CourseId == courseId);
            return queryDate.Count();
        }

        /// <summary>
        /// 编辑课程人员绑定表
        /// </summary>
        public void UpdateCourseBoundPersonnel(CourseBoundPersonnelEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = _courseBoundPersonnelRepository.Get(input.Id.Value);
            input.MapTo(entity);

            _courseBoundPersonnelRepository.Update(entity);
        }

        /// <summary>
        /// 删除课程人员绑定表
        /// </summary>
        public void DeleteCourseBoundPersonnel(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            _courseBoundPersonnelRepository.Delete(input.Id);
        }

        /// <summary>
        /// 删除课程人员根据课程和用户Id
        /// </summary>
        public void DeleteCourseBoundPersonnelBy(int courseId, int sysNo)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            _courseBoundPersonnelRepository.Delete(c => c.CourseId == courseId && c.AccountSysNo == sysNo);
        }

        /// <summary>
        /// 批量删除课程人员绑定表
        /// </summary>
        public void BatchDeleteCourseBoundPersonnel(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            _courseBoundPersonnelRepository.Delete(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 根据课程获取所有用户
        /// </summary>
        public List<CourseBoundPersonnel> GetCouPerByCourseAll(int courseId)
        {
            var date = _courseBoundPersonnelRepository.GetAll().Where(c => c.CourseId == courseId).ToList();
            return date;
        }



        /// <summary>
        /// 获取部门下的所有用户
        /// </summary>
        public List<UserAccount> GetUserDateByDepartment(int courseId,DepartmentInfoListDto department)
        {
            List<UserAccount> accountlist = new List<UserAccount>();
            //获取当前部门以及子集部门
            var departmentList = departmentService.GetAllDepartmentInfos().Where(c => c.Path.StartsWith(department.Path)).Select(v => v.DepartmentId);
            if (departmentList.Any())
            {
                var userAccountDataList = userAccountService.GetAccountData().Where(c => departmentList.Contains(c.DepartmentID));//获取部门下的所有用户
                var CBCTUserSysNoList = GetCourseBoundUserId(courseId);//获取课程下的所有用户Id
                if (userAccountDataList.Any())
                {
                    accountlist = userAccountDataList.Where(v => !CBCTUserSysNoList.Contains(v.SysNO)).ToList();//排除课程下的用户                    
                }
            }
            return accountlist;
        }

        /// <summary>
        /// 获取岗位下的所有用户
        /// </summary>
        public List<UserAccount> GetUserDateByJobPost(int courseId, JobPostListDto job)
        {
            List<UserAccount> accountlist = new List<UserAccount>();
            var userAccountDataList = userAccountService.GetAccountData().Where(c => c.PostID == job.Id);//获取当前岗位的所有用户
            var CBCTUserSysNoList = GetCourseBoundUserId(courseId);//获取课程下的所有用户Id
            if (userAccountDataList.Any())
            {
                accountlist = userAccountDataList.Where(v => !CBCTUserSysNoList.Contains(v.SysNO)).ToList();//排除课程下的用户      
            }
            return accountlist;
        }

        /// <summary>
        /// 获取班级下的所有用户
        /// </summary>
        public List<UserAccount> GetUserDateByClasses(int courseId, ClassesInfoListDto classed)
        {
            List<UserAccount> accountlist = new List<UserAccount>();
            var userAccountDataList = userAccountService.GetAccountData();//获取所用用户
            var classUserIdList = classUserlService.GetAll().Where(c => c.ClassId == classed.Id).ToList().Select(v => v.UserId);//获取班级的所有用户
            if (userAccountDataList.Any() && classUserIdList.Any())
            {
                var apendingDate = userAccountDataList.Where(c => classUserIdList.Contains(c.SysNO));//得到班级的所有用户
                var CBCTUserSysNoList = GetCourseBoundUserId(courseId);//已存在的用户
                accountlist = apendingDate.Where(c => !CBCTUserSysNoList.Contains(c.SysNO)).ToList();//排除已存在的用户
            }            
            return accountlist;
        }

        /// <summary>
        /// 获取个人下的所有用户
        /// </summary>
        public List<UserAccount> GetUserDateByPerson(int courseId, int type)
        {
            List<UserAccount> accountlist = new List<UserAccount>();
            var userAccountDataList = userAccountService.GetAccountData();//获取所用用户
            var personList = cbPersonnelService.GetCTypeByCourseIdOrType(courseId, type).ToList();
            if (userAccountDataList.Any() && personList.Any())
            {
                var userListIds = personList.Select(v => v.BusinessId);
                accountlist = userAccountDataList.Where(c => userListIds.Contains(c.SysNO)).ToList();
            }
            return accountlist;
        }
        #endregion
    }
}
