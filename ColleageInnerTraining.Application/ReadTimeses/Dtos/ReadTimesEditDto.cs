using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Abp.Extensions;
using ColleageInnerTraining.Application;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 阅读人数编辑用Dto
    /// </summary>
    [AutoMap(typeof(ReadTimes))]
    public class ReadTimesEditDto 
    {

	/// <summary>
    ///   主键Id
    /// </summary>
    [DisplayName("主键Id")]
	public long? Id{get;set;}

        /// <summary>
        /// 阅读人数对像名称
        /// </summary>
        [DisplayName("阅读人数对像名称")]
        [Required]
        public   string  BizName { get; set; }

        /// <summary>
        /// 阅读人数对像类型
        /// </summary>
        [DisplayName("阅读人数对像类型")]
        [Required]
        public   string  BizType { get; set; }

        /// <summary>
        /// 阅读人数对像Id
        /// </summary>
        [DisplayName("阅读人数对像Id")]
        [Required]
        public   int  BizId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [DisplayName("用户Id")]
        [Required]
        public   int  UserId { get; set; }

    }
}
