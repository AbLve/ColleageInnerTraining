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
using System;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 班级和部门或岗位服务实现
    /// </summary>
    public class ClassDepJobAppService : ColleageInnerTrainingAppServiceBase, IClassDepJobAppService
    {
        private readonly IRepository<ClassDepJob, long> _ClassDepJobRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        public ClassDepJobAppService(IRepository<ClassDepJob, long> ClassDepJobRepository)
        {
            _ClassDepJobRepository = ClassDepJobRepository;
        }

        #region 班级和部门或岗位管理

        /// <summary>
        /// 根据查询条件获取班级和部门或岗位分页列表
        /// </summary>
        public PagedResultDto<ClassDepJobListDto> GetPagedClassDepJobs(GetClassDepJobInput input)
        {

            var query = _ClassDepJobRepository.GetAll().WhereIf(input.cId > 0, t => t.ClassId == input.cId);
            //TODO:根据传入的参数添加过滤条件

            var ClassDepJobCount = query.Count();

            var ClassDepJobs = query
            .OrderByDescending(t => t.CreationTime)
            .PageBy(input)
            .ToList();

            var ClassDepJobListDtos = ClassDepJobs.MapTo<List<ClassDepJobListDto>>();
            return new PagedResultDto<ClassDepJobListDto>(
            ClassDepJobCount,
            ClassDepJobListDtos
            );
        }
        public List<int> GetClassDepJobCIdsOrJids(int type)
        {
            return _ClassDepJobRepository.GetAll().Where(t => t.type == type).Select(t => t.BusinessId).ToList();
        }
        /// <summary>
        /// 通过Id获取班级和部门或岗位信息进行编辑或修改 
        /// </summary>
        public GetClassDepJobForEditOutput GetClassDepJobForEdit(NullableIdDto<long> input)
        {
            var output = new GetClassDepJobForEditOutput();

            ClassDepJobEditDto ClassDepJobEditDto;

            if (input.Id.HasValue)
            {
                var entity = _ClassDepJobRepository.Get(input.Id.Value);
                ClassDepJobEditDto = entity.MapTo<ClassDepJobEditDto>();
            }
            else
            {
                ClassDepJobEditDto = new ClassDepJobEditDto();
            }

            output.ClassDepJob = ClassDepJobEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取班级和部门或岗位ListDto信息
        /// </summary>
        public ClassDepJobListDto GetClassDepJobById(EntityDto<long> input)
        {
            var entity = _ClassDepJobRepository.Get(input.Id);

            return entity.MapTo<ClassDepJobListDto>();
        }

        /// <summary>
        /// 通过班级ID和类型获取数据
        /// </summary>
        public List<ClassDepJob> GetCDJByCIdOrTypeId(int CId, int TId)
        {
            return _ClassDepJobRepository.GetAll().Where(c => c.ClassId == CId && c.type == TId).ToList();
        }


        /// <summary>
        /// 新增或更改班级和部门或岗位
        /// </summary>
        public void CreateOrUpdateClassDepJob(CreateOrUpdateClassDepJobInput input)
        {
            if (input.ClassDepJobEditDto.Id.HasValue)
            {
                UpdateClassDepJob(input.ClassDepJobEditDto);
            }
            else
            {
                CreateClassDepJob(input.ClassDepJobEditDto);
            }
        }

        /// <summary>
        /// 新增班级和部门或岗位
        /// </summary>
        public ClassDepJobEditDto CreateClassDepJob(ClassDepJobEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<ClassDepJob>();

            entity = _ClassDepJobRepository.Insert(entity);
            return entity.MapTo<ClassDepJobEditDto>();
        }

        /// <summary>
        /// 编辑班级和部门或岗位
        /// </summary>
        public void UpdateClassDepJob(ClassDepJobEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = _ClassDepJobRepository.Get(input.Id.Value);
            input.MapTo(entity);

            _ClassDepJobRepository.Update(entity);
        }

        /// <summary>
        /// 删除班级和部门或岗位
        /// </summary>
        public void DeleteClassDepJob(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            _ClassDepJobRepository.Delete(input.Id);
        }

        #endregion


    }
}
