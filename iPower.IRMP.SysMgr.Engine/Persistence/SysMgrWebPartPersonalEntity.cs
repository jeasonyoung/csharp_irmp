//================================================================================
// FileName: SysMgrWebPartPersonalEntity.cs
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
	///SysMgrWebPartPersonalEntity实体类。
	///</summary>
    internal class SysMgrWebPartPersonalEntity : DbModuleEntity<SysMgrWebPartPersonal>
    {
        #region 成员变量，构造函数。
        ///<summary>
        ///构造函数
        ///</summary>
        public SysMgrWebPartPersonalEntity()
        {

        }
        #endregion

        /// <summary>
        /// 首页设置列表
        /// </summary>
        /// <param name="WebPartName">部件名称</param>
        /// <returns></returns>
        public DataTable ListDataSource(string WebPartName)
        {
            const string sql = "exec spSysMgrWebPartPersonalListView '{0}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, WebPartName)).Tables[0].Copy();
        }
    }

}
