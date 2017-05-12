using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 班级列表Dto
    /// </summary>
    [AutoMapFrom(typeof(ClassesInfo))]
    public class ClassesInfoListDto : EntityDto<long>
    {
        /// <summary>
        /// 班级名称
        /// </summary>
        [DisplayName("班级名称")]
        public string ClassesName { get; set; }
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
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreationTime { get; set; }
    }
}
