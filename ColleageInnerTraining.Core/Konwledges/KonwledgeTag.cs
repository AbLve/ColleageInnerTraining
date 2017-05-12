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
    /// 知识库标签关联表
    /// </summary>
    [Table("px_knowledge_tags")]
    public class KonwledgeTag : FullAuditedEntity<long>
    {
        /// <summary>
        /// 知识库id
        /// </summary>
        [Column("knowledge_id")]
        public int knowledgeId { get; set; }
        /// <summary>
        /// 标签id
        /// </summary>
        [Column("tag_id")]
        public int TagId { get; set; }
    }
}
