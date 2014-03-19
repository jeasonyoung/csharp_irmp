//================================================================================
// FileName: FlowProcessInstanceEntity.cs
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
using iPower.IRMP.Flow.Engine.Domain;
namespace iPower.IRMP.Flow.Engine.Persistence
{
	///<summary>
	///FlowProcessInstanceEntityʵ���ࡣ
	///</summary>
	internal class FlowProcessInstanceEntity: DbModuleEntity<FlowProcessInstance>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public FlowProcessInstanceEntity()
		{

		}
		#endregion

        /// <summary>
        /// ��ȡ�б�����Դ��
        /// </summary>
        /// <param name="processInstanceName"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string processInstanceName)
        {
            const string sql = "exec spFlowProcessInstanceListView '{0}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, processInstanceName)).Tables[0].Copy();
        }

        /// <summary>
        /// ��ȡ�쳣����Դ��
        /// </summary>
        /// <param name="processInstanceID"></param>
        /// <returns></returns>
        public DataTable StepInstanceRunErrorListDataSource(GUIDEx processInstanceID)
        {
            const string sql = "exec spFlowInstanceRunErrorListView '{0}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, processInstanceID)).Tables[0].Copy();
        }

        /// <summary>
        /// ��������ʵ��״̬��
        /// </summary>
        /// <param name="processInstanceID">����ʵ��ID��</param>
        /// <param name="status">״̬</param>
        /// <returns></returns>
        public bool ChangeFlowInstanceStatus(GUIDEx processInstanceID,EnumInstanceProcessStatus status)
        {
            lock (this)
            {
                bool result = false;
                const string sql = "update {0} set InstanceProcessStatus='{1}' where ProcessInstanceID='{2}'";
                string strSql = string.Format(sql, this.TableName, (int)status, processInstanceID);

                result = this.DatabaseAccess.ExecuteNonQuery(strSql) > 0;

                return result;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="processInstanceID"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool DeleteRecord(GUIDEx processInstanceID, out string err)
        {
            const string sql = "exec spFlowDeteleProcessInstance '{0}'";
            err = null;
            if (processInstanceID.IsValid)
            {
                string result = this.DatabaseAccess.ExecuteScalar(string.Format(sql, processInstanceID)).ToString();
                string[] array = result.Split('|');
                err = array[1];
                return array[0] == "0";
            }
            return false;
        }
	}

}
