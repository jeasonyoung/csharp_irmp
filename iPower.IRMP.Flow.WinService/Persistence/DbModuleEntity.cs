//================================================================================
//  FileName: DbBaseEntity.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/23
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

using iPower.Data;
using iPower.Data.ORM;
namespace iPower.IRMP.Flow.WinService.Persistence
{ 
    /// <summary>
    /// ORM基类。
    /// </summary>
    internal class DbModuleEntity<T> : ORMDbEntity<T>
        where T : new()
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public DbModuleEntity()
        {
        }
        #endregion

        #region 重载。
        /// <summary>
        /// 创建数据。
        /// </summary>
        /// <returns></returns>
        protected override IDBAccess CreateDBAccess()
        {
            return new ModuleConfiguration().ModuleDefaultDatabase;
        }
        #endregion
    }
}
