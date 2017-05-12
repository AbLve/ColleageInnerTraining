using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 所属类型配置编辑用Dto
    /// </summary>
    [AutoMap(typeof(CourseBoundConfigureType))]
    public class CourseBoundConfigureTypeEditDto
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
        /// 类型
        /// </summary>
        [DisplayName("类型")]
        public int type { get; set; }

        /// <summary>
        /// 业务Id
        /// </summary>
        [DisplayName("业务Id")]
        public int BusinessId { get; set; }

        /// <summary>
        /// 业务名称
        /// </summary>
        [DisplayName("业务名称")]
        [MaxLength(255)]
        public string BusinessName { get; set; }

    }
}
