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
    /// 公告服务实现
    /// </summary>
    public class NoticeAppService : ColleageInnerTrainingAppServiceBase, INoticeAppService
    {
        private readonly IRepository<Notice, long> noticeservice;


        private readonly IRepository<UserAccount, long> userservice;
        /// <summary>
        /// 构造方法
        /// </summary>
        public NoticeAppService(IRepository<Notice, long> _noticeservice, IRepository<UserAccount, long> _userservice)
        {
            noticeservice = _noticeservice;
            userservice = _userservice;

        }

        #region 公告管理

        /// <summary>
        /// 根据查询条件获取公告分页列表
        /// </summary>
        public PagedResultDto<NoticeListDto> GetPagedNotices(GetNoticeInput input)
        {

            var query = from a in noticeservice.GetAll()
                        join b in userservice.GetAll() on a.CreatorUserId equals b.Id into c
                        from d in c.DefaultIfEmpty()
                        select new NoticeListDto
                        {
                            Id = a.Id,
                            Title = a.Title,
                            Content = a.Content,
                            CreatorUserName = d.DisplayName,
                            Enabled = a.Enabled,
                            StartTime = a.StartTime,
                            EndTime = a.EndTime,
                            CreationTime = a.CreationTime
                        };
            query = query.WhereIf(!string.IsNullOrEmpty(input.FilterText), t => t.CreatorUserName.Contains(input.FilterText) || t.Title.Contains(input.FilterText));

            //TODO:根据传入的参数添加过滤条件
            var Notices = query.OrderByDescending(t => t.CreationTime).PageBy(input).ToList();
            var NoticeListDto = Notices.MapTo<List<NoticeListDto>>();
            return new PagedResultDto<NoticeListDto>(query.Count(), NoticeListDto);
        }

        /// <summary>
        /// 获取所有公告
        /// </summary>
        /// <returns></returns>
        public List<NoticeListDto> GetAllNotices()
        {
            try
            {
                var query = noticeservice.GetAll();
                //TODO:根据传入的参数添加过滤条件
                var Notices = query.ToList();
                var NoticeListDto = Notices.MapTo<List<NoticeListDto>>();
                return NoticeListDto;
            }
            catch (Exception e)
            {
                return new List<NoticeListDto>();
            }

        }

        /// <summary>
        /// 通过Id获取公告信息进行编辑或修改 
        /// </summary>
        public GetNoticeForEditOutput GetNoticeForEdit(NullableIdDto<long> input)
        {
            var output = new GetNoticeForEditOutput();

            NoticeEditDto NoticeEditDto;

            if (input.Id.HasValue)
            {
                var entity = noticeservice.Get(input.Id.Value);
                NoticeEditDto = entity.MapTo<NoticeEditDto>();
            }
            else
            {
                NoticeEditDto = new NoticeEditDto();
            }

            output.Notice = NoticeEditDto;
            return output;
        }

        /// <summary>
        /// 通过指定id获取公告ListDto信息
        /// </summary>
        public NoticeListDto GetNoticeById(EntityDto<long> input)
        {
            var entity = noticeservice.Get(input.Id);

            return entity.MapTo<NoticeListDto>();
        }
        /// <summary>
        /// 获取所有公告
        /// </summary>
        public List<Notice> GetNoticeAll()
        {
            var entity = noticeservice.GetAll().ToList();

            return entity;
        }

        /// <summary>
        /// 新增或更改公告
        /// </summary>
        public async Task CreateOrUpdateNoticeAsync(CreateOrUpdateNoticeInput input)
        {
            if (input.NoticeEditDto.Id.HasValue)
            {
                await UpdateNoticeAsync(input.NoticeEditDto);
            }
            else
            {
                await CreateNoticeAsync(input.NoticeEditDto);
            }
        }

        /// <summary>
        /// 新增公告
        /// </summary>
        public virtual async Task<NoticeEditDto> CreateNoticeAsync(NoticeEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<Notice>();

            entity = await noticeservice.InsertAsync(entity);
            return entity.MapTo<NoticeEditDto>();
        }

        /// <summary>
        /// 编辑公告
        /// </summary>
        public virtual async Task UpdateNoticeAsync(NoticeEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await noticeservice.GetAsync(input.Id.Value);
            input.MapTo(entity);

            await noticeservice.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        public async Task DeleteNoticeAsync(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await noticeservice.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除公告
        /// </summary>
        public async Task BatchDeleteNoticeAsync(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await noticeservice.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion


    }
}
