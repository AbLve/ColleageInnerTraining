using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 线下培训信息
    /// </summary>
    [Table("px_class_training_info")]
    public class ClassTrainingInfo : FullAuditedEntity<long>
    {
        public const int MaxNameLength = 255;

        /// <summary>
        /// 班级id
        /// </summary>
        [Column("class_id")]
        public int ClassId { get; set; }
        /// <summary>
        /// 培训名称
        /// </summary>
        [Column("name")]
        [StringLength(MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// 培训开始时间
        /// </summary>
        [Column("start_time")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 培训结束时间
        /// </summary>
        [Column("end_time")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column("sort")]
        public int Sort { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        [Column("enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        /// 项目类型（1-课程、2-考试、3-问卷、4-线下培训）
        /// </summary>
        [Column("biz_type")]
        public int TrainingType { get; set; }
        /// <summary>
        /// 业务id
        /// </summary>
        [Column("biz_id")]
        public int TypeId { get; set; }
    }
}
