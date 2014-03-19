//================================================================================
//  FileName: DbModuleEntity.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/2/22
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

using iPower.Platform.Engine.DataSource;
using iPower.Platform.Engine.Persistence;
namespace iPower.IRMP.Engine.Persistence
{
    /// <summary>
    ///  实体基类。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class DbModuleEntity<T> :DbBaseEntity<T,ModuleConfiguration>
        where T:class,new()
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public DbModuleEntity() :
            base(ModuleConfiguration.ModuleConfig)
        {
        }
        #endregion
    }
}
