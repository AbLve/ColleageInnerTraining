using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 部门
    /// </summary>
    [Table("px_common_department")]
    public class DepartmentInfo : FullAuditedEntity<long>
    {
        public const int MaxNameLength = 255;

        /// <summary>
        /// 部门Id
        /// </summary>
        [Column("department_id")]
        public int DepartmentId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [Column("displayname")]
        [StringLength(MaxNameLength)]
        public string DisplayName { get; set; }

        /// <summary>
        /// 父级部门id
        /// </summary>
        [Column("parent_id")]
        public int ParentId { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        [Column("level")]
        public int Level{ get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        [Column("path")]
        public string Path { get; set; }

        /// <summary>
        /// 位置名称
        /// </summary>
        [Column("pathname")]
        public string PathName { get; set; }
        /// <summary>
        /// 是否为新
        /// </summary>
        [Column("isnew")]
        public bool IsNew { get; set; }
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
