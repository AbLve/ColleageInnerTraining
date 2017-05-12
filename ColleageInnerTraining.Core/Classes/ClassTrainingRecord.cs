using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 线下培训记录
    /// </summary>
    [Table("class_training_record")]
    public class ClassTrainingRecord : FullAuditedEntity<long>
    {
        /// <summary>
        /// 线下培训ID
        /// </summary>
        [Column("training_id")]
        public int TrainingId { get; set; }

        /// <summary>
        /// 班级ID
        /// </summary>
        [Column("class_id")]
        public int ClassId { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        [Column("user_id")]
        public int UserId { get; set; }
        /// <summary>
        /// 是否签到
        /// </summary>
        [Column("is_sign")]
        public int IsSign { get; set; }
        /// <summary>
        /// 签到时间
        /// </summary>
        [Column("sign_time")]
        public int SignTime { get; set; }
        /// <summary>
        /// 考试ID
        /// </summary>
        [Column("exam_record_id")]
        public int ExamRecordId { get; set; }
        /// <summary>
        /// 考试得分
        /// </summary>
        [Column("score")]
        public int Score { get; set; }
        /// <summary>
        /// 培训前问卷ID
        /// </summary>
        [Column("pre_poll_record_id")]
        public int PrePollRecordId { get; set; }
        /// <summary>
        /// 培训后文件ID
        /// </summary>
        [Column("post_poll_record_id")]
        public int PostPollRecordId { get; set; }
    }
}
