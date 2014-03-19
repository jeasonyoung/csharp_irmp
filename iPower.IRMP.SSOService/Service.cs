//================================================================================
//  FileName: Service.asmx.cs
//  Desc:单点登录服务。
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/5/26
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
using System.Web.Services.Protocols;
using System.ComponentModel;

using iPower;
using iPower.Logs;
using iPower.IRMP.SSO;
using iPower.Handlers;
namespace iPower.IRMP.SSOService
{
    /// <summary>
    /// 单点登录服务。
    /// </summary>
    [WebService(Namespace = "http://ipower.org/",
                Name = "单点登录服务。",
                Description = "单点登录服务。")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public sealed class Service : WebServiceHandler
    {
        #region 成员变量，构造函数。
        LogContainer log;
        CredentialSoapHeader credentials;
        AuthenticationProviderFactory factory;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public Service()
        {
            this.credentials = new CredentialSoapHeader();
            this.log = new LogContainer(SSOServiceModuleConfiguration.ModuleConfig);
            this.factory = new AuthenticationProviderFactory(SSOServiceModuleConfiguration.ModuleConfig);
        }
        #endregion

        #region 属性。
        /// <summary>
        /// 获取或设置验证。
        /// </summary>
        public CredentialSoapHeader Credentials
        {
            get { return this.credentials; }
            set { this.credentials = value; }
        }
        #endregion

        #region 辅助函数。
        /// <summary>
        /// 验证系统访问授权。
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        bool VerifyCredential(Service service)
        {
            try
            {
                if (service != null && service.Credentials != null)
                {
                    string appSystemID = service.Credentials.AppSystemID;
                    string appSystemPwd = service.Credentials.AppSystemPwd;
                    if (string.IsNullOrEmpty(appSystemID) || string.IsNullOrEmpty(appSystemPwd))
                        throw new ArgumentException("访问该函数需要使用应用系统ID和密码。");
                    string err = null;
                    bool result = this.factory.AppAuthorizationVerification(appSystemID, appSystemPwd, out err);
                    if (!result)
                        throw new Exception(err);
                    return result;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new SoapException(e.Message, SoapException.ClientFaultCode, e);
            }
        }
        #endregion

        #region SSO函数处理。
        /// <summary>
        /// 用户登录验证。
        /// </summary>
        /// <param name="userSign">用户账号。</param>
        /// <param name="password">用户密码。</param>
        /// <returns>返回结果。</returns>
        [SoapHeader("Credentials")]
        [WebMethod(Description = "用户登录验证。<br/>userSign:用户账号。<br/> password:用户密码。")]
        public SSOCallResult SignIn(string userSign, string password)
        {
            try
            {
                SSOCallResult callResult = null;
                if (this.VerifyCredential(this))
                {
                    if (string.IsNullOrEmpty(userSign) || string.IsNullOrEmpty(password))
                        callResult = new SSOCallResult(-1, "账号或密码不能为空！");
                    else
                    {
                        string err = null;
                        SSOAuthTicket ticket = this.factory.UserAuthorizationVerification(this.Credentials.AppSystemID, userSign, password, this.Context.Request.UserHostAddress, out err);
                        if (ticket == null)
                            callResult = new SSOCallResult(-1, err);
                        else
                            callResult = new SSOCallResult(0, ticket.ToString(), "登录成功。");
                    }
                }
                return callResult;
            }
            catch (Exception e)
            {
                this.log.CreateErrorLog(e.Message);
                return new SSOCallResult(-1, e.Message);
            }
        }
        /// <summary>
        /// 验证票据。
        /// </summary>
        /// <param name="ticket">票据。</param>
        /// <returns>返回结果。</returns>
        [SoapHeader("Credentials")]
        [WebMethod(Description = "验证票据。<br/>ticket:票据。")]
        public SSOCallResult TicketVerification(string ticket)
        {
            try
            {
                SSOCallResult callResult = null;
                if (this.VerifyCredential(this))
                {
                    if (string.IsNullOrEmpty(ticket))
                        throw new ArgumentNullException("ticket", "票据为空！");
                    SSOAuthTicket authTicket = new SSOAuthTicket(ticket);
                    if (authTicket == null)
                        throw new ArgumentException("票据格式不正确！");
                    if (!authTicket.HasValid)
                        callResult = new SSOCallResult(-1, "票据无效！");
                    else
                    {
                        string clientIP = this.Context.Request.UserHostAddress;
                        string err = null;
                        bool result = this.factory.TicketVerification(ref authTicket, clientIP, out err);
                        if (result)
                            callResult = new SSOCallResult(0, authTicket.ToString(), "验证票据成功。");
                        else
                            callResult = new SSOCallResult(-1, err);
                    }
                }
                return callResult;
            }
            catch (Exception e)
            {
                this.log.CreateErrorLog(e.Message);
                return new SSOCallResult(-1, e.Message);
            }
        }
        /// <summary>
        /// 注销用户。
        /// </summary>
        /// <param name="ticket">票据。</param>
        /// <returns>返回结果。</returns>
        [SoapHeader("Credentials")]
        [WebMethod(Description = "注销用户。<br/>ticket:票据。")]
        public SSOCallResult SignOut(string ticket)
        {
            try
            {
                SSOCallResult callResult = null;
                if (this.VerifyCredential(this))
                {
                    if (string.IsNullOrEmpty(ticket))
                        throw new ArgumentNullException("ticket", "票据为空！");
                    SSOAuthTicket authTicket = new SSOAuthTicket(ticket);
                    if (authTicket == null)
                        throw new ArgumentException("票据格式不正确！");
                    if (!authTicket.HasValid)
                        callResult = new SSOCallResult(-1, "票据无效！");
                    else
                    {
                        string clientIP = this.Context.Request.UserHostAddress;
                        bool result = this.factory.DestroyTicket(ref authTicket, clientIP);
                        if (result)
                            callResult = new SSOCallResult(0, authTicket.ToString(), "注销用户成功。");
                        else
                            callResult = new SSOCallResult(-1, "注销失败。");
                    }
                }
                return callResult;
            }
            catch (Exception e)
            {
                this.log.CreateErrorLog(e.Message);
                return new SSOCallResult(-1, e.Message);
            }
        }
        /// <summary>
        /// 修改用户密码。
        /// </summary>
        /// <param name="userSign">用户账号。</param>
        /// <param name="oldPassword">旧用户密码。</param>
        /// <param name="newPassword">新用户密码。</param>
        /// <returns>如果修改成功，则返回True；否则返回false。</returns>
        [SoapHeader("Credentials")]
        [WebMethod(Description = "修改用户密码。<br/>userSign:用户账号。<br/>oldPassword:旧用户密码。<br/>newPassword:新用户密码。")]
        public SSOCallResult ChangePassword(string userSign, string oldPassword, string newPassword)
        {
            try
            {
                SSOCallResult callResult = null;
                if (this.VerifyCredential(this))
                {
                    if (string.IsNullOrEmpty(userSign))
                        throw new ArgumentNullException("userSign", "用户账号不能为空！");
                    else if (string.IsNullOrEmpty(oldPassword))
                        throw new ArgumentNullException("oldPassword", "旧用户密码不能为空！");
                    else if (string.IsNullOrEmpty(newPassword))
                        throw new ArgumentNullException("newPassword", "新用户密码不能为空！");
                    else
                    {
                        string err = null;
                        bool result = this.factory.ChangePassword(userSign, oldPassword, newPassword, out err);
                        if (result)
                            callResult = new SSOCallResult(0, "修改用户密码成功。");
                        else
                            callResult = new SSOCallResult(-1, err);
                    }
                }
                return callResult;
            }
            catch (Exception e)
            {
                this.log.CreateErrorLog(e.Message);
                return new SSOCallResult(-1, e.Message);
            }
        }
        /// <summary>
        /// 验证用户系统授权。
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <param name="systemID">系统ID。</param>
        /// <param name="clientIP">用户登录IP地址。</param>
        /// <returns>获得授权返回true,否则返回false。</returns>
        [SoapHeader("Credentials")]
        [WebMethod(Description = "验证用户系统授权。<br/>employeeID:用户ID。<br/>systemID:系统ID。<br/>clientIP:用户登录IP地址。")]
        public CallResult UserAuthorizationVerification(string employeeID, string systemID, string clientIP)
        {
            try
            {
                CallResult callResult = null;
                if (this.VerifyCredential(this))
                {
                    if (string.IsNullOrEmpty(employeeID))
                        throw new ArgumentNullException("employeeID", "用户ID为空！");
                    else if (string.IsNullOrEmpty(systemID))
                        throw new ArgumentNullException("systemID", "系统ID为空！");
                    else if (string.IsNullOrEmpty(clientIP))
                        throw new ArgumentNullException("newPassword", "用户登录IP地址为空！");
                    else
                    {
                        string err = null;
                        bool result = this.factory.UserAuthorizationVerification(employeeID, systemID, clientIP, out err);
                        callResult = new CallResult(result ? 0 : -1, err);
                    }
                }
                return callResult;
            }
            catch (Exception e)
            {
                this.log.CreateErrorLog(e.Message);
                return new CallResult(-1, e.Message);
            }
        }
        #endregion
    }
}