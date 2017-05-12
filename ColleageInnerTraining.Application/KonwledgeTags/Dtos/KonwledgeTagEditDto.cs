using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Abp.Extensions;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 班级编辑用Dto
    /// </summary>
    [AutoMap(typeof(KonwledgeTag))]
    public class KonwledgeTagEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        public long? Id { get; set; }
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
