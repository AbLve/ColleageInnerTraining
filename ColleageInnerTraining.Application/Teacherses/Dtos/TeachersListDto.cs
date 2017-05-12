
using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 内训师列表Dto
    /// </summary>
    [AutoMapFrom(typeof(Teachers))]
    public class TeachersListDto : EntityDto<long>
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [DisplayName("用户编号")]
        public int SysNo { get; set; }
        /// <summary>
        /// 内训师名称
        /// </summary>
        [DisplayName("内训师名称")]
        public string UserName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [DisplayName("手机号")]
        public string UserPhone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [DisplayName("邮箱")]
        public string UserEmail { get; set; }
        /// <summary>
        /// 部门Id
        /// </summary>
        [DisplayName("部门Id")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [DisplayName("部门名称")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 岗位Id
        /// </summary>
        [DisplayName("岗位Id")]
        public int JobpostId { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        [DisplayName("角色")]
        public int Role { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        public int Status { get; set; }
        /// <summary>
        /// 头像路径
        /// </summary>
        [DisplayName("头像路径")]
        public string PortraitUrl { get; set; }
        /// <summary>
        /// 主讲课程
        /// </summary>
        [DisplayName("主讲课程")]
        public string SpeakerCourse { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 培训课程数
        /// </summary>
        [DisplayName("培训课程数")]
        public int TrainCount { get; set; }

    }
}
