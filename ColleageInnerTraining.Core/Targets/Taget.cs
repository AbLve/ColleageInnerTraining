using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 标签
    /// </summary>
    [Table("px_common_tags")]
    public class Taget : FullAuditedEntity<long>
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column("enabled")]
        public bool Enabled { get; set; }
    }
}
