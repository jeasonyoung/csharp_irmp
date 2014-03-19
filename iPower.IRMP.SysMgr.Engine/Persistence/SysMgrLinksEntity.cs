//================================================================================
// FileName: SysMgrLinksEntity.cs
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
	///SysMgrLinksEntity实体类。
	///</summary>
	internal class SysMgrLinksEntity: DbModuleEntity<SysMgrLinks>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public SysMgrLinksEntity()
		{

		}
		#endregion

        /// <summary>
        /// 获取列表数据源。
        /// </summary>
        /// <param name="linkName"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string linkName)
        {
            return this.GetAllRecord(string.Format("LinkName like '%{0}%'", linkName), "OrderNo");
        }
	}
}
