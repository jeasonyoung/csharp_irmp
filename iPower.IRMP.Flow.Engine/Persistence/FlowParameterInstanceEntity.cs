//================================================================================
// FileName: FlowParameterInstanceEntity.cs
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
	///FlowParameterInstanceEntityʵ���ࡣ
	///</summary>
	internal class FlowParameterInstanceEntity: DbModuleEntity<FlowParameterInstance>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public FlowParameterInstanceEntity()
		{

		}
		#endregion

        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="stepInstanceID"></param>
        /// <returns></returns>
        public Dictionary<string, string> LoadPrameters(string stepInstanceID)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(stepInstanceID))
            {
                DataTable dtSource = this.GetAllRecord(string.Format("StepInstanceID='{0}'", stepInstanceID));
                foreach (DataRow row in dtSource.Rows)
                {
                    dic.Add(Convert.ToString(row["ParameterID"]), Convert.ToString(row["ParameterValue"]));
                }
            }
            return dic;
        }
        /// <summary>
        /// ɾ������ʵ���µ�ȫ��������
        /// </summary>
        /// <param name="stepInstanceID"></param>
        /// <returns></returns>
        public bool DeleteRecord(GUIDEx stepInstanceID)
        {
            return base.DeleteRecord(string.Format("StepInstanceID='{0}'", stepInstanceID));
        }
	}

}
