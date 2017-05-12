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
    /// 班级成员编辑用Dto
    /// </summary>
    [AutoMap(typeof(ClassTrainingInfo))]
    public class ClassTrainingInfoEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public long? Id { get; set; }

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
