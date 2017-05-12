using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Core
{
    [Table("px_course_category")]
    public class CourseCategory : FullAuditedEntity<long>
    {
        public const int MaxLength = 255;
        public const int MaxDescriptionLength = 255;

        /// <summary>
        /// 分类Id
        /// </summary>
        [Column("category_id")]
        public int CategoryId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        [Column("name")]
        [StringLength(MaxLength)]
        public string CourseCategoryName { get; set; }

        /// <summary>
        /// 父级课程id
        /// </summary>
        [Column("parent_id")]
        public int ParentId { get; set; }

        /// <summary>
        /// 是否为新
        /// </summary>
        [Column("isnew")]
        public bool IsNew { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        [Column("level")]
        public int Level { get; set; }


        /// <summary>
        /// 简介
        /// </summary>
        [Column("description")]
        [StringLength(MaxDescriptionLength)]
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
        /// <summary>
        /// 路径
        /// </summary>
        [Column("path")]
        [StringLength(MaxLength)]
        public string Path { get; set; }
        /// <summary>
        /// 路径名称
        /// </summary>
        [Column("path_name")]
        [StringLength(MaxLength)]
        public string PathName { get; set; }
    }
}
