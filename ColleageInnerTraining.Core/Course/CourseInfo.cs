using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Core
{
    [Table("px_course_info")]
    public class CourseInfo : FullAuditedEntity<long>
    {
        public const int MaxNameLength = 255;
        public const int MaxDescriptionLength = 255;
        public const int MaxContentLength = 1000;
        public const int MaxImageUrlLength = 255;

        /// <summary>
        /// 课程名称
        /// </summary>
        [Column("name")]
        [StringLength(MaxNameLength)]
        public string CourseName { get; set; }
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
        /// 内容
        /// </summary>
        [Column("content")]
        [StringLength(MaxContentLength)]
        public string Content { get; set; }
        /// <summary>
        /// 课程分类
        /// </summary>
        [Column("type")]
        public int Type { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        [Column("type_name")]
        [StringLength(MaxNameLength)]
        public string TypeName { get; set; }
        /// <summary>
        /// 课程类型
        /// </summary>
        [Column("category_type")]
        public int CategoryType { get; set; }
        /// <summary>
        /// 第一阶段名称
        /// </summary>
        [Column("stage_name")]
        [StringLength(MaxContentLength)]
        public string StageName { get; set; }

        /// <summary>
        /// 课程上下架状态
        /// </summary>
        [Column("status")]
        public int Status { get; set; }
        /// <summary>
        /// 讲师ID
        /// </summary>
        [Column("teacher_id")]
        public int TeacherId { get; set; }
        /// <summary>
        /// 讲师姓名
        /// </summary>
        [Column("teacher_name")]
        [StringLength(MaxNameLength)]
        public string TeacherName { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        [Column("image_url")]
        [StringLength(MaxNameLength)]
        public string ImageUrl { get; set; }
        /// <summary>
        /// 直播状态
        /// </summary>
        [Column("live_status")]
        public int LiveStatus { get; set; }
        /// <summary>
        /// 直播开始时间
        /// </summary>
        [Column("start_time")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 直播结束时间
        /// </summary>
        [Column("end_time")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 展示位置
        /// </summary>
        [Column("display_position")]
        public int DisplayPosition { get; set; }

        /// <summary>
        /// 考试Id
        /// </summary>
        [Column("examination_id")]
        public int ExaminationId { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        [Column("time_length")]
        public int TimeLength { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        [Column("read_times")]
        public int ReadTimes { get; set; }

        /// <summary>
        /// 收藏人数
        /// </summary>
        [Column("collection_times")]
        public int CollectionTimes { get; set; }

        /// <summary>
        /// 报名人数
        /// </summary>
        [Column("enrollment")]
        public int Enrollment { get; set; }

        /// <summary>
        /// 签到人数
        /// </summary>
        [Column("checkin_num")]
        public int CheckinNum { get; set; }

        /// <summary>
        /// 问卷Id
        /// </summary>
        [Column("poll_id")]
        public int PollId { get; set; }
        /// <summary>
        /// 培训地点
        /// </summary>
        [Column("training_location")]
        [StringLength(MaxNameLength)]
        public string TrainingLocation { get; set; }

    }
}
