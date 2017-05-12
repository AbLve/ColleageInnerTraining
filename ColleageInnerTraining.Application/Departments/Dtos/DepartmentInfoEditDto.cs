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
    /// 部门编辑用Dto
    /// </summary>
    [AutoMap(typeof(DepartmentInfo))]
    public class DepartmentInfoEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public long? Id { get; set; }
        /// <summary>
        ///   部门Id
        /// </summary>
        [DisplayName("部门Id")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [DisplayName("部门名称")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 父级部门id
        /// </summary>
        [DisplayName("父级部门id")]
        public int ParentId { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        [DisplayName("等级")]
        public int Level { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        [DisplayName("位置")]
        public string Path { get; set; }
        /// <summary>
        /// 位置名称
        /// </summary>
        [DisplayName("位置名称")]
        public string PathName { get; set; }

        /// <summary>
        /// 是否为新
        /// </summary>
        [DisplayName("是否为新")]
        public bool IsNew { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        [DisplayName("是否有效")]
        public bool Enabled { get; set; }

    }
}
