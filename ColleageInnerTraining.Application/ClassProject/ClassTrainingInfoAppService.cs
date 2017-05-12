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

namespace ColleageInnerTraining.AbpZeroTemplate.Application
{
    /// <summary>
    /// 班级项目服务实现
    /// </summary>
    public class ClassTrainingInfoAppService : ColleageInnerTrainingAppServiceBase, IClassTrainingInfoAppService
    {
        private readonly IRepository<ClassTrainingInfo, long> _ClassTrainingInfoRepository;


        /// <summary>
        /// 构造方法
        /// </summary>
        public ClassTrainingInfoAppService(IRepository<ClassTrainingInfo, long> ClassTrainingInfoRepository)
        {
            _ClassTrainingInfoRepository = ClassTrainingInfoRepository;

        }

        #region 班级项目管理

        /// <summary>
        /// 获取全部
        /// </summary>
        public List<ClassTrainingInfoListDto> GetAll()
        {
            var query = _ClassTrainingInfoRepository.GetAll().ToList();
            if (query.Count < 1)
            {
                return new List<ClassTrainingInfoListDto>();
            }
            return query.MapTo<List<ClassTrainingInfoListDto>>();
        }

        /// <summary>
        /// 根据查询条件获取班级项目分页列表
        /// </summary>
        public PagedResultDto<ClassTrainingInfoListDto> GetPagedClassTrainingInfos(GetClassTrainingInfoInput input)
        {

            var query = _ClassTrainingInfoRepository.GetAll()
                       .WhereIf(input.CId > 0, item => item.ClassId == input.CId)
                       .WhereIf(input.TrainingType > 0, item => item.TrainingType == input.TrainingType)
                       .WhereIf(input.Id > 0, item => item.Id == input.Id)
                       .WhereIf(!string.IsNullOrEmpty(input.Name), item => item.Name == input.Name);
            //TODO:根据传入的参数添加过滤条件

            var ClassTrainingInfoCount = query.Count();

            var ClassTrainingInfos = query
            .OrderByDescending(t=>t.CreationTime)
            .PageBy(input)
            .ToList();
            var ClassTrainingInfoListDtos = ClassTrainingInfos.MapTo<List<ClassTrainingInfoListDto>>();
            return new PagedResultDto<ClassTrainingInfoListDto>(ClassTrainingInfoCount, ClassTrainingInfoListDtos);
        }
        /// <summary>
        /// 通过Id获取班级项目信息进行编辑或修改 
        /// </summary>
        public async Task<GetClassTrainingInfoForEditOutput> GetClassTrainingInfoForEditAsync(NullableIdDto<long> input)
        {
            var output = new GetClassTrainingInfoForEditOutput();

            ClassTrainingInfoEditDto EditDto;

            if (input.Id.HasValue)
            {
                var entity = await _ClassTrainingInfoRepository.GetAsync(input.Id.Value);
                EditDto = entity.MapTo<ClassTrainingInfoEditDto>();
            }
            else
            {
                EditDto = new ClassTrainingInfoEditDto();
            }

            output.ClassTrainingInfoEditDto = EditDto;
            return output;
        }
        /// <summary>
        /// 新增或更改班级项目
        /// </summary>
        public async Task CreateOrUpdateClassTrainingInfoAsync(CreateOrUpdateClassTrainingInfoInput input)
        {
            if (input.ClassTrainingInfoEditDto.Id.HasValue)
            {
                await UpdateClassTrainingInfoAsync(input.ClassTrainingInfoEditDto);
            }
            else
            {
                await CreateClassTrainingInfoAsync(input.ClassTrainingInfoEditDto);
            }
        }
        /// <summary>
        /// 新增班级项目
        /// </summary>
        public virtual async Task<ClassTrainingInfoEditDto> CreateClassTrainingInfoAsync(ClassTrainingInfoEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<ClassTrainingInfo>();

            entity = await _ClassTrainingInfoRepository.InsertAsync(entity);
            return entity.MapTo<ClassTrainingInfoEditDto>();
        }

        /// <summary>
        /// 编辑班级项目
        /// </summary>
        public virtual async Task UpdateClassTrainingInfoAsync(ClassTrainingInfoEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _ClassTrainingInfoRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            await _ClassTrainingInfoRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除班级项目
        /// </summary>
        public async Task DeleteClassTrainingInfoAsync(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _ClassTrainingInfoRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除班级项目
        /// </summary>
        public async Task BatchDeleteClassTrainingInfoAsync(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _ClassTrainingInfoRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion

    }
}
