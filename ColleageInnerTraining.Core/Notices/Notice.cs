using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 公告
    /// </summary>
    [Table("px_common_notice")]
    public class Notice : FullAuditedEntity<long>
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Column("title")]
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Column("content")]
        public string Content { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [Column("image_url")]
        public string ImageUrl { get; set; }
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

        /// 是否有效
        /// </summary>
        [Column("enabled")]
        public bool Enabled { get; set; }
    }
}
