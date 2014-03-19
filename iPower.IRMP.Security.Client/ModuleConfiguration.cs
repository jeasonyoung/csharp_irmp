//================================================================================
//  FileName: ModuleConfiguration.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/4/13
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

using iPower;
using iPower.Data;
using iPower.Logs;
using iPower.Utility;
using iPower.Configuration;
using iPower.Platform.Security;

using iPower.IRMP.Security;
namespace iPower.IRMP.Security.Client
{
    /// <summary>
    /// 模块配置键名。
    /// </summary>
    internal class ModuleConfigurationKeys : iPowerConfigurationKeys
    {
        /// <summary>
        /// 权限模块程序集配置。
        /// </summary>
        public const string SecurityPermissionFactoryAssemblyKey = "SecurityPermissionFactoryAssembly";

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
    /// 模块配置类。
    /// </summary>
    internal class ModuleConfiguration : iPowerConfiguration, ILogFileHead
    {
        #region 成员变量，构造函数。
        static Hashtable Cache = Hashtable.Synchronized(new Hashtable());
        LogContainer log = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public ModuleConfiguration()
            : base("SecurityClient")
        {
            this.log = new LogContainer(this);
        }
        #endregion

        #region 静态属性。
        static ModuleConfiguration m_config;
        /// <summary>
        /// 模块配置静态实例。
        /// </summary>
        public static ModuleConfiguration ModuleConfigurationInstance
        {
            get
            {
                lock (typeof(ModuleConfiguration))
                {
                    if (m_config == null)
                        m_config = new ModuleConfiguration();
                    return m_config;
                }
            }
        }
        #endregion

        /// <summary>
        /// 获取安全权限工厂实例。
        /// </summary>
        public ISecurityPermissionFactory SecurityPermissionFactory
        {
            get
            {
                lock (this)
                {
                    ISecurityPermissionFactory factory = Cache[ModuleConfigurationKeys.SecurityPermissionFactoryAssemblyKey] as ISecurityPermissionFactory;
                    if (factory == null)
                    {
                        try
                        {
                            string assembly = this[ModuleConfigurationKeys.SecurityPermissionFactoryAssemblyKey];
                            if (!string.IsNullOrEmpty(assembly))
                            {
                                if (assembly.EndsWith(".asmx", StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase))
                                    factory = new SecurityPermissionFacotryProvider(assembly);
                                else
                                    factory = TypeHelper.Create(assembly) as ISecurityPermissionFactory;
                            }
                            if (factory != null)
                            {
                                Cache[ModuleConfigurationKeys.SecurityPermissionFactoryAssemblyKey] = factory;
                            }
                        }
                        catch (Exception e)
                        {
                            this.log.CreateErrorLog(e.Message);
                        }
                    }
                    return factory;
                }
            }
        }

        #region ILogFileHead 成员
        /// <summary>
        /// 获取日志文件头。
        /// </summary>
        public string LogFileHead
        {
            get { return this[ModuleConfigurationKeys.LogFileHeadKey]; }
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
                    string strRule = this[ModuleConfigurationKeys.LogFileRuleKey];
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
    class SecurityPermissionFacotryProvider : ISecurityPermissionFactory
    {
        #region 成员变量，构造函数。
        Poxy.SecurityPermissionFacotryService service = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="url"></param>
        public SecurityPermissionFacotryProvider(string url)
        {
            this.service = new Poxy.SecurityPermissionFacotryService();
            this.service.Url = url;
        }
        #endregion

        #region ISecurityPermissionFacotry 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemID"></param>
        /// <param name="moduleID"></param>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public SecurityPermissionCollection ModulePermissions(string systemID, string moduleID, string employeeID)
        {
            SecurityPermissionCollection collection = new SecurityPermissionCollection();
            Poxy.SecurityPermission[] permissions = this.service.ModulePermissions(systemID, moduleID, employeeID);
            if (permissions != null && permissions.Length > 0)
            {
                foreach (Poxy.SecurityPermission sp in permissions)
                {
                    SecurityPermission securityPermission = new SecurityPermission();
                    securityPermission.PermissionID = new GUIDEx(sp.PermissionID);
                    securityPermission.PermissionName = sp.PermissionName;
                    collection.Add(securityPermission);
                }
            }
            return collection;
        }

        #endregion
    }
    #endregion
}
