//================================================================================
//  FileName: ModuleConfiguration.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/2/24
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

using iPower;
using iPower.Data;
using iPower.Utility;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Persistence;

using iPower.IRMP.Org;
namespace iPower.IRMP.Security.Engine.Persistence
{
    /// <summary>
    ///  模块配置键。
    /// </summary>
    public class ModuleConfigurationKeys : BaseModuleConfigurationKeys
    {
        /// <summary>
        /// 用户信息程序集配置键。
        /// </summary>
        public const string OrgFactoryAssemblyKey = "OrgFactoryAssembly";
    }

    /// <summary>
    /// 模块配置类。
    /// </summary>
    public class ModuleConfiguration : BaseModuleConfiguration
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public ModuleConfiguration()
            : base("Security")
        {
        }
        #endregion

        #region 静态实例。
        static ModuleConfiguration m_config;
        /// <summary>
        /// 获取静态实例。
        /// </summary>
        public static ModuleConfiguration ModuleConfig
        {
            get
            {
                lock (typeof(ModuleConfiguration))
                {
                    m_config = new ModuleConfiguration();
                    return m_config;
                }
            }
        }
        #endregion

        #region 用户信息。
        /// <summary>
        /// 获取用户信息。
        /// </summary>
        public IOrgFactory OrgFactory
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
    }
}
