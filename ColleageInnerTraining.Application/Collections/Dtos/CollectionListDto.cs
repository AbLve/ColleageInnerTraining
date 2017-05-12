using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 收藏列表Dto
    /// </summary>
    [AutoMapFrom(typeof(Collection))]
    public class CollectionListDto : EntityDto<long>
    {
        /// <summary>
        /// 收藏对像名称
        /// </summary>
        [DisplayName("收藏对像名称")]
        public string BizName { get; set; }
        /// <summary>
        /// 收藏对像类型
        /// </summary>
        [DisplayName("收藏对像类型")]
        public string BizType { get; set; }
        /// <summary>
        /// 收藏对像Id
        /// </summary>
        [DisplayName("收藏对像Id")]
        public int BizId { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [DisplayName("用户Id")]
        public int UserId { get; set; }
        /// <summary>
        /// 收藏对像图片
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 收藏对像图片
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 报名人数
        /// </summary>
        public int Enrollment { get; set; }
        /// <summary>
        /// 时长
        /// </summary>
        public int TimeLength { get; set; }
        /// <summary>
        /// 最后编辑时间
        /// </summary>
        [DisplayName("最后编辑时间")]
        public DateTime? LastModificationTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreationTime { get; set; }
    }
}
