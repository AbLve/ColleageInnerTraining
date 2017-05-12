
using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 用户账号信息列表Dto
    /// </summary>
    [AutoMapFrom(typeof(UserAccount))]
    public class UserAccountListDto : EntityDto<long>
    {
        /// <summary>
        /// 用户No
        /// </summary>
        [DisplayName("用户No")]
        public int SysNO { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        [DisplayName("登录名")]
        public string LoginName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [DisplayName("显示名称")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [DisplayName("部门ID")]
        public int DepartmentID { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [DisplayName("部门名称")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [DisplayName("省")]
        public string province { get; set; }

        /// <summary>
        /// 省ID
        /// </summary>
        [DisplayName("省ID")]
        public int ProvinceID { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [DisplayName("市")]
        public string City { get; set; }

        /// <summary>
        /// 市ID
        /// </summary>
        [DisplayName("市ID")]
        public int CityID { get; set; }

        /// <summary>
        /// 区（县）
        /// </summary>
        [DisplayName("区（县）")]
        public string Area { get; set; }

        /// <summary>
        /// 区（县）
        /// </summary>
        [DisplayName("区（县）")]
        public int AreaID { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [DisplayName("详细地址")]
        public string DetailedAddress { get; set; }

        /// <summary>
        /// 岗位ID
        /// </summary>
        [DisplayName("岗位ID")]
        public int PostID { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        [DisplayName("岗位名称")]
        public string PostName { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [DisplayName("电话号码")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DisplayName("邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        public string Password { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        [DisplayName("位置")]
        public string Path { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        [DisplayName("是否有效")]
        public bool Status { get; set; }
        /// <summary>
        /// 是否在所选班级
        /// </summary>
        [DisplayName("是否有效")]
        public bool IsExitClass { get; set; }

        /// <summary>
        /// 是否在所选考试
        /// </summary>
        [DisplayName("是否有效")]
        public bool IsExitCourse { get; set; }

        /// <summary>
        /// 是否必学
        /// </summary>
        //[DisplayName("是否必学")]
        //public bool IsNeedLearn { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreationTime { get; set; }
    }
}
