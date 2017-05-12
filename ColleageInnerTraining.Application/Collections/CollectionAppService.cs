using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 收藏服务实现
    /// </summary>

    public class CollectionAppService : ColleageInnerTrainingAppServiceBase, ICollectionAppService
    {
        private readonly IRepository<Collection, long> _collectionRepository;
       
        private readonly IRepository<CourseInfo, long> _courseInfoAppService2;

        private readonly ICourseInfoAppService _courseInfoAppService;

        private readonly CollectionManage _collectionManage;
        /// <summary>
        /// 构造方法
        /// </summary>
        public CollectionAppService(IRepository<Collection, long> collectionRepository, CollectionManage collectionManage, IRepository<CourseInfo, long> courseInfoAppService2,
            ICourseInfoAppService courseInfoAppService)
        {
            _collectionRepository = collectionRepository;
            _collectionManage = collectionManage;

            _courseInfoAppService = courseInfoAppService;
            _courseInfoAppService2 = courseInfoAppService2;
        }

        #region 收藏管理

        /// <summary>
        /// 根据查询条件获取收藏分页列表
        /// </summary>
        public PagedResultDto<CollectionListDto> GetPagedCollections(GetCollectionInput input)
        {

            var query = from info in _collectionRepository.GetAll().WhereIf(input.BizType != null && input.BizType != string.Empty, t => t.BizType == input.BizType)
                        join courseInfo in _courseInfoAppService2.GetAll() on info.BizId equals courseInfo.Id
                        into all
                        from t in all
                        select new CollectionListDto()
                        {
                            Id = info.Id,
                            ImageUrl = t.ImageUrl,
                            BizId=  info.BizId,
                            BizType =info.BizType,
                            BizName = t.CourseName,
                            Enrollment = t.Enrollment,
                            TimeLength = t.TimeLength,
                            TypeName = t.TypeName
                        };
            //TODO:根据传入的参数添加过滤条件
            var collectionCount = query.Count();

            var collections = query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToList();

            var collectionListDtos = collections.MapTo<List<CollectionListDto>>();
            return new PagedResultDto<CollectionListDto>(
            collectionCount,
            collectionListDtos
            );
        }

        /// <summary>
        /// 通过Id获取收藏信息进行编辑或修改 
        /// </summary>
        public async Task<GetCollectionForEditOutput> GetCollectionForEditAsync(NullableIdDto<long> input)
        {
            var output = new GetCollectionForEditOutput();

            CollectionEditDto collectionEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _collectionRepository.GetAsync(input.Id.Value);
                collectionEditDto = entity.MapTo<CollectionEditDto>();
            }
            else
            {
                collectionEditDto = new CollectionEditDto();
            }

            output.Collection = collectionEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取收藏ListDto信息
        /// </summary>
        public async Task<CollectionListDto> GetCollectionByIdAsync(EntityDto<long> input)
        {
            var entity = await _collectionRepository.GetAsync(input.Id);

            return entity.MapTo<CollectionListDto>();
        }

        /// <summary>
        /// 新增或更改收藏
        /// </summary>
        public void CreateOrUpdateCollection(CreateOrUpdateCollectionInput input)
        {
            if (input.CollectionEditDto.Id.HasValue)
            {
                UpdateCollection(input.CollectionEditDto);
            }
            else
            {
                CreateCollection(input.CollectionEditDto);
            }
        }

        /// <summary>
        /// 新增收藏
        /// </summary>
        public virtual CollectionEditDto CreateCollection(CollectionEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<Collection>();
            long id = _collectionRepository.InsertAndGetId(entity);
            entity.Id = id;
            SetCollectionTimes(input.BizType, input.BizId,"Add");
            return entity.MapTo<CollectionEditDto>();
        }

        private void SetCollectionTimes(string type, int bizId,string option)
        {

            switch (type)
            {
                case "Course":
                    CourseInfoEditDto course = new CourseInfoEditDto();
                    course = _courseInfoAppService.GetCourseInfoEditById(new EntityDto<long>() { Id = bizId });
                    course.CollectionTimes = CountCollectionTimes(type, bizId, option);
                    _courseInfoAppService.UpdateCourseInfo(course);
                    break;

            }

        }

        private int CountCollectionTimes(string type,int bizId,string option) {

            var query = _collectionRepository.GetAll().Where(t=>t.IsDeleted == false).Where(t => t.BizId == bizId).Where(t => t.BizType == type).GroupBy(t=>t.UserId);
            //TODO:根据传入的参数添加过滤条件
            var collectionCount = query.Count();
            if (option == "Del") collectionCount--;
            if (collectionCount < 0)  collectionCount = 0;
            return collectionCount;
        }
        public virtual CollectionListDto GetCollectionByTypeAndId(string type, int bizId, int userId)
        {
            var query = _collectionRepository.GetAll().Where(t=>t.BizType == type).Where(t=>t.BizId ==bizId).Where(t => t.UserId == userId);
            var collections = query.ToList();
            var collectionListDtos = collections.MapTo<List<CollectionListDto>>();
            if (collectionListDtos != null&& collectionListDtos.Count()>0)
                return collectionListDtos[0];
            else
                return null;
        }
        /// <summary>
        /// 编辑收藏
        /// </summary>
        public void UpdateCollection(CollectionEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = _collectionRepository.Get(input.Id.Value);
            input.MapTo(entity);
            _collectionRepository.Update(entity);
        }

        /// <summary>
        /// 删除收藏
        /// </summary>
        public void DeleteCollection(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            _collectionRepository.Delete(input.Id);
        }

        /// <summary>
        /// 删除收藏
        /// </summary>
        public void DeleteCollectionByBizId(string type, int bizId, int userId)
        {
            var query = _collectionRepository.GetAll()
                .Where(t=>t.IsDeleted == false)
                .Where(t => t.BizType == type)
                .Where(t => t.BizId == bizId)
                .Where(t => t.UserId == userId);
            var collections = query.ToList();
            var collectionListDtos = collections.MapTo<List<CollectionListDto>>();
            foreach(var c in collectionListDtos)
            { 
            //TODO:删除前的逻辑判断，是否允许删除
                DeleteCollection(new EntityDto<long>() { Id= c.Id});
                SetCollectionTimes(type, bizId, "Del");
            }
        }

        /// <summary>
        /// 批量删除收藏
        /// </summary>
        public async Task BatchDeleteCollectionAsync(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _collectionRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion
    }
}
