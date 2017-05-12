using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Core
{
    [Table("px_course_category")]
    public class CourseComment : FullAuditedEntity<long>
    {
        /// <summary>
        /// 内容长度
        /// </summary>
        public const int MaxContentLength = 500;                
        /// <summary>
        /// 内容
        /// </summary>
        [Column("content")]
        [StringLength(MaxContentLength)]
        public string Content { get; set; }
        /// <summary>
        /// 评份
        /// </summary>
        [Column("score")]
        public int Score { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column("course_id")]
        public long? CourseId { get; set; }        

    }
}
