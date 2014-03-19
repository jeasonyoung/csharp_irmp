//================================================================================
//  FileName: SSOServiceModuleConfiguration.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/9
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
using System.Collections;
using System.Collections.Generic;
using System.Web;

using iPower;
using iPower.Logs;
using iPower.Utility;
using iPower.Configuration;
using iPower.IRMP.SSO;
using iPower.IRMP.SysMgr;
namespace iPower.IRMP.SSOService
{
    /// <summary>
    /// 单点登录服务器端配置键。
    /// </summary>
    internal static class SSOServiceModuleConfigurationKeys
    {
        /// <summary>
        /// 提供用户身份认证程序集键名。
        /// </summary>
        public const string AuthenticationProviderAssemblyKey = "iPower.SSO.AuthenticationProviderAssembly";
        /// <summary>
        /// 提供授权验证程序集键名。
        /// </summary>
        public const string AuthorizedToVerifyAssemblyKey = "iPower.SSO.AuthorizedToVerifyAssembly";
        /// <summary>
        /// 提供票据存储程序集键名。
        /// </summary>
        public const string SSOTicketDbProviderAssemblyKey = "iPower.SSO.SSOTicketDbProviderAssembly";
        /// <summary>
        /// 票据有效期长度(分钟)键名。
        /// </summary>
        public const string TicketExpiredIntervalKey = "iPower.SSO.TicketExpiredInterval";
        /// <summary>
        /// 日志文件头键名。
        /// </summary>
        public const string LogFileHeadKey = "iPower.Logs.FileHead";
        /// <summary>
        /// 日志文件生成规则(Year-年，Month-月，Week-周，Date-日， Hour-时)键名。
        /// </summary>
        public const string LogFileRuleKey = "iPower.Logs.LogFileRule";
    }
    /// <summary>
    ///  单点登录服务器端配置类。
    /// </summary>
    internal class SSOServiceModuleConfiguration : iPowerConfiguration, ILogFileHead
    {
        #region 成员变量，构造函数。
        static Hashtable Cache = Hashtable.Synchronized(new Hashtable());
        /// <summary>
        /// 构造函数。
        /// </summary>
        public SSOServiceModuleConfiguration()
            : base("SSOService")
        {
        }
        #endregion

        #region 静态实例。
        static SSOServiceModuleConfiguration m_config;
        static object synchronizationObject = new object();
        /// <summary>
        /// 获取静态实例。
        /// </summary>
        public static SSOServiceModuleConfiguration ModuleConfig
        {
            get
            {
                lock (synchronizationObject)
                {
                    m_config = new SSOServiceModuleConfiguration();
                    return m_config;
                }
            }
        }
        #endregion


