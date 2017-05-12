using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Core
{
    [Table("px_class_dep_job")]
    public class ClassDepJob : FullAuditedEntity<long>
    {
        public const int MaxLength = 255;
        /// <summary>
        /// 班级Id
        /// </summary>
        [Column("class_id")]
        public int ClassId { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        [Column("class_name")]
        [StringLength(MaxLength)]
        public string ClassName { get; set; }

        /// <summary>
        /// 类型
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
