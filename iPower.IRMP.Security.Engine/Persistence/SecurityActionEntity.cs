//================================================================================
// FileName: SecurityActionEntity.cs
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
	///SecurityActionEntityʵ���ࡣ
	///</summary>
	internal class SecurityActionEntity: DbModuleEntity<SecurityAction>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public SecurityActionEntity()
		{

		}
		#endregion

        /// <summary>
        /// ��ȡ������ݡ�
        /// </summary>
        public IListControlsData Action
        {
            get
            {
                return new ListControlsDataSource("ActionName", "ActionID", this.GetAllRecord());
            }
        }

        /// <summary>
        /// �б����ݡ�
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string actionName)
        {
            if (string.IsNullOrEmpty(actionName))
                return this.GetAllRecord();
            return this.GetAllRecord(string.Format("ActionID like '%{0}%' or ActionSign like '%{0}%' or ActionName like '%{0}%'", actionName));
        }
        /// <summary>
        /// ɾ��Ȩ��Ԫ������
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool DeleteAction(string actionID, out string err)
        {
            err = null;
            const string query = "select count(*) from tblSecurityRight where ActionID='{0}'";
            if (!string.IsNullOrEmpty(actionID))
            {
                int result = (int)this.DatabaseAccess.ExecuteScalar(string.Format(query, actionID));
                if (result > 0)
                {
                    err = string.Format("��Ȩ��Ԫ������{0}���Ѿ���Ȩ�޼���ʹ�ã��뽫����ɾ����", actionID);
                    return false;
                }
                return this.DeleteRecord(new string[] { actionID });
            }
            return false;
        }
	}

}
