using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Application;

namespace ColleageInnerTraining.Application.Dtos
{
	/// <summary>
    /// 阅读人数列表Dto
    /// </summary>
    [AutoMapFrom(typeof(ReadTimes))]
    public class ReadTimesListDto : EntityDto<long>
    {
        /// <summary>
        /// 阅读人数对像名称
        /// </summary>
        [DisplayName("阅读人数对像名称")]
        public      string BizName { get; set; }
        /// <summary>
        /// 阅读人数对像类型
        /// </summary>
        [DisplayName("阅读人数对像类型")]
        public      string BizType { get; set; }
        /// <summary>
        /// 阅读人数对像Id
        /// </summary>
        [DisplayName("阅读人数对像Id")]
        public      int BizId { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [DisplayName("用户Id")]
        public      int UserId { get; set; }
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
    }
}
