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
    /// 班级编辑用Dto
    /// </summary>
    [AutoMap(typeof(Taget))]
    public class TagetEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        public long? Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Enabled { get; set; }

    }
}
