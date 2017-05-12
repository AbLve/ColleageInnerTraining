using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using ColleageInnerTraining.Web.AuthServiceProxy;
using ECService.ERP.Utility;

namespace ColleageInnerTraining.Web.Auth
{

    /// <summary>
    /// 授权管理
    /// </summary>
    public static class AuthorizeManager
    {
        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="password">密码</param>
        /// <returns>AuthCenter用户信息</returns>
        public static SystemUser Login(string loginName, string password)
        {
            SystemUser systemUser = new SystemUser();
            AuthServiceSoapClient authServiceSoapClient = new AuthServiceSoapClient();
            var username = loginName; 
            var userpassword = password;
            Asym_RSA rsa = new Asym_RSA();
            username = rsa.Encrypt(username);
            userpassword = rsa.Encrypt(userpassword);
            ColleageInnerTraining.Web.AuthServiceProxy.SystemUser AuthSystemUser = authServiceSoapClient.LoginAuth(username, userpassword);
            if (AuthSystemUser != null)
            {
                //UtilityTools.EntityCopy(AuthSystemUser, systemUser);
                systemUser.LoginName = loginName;
                systemUser.Password = password;
                systemUser.PhoneNumber = AuthSystemUser.PhoneNumber;
                systemUser.DisplayName = AuthSystemUser.DisplayName;
                systemUser.DepartmentName = AuthSystemUser.DepartmentName;
                systemUser.SysNO = AuthSystemUser.SysNO;
            }
            else
                return null;
            return systemUser;
        }

        /// <summary>
        /// 判断当前用户在当前应用程序中是否存在此权限Key
        /// </summary>
        /// <param name="authKey">权限Key</param>
        /// <returns></returns>
        public static bool HasFunction(AuthKey authKey)
        {
            bool result = false;
            AuthServiceSoapClient authServiceSoapClient = new AuthServiceSoapClient();
            result = authServiceSoapClient.CheckUserFunction(SystemDataConst.ApplicationID, authKey.Key, SystemDataConst.UserName);
            return result;
        }

        /// <summary>
        /// 判断当前用户在当前应用程序中是否存在此角色Key
        /// </summary>
        /// <param name="roleKey">角色Key</param>
        /// <returns></returns>
        public static bool HasRole(RoleKey roleKey)
        {
            bool result = false;
            AuthServiceSoapClient authServiceSoapClient = new AuthServiceSoapClient();

            var systemRoleList = authServiceSoapClient.GetUserFunction(SystemDataConst.ApplicationID, SystemDataConst.UserName).ToList<SystemRole>();
            if (systemRoleList != null && systemRoleList.Count > 0)
            {
                result = systemRoleList.Find(x => { return x.RoleName == roleKey.Key; }) != null;
            }
            return result;
        }

        public static bool ChangePassword(string loginName,string oldPassword,string newPassword)
        {
            bool result = false;
            AuthServiceSoapClient authServiceSoapClient = new AuthServiceSoapClient();

            var AuthSystemUser = authServiceSoapClient.ChangePassword(loginName, oldPassword, newPassword);
            if (AuthSystemUser != null)
            {
                result = true;
            }
            return result;


        }
        /// <summary>
        /// 获取当权用户在当前应用程序中的所有权限Key
        /// </summary>
        /// <returns></returns>
        public static List<AuthKey> GetUserAuthKey(string userName)
        {
            List<AuthKey> authKeyList = new List<AuthKey>();
            AuthServiceSoapClient authServiceSoapClient = new AuthServiceSoapClient();
            var result = authServiceSoapClient.GetUserAuthKey(SystemDataConst.ApplicationID,userName);
            foreach (var item in result)
            {
                authKeyList.Add(new AuthKey(item));
            }
            return authKeyList;
        }

        /// <summary>
        /// 获取当权用户在当前应用程序中的所有角色Key
        /// </summary>
        /// <returns></returns>
        public static List<RoleKey> GetUserRoleKey(string userName)
        {
            List<RoleKey> roleKeyList = new List<RoleKey>();
            AuthServiceSoapClient authServiceSoapClient = new AuthServiceSoapClient();
            var result = authServiceSoapClient.GetUserFunction(SystemDataConst.ApplicationID, userName);
            foreach (var item in result)
            {
                roleKeyList.Add(new RoleKey(item.RoleName));
            }
            return roleKeyList;
        }



    }

