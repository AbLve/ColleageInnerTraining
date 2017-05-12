
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 阅读人数服务接口
    /// </summary>
    public interface IReadTimesAppService : IApplicationService
    {
        #region 阅读人数管理

        /// <summary>
        /// 查询用户是否已阅读人数对像
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="bizId">收藏对像Id</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        ReadTimesListDto GetReadTimesByTypeAndId(string type, int bizId, int userId);
 
        /// <summary>
        /// 新增阅读人数
        /// </summary>
        ReadTimesEditDto CreateReadTimes(ReadTimesEditDto input);
         
        #endregion




    }
}
