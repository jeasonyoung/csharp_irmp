//================================================================================
//  FileName: SSOClientModule.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/14
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
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;

using iPower;
using iPower.Logs;
using iPower.IRMP.SSO;
namespace iPower.IRMP.SSOClient
{
    /// <summary>
    /// 单点登录客户端模块。
    /// </summary>
    public sealed class SSOClientModule : IHttpModule,IRequiresSessionState,IUserLogin
    {
        #region 成员变量，构造函数。
        SSOClientModuleConfiguration config = null;
        LogContainer log = null;
        SSOServicePoxy ssoServicePoxy = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public SSOClientModule()
        {
            this.config = SSOClientModuleConfiguration.ModuleConfig;
            this.log = new LogContainer(this.config);
        }
        #endregion

        #region 属性。
        /// <summary>
        /// 获取服务代理对象。
        /// </summary>
        SSOServicePoxy ServicePoxy
        {
            get
            {
                lock (this)
                {
                    if (this.ssoServicePoxy == null)
                    {
                        string serviceUrl = this.config.ServiceURL;
                        if (!string.IsNullOrEmpty(serviceUrl))
                        {
                            CredentialSoapHeader soapHeader = new CredentialSoapHeader();
                            soapHeader.AppSystemID = this.config.SystemID;
                            soapHeader.AppSystemPwd = this.config.AuthPwd;

                            this.ssoServicePoxy = new SSOServicePoxy();
                            this.ssoServicePoxy.Url = serviceUrl;
                            this.ssoServicePoxy.CredentialSoapHeaderValue = soapHeader;
                        }
                    }
                    return this.ssoServicePoxy;
                }
            }
        }
        #endregion

