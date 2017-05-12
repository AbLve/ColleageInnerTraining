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
    /// 轮播图编辑用Dto
    /// </summary>
    [AutoMap(typeof(Banner))]
    public class BannerEditDto
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public long? Id { get; set; }
        /// <summary>
        /// 图片id
        /// </summary>
        public int ImageId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 开启时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 关闭时间
        /// </summary>
        public DateTime EndTime { get; set; }


        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 客户端类型（1-PC、2-WAP）
        /// </summary>
        public int ClientType { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

    }
}
