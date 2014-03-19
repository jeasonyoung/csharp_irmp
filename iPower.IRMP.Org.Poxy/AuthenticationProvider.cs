//================================================================================
//  FileName: AuthenticationProvider.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2012-01-10 
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
using System.Text;
using iPower.Logs;
using iPower.IRMP.SSO;

namespace iPower.IRMP.Org.Poxy
{
    /// <summary>
    /// 提供单点登录身份认证。
    /// </summary>
    public class AuthenticationProvider : IAuthenticationProvider
    {
        #region 成员变量，构造函数。
        LogContainer log = null;
        ModuleConfiguration config = null;
        AuthenticationServicePoxy poxy;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public AuthenticationProvider()
        {
            this.config = ModuleConfiguration.ModuleConfig;
            this.log = new LogContainer(this.config);
            this.poxy = new AuthenticationServicePoxy();
            this.poxy.Url = this.config.AuthenticationProviderURL;
        }
        #endregion

        #region IAuthenticationProvider 成员
        /// <summary>
        /// 验证用户名和密码。
        /// </summary>
        /// <param name="userSign">用户账号。</param>
        /// <param name="password">用户密码。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>如果用户名和密码有效，则返回用户信息；否则返回null。</returns>
        public IUser UserAuthorizationVerification(string userSign, string password, out string err)
        {
            try
            {
                UserInfo userInfo = null;
                CallResult result = this.poxy.UserAuthorizationVerification(userSign, password, out userInfo);
                err = result.ResultMessage;
                if (result.ResultCode == 0 && userInfo != null)
                {
                    EmployeeUser user = new EmployeeUser();
                    user.CurrentUserID = userInfo.CurrentUserID;
                    user.CurrentUserName = userInfo.CurrentUserName;
                    return user;
                }
                return null;
            }
            catch (Exception e)
            {
                this.log.CreateErrorLog(err = e.Message);
                throw e;
            }
        }
        /// <summary>
        /// 修改密码。
        /// </summary>
        /// <param name="userSign">用户账号。</param>
        /// <param name="oldPassword">用户旧密码。</param>
        /// <param name="newPassword">用户新密码。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>如果修改成功，则返回True；否则返回false。</returns>
        public bool ChangePassword(string userSign, string oldPassword, string newPassword, out string err)
        {
            try
            {
                CallResult result = this.poxy.ChangePassword(userSign, oldPassword, newPassword);
                err = result.ResultMessage;
                return result.ResultCode == 0;
            }
            catch (Exception e)
            {
                this.log.CreateErrorLog(err = e.Message);
                throw e;
            }
        }

        #endregion

        #region 内置类。
        class EmployeeUser : IUser
        {
            #region IUser 成员

            public GUIDEx CurrentUserID
            {
                get;
                set;
            }

            public string CurrentUserName
            {
                get;
                set;
            }

            #endregion
        }
        #endregion
    }
}
