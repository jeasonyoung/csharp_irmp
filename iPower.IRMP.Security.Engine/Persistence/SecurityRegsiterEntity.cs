//================================================================================
// FileName: SecurityRegsiterEntity.cs
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
	///SecurityRegsiterEntityʵ���ࡣ
	///</summary>
	internal class SecurityRegsiterEntity: DbModuleEntity<SecurityRegsiter>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
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
        /// �б�����Դ��
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
        /// ɾ�����ݡ�
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
        /// ������ʼ��ģ��Ȩ�ޡ�
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