        /// <summary>
        /// 获取用户身份认证程序集。
        /// </summary>
        public IAuthenticationProvider AuthenticationProvideAssembly
        {
            get
            {
                lock (this)
                {
                    IAuthenticationProvider provider = Cache[SSOServiceModuleConfigurationKeys.AuthenticationProviderAssemblyKey] as IAuthenticationProvider;
                    if (provider == null)
                    {
                        string path = this[SSOServiceModuleConfigurationKeys.AuthenticationProviderAssemblyKey];
                        if (!string.IsNullOrEmpty(path))
                        {
                            if (path.EndsWith(".asmx", StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase))
                                provider = new AuthenticationProvider(path);
                            else
                                provider = TypeHelper.Create(path) as IAuthenticationProvider;
                        }
                        if (provider != null)
                            Cache[SSOServiceModuleConfigurationKeys.AuthenticationProviderAssemblyKey] = provider;
                    }
                    return provider;
                }
            }
        }
        /// <summary>
        /// 获取授权验证程序集。
        /// </summary>
        public IAuthorizedToVerify AuthorizedToVerifyAssembly
        {
            get
            {
                lock (this)
                {
                    IAuthorizedToVerify authorizedToVerify = Cache[SSOServiceModuleConfigurationKeys.AuthorizedToVerifyAssemblyKey] as IAuthorizedToVerify;
                    if (authorizedToVerify == null)
                    {
                        string path = this[SSOServiceModuleConfigurationKeys.AuthorizedToVerifyAssemblyKey];
                        if (!string.IsNullOrEmpty(path))
                        {
                            if (path.EndsWith(".asmx", StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase))
                                authorizedToVerify = new AuthorizedToVerifyProvider(path);
                            else
                                authorizedToVerify = TypeHelper.Create(path) as IAuthorizedToVerify;
                        }
                        
                        if (authorizedToVerify != null)
                            Cache[SSOServiceModuleConfigurationKeys.AuthorizedToVerifyAssemblyKey] = authorizedToVerify;
                    }
                    return authorizedToVerify;
                }
            }
        }
        /// <summary>
        /// 获取票据存储程序集。
        /// </summary>
        public ISSOTicketDbProvider SSOTicketDbProviderAssembly
        {
            get
            {
                lock (this)
                {
                    ISSOTicketDbProvider provider = Cache[SSOServiceModuleConfigurationKeys.SSOTicketDbProviderAssemblyKey] as ISSOTicketDbProvider;
                    if (provider == null)
                    {
                        string path = this[SSOServiceModuleConfigurationKeys.SSOTicketDbProviderAssemblyKey];
                        if (!string.IsNullOrEmpty(path))
                        {
                            if (path.EndsWith(".asmx", StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase))
                                provider = new SSOTicketDbProvider(path);
                            else
                                provider = TypeHelper.Create(path) as ISSOTicketDbProvider;
                        }
                        if (provider != null)
                            Cache[SSOServiceModuleConfigurationKeys.SSOTicketDbProviderAssemblyKey] = provider;
                    }
                    return provider;
                }
            }
        }
        /// <summary>
        /// 获取票据有效期长度(分钟)。
        /// </summary>
        public double TicketExpiredInterval
        {
            get
            {
                try
                {
                    return double.Parse(this[SSOServiceModuleConfigurationKeys.TicketExpiredIntervalKey]);
                }
                catch (Exception)
                {
                    return 30.0;
                }
            }
        }

        #region ILogFileHead 成员
        /// <summary>
        /// 获取日志文件头。
        /// </summary>
        public string LogFileHead
        {
            get { return this[SSOServiceModuleConfigurationKeys.LogFileHeadKey]; }
        }
        /// <summary>
        /// 获取日志文件生成规则(Year-年，Month-月，Week-周，Date-日， Hour-时)。
        /// </summary>
        public EnumLogFileRule LogFileRule
        {
            get
            {
                try
                {
                    string strRule = this[SSOServiceModuleConfigurationKeys.LogFileRuleKey];
                    if (!string.IsNullOrEmpty(strRule))
                        return (EnumLogFileRule)Enum.Parse(typeof(EnumLogFileRule), strRule);
                    return EnumLogFileRule.Week;
                }
                catch (Exception)
                {
                    return EnumLogFileRule.Week;
                }
            }
        }

        #endregion
    }

    #region 内置类。
    /// <summary>
    /// 
    /// </summary>
    class AuthenticationProvider : IAuthenticationProvider
    {
        #region 成员变量，构造函数。
        Poxy.AuthenticationProviderService providerService = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="serviceURL"></param>
        public AuthenticationProvider(string serviceURL)
        {
            this.providerService = new iPower.IRMP.SSOService.Poxy.AuthenticationProviderService();
            this.providerService.Url = serviceURL;
        }
        #endregion

        #region IAuthenticationProvider 成员

        public IUser UserAuthorizationVerification(string userSign, string password, out string err)
        {
            try
            {
                Poxy.UserInfo userInfo = null;
                Poxy.CallResult callResult = this.providerService.UserAuthorizationVerification(userSign, password, out userInfo);
                err = callResult.ResultMessage;
                if (callResult.ResultCode == 0 && userInfo != null)
                {
                    UserInfo info = new UserInfo();
                    info.CurrentUserID = userInfo.CurrentUserID;
                    info.CurrentUserName = userInfo.CurrentUserName;
                    return (IUser)info;
                }
                return null;
            }
            catch (Exception e)
            {
                err = e.Message;
                return null;
            }
        }

