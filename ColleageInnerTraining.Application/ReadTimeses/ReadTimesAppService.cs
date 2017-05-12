
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 阅读人数服务实现
    /// </summary>
    public class ReadTimesAppService : ColleageInnerTrainingAppServiceBase, IReadTimesAppService
    {
        private readonly IRepository<ReadTimes, long> _readTimesRepository;


        private readonly ReadTimesManage _readTimesManage;
        /// <summary>
        /// 课程接口对像
        /// </summary>
        private readonly ICourseInfoAppService _courseInfoAppService;

        /// <summary>
        /// 构造方法
        /// </summary>
        public ReadTimesAppService(IRepository<ReadTimes, long> readTimesRepository,ReadTimesManage readTimesManage, ICourseInfoAppService courseInfoAppService)
        {
            _readTimesRepository = readTimesRepository;
            _readTimesManage = readTimesManage;
            _courseInfoAppService = courseInfoAppService;

        }

        #region 阅读人数管理


        public virtual ReadTimesListDto GetReadTimesByTypeAndId(string type, int bizId, int userId)
        {
            var query = _readTimesRepository.GetAll().Where(t => t.BizType == type).Where(t => t.BizId == bizId).Where(t => t.UserId == userId);
            var collections = query.ToList();
            var collectionListDtos = collections.MapTo<List<ReadTimesListDto>>();
            if (collectionListDtos != null && collectionListDtos.Count() > 0)
                return collectionListDtos[0];
            else
                return null;
        }
        
        /// <summary>
        /// 新增阅读人数
        /// </summary>
        public virtual ReadTimesEditDto CreateReadTimes(ReadTimesEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<ReadTimes>();
            long id = _readTimesRepository.InsertAndGetId(entity);
            entity.Id = id;
            SetCollectionTimes(input.BizType, input.BizId);

            return entity.MapTo<ReadTimesEditDto>();
        }


        private void SetCollectionTimes(string type, int bizId)
        {

            switch (type)
            {
                case "Course":
                    CourseInfoEditDto course = new CourseInfoEditDto();
                    course = _courseInfoAppService.GetCourseInfoEditById(new EntityDto<long>() { Id = bizId });
                    course.ReadTimes = CountReadTimes(type, bizId);
                    _courseInfoAppService.UpdateCourseInfo(course);
                    break;

            }

        }

        private int CountReadTimes(string type, int bizId)
        {

            var query = _readTimesRepository.GetAll().Where(t => t.IsDeleted == false).Where(t => t.BizId == bizId).Where(t => t.BizType == type).GroupBy(t => t.UserId);
            //TODO:根据传入的参数添加过滤条件
            var collectionCount = query.Count();
            return collectionCount;
        }


        #endregion
    }
}
