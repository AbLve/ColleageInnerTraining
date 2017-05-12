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
    [AutoMap(typeof(Notice))]
    public class NoticeEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        public long? Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 开启时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 关闭时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Enabled { get; set; }

    }
}
