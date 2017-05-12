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
    /// JobPost编辑用Dto
    /// </summary>
    [AutoMap(typeof(JobPost))]
    public class JobPostEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public long? Id { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        [DisplayName("岗位名称")]
        public string Name { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [DisplayName("简介")]
        public string Description { get; set; }

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

    }
}
