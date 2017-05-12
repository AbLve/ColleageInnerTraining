using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Abp.Extensions;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 班级编辑用Dto
    /// </summary>
    [AutoMap(typeof(ClassesInfo))]
    public class ClassesInfoEditDto 
    {

	/// <summary>
    ///   主键Id
    /// </summary>
    [DisplayName("主键Id")]
	public long? Id{get;set;}

        /// <summary>
        /// 班级名称
        /// </summary>
        [DisplayName("班级名称")]
        public   string  ClassesName { get; set; }
        /// <summary>
        /// 成员数量
        /// </summary>
        [DisplayName("成员数量")]
        public int MemberCount { get; set; }

        /// <summary>
        /// 课程数量
        /// </summary>
        [DisplayName("课程数量")]
        public int CourseCount { get; set; }

        /// <summary>
        /// 问卷数量
        /// </summary>
        [DisplayName("问卷数量")]
        public int PollCount { get; set; }
        /// <summary>
        /// 培训数量
        /// </summary>
        [DisplayName("培训数量")]
        public int TrainCount { get; set; }

        /// <summary>
        /// 考试数量
        /// </summary>
        [DisplayName("考试数量")]
        public int ExamCount { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [DisplayName("简介")]
        public   string  Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public   int  Sort { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        [DisplayName("是否有效")]
        public   bool  Enabled { get; set; }

    }
}
