//================================================================================
// FileName: FlowTaskDataEntity.cs
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
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.DataSource;
using iPower.Platform.Engine.Persistence;
using iPower.IRMP.Flow.Engine.Domain;
namespace iPower.IRMP.Flow.Engine.Persistence
{
	///<summary>
	///FlowTaskDataEntity实体类。
	///</summary>
    internal class FlowStepInstanceDataEntity : DbModuleEntity<FlowStepInstanceData>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public FlowStepInstanceDataEntity()
		{

		}
		#endregion

        #region 数据操作。
        /// <summary>
        /// 获取流程履历数据。
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
