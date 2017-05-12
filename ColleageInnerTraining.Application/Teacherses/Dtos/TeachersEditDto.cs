using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 内训师编辑用Dto
    /// </summary>
    [AutoMap(typeof(Teachers))]
    public class TeachersEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public long? Id { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        [DisplayName("用户编号")]
        public int SysNo { get; set; }

        /// <summary>
        /// 内训师名称
        /// </summary>
        [DisplayName("内训师名称")]
        [MaxLength(255)]
        public string UserName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [DisplayName("手机号")]
        [MaxLength(255)]
        public string UserPhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DisplayName("邮箱")]
        [MaxLength(255)]
        public string UserEmail { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        [DisplayName("部门Id")]
        public int DepartmentId { get; set; }

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
        [MaxLength(255)]
        public string PortraitUrl { get; set; }

        /// <summary>
        /// 主讲课程
        /// </summary>
        [DisplayName("主讲课程")]
        [MaxLength(255)]
        public string SpeakerCourse { get; set; }

    }
}
