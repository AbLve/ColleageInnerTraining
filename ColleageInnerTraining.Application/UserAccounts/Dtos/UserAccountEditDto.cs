using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 用户账号信息编辑用Dto
    /// </summary>
    [AutoMap(typeof(UserAccount))]
    public class UserAccountEditDto 
    {
        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public long? Id { get; set; }
        /// <summary>
        /// 用户No
        /// </summary>
        [DisplayName("用户编号")]
        public int SysNO { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        [DisplayName("登录名")]
        [MaxLength(255)]
        public string LoginName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [DisplayName("显示名称")]
        [MaxLength(255)]
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
        [MaxLength(255)]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [DisplayName("省")]
        [MaxLength(255)]
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
        [MaxLength(255)]
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
        [MaxLength(255)]
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
        [MaxLength(255)]
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
        [MaxLength(255)]
        public string PostName { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [DisplayName("电话号码")]
        [MaxLength(255)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DisplayName("邮箱")]
        [MaxLength(255)]
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        [MaxLength(255)]
        public string Password { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        [DisplayName("是否有效")]
        public bool Status { get; set; }

    }
}
