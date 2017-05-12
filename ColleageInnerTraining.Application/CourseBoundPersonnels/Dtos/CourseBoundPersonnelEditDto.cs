using System.ComponentModel;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;
using System.ComponentModel.DataAnnotations;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 课程人员绑定表编辑用Dto
    /// </summary>
    [AutoMap(typeof(CourseBoundPersonnel))]
    public class CourseBoundPersonnelEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public long? Id { get; set; }

        /// <summary>
        /// 课程Id
        /// </summary>
        [DisplayName("课程Id")]
        public int CourseId { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        [DisplayName("课程名称")]
        [MaxLength(255)]
        public string CourseName { get; set; }

        /// <summary>
        /// 人员Id
        /// </summary>
        [DisplayName("人员Id")]
        public int AccountSysNo { get; set; }
        /// <summary>
        /// 人员名称
        /// </summary>
        [DisplayName("人员名称")]
        [MaxLength(255)]
        public string AccountUserName { get; set; }
        /// <summary>
        /// 是否必学
        /// </summary>
        //[DisplayName("是否必学")]
        //public bool IsNeedLearn { get; set; }
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
