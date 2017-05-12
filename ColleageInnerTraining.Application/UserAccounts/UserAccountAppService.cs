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
using Castle.Core.Internal;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 用户账号信息服务实现
    /// </summary>
    public class UserAccountAppService : ColleageInnerTrainingAppServiceBase, IUserAccountAppService
    {
        private readonly IRepository<UserAccount, long> _userAccountRepository;
        private readonly UserAccountManage _userAccountManage;

        private readonly IRepository<DepartmentInfo, long> _departmentRepository;
        private readonly DepartmentInfoManage _departmentInfoManage;

        private readonly IRepository<JobPost, long> _jobPostRepository;
        private readonly JobPostManage _jobPostManage;
        /// <summary>
        /// 构造方法
        /// </summary>
        public UserAccountAppService(IRepository<UserAccount, long> userAccountRepository, UserAccountManage userAccountManage,
        IRepository<DepartmentInfo, long> departmentRepository, DepartmentInfoManage departmentInfoManage,
        IRepository<JobPost, long> jobPostRepository, JobPostManage jobPostManage)
        {
            _userAccountRepository = userAccountRepository;
            _userAccountManage = userAccountManage;
            _departmentRepository = departmentRepository;
            _departmentInfoManage = departmentInfoManage;
            _jobPostRepository = jobPostRepository;
            _jobPostManage = jobPostManage;
        }

        #region 用户账号信息管理

        /// <summary>
        /// 根据查询条件获取用户账号信息分页列表
        /// </summary>
        public PagedResultDto<UserAccountListDto> GetPagedUserAccounts(GetUserAccountInput input)
        {

            var query = from user in _userAccountRepository.GetAll()
                        join de in _departmentRepository.GetAll() on user.DepartmentID equals de.DepartmentId into gc
                        from gci in gc.DefaultIfEmpty()
                        join job in _jobPostRepository.GetAll() on user.PostID equals job.Id into jc
                        from jci in jc.DefaultIfEmpty()
                        select new UserAccountListDto
                        {
                            Id = user.Id,
                            SysNO = user.SysNO,
                            LoginName = user.LoginName,
                            DisplayName = user.DisplayName,
                            DepartmentID = user.DepartmentID,
                            DepartmentName = gci.DisplayName,
                            province = user.province,
                            ProvinceID = user.ProvinceID,
                            City = user.City,
                            CityID = user.CityID,
                            Area = user.Area,
                            AreaID = user.AreaID,
                            DetailedAddress = user.DetailedAddress,
                            PostID = user.PostID,
                            PostName = jci.Name,
                            PhoneNumber = user.PhoneNumber,
                            Email = user.Email,
                            Status = user.Status,
                            CreationTime = user.CreationTime
                        };
            IQueryable<UserAccountListDto> userAccountList = query;
            //TODO:根据传入的参数添加过滤条件
            if (!string.IsNullOrEmpty(input.Username))
            {
                userAccountList = userAccountList.Where(c => c.DisplayName.Contains(input.Username));
            }
            if (input.DepartmentId > 0)
            {
                userAccountList = userAccountList.Where(c => c.DepartmentID == input.DepartmentId);
            }
            if (input.Isbound != 0)
            {
                if (input.Isbound == 1)
                {
                    userAccountList = userAccountList.Where(c => input.UserIds.Contains((int)c.SysNO));
                    userAccountList.ToList().ForEach(t => t.IsExitCourse = true);
                }
                else if (input.Isbound == 2)
                {
                    userAccountList = userAccountList.Where(c => !input.UserIds.Contains((int)c.SysNO));
                    userAccountList.ToList().ForEach(t => t.IsExitCourse = false);
                }
            }
            if (input.jobId > 0)
            {
                userAccountList = userAccountList.Where(c => c.PostID == input.jobId);
            }

            var userAccountCount = userAccountList.Count();

            var userAccounts = userAccountList
           .OrderByDescending(t => t.CreationTime)
            .PageBy(input)
            .ToList();

            var userAccountListDtos = userAccounts.MapTo<List<UserAccountListDto>>();

            return new PagedResultDto<UserAccountListDto>(
            userAccountCount,
            userAccountListDtos
            );
        }

        /// <summary>
        /// 根据查询条件获取用户账号信息分页列表
        /// </summary>
        public IEnumerable<UserAccountListDto> GetPagedUserAccountsUD(GetUserAccountInput input)
        {

            var query = from user in _userAccountRepository.GetAll()
                        join de in _departmentRepository.GetAll() on user.DepartmentID equals de.DepartmentId into gc
                        from gci in gc.DefaultIfEmpty()
                        join job in _jobPostRepository.GetAll() on user.PostID equals job.Id into jc
                        from jci in jc.DefaultIfEmpty()
                        select new UserAccountListDto
                        {
                            Id = user.Id,
                            SysNO = user.SysNO,
                            LoginName = user.LoginName,
                            DisplayName = user.DisplayName,
                            DepartmentID = user.DepartmentID,
                            DepartmentName = gci.DisplayName,
                            province = user.province,
                            ProvinceID = user.ProvinceID,
                            City = user.City,
                            CityID = user.CityID,
                            Area = user.Area,
                            AreaID = user.AreaID,
                            DetailedAddress = user.DetailedAddress,
                            PostID = user.PostID,
                            PostName = jci.Name,
                            PhoneNumber = user.PhoneNumber,
                            Email = user.Email,
                            Status = user.Status,
                            CreationTime = user.CreationTime,
                            Path = gci.Path
                        };
            IEnumerable<UserAccountListDto> userAccountList = query.MapTo<List<UserAccountListDto>>();
            //TODO:根据传入的参数添加过滤条件
            if (!string.IsNullOrEmpty(input.Username))
            {
                userAccountList = userAccountList.Where(c => c.DisplayName.Contains(input.Username));
            }
            if (input.DepartmentId > 0)
            {
                var depaertmentDate = _departmentRepository.GetAll().FirstOrDefault(c => c.DepartmentId == input.DepartmentId);
                if (depaertmentDate != null)
                {
                    userAccountList = userAccountList.Where(c => c.Path.StartsWith(depaertmentDate.Path));
                }               
            }
            if (input.Isbound != 0)
            {
                if (input.Isbound == 1)
                {
                    userAccountList = userAccountList.Where(c => input.UserIds.Contains((int)c.SysNO));
                    userAccountList.ToList().ForEach(t => t.IsExitCourse = true);
                }
                else if (input.Isbound == 2)
                {
                    userAccountList = userAccountList.Where(c => !input.UserIds.Contains((int)c.SysNO));
                    userAccountList.ToList().ForEach(t => t.IsExitCourse = false);
                }
            }
            if (input.jobId > 0)
            {
                userAccountList = userAccountList.Where(c => c.PostID == input.jobId);
            }

            return userAccountList;
        }

        /// <summary>
        /// 通过Id获取用户账号信息信息进行编辑或修改 
        /// </summary>
        public GetUserAccountForEditOutput GetUserAccountForEdit(NullableIdDto<long> input)
        {
            var output = new GetUserAccountForEditOutput();

            UserAccountEditDto userAccountEditDto;

            if (input.Id.HasValue)
            {
                var entity = _userAccountRepository.Get(input.Id.Value);
                userAccountEditDto = entity.MapTo<UserAccountEditDto>();
            }
            else
            {
                userAccountEditDto = new UserAccountEditDto();
            }

            output.UserAccount = userAccountEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取用户账号信息ListDto信息
        /// </summary>
        public UserAccountListDto GetUserAccountById(EntityDto<long> input)
        {
            var entity = _userAccountRepository.Get(input.Id);

            return entity.MapTo<UserAccountListDto>();
        }


        /// <summary>
        /// 通过指定id获取用户账号信息ListDto信息
        /// </summary>
        public UserAccountListDto GetUserAccountBySysNo(int sysNo)
        {
            try
            {
                var query = from user in _userAccountRepository.GetAll().Where(p => p.SysNO == sysNo)
                            join de in _departmentRepository.GetAll() on user.DepartmentID equals de.DepartmentId into gc
                            from gci in gc.DefaultIfEmpty()
                            join job in _jobPostRepository.GetAll() on user.PostID equals job.Id into jc
                            from jci in jc.DefaultIfEmpty()
                            select new UserAccountListDto
                            {
                                Id = user.Id,
                                SysNO = user.SysNO,
                                LoginName = user.LoginName,
                                DisplayName = user.DisplayName,
                                DepartmentID = user.DepartmentID,
                                DepartmentName = gci.DisplayName,
                                province = user.province,
                                ProvinceID = user.ProvinceID,
                                City = user.City,
                                CityID = user.CityID,
                                Area = user.Area,
                                AreaID = user.AreaID,
                                DetailedAddress = user.DetailedAddress,
                                PostID = user.PostID,
                                PostName = jci.Name,
                                PhoneNumber = user.PhoneNumber,
                                Email = user.Email,
                                Status = user.Status
                            };

                var userAccountListDtos = query.ToList();


                if (userAccountListDtos != null)
                    return userAccountListDtos[0];
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取所有信息
        /// </summary>
        public List<UserAccount> GetAccountData()
        {
            var queryList = _userAccountRepository.GetAll().ToList();
            return queryList;
        }
        /// <summary>
        /// 获取所有信息
        /// </summary>
        public List<UserAccountListDto> GetAll()
        {
            var list = _userAccountRepository.GetAll();
            return list.MapTo<List<UserAccountListDto>>();

        }
        /// <summary>
        /// 获取班级成员信息
        /// </summary>
        public IEnumerable<UserAccountListDto> GetAll(string keyword, int jId, int dId, int isExit, IEnumerable<int> userIds)
        {
            var dto = _departmentRepository.GetAll().FirstOrDefault(t => t.DepartmentId == dId);
            var list = from a in _userAccountRepository.GetAll().WhereIf(jId > 0, t => t.PostID == jId).WhereIf(!string.IsNullOrEmpty(keyword), t => t.DisplayName.Contains(keyword))
                       join b in _departmentRepository.GetAll() on a.DepartmentID equals b.DepartmentId into c
                       from d in c.DefaultIfEmpty()
                       join e in _jobPostRepository.GetAll() on a.PostID equals e.Id into f
                       from g in f.DefaultIfEmpty()
                       select new UserAccountListDto
                       {
                           DepartmentID = a.DepartmentID,
                           DepartmentName = d.DisplayName,
                           Id = a.Id,
                           DisplayName = a.DisplayName,
                           PostID = a.PostID,
                           PostName = g.Name,
                           SysNO = a.SysNO,
                           Status = a.Status,
                           Path = d.Path,
                       };
            list = list.WhereIf(dto != null, t => t.Path.Contains(dto.Path));
            IEnumerable<UserAccountListDto> List = list.MapTo<List<UserAccountListDto>>();

            if (isExit > 0)
            {
                if (isExit == 1)
                {
                    List = List.Where(c => userIds.Contains(c.SysNO));
                    List.ForEach(t => t.IsExitClass = true);
                    ;
                }
                else
                {
                    List = List.Where(c => !userIds.Contains(c.SysNO));
                    List.ForEach(t => t.IsExitClass = false);
                }
            }
            return List;

        }

        /// <summary>
        /// 新增或更改用户账号信息
        /// </summary>
        public void CreateOrUpdateUserAccount(CreateOrUpdateUserAccountInput input)
        {
            if (input.UserAccountEditDto.Id.HasValue)
            {
                UpdateUserAccount(input.UserAccountEditDto);
            }
            else
            {
                CreateUserAccount(input.UserAccountEditDto);
            }
        }

        /// <summary>
        /// 新增用户账号信息
        /// </summary>
        public virtual UserAccountEditDto CreateUserAccount(UserAccountEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<UserAccount>();

            entity = _userAccountRepository.Insert(entity);
            return entity.MapTo<UserAccountEditDto>();
        }

        /// <summary>
        /// 编辑用户账号信息
        /// </summary>
        public virtual void UpdateUserAccount(UserAccountEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = _userAccountRepository.Get(input.Id.Value);
            input.MapTo(entity);

            _userAccountRepository.Update(entity);
        }

        public void UpdateUserAccountByNo(UserAccountEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新            
            var entity = _userAccountRepository.FirstOrDefault(c => c.SysNO == input.SysNO);
            var id = entity.Id;
            input.MapTo(entity);
            entity.Id = id;
            _userAccountRepository.Update(entity);
        }

        /// <summary>
        /// 删除用户账号信息
        /// </summary>
        public void DeleteUserAccount(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            _userAccountRepository.Delete(input.Id);
        }

        /// <summary>
        /// 批量删除用户账号信息
        /// </summary>
        public void BatchDeleteUserAccount(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            _userAccountRepository.Delete(s => input.Contains(s.Id));
        }

        #endregion












    }
}
