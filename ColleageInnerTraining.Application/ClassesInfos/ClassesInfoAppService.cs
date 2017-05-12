using System;
using System.Collections.Generic;
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
using ColleageInnerTraining.Core;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Application;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 班级服务实现
    /// </summary>
    public class ClassesInfoAppService : ColleageInnerTrainingAppServiceBase, IClassesInfoAppService
    {
        private readonly IRepository<ClassesInfo, long> _classesInfoRepository;


        private readonly IRepository<ClassUser, long> _classUserRepository;
        /// <summary>
        /// 构造方法
        /// </summary>
        public ClassesInfoAppService(IRepository<ClassesInfo, long> classesInfoRepository, IRepository<ClassUser, long> classUserRepository)
        {
            _classesInfoRepository = classesInfoRepository;
            _classUserRepository = classUserRepository;

        }

        #region 班级管理

        /// <summary>
        /// 根据查询条件获取班级分页列表
        /// </summary>
        public PagedResultDto<ClassesInfoListDto> GetPagedClassesInfos(GetClassesInfoInput input)
        {

            var query = _classesInfoRepository.GetAll().
                        WhereIf(!string.IsNullOrEmpty(input.FilterText), t => t.ClassesName.Contains(input.FilterText));
        
            //TODO:根据传入的参数添加过滤条件
            var classesInfos = query.OrderByDescending(t => t.CreationTime).PageBy(input).ToList();
            var classesInfoListDto = classesInfos.MapTo<List<ClassesInfoListDto>>();
            return new PagedResultDto<ClassesInfoListDto>(query.Count(), classesInfoListDto);
        }

        /// <summary>
        /// 获取所有班级
        /// </summary>
        /// <returns></returns>
        public List<ClassesInfoListDto> GetAllClassesInfos()
        {
            try
            {
                var query = _classesInfoRepository.GetAll();
                //TODO:根据传入的参数添加过滤条件
                var classesInfos = query.ToList();
                var classesInfoListDto = classesInfos.MapTo<List<ClassesInfoListDto>>();
                return classesInfoListDto;
            }
            catch (Exception e)
            {
                return new List<ClassesInfoListDto>();
            }

        }

        /// <summary>
        /// 通过Id获取班级信息进行编辑或修改 
        /// </summary>
        public GetClassesInfoForEditOutput GetClassesInfoForEdit(NullableIdDto<long> input)
        {
            var output = new GetClassesInfoForEditOutput();

            ClassesInfoEditDto classesInfoEditDto;

            if (input.Id.HasValue)
            {
                var entity =  _classesInfoRepository.Get(input.Id.Value);
                classesInfoEditDto = entity.MapTo<ClassesInfoEditDto>();
            }
            else
            {
                classesInfoEditDto = new ClassesInfoEditDto();
            }

            output.ClassesInfo = classesInfoEditDto;
            return output;
        }

        /// <summary>
        /// 通过指定id获取班级ListDto信息
        /// </summary>
        public ClassesInfoListDto GetClassesInfoById(EntityDto<long> input)
        {
            var entity = _classesInfoRepository.Get(input.Id);

            return entity.MapTo<ClassesInfoListDto>();
        }

        /// <summary>
        /// 通过指定id获取班级ListDto信息
        /// </summary>
        public ClassesInfoEditDto GetClassesInfoEditById(EntityDto<long> input)
        {
            var entity = _classesInfoRepository.Get(input.Id);

            return entity.MapTo<ClassesInfoEditDto>();
        }
        /// <summary>
        /// 获取所有班级
        /// </summary>
        public List<ClassesInfo> GetClassesInfoAll()
        {
            var entity = _classesInfoRepository.GetAll().ToList();

            return entity;
        }

        /// <summary>
        /// 新增或更改班级
        /// </summary>
        public async Task CreateOrUpdateClassesInfoAsync(CreateOrUpdateClassesInfoInput input)
        {
            if (input.ClassesInfoEditDto.Id.HasValue)
            {
                await UpdateClassesInfoAsync(input.ClassesInfoEditDto);
            }
            else
            {
                await CreateClassesInfoAsync(input.ClassesInfoEditDto);
            }
        }

        /// <summary>
        /// 新增班级
        /// </summary>
        public virtual async Task<ClassesInfoEditDto> CreateClassesInfoAsync(ClassesInfoEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<ClassesInfo>();

            entity = await _classesInfoRepository.InsertAsync(entity);
            return entity.MapTo<ClassesInfoEditDto>();
        }

        /// <summary>
        /// 编辑班级
        /// </summary>
        public virtual async Task UpdateClassesInfoAsync(ClassesInfoEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _classesInfoRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            await _classesInfoRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除班级
        /// </summary>
        public async Task DeleteClassesInfoAsync(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _classesInfoRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除班级
        /// </summary>
        public async Task BatchDeleteClassesInfoAsync(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _classesInfoRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion

    }
}
