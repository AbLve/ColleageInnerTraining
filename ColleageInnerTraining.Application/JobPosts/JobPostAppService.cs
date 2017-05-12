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
    /// 岗位服务实现
    /// </summary>
    public class JobPostAppService : ColleageInnerTrainingAppServiceBase, IJobPostAppService
    {
        private readonly IRepository<JobPost, long> _jobPostRepository;
        private readonly IRepository<UserAccount, long> userservice;

        private readonly JobPostManage _jobPostManage;
        /// <summary>
        /// 构造方法
        /// </summary>
        public JobPostAppService(IRepository<JobPost, long> jobPostRepository, JobPostManage jobPostManage, IRepository<UserAccount, long> _userservice)
        {
            _jobPostRepository = jobPostRepository;
            _jobPostManage = jobPostManage;
            userservice = _userservice;

        }

        #region 岗位管理

        /// <summary>
        /// 根据查询条件获取岗位分页列表
        /// </summary>
        public PagedResultDto<JobPostListDto> GetPagedJobPosts(GetJobPostInput input)
        {

            var query = from a in _jobPostRepository.GetAll()
                        join b in userservice.GetAll() on a.CreatorUserId equals b.Id into c
                        from d in c.DefaultIfEmpty()
                        select new JobPostListDto
                        {
                            Id = a.Id,
                            Name = a.Name,
                            CreationTime = a.CreationTime,
                            Enabled = a.Enabled,
                            Sort = a.Sort,
                            CreationName = d.DisplayName
                        };

            //TODO:根据传入的参数添加过滤条件
            query = query.WhereIf(!input.FilterText.IsNullOrWhiteSpace(), item => item.Name.Contains(input.FilterText));
            var jobPostCount = query.Count();

            var jobPosts = query
            .OrderByDescending(t=>t.CreationTime)
            .PageBy(input)
            .ToList();
            var jobPostListDtos = jobPosts.MapTo<List<JobPostListDto>>();
            return new PagedResultDto<JobPostListDto>(jobPostCount, jobPostListDtos);
        }

        /// <summary>
        /// 获取所有岗位
        /// </summary>
        /// <returns></returns>
        public List<JobPostListDto> GetAllJobPosts()
        {
            try
            {
                var query = _jobPostRepository.GetAll();
                //TODO:根据传入的参数添加过滤条件
                var posts = query.ToList();
                var jobPostListDto = posts.MapTo<List<JobPostListDto>>();
                return jobPostListDto;
            }
            catch (Exception e)
            {
                return new List<JobPostListDto>();
            }
        }

        /// <summary>
        /// 通过Id获取岗位信息进行编辑或修改 
        /// </summary>
        public async Task<GetJobPostForEditOutput> GetJobPostForEditAsync(NullableIdDto<long> input)
        {
            var output = new GetJobPostForEditOutput();

            JobPostEditDto jobPostEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _jobPostRepository.GetAsync(input.Id.Value);
                jobPostEditDto = entity.MapTo<JobPostEditDto>();
            }
            else
            {
                jobPostEditDto = new JobPostEditDto();
            }

            output.JobPost = jobPostEditDto;
            return output;
        }

        /// <summary>
        /// 通过指定id获取岗位ListDto信息
        /// </summary>
        public JobPostListDto GetJobPostById(EntityDto<long> input)
        {
            var entity = _jobPostRepository.Get(input.Id);

            return entity.MapTo<JobPostListDto>();
        }

        /// <summary>
        /// 新增或更改岗位
        /// </summary>
        public async Task CreateOrUpdateJobPostAsync(CreateOrUpdateJobPostInput input)
        {
            if (input.JobPostEditDto.Id.HasValue)
            {
                await UpdateJobPostAsync(input.JobPostEditDto);
            }
            else
            {
                await CreateJobPostAsync(input.JobPostEditDto);
            }
        }

        /// <summary>
        /// 新增岗位
        /// </summary>
        public virtual async Task<JobPostEditDto> CreateJobPostAsync(JobPostEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<JobPost>();

            entity = await _jobPostRepository.InsertAsync(entity);
            return entity.MapTo<JobPostEditDto>();
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        public List<JobPostEditDto> GetJobPostDataList()
        {
            var entity = _jobPostRepository.GetAll();
            return entity.MapTo<List<JobPostEditDto>>();
        }

        /// <summary>
        /// 编辑岗位
        /// </summary>
        public virtual async Task UpdateJobPostAsync(JobPostEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _jobPostRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            await _jobPostRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除岗位
        /// </summary>
        public async Task DeleteJobPostAsync(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _jobPostRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除岗位
        /// </summary>
        public async Task BatchDeleteJobPostAsync(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _jobPostRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion

    }
}
