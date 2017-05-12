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
using System;
using System.Globalization;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 互动社区服务实现
    /// </summary>
    public class CommunityInteractionAppService : ColleageInnerTrainingAppServiceBase, ICommunityInteractionAppService
    {
        private readonly IRepository<CommunityInteraction, long> _communityInteractionRepository;
        private readonly CommunityInteractionManage _communityInteractionManage;

        private readonly IRepository<UserAccount, long> _userAccountRepository;
        private readonly UserAccountManage _userAccountManage;

        /// <summary>
        /// 构造方法
        /// </summary>
        public CommunityInteractionAppService(IRepository<CommunityInteraction, long> communityInteractionRepository,
        CommunityInteractionManage communityInteractionManage, IRepository<UserAccount, long> userAccountRepository,
        UserAccountManage userAccountManage)
        {
            _communityInteractionRepository = communityInteractionRepository;
            _communityInteractionManage = communityInteractionManage;
            _userAccountRepository = userAccountRepository;
            _userAccountManage = userAccountManage;
        }

        #region 互动社区管理

        /// <summary>
        /// 根据查询条件获取互动社区分页列表
        /// </summary>
        public PagedResultDto<CommunityInteractionListDto> GetPagedCommunityInteractions(GetCommunityInteractionInput input)
        {
            var query = from comm in _communityInteractionRepository.GetAll()
                join user in _userAccountRepository.GetAll() on comm.UserId equals user.SysNO
                 into cu from cui in cu.DefaultIfEmpty() 
                select  new CommunityInteractionListDto()
                {
                    Id = comm.Id,
                    UserId = comm.UserId,
                    UserName = cui.DisplayName,
                    ParentUserId = comm.ParentUserId,
                    Content = comm.Content,
                    CountUser = comm.CountUser,
                    PublicationTime = comm.PublicationTime,
                    CreationTime = comm.CreationTime
                };
            //TODO:根据传入的参数添加过滤条件         
            query = query.Where(c => c.ParentUserId == input.IdType);
            if (!string.IsNullOrEmpty(input.UserName))
            {
                query = query.Where(c => c.UserName.Contains(input.UserName));
            }
            if (!string.IsNullOrEmpty(input.Quest))
            {
                query = query.Where(c => c.Content.Contains(input.Quest));
            }
            if (input.BTIme != null)
            {
                query = query.Where(c => c.PublicationTime >= input.BTIme);
            }
            if (input.ETime != null)
            {
                query = query.Where(c => c.PublicationTime <= input.ETime);
            }

            var communityInteractionCount = query.Count();
            var communityInteractions = query
            .OrderByDescending(t => t.CreationTime)
            .PageBy(input)
            .ToList();

            var communityInteractionListDtos = communityInteractions.MapTo<List<CommunityInteractionListDto>>();
            return new PagedResultDto<CommunityInteractionListDto>(
            communityInteractionCount,
            communityInteractionListDtos
            );
        }

        /// <summary>
        /// 通过Id获取互动社区信息进行编辑或修改 
        /// </summary>
        public GetCommunityInteractionForEditOutput GetCommunityInteractionForEdit(NullableIdDto<long> input)
        {
            var output = new GetCommunityInteractionForEditOutput();

            CommunityInteractionEditDto communityInteractionEditDto;

            if (input.Id.HasValue)
            {
                var entity = _communityInteractionRepository.Get(input.Id.Value);
                communityInteractionEditDto = entity.MapTo<CommunityInteractionEditDto>();
            }
            else
            {
                communityInteractionEditDto = new CommunityInteractionEditDto();
            }

            output.CommunityInteraction = communityInteractionEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取互动社区ListDto信息
        /// </summary>
        public CommunityInteractionListDto GetCommunityInteractionById(EntityDto<long> input)
        {
            var entity = _communityInteractionRepository.Get(input.Id);

            return entity.MapTo<CommunityInteractionListDto>();
        }







        /// <summary>
        /// 新增或更改互动社区
        /// </summary>
        public void CreateOrUpdateCommunityInteraction(CreateOrUpdateCommunityInteractionInput input)
        {
            if (input.CommunityInteractionEditDto.Id.HasValue)
            {
                UpdateCommunityInteraction(input.CommunityInteractionEditDto);
            }
            else
            {
                CreateCommunityInteraction(input.CommunityInteractionEditDto);
            }
        }

        /// <summary>
        /// 新增互动社区
        /// </summary>
        public CommunityInteractionEditDto CreateCommunityInteraction(CommunityInteractionEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<CommunityInteraction>();

            entity = _communityInteractionRepository.Insert(entity);
            return entity.MapTo<CommunityInteractionEditDto>();
        }

        /// <summary>
        /// 编辑互动社区
        /// </summary>
        public void UpdateCommunityInteraction(CommunityInteractionEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = _communityInteractionRepository.Get(input.Id.Value);
            input.MapTo(entity);

            _communityInteractionRepository.Update(entity);
        }

        /// <summary>
        /// 删除互动社区
        /// </summary>
        public void DeleteCommunityInteraction(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            _communityInteractionRepository.Delete(input.Id);
        }

        /// <summary>
        /// 批量删除互动社区
        /// </summary>
        public void BatchDeleteCommunityInteraction(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            _communityInteractionRepository.Delete(s => input.Contains(s.Id));
        }

        #endregion












    }
}
