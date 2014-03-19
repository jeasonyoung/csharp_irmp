//================================================================================
// FileName: SysMgrSettingPersonalEntity.cs
// Desc:
// Called by
// Auth: 本代码由代码生成器自动生成。
// Date:
//================================================================================
// Change History
//================================================================================
// Date  Author  Description
// ----  ------  -----------
//
//================================================================================
// Copyright (C) 2009-2010 Jeason Young Corporation
//================================================================================
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Data;
	
using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.DataSource;
using iPower.Platform.Engine.Persistence;
using iPower.IRMP.SysMgr.Engine.Domain;
namespace iPower.IRMP.SysMgr.Engine.Persistence
{
	///<summary>
	///SysMgrSettingPersonalEntity实体类。
	///</summary>
    internal class SysMgrSettingPersonalEntity : DbModuleEntity<SysMgrSettingPersonal>
    {
        #region 成员变量，构造函数。
        ///<summary>
        ///构造函数
        ///</summary>
        public SysMgrSettingPersonalEntity()
        {

        }
        #endregion

        /// <summary>
        /// 数据列表
        /// </summary>
        public DataTable ListDataSource(string EmployeeName)
        {
            const string sql = "spSysMgrSettingPersonalListView '{0}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, EmployeeName)).Tables[0].Copy();
        }
    }
}
