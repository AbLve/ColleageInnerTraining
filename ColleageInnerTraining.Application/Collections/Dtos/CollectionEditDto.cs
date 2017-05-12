using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Abp.Extensions;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 收藏编辑用Dto
    /// </summary>
    [AutoMap(typeof(Collection))]
    public class CollectionEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public long? Id { get; set; }

        /// <summary>
        /// 收藏对像名称
        /// </summary>
        [DisplayName("收藏对像名称")]
        [Required]
        [MaxLength(255)]
        public string BizName { get; set; }

        /// <summary>
        /// 收藏对像类型
        /// </summary>
        [DisplayName("收藏对像类型")]
        [Required]
        [MaxLength(10)]
        public string BizType { get; set; }

        /// <summary>
        /// 收藏对像Id
        /// </summary>
        [DisplayName("收藏对像Id")]
        [Required]
        public int BizId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [DisplayName("用户Id")]
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [DisplayName("创建人")]
        [Required]
        public int CreatorUserId { get; set; }



    }
}
