using System.ComponentModel;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;
using System.ComponentModel.DataAnnotations;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 课程分类编辑用Dto
    /// </summary>
    [AutoMap(typeof(CourseCategory))]
    public class CourseCategoryEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public long? Id { get; set; }

        public int CategoryId { get; set; }

        /// <summary>
        /// 父级课程id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        [DisplayName("等级")]
        public int Level { get; set; }


        /// <summary>
        /// 是否为新
        /// </summary>
        [DisplayName("是否为新")]
        public bool IsNew { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        [DisplayName("分类名称")]
        [MaxLength(255)]
        public string CourseCategoryName { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [DisplayName("简介")]
        [MaxLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        [DisplayName("是否有效")]
        public bool Enabled { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        [DisplayName("路径")]
        [MaxLength(255)]
        public string Path { get; set; }

        /// <summary>
        /// 路径名称
        /// </summary>
        [DisplayName("路径名称")]
        [MaxLength(255)]
        public string PathName { get; set; }
    }
}
