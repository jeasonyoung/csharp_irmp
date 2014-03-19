//================================================================================
//  FileName:ModuleConfiguration.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-10 17:05:17
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
// ----  ------  -----------
//
//================================================================================
//  Copyright (C) 2009-2010 Jeason Young Corporation
//================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using iPower;
using iPower.Utility;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Persistence;

using iPower.IRMP.Org;
using iPower.IRMP.Security;
using iPower.IRMP.Flow;
namespace iPower.IRMP.Flow.Engine.Persistence
{
    /// <summary>
    /// 模块配置键。
    /// </summary>
    public class ModuleConfigurationKeys : BaseModuleConfigurationKeys
    {
        /// 用户信息程序集配置键。
        /// </summary>
        public const string OrgFactoryAssemblyKey = "OrgFactoryAssembly";
        /// <summary>
        /// 安全管理应用系统注册程序集配置键。
        /// </summary>
        public const string SecurityFactoryAssemblyKey = "SecurityFactoryAssembly";
    }

    /// <summary>
    /// 模块配置类。
    /// </summary>
    public class ModuleConfiguration : BaseModuleConfiguration
    {
        #region 成员变量、构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public ModuleConfiguration()
            : base("Flow")
        {
        }
        #endregion

        #region 静态实例。
        static ModuleConfiguration mconfig;
        /// <summary>
        /// 获取静态实例。
        /// </summary>
        public static ModuleConfiguration ModuleConfig
        {
            get
            {
                lock (typeof(ModuleConfiguration))
                {
                    if (mconfig == null)
                        mconfig = new ModuleConfiguration();
                    return mconfig;
                }
            }
        }
        #endregion

        #region 用户信息。
        /// <summary>
        /// 获取用户信息。
        /// </summary>
        public IOrgFactory OrgFacotry
        {
            get
            {
                lock (this)
                {
                    IOrgFactory facotry = htbCache[ModuleConfigurationKeys.OrgFactoryAssemblyKey] as IOrgFactory;
                    if (facotry == null)
                    {
                        facotry = TypeHelper.Create(this[ModuleConfigurationKeys.OrgFactoryAssemblyKey]) as IOrgFactory;
                        if (facotry != null)
                            htbCache[ModuleConfigurationKeys.OrgFactoryAssemblyKey] = facotry;
                    }
                    return facotry;
                }
            }
        }
        #endregion

        #region 系统注册数据。
        /// <summary>
        /// 获取系统注册数据。
        /// </summary>
        public ISecurityFactory SecurityFactory
        {
            get
            {
                lock (this)
                {
                    ISecurityFactory facotry = htbCache[ModuleConfigurationKeys.SecurityFactoryAssemblyKey] as ISecurityFactory;
                    if (facotry == null)
                    {
                        facotry = TypeHelper.Create(this[ModuleConfigurationKeys.SecurityFactoryAssemblyKey]) as ISecurityFactory;
                        if (facotry != null)
                            htbCache[ModuleConfigurationKeys.SecurityFactoryAssemblyKey] = facotry;
                    }
                    return facotry;
                }
            }
        }
        #endregion
    }
}