        public bool ChangePassword(string userSign, string oldPassword, string newPassword, out string err)
        {
            try
            {
                err = null;
                Poxy.CallResult callResult = this.providerService.ChangePassword(userSign, oldPassword, newPassword);
                if (callResult != null)
                {
                    err = callResult.ResultMessage;
                    return callResult.ResultCode == 0;
                }
                return false;
            }
            catch (Exception e)
            {
                err = e.Message;
                return false;
            }
        }

        #endregion

        #region 内置类。
        class UserInfo : IUser
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
    /// <summary>
    /// 
    /// </summary>
    class AuthorizedToVerifyProvider : IAuthorizedToVerify
    {
        #region 成员变量，构造函数。
        Poxy.AuthorizedToVerifyProviderService providerService = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="serviceURL"></param>
        public AuthorizedToVerifyProvider(string serviceURL)
        {
            this.providerService = new iPower.IRMP.SSOService.Poxy.AuthorizedToVerifyProviderService();
            this.providerService.Url = serviceURL;
        }
        #endregion

        #region IAuthorizedToVerify 成员

        public bool AppAuthorization(GUIDEx systemID, string authPassword, out string err)
        {
            try
            {
                Poxy.CallResult callResult = this.providerService.AppAuthorization(systemID, authPassword);
                err = callResult.ResultMessage;
                return callResult.ResultCode == 0;
            }
            catch (Exception e)
            {
                err = e.Message;
                return false;
            }
        }

        public bool UserAuthorizationVerification(GUIDEx employeeID, GUIDEx systemID, string clientIP, out string err)
        {
            try
            {
                Poxy.CallResult callResult = this.providerService.UserAuthorizationVerification(employeeID, systemID, clientIP);
                err = callResult.ResultMessage;
                return callResult.ResultCode == 0;
            }
            catch (Exception e)
            {
                err = e.Message;
                return false;
            }
        }

        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    class SSOTicketDbProvider : ISSOTicketDbProvider
    {
        #region 成员变量，构造函数。
        Poxy.SSOTicketDbProviderService providerService = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="serviceURL"></param>
        public SSOTicketDbProvider(string serviceURL)
        {
            this.providerService = new iPower.IRMP.SSOService.Poxy.SSOTicketDbProviderService();
            this.providerService.Url = serviceURL;
        }
        #endregion

        #region ISSOTicketDbProvider 成员

        public bool CreateTicket(SSOAuthTicket ticket, string clientIP)
        {
            Poxy.CallResult callResult = this.providerService.CreateTicket(ticket.ToString(), clientIP);
            bool result = (callResult.ResultCode == 0);
            if (!result)
                throw new Exception(callResult.ResultMessage);
            return result;
        }

        public bool TicketVerification(ref SSOAuthTicket ticket, string clientIP, out string err)
        {
            try
            {
                Poxy.SSOCallResult callResult = this.providerService.TicketVerification(ticket.ToString(), clientIP);
                err = callResult.ResultMessage;
                bool result = callResult.ResultCode == 0;
                if (result)
                    ticket = new SSOAuthTicket(callResult.Ticket);
                return result;
            }
            catch (Exception e)
            {
                err = e.Message;
                return false;
            }
        }

        public bool DestroyTicket(ref SSOAuthTicket ticket, string clientIP)
        {
            Poxy.SSOCallResult callResult = this.providerService.DestroyTicket(ticket.ToString(), clientIP);
            bool result = callResult.ResultCode == 0;
            if (result)
                ticket = new SSOAuthTicket(callResult.Ticket);
            else
                throw new Exception(callResult.ResultMessage);
            return result;
        }

        #endregion
    }
    #endregion
}
