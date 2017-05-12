using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 班级成员列表Dto
    /// </summary>
    [AutoMapFrom(typeof(ClassTrainingInfo))]
    public class ClassTrainingInfoListDto : EntityDto<long>
    {

        /// <summary>
        /// 班级id
        /// </summary>
        [DisplayName("班级id")]
        public int ClassId { get; set; }

        /// 培训名称
        /// </summary>
        [DisplayName("培训名称")]
        public string Name { get; set; }

        /// <summary>
        /// 培训开始时间
        /// </summary>
        [DisplayName("培训开始时间")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 培训结束时间
        /// </summary>
        [DisplayName("培训结束时间")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 项目类型（1-课程、2-考试、3-问卷、4-线下培训）
        /// </summary>
        [DisplayName("培训类型")]
        public int TrainingType { get; set; }
        /// <summary>
        /// 业务id
        /// </summary>
        [DisplayName("培训类型id")]
        public int TypeId { get; set; }
    }
}
