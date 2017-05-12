using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Core
{
    [Table("px_notice_dep_job")]
    public class NoticeDepJob : FullAuditedEntity<long>
    {
        public const int MaxLength = 255;
        /// <summary>
        /// 公告Id
        /// </summary>
        [Column("notice_id")]
        public int NoticeId { get; set; }

        /// <summary>
        /// 公告标题
        /// </summary>
        [Column("notice_title")]
        [StringLength(MaxLength)]
        public string NoticeTitle { get; set; }

        /// <summary>
        /// 类型(0-部门，1-岗位)
        /// </summary>
        [Column("type")]
        public int type { get; set; }

        /// <summary>
        /// 业务Id
        /// </summary>
        [Column("business_id")]
        public int BusinessId { get; set; }

        /// <summary>
        /// 业务名称
        /// </summary>
        [Column("business_name")]
        [StringLength(MaxLength)]
        public string BusinessName { get; set; }
    }
}
