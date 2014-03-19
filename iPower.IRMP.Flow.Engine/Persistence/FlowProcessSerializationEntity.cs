//================================================================================
// FileName: FlowProcessSerializationEntity.cs
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
	///FlowProcessSerializationEntityʵ���ࡣ
	///</summary>
	internal class FlowProcessSerializationEntity: DbModuleEntity<FlowProcessSerialization>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public FlowProcessSerializationEntity()
		{

		}
		#endregion

        /// <summary>
        /// ����״̬��
        /// </summary>
        /// <param name="processID">����ID��</param>
        /// <param name="status">״ֵ̬</param>
        /// <returns></returns>
        public bool UpdateStatus(GUIDEx processID, int status)
        {
            bool result = false;
            const string sql = "update {0} set Status='{1}' where ProcessID='{2}'";
            if (processID.IsValid)
            {
                string strSql = string.Format(sql, this.TableName, status, processID);
                result = this.DatabaseAccess.ExecuteNonQuery(strSql) > 0;
            }
            return result;
        }
	}

}
