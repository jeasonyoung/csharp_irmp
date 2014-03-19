//================================================================================
// FileName: FlowStepInstanceEntity.cs
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
using iPower.IRMP.Flow;
using iPower.IRMP.Flow.WinService.Domain;
namespace iPower.IRMP.Flow.WinService.Persistence
{
	///<summary>
	///FlowStepInstanceEntityʵ���ࡣ
	///</summary>
	internal class FlowStepInstanceEntity: DbModuleEntity<FlowStepInstance>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public FlowStepInstanceEntity()
		{

		}
		#endregion

        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<FlowStepInstance> LoadFlowStepInstance(EnumInstanceStepStatus status)
        {
            List<FlowStepInstance> list = new List<FlowStepInstance>();
            DataTable dtSource = this.GetAllRecord(string.Format("InstanceStepStatus={0}", (int)status));
            if (dtSource != null)
            {
                foreach (DataRow row in dtSource.Rows)
                {
                    list.Add(this.Assignment(row));
                }
            }
            return list;
        }
	}

}
