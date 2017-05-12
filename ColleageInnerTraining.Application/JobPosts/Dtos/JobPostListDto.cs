using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 岗位列表Dto
    /// </summary>
    [AutoMapFrom(typeof(JobPost))]
    public class JobPostListDto : EntityDto<long>
    {
        /// <summary>
        /// 岗位名称
        /// </summary>
        [DisplayName("岗位名称")]
        public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [DisplayName("是否有效")]
        public bool Enabled { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreationName { get; set; }
    }
}
