//================================================================================
// FileName: FlowTaskEntity.cs
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
	///FlowTaskEntityʵ���ࡣ
	///</summary>
    internal class FlowInstanceTaskEntity : DbModuleEntity<FlowInstanceTask>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public FlowInstanceTaskEntity()
		{

		}
		#endregion

        #region ���ݴ���
        /// <summary>
        /// ��ȡ���ݡ�
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="taskCategory"></param>
        /// <returns></returns>
        public List<FlowInstanceTask> GetFlowInstanceTask(GUIDEx employeeID, EnumTaskCategory taskCategory)
        {
            List<FlowInstanceTask> list = new List<FlowInstanceTask>();
            if (employeeID.IsValid)
            {
                DataTable dtSource = this.GetAllRecord(string.Format("EndMode='{0}' and TaskCategory='{1}' and (EmployeeID='{2}' or AuthorizeEmployeeID='{2}')",
                                                                    (int)EnumTaskEndMode.None,
                                                                    (int)taskCategory, employeeID));
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    foreach (DataRow row in dtSource.Rows)
                    {
                        list.Add(this.Assignment(row));
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// ��ȡ�б�����Դ��
        /// </summary>
        /// <param name="processInstanceID"></param>
        /// <returns></returns>
        public DataTable ListDataSource(GUIDEx processInstanceID)
        {
            const string sql = "exec spFlowInstanceTaskListView '{0}'";
            if (processInstanceID.IsValid)
            {
                return this.DatabaseAccess.ExecuteDataset(string.Format(sql, processInstanceID)).Tables[0].Copy();
            }
            return null;
        }
        #endregion
    }

}
