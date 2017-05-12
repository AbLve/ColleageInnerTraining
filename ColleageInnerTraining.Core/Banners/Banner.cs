using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 网站和移动端轮播图
    /// </summary>
    [Table("px_common_banner")]
    public class Banner : FullAuditedEntity<long>
    {
        public const int MaxNameLength = 255;


        /// <summary>
        /// 图片id
        /// </summary>
        [Column("image_id")]
        public int ImageId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Column("title")]
        [StringLength(MaxNameLength)]
        public string Title { get; set; }

        /// <summary>
        /// 开启时间
        /// </summary>
        [Column("start_time")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 关闭时间
        /// </summary>
        [Column("end_time")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column("sort")]
        public int Sort { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [Column("image_url")]
        [StringLength(MaxNameLength)]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        [Column("link")]
        [StringLength(MaxNameLength)]
        public string Link { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        [Column("enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        /// 客户端类型（1-PC、2-WAP）
        /// </summary>
        [Column("client_type")]
        public int ClientType { get; set; }
   
    }
}
