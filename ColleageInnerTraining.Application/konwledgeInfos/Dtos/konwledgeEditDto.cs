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
    /// 编辑用Dto
    /// </summary>
    [AutoMap(typeof(KonwledgeInfo))]
    public class KonwledgeInfoEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        public long? Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 阅读量
        /// </summary>
        public int ReadingCount { get; set; }
        /// <summary>
        /// 收藏量
        /// </summary>
        public int CollectionCount { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int Stauts { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public long? Auditor { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ReviewedDate { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public long TagetId { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public long TypeId { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Enabled { get; set; }

    }
}
