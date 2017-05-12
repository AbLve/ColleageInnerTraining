
using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
	/// <summary>
    /// 菜单列表Dto
    /// </summary>
    [AutoMapFrom(typeof(Menu))]
    public class MenuListDto : EntityDto<long>
    {
        /// <summary>
        /// 是否有效
        /// </summary>
        [DisplayName("是否有效")]
        public      bool Enabled { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [DisplayName("用户名")]
        public      string MenuName { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [DisplayName("用户名")]
        public      string Url { get; set; }
        /// <summary>
        /// 父路径
        /// </summary>
        [DisplayName("父路径")]
        public      int ParentId { get; set; }
        /// <summary>
        /// 权限代码
        /// </summary>
        [DisplayName("权限代码")]
        public      string PermissionCode { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public      int Sort { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public      string MenuType { get; set; }
        /// <summary>
        /// 最后编辑时间
        /// </summary>
        [DisplayName("最后编辑时间")]
        public      DateTime? LastModificationTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public      DateTime CreationTime { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("路径")]
        public string Path { get; set; }
    }
}
