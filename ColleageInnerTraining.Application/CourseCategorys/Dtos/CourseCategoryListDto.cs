using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;
using System.Collections.Generic;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 课程分类列表Dto
    /// </summary>
    [AutoMapFrom(typeof(CourseCategory))]
    public class CourseCategoryListDto : EntityDto<long>
    {
        /// <summary>
        /// 父级课程id
        /// </summary>
        public int ParentId { get; set; }

        public int CategoryId { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        [DisplayName("分类名称")]
        public      string CourseCategoryName { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [DisplayName("简介")]
        public      string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public      int Sort { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [DisplayName("是否有效")]
        public      bool Enabled { get; set; }
        /// <summary>
        /// 最后编辑时间
        /// </summary>
        [DisplayName("最后编辑时间")]
        public      DateTime? LastModificationTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public      DateTime CreationTime { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 路径名称
        /// </summary>
        public string PathName { get; set; }

        public List<CourseInfoListDto> CourseInfoList { get; set; }

        public List<CourseCategoryListDto> CourseCategorys { get; set; }
    }
}
