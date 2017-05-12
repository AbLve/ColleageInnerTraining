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
    [AutoMap(typeof(ClassUser))]
    public class ClassUserEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public long? Id { get; set; }

        /// <summary>
        /// 班级名称
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
