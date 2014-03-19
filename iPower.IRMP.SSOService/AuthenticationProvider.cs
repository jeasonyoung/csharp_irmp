//================================================================================
//  FileName: AuthenticationProvider.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/10
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

using iPower;
using iPower.Logs;
using iPower.IRMP.SSO;
using iPower.IRMP.SysMgr;
namespace iPower.IRMP.SSOService
{
    /// <summary>
    /// 验证提供类。
    /// </summary>
    internal class AuthenticationProviderFactory
    {
        #region 成员变量，构造函数。
        SSOServiceModuleConfiguration config = null;
        IAuthenticationProvider authenticationProvider = null;
        IAuthorizedToVerify authorizedToVerify = null;
        ISSOTicketDbProvider ssoTicketDbProvider = null;
        LogContainer log = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public AuthenticationProviderFactory(SSOServiceModuleConfiguration moduleConfig)
        {
            try
            {
                this.config = moduleConfig;
                this.log = new LogContainer(moduleConfig);

                this.authenticationProvider = this.config.AuthenticationProvideAssembly;
                this.authorizedToVerify = this.config.AuthorizedToVerifyAssembly;
                this.ssoTicketDbProvider = this.config.SSOTicketDbProviderAssembly;
            }
            catch (Exception e)
            {
                this.log.CreateErrorLog(e.Message);
            }
        }
        #endregion

        #region 公开函数。
        /// <summary>
        /// 验证系统授权。
        /// </summary>
        /// <param name="systemID">系统ID。</param>
        /// <param name="authPassword">授权密码。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>获得授权返回true,否则返回false。</returns>
        public bool AppAuthorizationVerification(string systemID, string systemPwd, out string err)
        {
            bool result = false;
            try
            {
                if (string.IsNullOrEmpty(systemID))
                    err = "系统ID为空！";
                else if (string.IsNullOrEmpty(systemPwd))
                    err = "授权密码为空！";
                else
                     result = this.authorizedToVerify.AppAuthorization(systemID, systemPwd, out err);
             }
            catch (Exception e)
            {
                err = e.Message;
            }
            return result;
        }
        /// <summary>
        /// 验证用户授权。
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <param name="systemID">系统ID。</param>
        /// <param name="clientIP">用户登录IP地址。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>获得授权返回true,否则返回false。</returns>
        public bool UserAuthorizationVerification(GUIDEx employeeID, GUIDEx systemID, string clientIP, out string err)
        {
            bool result = false;
            err = null;
            try
            {
                if (!employeeID.IsValid)
                    err = "用户ID为空！";
                else if (!systemID.IsValid)
                    err = "授权密码不能为空！";
                else if (string.IsNullOrEmpty(clientIP))
                    err = "用户登录IP地址为空！";
                else
                    result = this.authorizedToVerify.UserAuthorizationVerification(employeeID, systemID, clientIP, out err);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return result;
        }
        /// <summary>
        /// 验证用户名和密码。
        /// </summary>
        /// <param name="systemID">系统ID。</param>
        /// <param name="userSign">用户账号。</param>
        /// <param name="password">用户密码。</param>
        /// <param name="clientIP">客户端IP地址。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>如果用户名和密码有效，则返回用户信息；否则返回null。</returns>
        public SSOAuthTicket UserAuthorizationVerification(string systemID, string userSign, string password, string clientIP, out string err)
        {
            SSOAuthTicket ticket = null;
            err = null;
            try
            {
                if (string.IsNullOrEmpty(systemID))
                    throw new ArgumentNullException("systemID", "系统ID不能为空！");
                else if (string.IsNullOrEmpty(userSign))
                    throw new ArgumentNullException("userSign", "用户账号不能为空！");
                else if (string.IsNullOrEmpty(password))
                    throw new ArgumentNullException("password", "用户密码不能为空！");
                else if (string.IsNullOrEmpty(clientIP))
                    throw new ArgumentNullException("clientIP", "客户端IP地址不能为空！");
                IUser userInfo = this.authenticationProvider.UserAuthorizationVerification(userSign, password, out err);
                if (userInfo != null)
                {
                    bool result = this.authorizedToVerify.UserAuthorizationVerification(userInfo.CurrentUserID, systemID, clientIP, out err);
                    if (result)
                    {
                        DateTime issueDate = DateTime.Now;
                        DateTime expiration = issueDate.AddMinutes(this.config.TicketExpiredInterval);
                        ticket = new SSOAuthTicket(GUIDEx.New, userInfo, issueDate, expiration);
                        result = this.ssoTicketDbProvider.CreateTicket(ticket, clientIP);
                        if (!result)
                        {
                            ticket = null;
                            throw new Exception("创建票据存储发生错误！");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return ticket;
        }
        /// <summary>
        /// 验证票据的存储。
        /// </summary>
        /// <param name="ticket">票据。</param>
        /// <param name="clientIP">客户端IP地址。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns></returns>
        public bool TicketVerification(ref SSOAuthTicket ticket,string clientIP, out string err)
        {
            bool result = false;
            err = null;
            try
            {
                if (string.IsNullOrEmpty(clientIP))
                    throw new ArgumentNullException("clientIP", "客户端IP地址不能为空！");
                if (!ticket.HasValid)
                    err = "票据无效！";
                else
                    result = this.ssoTicketDbProvider.TicketVerification(ref ticket, clientIP, out err);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return result;
        }
        /// <summary>
        /// 销毁票据。
        /// </summary>
        /// <param name="ticket">票据。</param>
        /// <param name="clientIP">客户端IP地址。</param>
        /// <returns></returns>
        public bool DestroyTicket(ref SSOAuthTicket ticket, string clientIP)
        {
            if (string.IsNullOrEmpty(clientIP))
                throw new ArgumentNullException("clientIP", "客户端IP地址不能为空！");
            return this.ssoTicketDbProvider.DestroyTicket(ref ticket, clientIP);
        }
        /// <summary>
        /// 修改密码。
        /// </summary>
        /// <param name="userSign">用户名。</param>
        /// <param name="oldPassword">旧密码。</param>
        /// <param name="newPassword">新密码。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>如果修改成功，则返回True；否则返回false。</returns>
        public bool ChangePassword(string userSign, string oldPassword, string newPassword, out string err)
        {
            bool result = false;
            err = null;
            try
            {
                if (string.IsNullOrEmpty(userSign))
                    err = "用户账号不能为空！";
                else if (string.IsNullOrEmpty(oldPassword))
                    err = "旧用户密码不能为空！";
                else if (string.IsNullOrEmpty(newPassword))
                    err = "新用户密码不能为空！";
                else
                    result = this.authenticationProvider.ChangePassword(userSign, oldPassword, newPassword, out err);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return result;
        }

        #endregion
    }
}
