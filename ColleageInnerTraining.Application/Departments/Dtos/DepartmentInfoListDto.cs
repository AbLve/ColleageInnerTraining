using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 部门列表Dto
    /// </summary>
    [AutoMapFrom(typeof(DepartmentInfo))]
    public class DepartmentInfoListDto : EntityDto<long>
    {
        /// <summary>
        ///   真实Id
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
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreationTime { get; set; }
    }
}
