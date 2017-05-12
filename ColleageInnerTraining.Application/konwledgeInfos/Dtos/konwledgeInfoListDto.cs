using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 列表Dto
    /// </summary>
    [AutoMapFrom(typeof(KonwledgeInfo))]
    public class KonwledgeInfoListDto : EntityDto<long>
    {
        /// <summary>
        /// 标题
        /// </summary>
        [DisplayName("标题")]
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [DisplayName("内容")]
        public string Content { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 阅读量
        /// </summary>
        [DisplayName("阅读量")]
        public int ReadingCount { get; set; }
        /// <summary>
        /// 收藏量
        /// </summary>
        [DisplayName("收藏量")]
        public int CollectionCount { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int Stauts { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string AuditorName { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ReviewedDate { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        [DisplayName("标签名称")]
        public string TagetName { get; set; }
        /// <summary>
        /// 标签id
        /// </summary>
        [DisplayName("标签id")]
        public long? TagetId { get; set; }
        /// <summary>
        /// 分类id
        /// </summary>
        [DisplayName("分类id")]
        public long TypeId { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        [DisplayName("分类名称")]
        public string TypeName { get; set; }
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
        /// 创建时间
        /// </summary>
        [DisplayName("创建人")]
        public string CreatorUserName { get; set; }
    }
}
