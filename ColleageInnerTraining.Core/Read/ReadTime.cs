using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Application
{
    [Table("px_common_read")]
    public class ReadTimes : FullAuditedEntity<long>
    {
        public const int MaxNameLength = 255;
        public const int MaxTypeLength = 10;
        /// <summary>
        /// 阅读人数对像名称
        /// </summary>
        [Column("biz_name")]
        [StringLength(MaxNameLength)]
        public string BizName { get; set; }

        /// <summary>
        /// 阅读人数对像类型
        /// </summary>
        [Column("biz_type")]
        [StringLength(MaxTypeLength)]
        public string BizType { get; set; }

        /// <summary>
        /// 阅读人数对像Id
        /// </summary>
        [Column("biz_id")]
        public int BizId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Column("sys_no")]
        public int UserId { get; set; }

    }
}
