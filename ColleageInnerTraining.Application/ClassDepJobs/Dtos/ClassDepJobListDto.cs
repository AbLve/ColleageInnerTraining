using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 班级和部门或岗位列表Dto
    /// </summary>
    [AutoMapFrom(typeof(ClassDepJob))]
    public class ClassDepJobListDto : EntityDto<long>
    {
        /// <summary>
        /// 班级Id
        /// </summary>
        [DisplayName("班级Id")]
        public int ClassId { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        [DisplayName("班级名称")]
        public string ClassName { get; set; }
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
        public string BusinessName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreationTime { get; set; }
    }
}
