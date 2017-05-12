using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 互动社区编辑用Dto
    /// </summary>
    [AutoMap(typeof(CommunityInteraction))]
    public class CommunityInteractionEditDto
    {
        public const int MaxLength = 255;
        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public long? Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [DisplayName("用户Id")]
        public int UserId { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        [DisplayName("父Id")]
        public int ParentUserId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DisplayName("内容")]
        [MaxLength(255)]
        public string Content { get; set; }

        /// <summary>
        /// 参与人数
        /// </summary>
        [DisplayName("参与人数")]
        public int CountUser { get; set; }

        /// <summary>
        /// 发表时间
        /// </summary>
        [DisplayName("发表时间")]
        public DateTime PublicationTime { get; set; }

    }
}
