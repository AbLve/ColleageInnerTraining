using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Core
{
    [Table("px_common_userteachers")]
    public class Teachers : FullAuditedEntity<long>
    {
        public const int MaxLength = 255;

        /// <summary>
        /// 用户编号
        /// </summary>
        [Column("sys_no")]
        public int SysNo { get; set; }

        /// <summary>
        /// 内训师名称
        /// </summary>
        [Column("user_name")]
        [StringLength(MaxLength)]
        public string UserName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Column("user_phone")]
        [StringLength(MaxLength)]
        public string UserPhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Column("user_email")]
        [StringLength(MaxLength)]
        public string UserEmail { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        [Column("department_id")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// 岗位Id
        /// </summary>
        [Column("jobpost_id")]
        public int JobpostId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [Column("role")]
        public int Role { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        public int Status { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        [Column("portrait_url")]
        [StringLength(MaxLength)]
        public string PortraitUrl { get; set; }

        /// <summary>
        /// 主讲课程
        /// </summary>
        [Column("speaker_course")]
        [StringLength(MaxLength)]
        public string SpeakerCourse { get; set; }
    }
}
