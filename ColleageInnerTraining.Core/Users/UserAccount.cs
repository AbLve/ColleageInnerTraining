using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleageInnerTraining.Core
{
    [Table("px_user_account")]
    public class UserAccount : FullAuditedEntity<long>
    {
        public const int MaxLength = 255;
        /// <summary>
        /// 用户No
        /// </summary>
        [Column("sys_no")]
        public int SysNO { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        [Column("login_Name")]
        [StringLength(MaxLength)]
        public string LoginName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Column("display_name")]
        [StringLength(MaxLength)]
        public string DisplayName { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [Column("department_id")]
        public int DepartmentID { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Column("department_name")]
        [StringLength(MaxLength)]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [Column("province")]
        [StringLength(MaxLength)]
        public string province { get; set; }

        /// <summary>
        /// 省ID
        /// </summary>
        [Column("province_id")]
        public int ProvinceID { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [Column("city")]
        [StringLength(MaxLength)]
        public string City { get; set; }

        /// <summary>
        /// 市ID
        /// </summary>
        [Column("city_id")]
        public int CityID { get; set; }

        /// <summary>
        /// 区（县）
        /// </summary>
        [Column("area")]
        [StringLength(MaxLength)]
        public string Area { get; set; }

        /// <summary>
        /// 区（县）
        /// </summary>
        [Column("area_id")]
        public int AreaID { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [Column("detailed_address")]
        [StringLength(MaxLength)]
        public string DetailedAddress { get; set; }

        /// <summary>
        /// 岗位ID
        /// </summary>
        [Column("post_id")]
        public int PostID { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        [Column("post_name")]
        [StringLength(MaxLength)]
        public string PostName { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [Column("phone_number")]
        [StringLength(MaxLength)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Column("email")]
        [StringLength(MaxLength)]
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Column("password")]
        [StringLength(MaxLength)]
        public string Password { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        [Column("enabled")]
        public bool Status { get; set; }
    }
}
