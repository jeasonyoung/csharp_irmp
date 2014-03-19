//================================================================================
// FileName: SecurityModuleEntity.cs
// Desc:
// Called by
// Auth: �������ɴ����������Զ����ɡ�
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
	///SecurityModuleEntityʵ���ࡣ
	///</summary>
	internal class SecurityModuleEntity: DbModuleEntity<SecurityModule>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public SecurityModuleEntity()
		{

		}
		#endregion

        /// <summary>
        /// ��ȡ���е�ģ�����ݡ�
        /// </summary>
        public IListControlsTreeViewData ParentModule(GUIDEx systemID)
        {
            return new ListControlsTreeViewDataSource("ModuleName", "ModuleID", "ParentModuleID", "OrderNo", this.GetAllRecord(string.Format("SystemID='{0}'", systemID)));
        }
        
        /// <summary>
        /// �б����ݡ�
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
        /// ɾ�����ݡ�
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
