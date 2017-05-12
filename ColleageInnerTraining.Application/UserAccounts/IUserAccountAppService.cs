using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 用户账号信息服务接口
    /// </summary>
    public interface IUserAccountAppService : IApplicationService
    {
        #region 用户账号信息管理

        /// <summary>
        /// 根据查询条件获取用户账号信息分页列表
        /// </summary>
        PagedResultDto<UserAccountListDto> GetPagedUserAccounts(GetUserAccountInput input);

        /// <summary>
        /// 根据查询条件获取用户账号信息分页列表
        /// </summary>
        IEnumerable<UserAccountListDto> GetPagedUserAccountsUD(GetUserAccountInput input);

        /// <summary>
        /// 通过Id获取用户账号信息信息进行编辑或修改 
        /// </summary>
        GetUserAccountForEditOutput GetUserAccountForEdit(NullableIdDto<long> input);

		  /// <summary>
        /// 通过指定id获取用户账号信息ListDto信息
        /// </summary>
		UserAccountListDto GetUserAccountById(EntityDto<long> input);

        /// <summary>
        /// 通过指定id获取用户账号信息ListDto信息
        /// </summary>
        UserAccountListDto GetUserAccountBySysNo(int sysNo);
        /// <summary>
        /// 获取所有信息
        /// </summary>
        List<UserAccount> GetAccountData();
        List<UserAccountListDto> GetAll();
        /// <summary>
        /// 获取班级成员信息
        /// </summary>
        IEnumerable<UserAccountListDto> GetAll(string keyword,int jid,int did,int isExit, IEnumerable<int> userIds);

        /// <summary>
        /// 新增或更改用户账号信息
        /// </summary>
        void CreateOrUpdateUserAccount(CreateOrUpdateUserAccountInput input);        

        /// <summary>
        /// 新增用户账号信息
        /// </summary>
        UserAccountEditDto CreateUserAccount(UserAccountEditDto input);

        /// <summary>
        /// 更新用户账号信息
        /// </summary>
        void UpdateUserAccount(UserAccountEditDto input);

        /// <summary>
        /// 更新用户账号信息
        /// </summary>
        void UpdateUserAccountByNo(UserAccountEditDto input);

        /// <summary>
        /// 删除用户账号信息
        /// </summary>
        void DeleteUserAccount(EntityDto<long> input);

        /// <summary>
        /// 批量删除用户账号信息
        /// </summary>
        void BatchDeleteUserAccount(List<long> input);

        #endregion




    }
}
