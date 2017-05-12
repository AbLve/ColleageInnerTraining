using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 课程基本信息编辑用Dto
    /// </summary>
    [AutoMap(typeof(CourseInfo))]
    public class CourseInfoEditDto 
    {
        /// <summary>
        ///   主键Id
        /// </summary>
        public long? Id{get;set;}

        /// <summary>
        /// 课程名称
        /// </summary>
        [DisplayName("课程名称")]
        [MaxLength(255)]
        public string CourseName { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [DisplayName("简介")]
        [MaxLength(1000)]
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
        /// 内容
        /// </summary>
        [DisplayName("内容")]
        [MaxLength(1000)]
        public string Content { get; set; }
        /// <summary>
        /// 课程分类
        /// </summary>
        [DisplayName("课程分类")]
        public int Type { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        [DisplayName("类型名称")]
        [MaxLength(255)]
        public string TypeName { get; set; }
        /// <summary>
        /// 课程类型
        /// </summary>
        [DisplayName("课程类型")]
        public int CategoryType { get; set; }
        /// <summary>
        /// 第一阶段名称
        /// </summary>
        [DisplayName("第一阶段名称")]
        [MaxLength(255)]
        public string StageName { get; set; }

        /// <summary>
        /// 课程上下架状态
        /// </summary>
        [DisplayName("课程上下架状态")]
        public int Status { get; set; }
        /// <summary>
        /// 封面路径
        /// </summary>
        [DisplayName("封面路径")]
        [MaxLength(255)]
        public string ImageUrl { get; set; }
        /// <summary>
        /// 讲师ID
        /// </summary>
        [DisplayName("讲师ID")]
        public int TeacherId { get; set; }
        /// <summary>
        /// 讲师姓名
        /// </summary>
        [DisplayName("讲师姓名")]
        [MaxLength(255)]
        public string TeacherName { get; set; }
        /// <summary>
        /// 直播状态
        /// </summary>
        [DisplayName("直播状态")]
        public int LiveStatus { get; set; }
        /// <summary>
        /// 直播开始时间
        /// </summary>
        [DisplayName("直播开始时间")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 直播结束时间
        /// </summary>
        [DisplayName("直播结束时间")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 展示位置
        /// </summary>
        [DisplayName("展示位置")]
        public int DisplayPosition { get; set; }

        /// <summary>
        /// 考试Id
        /// </summary>
        [DisplayName("考试Id")]
        public int ExaminationId { get; set; }
        /// <summary>
        /// 时长
        /// </summary>
        [DisplayName("时长")]
        public int TimeLength { get; set; }
        /// <summary>
        /// 阅读次数
        /// </summary>
        [DisplayName("阅读次数")]
        public int ReadTimes { get; set; }

        /// <summary>
        /// 收藏人数
        /// </summary>
        [DisplayName("收藏人数")]
        public int CollectionTimes { get; set; }

        /// <summary>
        /// 报名人数
        /// </summary>
        [DisplayName("报名人数")]
        public int Enrollment { get; set; }

        /// <summary>
        /// 签到人数
        /// </summary>
        [DisplayName("签到人数")]
        public int CheckinNum { get; set; }

        /// <summary>
        /// 问卷Id
        /// </summary>
        [DisplayName("问卷Id")]
        public int PollId { get; set; }
        /// <summary>
        /// 培训地点
        /// </summary>
        [DisplayName("培训地点")]
        [MaxLength(1000)]
        public string TrainingLocation { get; set; }
    }
}
