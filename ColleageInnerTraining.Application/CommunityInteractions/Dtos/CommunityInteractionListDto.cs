using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 互动社区列表Dto
    /// </summary>
    [AutoMapFrom(typeof(CommunityInteraction))]
    public class CommunityInteractionListDto : EntityDto<long>
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [DisplayName("用户Id")]
        public int UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [DisplayName("用户名称")]
        public string UserName { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        [DisplayName("父Id")]
        public int ParentUserId { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [DisplayName("内容")]
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
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreationTime { get; set; }
    }
}
