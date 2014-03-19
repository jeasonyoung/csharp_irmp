//================================================================================
// FileName: SecurityModuleEntity.cs
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
using iPower.IRMP.Security.Engine.Domain;
namespace iPower.IRMP.Security.Engine.Persistence
{
	///<summary>
	///SecurityModuleEntity实体类。
	///</summary>
	internal class SecurityModuleEntity: DbModuleEntity<SecurityModule>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public SecurityModuleEntity()
		{

		}
		#endregion

        /// <summary>
        /// 获取所有的模块数据。
        /// </summary>
        public IListControlsTreeViewData ParentModule(GUIDEx systemID)
        {
            return new ListControlsTreeViewDataSource("ModuleName", "ModuleID", "ParentModuleID", "OrderNo", this.GetAllRecord(string.Format("SystemID='{0}'", systemID)));
        }
        
        /// <summary>
        /// 列表数据。
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="systemID"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string moduleName, string systemID)
        {
            const string sql = "exec spSecurityModuleListView '{0}','{1}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, moduleName, systemID)).Tables[0].Copy();
        }
        /// <summary>
        /// 删除数据。
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool DeleteModule(string moduleID, out string err)
        {
            const string sql = "spSecurityDeleteModule '{0}'";
            string result = this.DatabaseAccess.ExecuteScalar(string.Format(sql, moduleID)).ToString();
            string[] array = result.Split('|');
            err = array[1];
            return array[0] == "0";
        }
	}

}
