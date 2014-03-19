//================================================================================
//  FileName: AuthenticationProviderService.asmx.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/16
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
//  ----    ------  -----------------
//
//================================================================================
//  Copyright (C) 2004-2009 Jeason Young Corporation
//================================================================================
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;

using iPower;
using iPower.IRMP.Org.Engine;
namespace iPower.IRMP.Org.Web
{
    /// <summary>
    /// 单点登录身份认证。
    /// </summary>
    [WebService(Namespace = "http://ipower.org/",
                Name = "单点登录身份认证。",
                Description = "单点登录身份认证。")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AuthenticationService : System.Web.Services.WebService
    {
        #region 成员变量，构造函数。
        AuthenticationProvider provider = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public AuthenticationService()
        {
            this.provider = new AuthenticationProvider();
        }
        #endregion

         /// <summary>
        /// 验证用户账号和密码。
        /// </summary>
        /// <param name="userSign">用户账号。</param>
        /// <param name="password">密码。</param>
        /// <param name="userInfo">用户信息。</param>
        /// <returns>如果用户名和密码有效，则返回用户信息；否则返回null。</returns>
        [WebMethod(Description = "验证用户账号和密码。")]
        public CallResult UserAuthorizationVerification(string userSign, string password, out UserInfo userInfo)
        {
            try
            {
                string err = null;
                userInfo = null;
                IUser user = this.provider.UserAuthorizationVerification(userSign, password, out err);
                if (user != null)
                {
                    userInfo = new UserInfo();
                    userInfo.CurrentUserID = user.CurrentUserID;
                    userInfo.CurrentUserName = user.CurrentUserName;
                }
                return new CallResult(userInfo == null ? -1 : 0, err);
            }
            catch (Exception e)
            {
                userInfo = null;
                return new CallResult(-1, e.Message);
            }
        }
         /// <summary>
        /// 修改用户登录密码。
        /// </summary>
        /// <param name="userSign">用户登录账号。</param>
        /// <param name="oldPassword">用户旧密码。</param>
        /// <param name="newPassword">用户新密码。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>如果修改成功，则返回True；否则返回false。</returns>
        [WebMethod(Description = " 修改用户登录密码。")]
        public CallResult ChangePassword(string userSign, string oldPassword, string newPassword)
        {
            try
            {
                string err = null;
                bool result = this.provider.ChangePassword(userSign, oldPassword, newPassword, out err);
                return new CallResult(result ? 0 : -1, err);
            }
            catch (Exception e)
            {
                return new CallResult(-1, e.Message);
            }
        }
    }

    /// <summary>
    /// 用户信息。
    /// </summary>
    [XmlRoot("UserInfo")]
    public class UserInfo : IUser
    {
        #region IUser 成员
        /// <summary>
        /// 用户ID。
        /// </summary>
        [XmlElement("CurrentUserID", typeof(string))]
        public GUIDEx CurrentUserID
        {
            get;
            set;
        }
        /// <summary>
        /// 用户名称。
        /// </summary>
        [XmlElement("CurrentUserName")]
        public string CurrentUserName
        {
            get;
            set;
        }

        #endregion
    }
}
