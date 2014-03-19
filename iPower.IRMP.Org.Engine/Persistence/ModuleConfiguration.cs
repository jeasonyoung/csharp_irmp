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
using iPower.Platform.Engine;
using iPower.Platform.Engine.Persistence;
namespace iPower.IRMP.Org.Engine.Persistence
{
    /// <summary>
    ///  模块配置键。
    /// </summary>
    public class ModuleConfigurationKeys : BaseModuleConfigurationKeys
    {
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
            : base("Org")
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
                    if (m_config == null)
                        m_config = new ModuleConfiguration();
                    return m_config;
                }
            }
        }
        #endregion
    }
}
