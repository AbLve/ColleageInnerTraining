using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 班级信息
    /// </summary>
    [Table("px_classes_info")]
    public class ClassesInfo : FullAuditedEntity<long>
    {
        public const int MaxNameLength = 255;
      

        /// <summary>
        /// 班级名称
        /// </summary>
        [Column("name")]
        [StringLength(MaxNameLength)]
        public string ClassesName { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [Column("description")]
        [StringLength(MaxNameLength)]
        public string Description { get; set; }

        /// <summary>
        /// 成员数量
        /// </summary>
        [Column("membercount")]
        public int? MemberCount { get; set; }

        /// <summary>
        /// 课程数量
        /// </summary>
        [Column("coursecount")]
        public int? CourseCount { get; set; }

        /// <summary>
        /// 问卷数量
        /// </summary>
        [Column("pollcount")]
        public int? PollCount { get; set; }
        /// <summary>
        /// 培训数量
        /// </summary>
        [Column("traincount")]
        public int? TrainCount { get; set; }

        /// <summary>
        /// 考试数量
        /// </summary>
        [Column("examcount")]
        public int? ExamCount { get; set; }

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

    }
}
