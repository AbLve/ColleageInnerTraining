using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Core
{
    [Table("px_course_bound_configure")]
    public class CourseBoundConfigureType: FullAuditedEntity<long>
    {
        public const int MaxLength = 255;
        /// <summary>
        /// 课程Id
        /// </summary>
        [Column("course_id")]
        public int CourseId { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        [Column("course_name")]
        [StringLength(MaxLength)]
        public string CourseName { get; set; }

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
