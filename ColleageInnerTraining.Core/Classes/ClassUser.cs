using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 班级成员信息
    /// </summary>
    [Table("px_class_user")]
    public class ClassUser : FullAuditedEntity<long>
    {
        /// <summary>
        /// 排序
        /// </summary>
        [Column("class_id")]
        public int ClassId { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        [Column("user_id")]
        public int UserId { get; set; }

    }
}
