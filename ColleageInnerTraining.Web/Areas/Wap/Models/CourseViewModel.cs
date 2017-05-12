using ColleageInnerTraining.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColleageInnerTraining.Web.Areas.Wap.Models
{
    public class CourseViewModel
    {
        /// <summary>
        /// 课程id
        /// </summary>
        public int CourseId { get; set; }
        /// <summary>
        /// 课程
        /// </summary>
        public CourseInfoListDto CourseInfo { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public List<CourseCategoryListDto> CourseCategorys { get; set; }
        /// <summary>
        /// 课程内训师
        /// </summary>
        public TeachersListDto teacher { get; set; }
        /// <summary>
        /// 是否已报名
        /// </summary>
        public bool IsSingleUp { get; set; }
        /// <summary>
        /// 就否已完成的课程
        /// </summary>
        public bool IsComplete { get; set; }

    }
}