using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Core
{
    [Table("px_community_interaction")]
    public class CommunityInteraction : FullAuditedEntity<long>
    {
        public const int MaxLength = 255;

        /// <summary>
        /// 用户Id
        /// </summary>
        [Column("user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        [Column("parent_user_id")]
        public int ParentUserId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Column("content")]
        [StringLength(MaxLength)]
        public string Content { get; set; }

        /// <summary>
        /// 参与人数
        /// </summary>
        [Column("count_user")]
        public int CountUser { get; set; }

        /// <summary>
        /// 发表时间
        /// </summary>
        [Column("publication_time")]
        public DateTime PublicationTime { get; set; }
    }
}
