using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 标签列表Dto
    /// </summary>
    [AutoMapFrom(typeof(KonwledgeTag))]
    public class KonwledgeTagListDto : EntityDto<long>
    {
        /// <summary>
        /// 知识库id
        /// </summary>
        public int knowledgeId { get; set; }
        /// <summary>
        /// 标签id
        /// </summary>
        public int TagId { get; set; }
    }
}
