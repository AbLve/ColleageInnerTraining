using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 知识库信息
    /// </summary>
    [Table("px_knowledge_info")]
    public class KonwledgeInfo : FullAuditedEntity<long>
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Column("title")]
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Column("content")]
        public string Content { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        [Column("image_url")]
        public string ImageUrl { get; set; }
        /// <summary>
        /// 阅读量
        /// </summary>
        [Column("reading_count")]
        public int ReadingCount { get; set; }
        /// <summary>
        /// 收藏量
        /// </summary>
        [Column("collection_count")]
        public int CollectionCount { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        [Column("stauts")]
        public int Stauts { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        [Column("auditor")]
        public long? Auditor { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        [Column("reviewed_date")]
        public DateTime? ReviewedDate { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        [Column("taget_id")]
        public long TagetId { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        [Column("type_id")]
        public long TypeId { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column("enabled")]
        public bool Enabled { get; set; }

    }
}
