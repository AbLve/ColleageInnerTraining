using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 课程人员绑定表列表Dto
    /// </summary>
    [AutoMapFrom(typeof(CourseBoundPersonnel))]
    public class CourseBoundPersonnelListDto : EntityDto<long>
    {
        /// <summary>
        /// 课程Id
        /// </summary>
        [DisplayName("课程Id")]
        public int CourseId { get; set; }
        /// <summary>
        /// 人员Id
        /// </summary>
        [DisplayName("人员Id")]
        public int AccountUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 是否签到
        /// </summary>
        public bool CheckIN { get; set; }
        /// <summary>
        /// 是否完成学习
        /// </summary>   
        public bool IsComplete { get; set; }
        /// <summary>
        /// 是否为已绑定人员
        /// </summary>
        public bool IsBound { get; set; }
    }
}
