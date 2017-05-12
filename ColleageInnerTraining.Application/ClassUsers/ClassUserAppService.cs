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
    /// 班级成员服务实现
    /// </summary>
    public class ClassUserAppService : ColleageInnerTrainingAppServiceBase, IClassUserAppService
    {
        private readonly IRepository<ClassUser, long> _ClassUserRepository;
        private IClassesInfoAppService classesInfoAppService;

        /// <summary>
        /// 构造方法
        /// </summary>
        public ClassUserAppService(IRepository<ClassUser, long> ClassUserRepository, IClassesInfoAppService _classesInfoAppService)
        {
            _ClassUserRepository = ClassUserRepository;
            classesInfoAppService = _classesInfoAppService;

        }

        #region 班级成员管理

        /// <summary>
        /// 根据查询条件获取班级成员分页列表
        /// </summary>
        public PagedResultDto<ClassUserListDto> GetPagedClassUsers(GetClassUserInput input)
        {

            var query = _ClassUserRepository.GetAll()
                       .WhereIf(input.CId != 0, item => item.ClassId == input.CId);
            //TODO:根据传入的参数添加过滤条件

            var ClassUserCount = query.Count();

            var ClassUsers = query
            .OrderByDescending(t=>t.CreationTime)
            .PageBy(input)
            .ToList();
            var ClassUserListDtos = ClassUsers.MapTo<List<ClassUserListDto>>();
            return new PagedResultDto<ClassUserListDto>(ClassUserCount, ClassUserListDtos);
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        public List<ClassUserListDto> GetAll() {
            try
            {
                var list = _ClassUserRepository.GetAll().ToList();
                if (list.Count > 0)
                    return list.MapTo<List<ClassUserListDto>>();
                else
                    return new List<ClassUserListDto>();
            }
            catch
            {
                return new List<ClassUserListDto>();
            }
        }

        /// <summary>
        /// 新增或更改班级成员
        /// </summary>
        public void CreateOrUpdateClassUserAsync(CreateOrUpdateClassUserInput input)
        {
            if (input.ClassUserEditDto.Id.HasValue)
            {
                UpdateClassUserAsync(input.ClassUserEditDto);
            }
            else
            {
                CreateClassUser(input.ClassUserEditDto);
            }
        }
        /// <summary>
        /// 新增班级成员
        /// </summary>
        public ClassUserEditDto CreateClassUser(ClassUserEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<ClassUser>();

            entity.Id =  _ClassUserRepository.InsertAndGetId(entity);
            SetMemberCount(input.ClassId);
            return entity.MapTo<ClassUserEditDto>();
        }

        private void SetMemberCount(int classId)
        {
            ClassesInfoEditDto classInfo = new ClassesInfoEditDto();
            classInfo = classesInfoAppService.GetClassesInfoEditById(new EntityDto<long>() { Id = classId });
            classInfo.MemberCount = CountMembers(classId);
            classesInfoAppService.UpdateClassesInfoAsync(classInfo);

        }

        private int CountMembers(int classId)
        {
            var query = _ClassUserRepository.GetAll().Where(t => t.ClassId == classId).GroupBy(t => t.UserId);
            //TODO:根据传入的参数添加过滤条件
            var collectionCount = query.Count();
            return collectionCount;
        }


        /// <summary>
        /// 编辑班级成员
        /// </summary>
        public void UpdateClassUserAsync(ClassUserEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = _ClassUserRepository.Get(input.Id.Value);
            input.MapTo(entity);

            _ClassUserRepository.Update(entity);
        }

        /// <summary>
        /// 删除班级成员
        /// </summary>
        public void DeleteClassUserAsync(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            _ClassUserRepository.Delete(input.Id);
        }

        /// <summary>
        /// 批量删除班级成员
        /// </summary>
        public async Task BatchDeleteClassUserAsync(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _ClassUserRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion

    }
}