        #region IHttpModule 成员
        /// <summary>
        /// 处置由实现 IHttpModule 的模块使用的资源（内存除外）。
        /// </summary>
        public void Dispose()
        {
            
        }
        /// <summary>
        /// 初始化模块，并使其为处理请求做好准备。
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            if (context != null)
            {
                context.PreRequestHandlerExecute += new EventHandler(delegate(object sender, EventArgs e)
                {
                    #region 验证单点登录。
                    try
                    {
                        lock (this)
                        {
                            HttpApplication app = sender as HttpApplication;
                            if (app != null && app.Context != null)
                            {
                                IUser userInfo = app.Context.Handler as IUser;
                                if (userInfo != null)
                                {
                                    SSOAuthTicket ticket = this.CurrentAuthTicket(app.Context);
                                    if (ticket != null && ticket.HasValid)
                                    {
                                        SSOCallResult callResult = this.ServicePoxy.TicketVerification(ticket.ToString());
                                        if (callResult.ResultCode == 0)
                                        {
                                            ticket = new SSOAuthTicket(callResult.Ticket);
                                            userInfo.CurrentUserID = ticket.CurrentUserID;
                                            userInfo.CurrentUserName = ticket.CurrentUserName;
                                            this.SetCurrentAuthTicket(app.Context, ticket);
                                        }
                                        else
                                        {
                                            userInfo.CurrentUserID = GUIDEx.Null;
                                            userInfo.CurrentUserName = string.Empty;
                                            this.log.CreateWarningLog(callResult.ResultMessage);
                                        }
                                    }
                                    
                                    if (!userInfo.CurrentUserID.IsValid && !this.IsIgnore(app.Context.Request.Url))
                                    {
                                        this.RedirectLogin(app.Context);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.log.CreateErrorLog(ex.Message);
                        throw ex;
                    }
                    #endregion
                });
                context.PostRequestHandlerExecute += new EventHandler(delegate(object sender, EventArgs e)
                {
                    #region 验证用户系统授权。
                    lock (this)
                    {
                        HttpApplication app = sender as HttpApplication;
                        if (app != null && !this.IsIgnore(app.Context.Request.Url))
                        {
                            IUser userInfo = app.Context.Handler as IUser;
                            ISystem systemInfo = app.Context.Handler as ISystem;
                            if (userInfo != null && systemInfo != null && userInfo.CurrentUserID.IsValid && systemInfo.CurrentSystemID.IsValid)
                            {
                                CallResult callResult = this.ServicePoxy.UserAuthorizationVerification(userInfo.CurrentUserID, systemInfo.CurrentSystemID,
                                    app.Context.Request.UserHostAddress);
                                if (callResult != null && callResult.ResultCode == -1)
                                {
                                    throw new Exception(callResult.ResultMessage);
                                }
                            }
                        }
                    }
                    #endregion
                });
            }
        }

        #endregion

        #region 公开函数。
        /// <summary>
        /// 用户登录(不跳转)。
        /// </summary>
        /// <param name="userSign">用户登录账号。</param>
        /// <param name="password">用户登录密码。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>登录成功返回true,失败返回false。</returns>
        public bool SignInNotRedirect(string userSign, string password, out string err)
        {
            lock (this)
            {
                err = null;
                bool result = false;
                try
                {
                    if (string.IsNullOrEmpty(userSign) || string.IsNullOrEmpty(password))
                        err = "用户登录帐号或密码为空！";
                    else
                    {
                        SSOCallResult callResult = this.ServicePoxy.SignIn(userSign, password);
                        result = callResult.ResultCode == 0;
                        err = callResult.ResultMessage;
                        if (result)
                        {
                            SSOAuthTicket ticket = new SSOAuthTicket(callResult.Ticket);
                            result = this.SetCurrentAuthTicket(HttpContext.Current, ticket);
                        }
                    }
                }
                catch (Exception e)
                {
                    err = e.Message;
                }
                return result;
            }
        }
        #endregion

        #region IUserLogin 成员
        /// <summary>
        /// 用户登录。
        /// </summary>
        /// <param name="userSign">用户登录账号。</param>
        /// <param name="password">用户登录密码。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>登录成功返回true,失败返回false。</returns>
        public bool SignIn(string userSign, string password, out string err)
        {
            bool result = this.SignInNotRedirect(userSign, password, out err);
            if (result)
            {
                HttpContext context = HttpContext.Current;
                string requestUrl = context.Request[this.config.RequestUrlName];
                if (string.IsNullOrEmpty(requestUrl))
                    requestUrl = "~/";
                context.Response.Redirect(requestUrl, true);
            }
            return result;
        }
        /// <summary>
        /// 用户注销。
        /// </summary>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool SignOut(out string err)
        {
            lock (this)
            {
                err = null;
                bool result = false;
                string loginUrl = string.Empty;
                HttpContext context = null;
                try
                {
                    context = HttpContext.Current;
                    SSOAuthTicket ticket = this.CurrentAuthTicket(context);
                    if (ticket != null)
                    {
                        SSOCallResult callResult = this.ServicePoxy.SignOut(ticket.ToString());
                        err = callResult.ResultMessage;
                        result = callResult.ResultCode == 0;
                        if (result)
                            result = this.SetCurrentAuthTicket(context, ticket);
                    }
                    loginUrl = this.config.LoginURL;
                }
                catch (Exception e)
                {
                    err = e.Message;
                }
                finally
                {
                    if (string.IsNullOrEmpty(loginUrl))
                        loginUrl = "~/";
                    if (context != null)
                        context.Response.Redirect(loginUrl, true);
                }
                return result;
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
            lock (this)
            {
                err = null;
                bool result = false;
                try
                {
                    if (string.IsNullOrEmpty(userSign))
                        err = "用户账号为空！";
                    else if (string.IsNullOrEmpty(oldPassword))
                        err = "用户旧密码为空！";
                    else if (string.IsNullOrEmpty(newPassword))
                        err = "用户新密码为空！";
                    else
                    {
                        SSOCallResult callResult = this.ServicePoxy.ChangePassword(userSign, oldPassword, newPassword);
                        err = callResult.ResultMessage;
                        result = callResult.ResultCode == 0;
                    }
                }
                catch (Exception e)
                {
                    err = e.Message;
                }
                return result;
            }
        }
        #endregion

        #region 辅助函数。
        /// <summary>
        /// 验证URI是否是可以被忽略的。
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        bool IsIgnore(Uri uri)
        {
            Regex[] ignoredUrls = this.config.IgnoredUrls;
            if (uri != null && ignoredUrls != null)
            {
                string absoluteUri = uri.AbsoluteUri;
                foreach (Regex r in ignoredUrls)
                {
                    if (r.IsMatch(absoluteUri))
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 从当前上下文中获取票据。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        SSOAuthTicket CurrentAuthTicket(HttpContext context)
        {
            lock (this)
            {
                iPower.IRMP.SSO.SSOAuthTicket ticket = null;
                string ticketName = this.config.TicketName;
                if (context != null && !string.IsNullOrEmpty(ticketName))
                {
                    switch (this.config.LocalStorage)
                    {
                        case EnumLocalStorage.Cookies:
                            HttpCookie cookie = context.Request.Cookies[ticketName];
                            if (cookie != null)
                                ticket = new SSOAuthTicket(cookie.Value);
                            break;
                        case EnumLocalStorage.Session:
                            object obj = context.Session[ticketName];
                            if (obj != null)
                                ticket = new SSOAuthTicket(obj.ToString());
                            break;
                    }
                }
                return ticket;
            }
        }
        /// <summary>
        /// 设置当前上下文的票据。
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ticket"></param>
        /// <returns></returns>
        bool SetCurrentAuthTicket(HttpContext context, SSOAuthTicket ticket)
        {
            bool result = false;
            string ticketName = this.config.TicketName;
            if (context != null && ticket != null && !string.IsNullOrEmpty(ticketName))
            {
                switch (this.config.LocalStorage)
                {
                    case EnumLocalStorage.Cookies:
                        HttpCookie cookie = context.Request.Cookies[ticketName];
                        if (cookie != null)
                        {
                            cookie.Value = ticket.ToString();
                            context.Response.SetCookie(cookie);
                        }
                        else
                        {
                            cookie = new HttpCookie(ticketName, ticket.ToString());
                            context.Response.AppendCookie(cookie);
                        }
                        result = true;
                        break;
                    case EnumLocalStorage.Session:
                        context.Session[ticketName] = ticket.ToString();
                        result = true;
                        break;
                }
            }
            return result;
        }
        /// <summary>
        /// 跳转到登录。
        /// </summary>
        /// <param name="context"></param>
        void RedirectLogin(HttpContext context)
        {
            string loginUrl = this.config.LoginURL;
            string requestUrlName = this.config.RequestUrlName;
            if (context != null && !string.IsNullOrEmpty(loginUrl) && !string.IsNullOrEmpty(requestUrlName))
            {
                Uri uri = context.Request.Url;
                if (!this.IsIgnore(uri))
                {
                    string requestUrl = string.Format("{0}{1}={2}",
                        loginUrl.IndexOf('?') > 0 ? "&" : "?",
                        requestUrlName,
                        HttpUtility.UrlEncode(uri.AbsoluteUri));
                    context.Response.Redirect(string.Format("{0}{1}", loginUrl, requestUrl), true);
                }
            }
        }
        #endregion
    }
}
