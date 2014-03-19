//================================================================================
// FileName: FlowTaskDataEntity.cs
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
	///FlowTaskDataEntityʵ���ࡣ
	///</summary>
    internal class FlowStepInstanceDataEntity : DbModuleEntity<FlowStepInstanceData>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public FlowStepInstanceDataEntity()
		{

		}
		#endregion

        #region ���ݲ�����
        /// <summary>
        /// ��ȡ�����������ݡ�
        /// </summary>
        /// <param name="processInstanceID"></param>
        /// <returns></returns>
        public DataTable GetProcessResumes(GUIDEx processInstanceID)
        {
            const string sql = "exec spFlowProcessResumes '{0}'";
            if (processInstanceID.IsValid)
            {
                return this.DatabaseAccess.ExecuteDataset(string.Format(sql, processInstanceID)).Tables[0].Copy();
            }
            return null;
        }
        #endregion
    }

}