    public class AuthKey
    {
        public AuthKey(string key)
        {
            if (key == null || key.Trim().Length <= 0)
            {
                throw new Exception("The auth key cannot be null or empty!");
            }
            Key = key;
        }

        public string Key
        {
            get;
            private set;
        }
    }

    public class RoleKey
    {
        public RoleKey(string key)
        {
            if (key == null || key.Trim().Length <= 0)
            {
                throw new Exception("The auth key cannot be null or empty!");
            }
            Key = key;
        }

        public string Key
        {
            get;
            private set;
        }
    }

    public static partial class AuthKeyConst
    {
        #region [采购管理]

        public readonly static AuthKey POManagement_PO_Create = new AuthKey("POManagement_PO_Create");

        public readonly static AuthKey POManagement_PO_Audit = new AuthKey("POManagement_PO_Audit");

        #endregion

        #region [商品管理]

        public readonly static AuthKey CMS_Item_CreateItem = new AuthKey("CMS_Item_CreateItem");

        #endregion
    }

    public static partial class RoleKeyConst
    {
        #region [PM]

        public readonly static RoleKey CMS_Item_PMLeader = new RoleKey("CMS_Item_PMLeader");

        #endregion
    }

    public static partial class SystemDataConst
    {
        #region [应用程序ID]

        public readonly static string ApplicationID = "a81fd251-44d5-db2d-f55a-a1ab081ff8ca";

        #endregion

        #region Demo数据设置

        //实际项目中请使用一下代码获取UserName 
        //public readonly static string UserName = AppRuntime.BussinessContex.CurrentStore.UserName;

        //当前Demo 使用固定账号 Winnie 没有审核权限
        //public readonly static string UserName = "Winnie";

        //当前Demo 使用固定账号 Franky 有审核权限
        public readonly static string UserName = "Franky";
        #endregion
    }

    #region 辅助实体类


    [Serializable]
    public class SystemUser : SystemUserBase
    {
        public int CompanySysNO { get; set; }

        public string CompanyName { get; set; }

        /// <summary>
        /// Only in TrustedLogin
        /// </summary>
        public string CompanyCode { get; set; }
    }

    [Serializable]
    public class SystemUserBase
    {
        public SystemUserBase()
        {
            this.SysNO = 0;
            this.LoginName = string.Empty;
            this.DisplayName = string.Empty;
            this.DepartmentName = string.Empty;
            this.PhoneNumber = string.Empty;
            this.Email = string.Empty;
            this.Password = string.Empty;
            this.Status = UserStatus.Enabled;
            this.InUser = 0;
            this.InDate = DateTime.Now;
            this.EditUser = 0;
            this.EditDate = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        public int SysNO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int InUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime InDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int EditUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime EditDate { get; set; }

    }

    [XmlType(Namespace = "urn:Newegg.authcenter")]
    [XmlRoot(Namespace = "urn:Newegg.authcenter")]
    public enum UserStatus
    {
        Disabled = 0,
        Enabled = 1
    }

    #endregion

    #region 辅助工具类

    public static class UtilityTools
    {
        /// <summary>
        /// 实体类复制
        /// </summary>
        /// <param name="objold"></param>
        /// <param name="objnew"></param>
        public static void EntityCopy(object objold, object objnew)
        {
            Type myType = objold.GetType(),
                myType2 = objnew.GetType();
            PropertyInfo currobj = null;
            if (myType == myType2)
            {
                PropertyInfo[] myProperties = myType.GetProperties();
                for (int i = 0; i < myProperties.Length; i++)
                {
                    currobj = objold.GetType().GetProperties()[i];
                    currobj.SetValue(objnew, currobj.GetValue(objold, null), null);
                }
            }
        }

        /// <summary>
        /// 实体类克隆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="RealObject"></param>
        /// <returns></returns>
        public static T Clone<T>(T RealObject)
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, RealObject);
                objectStream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(objectStream);
            }
        }
    }

    #endregion
}
