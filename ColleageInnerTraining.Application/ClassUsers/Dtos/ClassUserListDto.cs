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
    [AutoMapFrom(typeof(ClassUser))]
    public class ClassUserListDto : EntityDto<long>
    {
        /// <summary>
        /// 班级Id
        /// </summary>
        [DisplayName("班级Id")]
        public int ClassId { get; set; }
        /// <summary>
        /// 成员Id
        /// </summary>
        [DisplayName("成员Id")]
        public int UserId { get; set; }
    }
}
