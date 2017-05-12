using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 考试记录表
    /// </summary>
    [Table("px_exam_record")]
    public class ExamRecord : FullAuditedEntity<long>
    {
        public const int MaxNameLength = 255;

        /// <summary>
        /// 考试ID
        /// </summary>
        [Column("exam_id")]
        public int ExamId { get; set; }
        /// <summary>
        /// 试卷ID
        /// </summary>
        [Column("paper_id")]
        public int PaperId { get; set; }

        /// <summary>
        /// 课程ID
        /// </summary>
        [Column("course_id")]
        public int CourseId { get; set; }

        /// <summary>
        /// 答题人ID
        /// </summary>
        [Column("apply_id")]
        public int UserId { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        [Column("path")]
        public string Path { get; set; }

        /// <summary>
        /// 开始答题时间
        /// </summary>
        [Column("start_time")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 交卷时间
        /// </summary>
        [Column("end_time")]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 考分
        /// </summary>
        [Column("score")]
        public int Score { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        public int Status { get; set; }

        /// <summary>
        /// 阅卷人ID
        /// </summary>
        [Column("review_id")]
        public bool ReviewId { get; set; }

    }
}
