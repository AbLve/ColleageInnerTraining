using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
	/// <summary>
    /// 所属类型配置列表Dto
    /// </summary>
    [AutoMapFrom(typeof(CourseBoundConfigureType))]
    public class CourseBoundConfigureTypeListDto : EntityDto<long>
    {
        /// <summary>
        /// 课程Id
        /// </summary>
        [DisplayName("课程Id")]
        public      int CourseId { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        [DisplayName("课程名称")]
        public      string CourseName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [DisplayName("类型")]
        public      int type { get; set; }
        /// <summary>
        /// 业务Id
        /// </summary>
        [DisplayName("业务Id")]
        public      int BusinessId { get; set; }
        /// <summary>
        /// 业务名称
        /// </summary>
        [DisplayName("业务名称")]
        public      string BusinessName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public      DateTime CreationTime { get; set; }
    }
}
