using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    ///公告和部门或岗位列表Dto
    /// </summary>
    [AutoMapFrom(typeof(NoticeDepJob))]
    public class NoticeDepJobListDto : EntityDto<long>
    {
        /// <summary>
        ///公告Id
        /// </summary>
        public int NoticeId { get; set; }
        /// <summary>
        ///公告标题
        /// </summary>
        public string NoticeTitle { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 业务Id
        /// </summary>
        public int BusinessId { get; set; }
        /// <summary>
        /// 业务名称
        /// </summary>
        public string BusinessName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
