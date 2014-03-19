//================================================================================
//  FileName:DbModuleEntity.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-10 17:06:33
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
using System.Collections.Generic;
using System.Text;

using iPower.Platform.Engine.DataSource;
using iPower.Platform.Engine.Persistence;
namespace iPower.IRMP.Flow.Engine.Persistence
{
    /// <summary>
    /// 实体基类。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class DbModuleEntity<T> : DbBaseEntity<T, ModuleConfiguration>
        where T : new()
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public DbModuleEntity()
            : base(ModuleConfiguration.ModuleConfig)
        {
        }
        #endregion
    }
}
