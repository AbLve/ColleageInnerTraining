using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Core
{
    [Table("px_course_bound_personnel")]
    public class CourseBoundPersonnel : FullAuditedEntity<long>
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
        /// 人员Id
        /// </summary>
        [Column("account_sys_no")]
        public int AccountSysNo { get; set; }
        /// <summary>
        /// 人员名称
        /// </summary>
        [Column("account_user_name")]
        [StringLength(MaxLength)]
        public string AccountUserName { get; set; }
        /// <summary>
        /// 是否必学
        /// </summary>
        [Column("is_need_learn")]
        public bool IsNeedLearn { get; set; }
        /// <summary>
        /// 是否签到
        /// </summary>
        [Column("checkin")]
        public bool CheckIN { get; set; }
        /// <summary>
        /// 是否完成学习
        /// </summary>
        [Column("is_complete")]
        public bool IsComplete { get; set; }

        /// <summary>
        /// 是否为已绑定人员
        /// </summary>
        [Column("is_bound")]
        public bool IsBound { get; set; }
    }
}
