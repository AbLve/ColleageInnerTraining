using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Core
{
    [Table("px_knowledge_dep_job")]
    public class KnowledgeDepJob : FullAuditedEntity<long>
    {
        public const int MaxLength = 255;
        /// <summary>
        /// 知识点Id
        /// </summary>
        [Column("knoledge_id")]
        public int KnoledgeId { get; set; }

        /// <summary>
        /// 知识点标题
        /// </summary>
        [Column("knoledge_title")]
        [StringLength(MaxLength)]
        public string KnoledgeTitle { get; set; }

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
