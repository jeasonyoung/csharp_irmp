//================================================================================
//  FileName: SSOClientModuleConfiguration.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using iPower;
using iPower.Logs;
using iPower.Configuration;
using iPower.IRMP.SSO;
namespace iPower.IRMP.SSOClient
{
    /// <summary>
    /// 单点登录客户端配置键。
    /// </summary>
    internal static class SSOClientModuleConfigurationKeys
    {
        /// <summary>
        /// 用户验证忽略的页面的正则表达式键名。
        /// </summary>
        public const string IgnoredUrlKey = "iPower.SSO.IgnoredUrl";
        /// <summary>
        /// 登录URL键名。
        /// </summary>
        public const string LoginURLKey = "iPower.SSO.LoginURL";
        /// <summary>
        /// 单点登录服务器地址键名。
        /// </summary>
        public const string ServiceURLKey = "iPower.SSO.ServiceURL";
        /// <summary>
        /// 系统ID键名。
        /// </summary>
        public const string SystemIDKey = "iPower.SystemID";
        /// <summary>
        /// 访问授权密码键名。
        /// </summary>
        public const string AuthPwdKey = "iPower.AuthPwd";
        /// <summary>
        /// 本地数据存储方式键名。
        /// </summary>
        public const string LocalStorageKey = "iPower.SSO.LocalStorage";
        /// <summary>
        /// 发起登录验证的请求Url的键名称键名。
        /// </summary>
        public const string RequestUrlNameKey = "iPower.SSO.RequestUrlName";
        /// <summary>
        /// 本地票据键名称键名。
        /// </summary>
        public const string TicketNameKey = "iPower.SSO.TicketName";

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
    /// 单点登录客户端配置。
    /// </summary>
    internal class SSOClientModuleConfiguration : iPowerConfiguration,ILogFileHead
    {
        #region 成员变量，构造函数。
        static Hashtable Cache = Hashtable.Synchronized(new Hashtable());
        /// <summary>
        /// 构造函数。
        /// </summary>
        public SSOClientModuleConfiguration()
            : base("SSOClient")
        {
           
        }
        #endregion

        #region 静态实例。
        static SSOClientModuleConfiguration m_config;
        static object synchronizationObject = new object();
        /// <summary>
        /// 获取静态实例。
        /// </summary>
        public static SSOClientModuleConfiguration ModuleConfig
        {
            get
            {
                lock (synchronizationObject)
                {
                    if (m_config == null)
                        m_config = new SSOClientModuleConfiguration();
                    return m_config;
                }
            }
        }
        #endregion

        /// <summary>
        /// 获取需忽略的页面的正则表达式。
        /// </summary>
        public Regex[] IgnoredUrls
        {
            get
            {
                Regex[] result = Cache[SSOClientModuleConfigurationKeys.IgnoredUrlKey] as Regex[];
                if (result == null)
                {
                    string strIgnore = this[SSOClientModuleConfigurationKeys.IgnoredUrlKey];
                    if (!string.IsNullOrEmpty(strIgnore))
                    {
                        string[] strUrls = strIgnore.Split(';');
                        List<Regex> list = new List<Regex>();
                        foreach (string str in strUrls)
                        {
                            if (!string.IsNullOrEmpty(str))
                                list.Add(new Regex(str, RegexOptions.Compiled | RegexOptions.IgnoreCase));
                        }
                        if (list.Count > 0)
                        {
                            result = new Regex[list.Count];
                            list.CopyTo(result);
                            Cache[SSOClientModuleConfigurationKeys.IgnoredUrlKey] = result;
                        }
                    }
                }
                return result;
            }
        }
        /// <summary>
        /// 获取登录URL。
        /// </summary>
        public string LoginURL
        {
            get
            {
                string loginUrl = this[SSOClientModuleConfigurationKeys.LoginURLKey];
                if (string.IsNullOrEmpty(loginUrl))
                    throw new ArgumentNullException("单点登录客户端未配置登录URL。");
                return loginUrl;
            }
        }
        /// <summary>
        /// 获取单点登录服务器URL。
        /// </summary>
        public string ServiceURL
        {
            get
            {
                string url = this[SSOClientModuleConfigurationKeys.ServiceURLKey];
                if (string.IsNullOrEmpty(url))
                    throw new ArgumentNullException("单点登录客户端未配置单点登录服务器URL。");
                return url;
            }
        }
        /// <summary>
        /// 获取系统ID。
        /// </summary>
        public string SystemID
        {
            get
            {
                return this[SSOClientModuleConfigurationKeys.SystemIDKey];
            }
        }
        /// <summary>
        /// 获取访问授权密码。
        /// </summary>
        public string AuthPwd
        {
            get
            {
                return this[SSOClientModuleConfigurationKeys.AuthPwdKey];
            }
        }
        /// <summary>
        /// 获取本地数据存储方式。
        /// </summary>
        public EnumLocalStorage LocalStorage
        {
            get
            {
                try
                {
                    string strLocal = this[SSOClientModuleConfigurationKeys.LocalStorageKey];
                    if (!string.IsNullOrEmpty(strLocal))
                    {
                        return (EnumLocalStorage)Enum.Parse(typeof(EnumLocalStorage), strLocal);
                    }
                }
                catch (Exception) { }
                return EnumLocalStorage.Cookies;
            }
        }
        /// <summary>
        /// 获取发起登录验证的请求Url的键名称。
        /// </summary>
        public string RequestUrlName
        {
            get
            {
                string requestUrl = this[SSOClientModuleConfigurationKeys.RequestUrlNameKey];
                if (string.IsNullOrEmpty(requestUrl))
                     requestUrl = "RequestUrl";
                 return requestUrl;
            }
        }
        /// <summary>
        /// 获取本地票据键名称。
        /// </summary>
        public string TicketName
        {
            get
            {
                string ticketName = this[SSOClientModuleConfigurationKeys.TicketNameKey];
                if (string.IsNullOrEmpty(ticketName))
                    ticketName = "ticket";
                return ticketName;
            }
        }

        #region ILogFileHead 成员
        /// <summary>
        /// 获取日志文件头。
        /// </summary>
        public string LogFileHead
        {
            get { return this[SSOClientModuleConfigurationKeys.LogFileHeadKey]; }
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
                    string strRule = this[SSOClientModuleConfigurationKeys.LogFileRuleKey];
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
}
