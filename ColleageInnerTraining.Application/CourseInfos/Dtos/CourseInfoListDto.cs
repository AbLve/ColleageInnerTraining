using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 课程基本信息列表Dto
    /// </summary>
    [AutoMapFrom(typeof(CourseInfo))]
    public class CourseInfoListDto : EntityDto<long>
    {
        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 课程类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 课程分类
        /// </summary>
        public int CategoryType { get; set; }
        /// <summary>
        /// 第一阶段名称
        /// </summary>
        public string StageName { get; set; }
        /// <summary>
        /// 课程上下架状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 封面ID
        /// </summary>
        public int ImageId { get; set; }
        /// <summary>
        /// 封面路径
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 讲师ID
        /// </summary>
        public int TeacherId { get; set; }
        /// <summary>
        /// 讲师姓名
        /// </summary>
        public string TeacherName { get; set; }
        /// <summary>
        /// 直播状态
        /// </summary>
        public int LiveStatus { get; set; }
        /// <summary>
        /// 直播开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 直播结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 课程类型
        /// </summary>
        public string CourseCategoryName { get; set; }

        /// <summary>
        /// 是否必学
        /// </summary>
        public bool IsNeedLearn { get; set; }
        /// <summary>
        /// 时长
        /// </summary>
        public int TimeLength { get; set; }
        /// <summary>
        /// 课程创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 阅读次数
        /// </summary>
        public int ReadTimes { get; set; }
        /// <summary>
        /// 收藏人数
        /// </summary>
        public int CollectionTimes { get; set; }
        /// <summary>
        /// 展示位置
        /// </summary>
        public int DisplayPosition { get; set; }
        /// <summary>
        /// 报名人数
        /// </summary>
        public int Enrollment { get; set; }

        /// <summary>
        /// 签到人数
        /// </summary>
        public int CheckinNum { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 岗位Id
        /// </summary>
        public int JobPostId { get; set; }

        /// <summary>
        /// 问卷Id
        /// </summary>
        public int PollId { get; set; }
        /// <summary>
        /// 考试Id
        /// </summary>
        public int ExaminationId { get; set; }
        /// <summary>
        /// 培训地点
        /// </summary>
        public string TrainingLocation { get; set; }
        /// <summary>
        /// 扩展类型（1.课程管理， 2.课程审核）
        /// </summary>
        public int IsTypeC { get; set; }
    }
}
