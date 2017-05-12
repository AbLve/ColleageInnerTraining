using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 岗位信息
    /// </summary>
    [Table("px_common_jobpost")]
    public class JobPost : FullAuditedEntity<long>
    {
        public const int MaxNameLength = 255;
      

        /// <summary>
        /// 岗位名称
        /// </summary>
        [Column("name")]
        [StringLength(MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [Column("description")]
        [StringLength(MaxNameLength)]
        public string Description { get; set; }

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
