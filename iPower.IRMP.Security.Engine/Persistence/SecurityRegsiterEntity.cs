//================================================================================
// FileName: SecurityRegsiterEntity.cs
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
	///SecurityRegsiterEntity实体类。
	///</summary>
	internal class SecurityRegsiterEntity: DbModuleEntity<SecurityRegsiter>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public SecurityRegsiterEntity()
		{

		}
		#endregion

        /// <summary>
        /// 
        /// </summary>
        public IListControlsTreeViewData RegsiterSystem
        {
            get
            {
                return new ListControlsTreeViewDataSource("SystemName", "SystemID", "ParentSystemID","SystemType", this.GetAllRecord(string.Format("SystemStatus='{0}'", (int)EnumSystemStatus.Start), "SystemType,SystemID"));
            }
        }

        /// <summary>
        /// 列表数据源。
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="parentSystemID"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string systemName, string parentSystemID)
        {
            const string sql = "exec spSecurityRegsiterListView '{0}','{1}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, systemName, parentSystemID)).Tables[0].Copy();
        }
        /// <summary>
        /// 删除数据。
        /// </summary>
        /// <param name="systemID"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool DeleteRegsiter(GUIDEx systemID, out string err)
        {
            const string sql = "exec spSecurityDeleteRegsiter '{0}'";
            string result = this.DatabaseAccess.ExecuteScalar(string.Format(sql, systemID)).ToString();
            string[] array = result.Split('|');
            err = array[1];
            return array[0] == "0";
        }
        /// <summary>
        /// 批量初始化模块权限。
        /// </summary>
        /// <param name="systemID"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool BatchInitAppModuleRight(GUIDEx systemID, out string err)
        {
            try
            {
                const string sql = "exec spSecurityBatchInitAppModuleRight '{0}'";
                err = null;
                return this.DatabaseAccess.ExecuteDataset(string.Format(sql, systemID)).Tables[0].Rows.Count > 0;
            }
            catch (Exception e)
            {
                err = e.Message;
                return false;
            }
        }
	}

}
