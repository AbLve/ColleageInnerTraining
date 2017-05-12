
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
    /// 菜单编辑用Dto
    /// </summary>
    [AutoMap(typeof(Menu))]
    public class MenuEditDto 
    {

	    /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
	    public long? Id{get;set;}

        /// <summary>
        /// 是否有效
        /// </summary>
        [DisplayName("是否有效")]
        public   bool  Enabled { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [DisplayName("用户名")]
        [MaxLength(255)]
        public   string  MenuName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [DisplayName("用户名")]
        [MaxLength(255)]
        public   string  Url { get; set; }

        /// <summary>
        /// 父路径
        /// </summary>
        [DisplayName("父路径")]
        public   int  ParentId { get; set; }

        /// <summary>
        /// 权限代码
        /// </summary>
        [DisplayName("权限代码")]
        public   string  PermissionCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public   int  Sort { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [DisplayName("类型")]
        public   string  MenuType { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        [DisplayName("路径")]
        public string Path { get; set; }

    }
}
