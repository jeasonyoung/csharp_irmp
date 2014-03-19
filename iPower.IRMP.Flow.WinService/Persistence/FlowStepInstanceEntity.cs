//================================================================================
// FileName: FlowStepInstanceEntity.cs
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
using iPower.IRMP.Flow;
using iPower.IRMP.Flow.WinService.Domain;
namespace iPower.IRMP.Flow.WinService.Persistence
{
	///<summary>
	///FlowStepInstanceEntity实体类。
	///</summary>
	internal class FlowStepInstanceEntity: DbModuleEntity<FlowStepInstance>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public FlowStepInstanceEntity()
		{

		}
		#endregion

        /// <summary>
        /// 加载数据。
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
