using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;
using System.ComponentModel.DataAnnotations;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    ///公告和部门或岗位列表Dto
    /// </summary>
    [AutoMapFrom(typeof(KnowledgeDepJob))]
    public class KnowledgeDepJobListDto : EntityDto<long>
    {
        /// <summary>
        /// 知识点Id
        /// </summary>
        public int KnoledgeId { get; set; }

        /// <summary>
        /// 知识点标题
        /// </summary>
        public string KnoledgeTitle { get; set; }

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
