using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace ColleageInnerTraining.Core
{
    [Table("px_common_menu")]
    public class Menu : FullAuditedEntity<long>
    {
        /// <summary>
        /// 名字长度
        /// </summary>
        public const int MaxNameLength = 255;
        /// <summary>
        /// url长度
        /// </summary>
        public const int MaxUrlLength = 255;
        /// <summary>
        /// 权限长度
        /// </summary>
        public const int MaxPermissionCodeLength = 255;
        /// <summary>
        /// 菜单类型长度
        /// </summary>
        public const int MaxMenuTypeLength = 10;
        /// <summary>
        /// 路径长度
        /// </summary>
        public const int MaxPathLength = 255;
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column("enabled")]
        public bool Enabled { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Column("menu_name")]
        [StringLength(MaxNameLength)]
        public string MenuName { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Column("url")]
        [StringLength(MaxUrlLength)]
        public string Url { get; set; }
        /// <summary>
        /// 父路径
        /// </summary>
        [Column("parent_id")]
        public int ParentId { get; set; }
        /// <summary>
        /// 权限代码
        /// </summary>
        [Column("permission_code")]
        [StringLength(MaxUrlLength)]
        public string PermissionCode { get; set; }        
        /// <summary>
        /// 排序
        /// </summary>
        [Column("sort")]
        public int Sort { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [Column("menu_type")]
        [StringLength(MaxMenuTypeLength)]
        public string  MenuType { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        [Column("path")]
        [StringLength(MaxPathLength)]
        public string Path { get; set; }

    }
}
