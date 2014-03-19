//================================================================================
// FileName: IRMPCommonLogEntity.cs
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
using iPower.IRMP.SysMgr.Engine.Domain;
namespace iPower.IRMP.SysMgr.Engine.Persistence
{
	///<summary>
	///IRMPCommonLogEntity实体类。
	///</summary>
	internal class IRMPCommonLogEntity: DbModuleEntity<IRMPCommonLog>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public IRMPCommonLogEntity()
		{

		}
		#endregion

        /// <summary>
        /// 列表数据源。
        /// </summary>
        /// <returns></returns>
        public DataTable ListDataSource(string systemName, string employeeName, string createDate, string logContext)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(systemName))
                sb.AppendFormat("(SystemID like '%{0}%' or SystemName like '%{0}%')  ", systemName);
            if (!string.IsNullOrEmpty(employeeName))
            {
                if (sb.Length > 0)
                    sb.Append(" and ");
                sb.AppendFormat(" (CreateEmployeeName like '%{0}%') ", employeeName);
            }
            if (!string.IsNullOrEmpty(createDate))
            {
                if (sb.Length > 0)
                    sb.Append(" and ");
                sb.AppendFormat(" (Convert(nvarchar(10),CreateDate,121) = '{0}') ", createDate);
            }
            if (!string.IsNullOrEmpty(logContext))
            {
                if (sb.Length > 0)
                    sb.Append(" and ");
                sb.AppendFormat(" (LogContext like '%{0}%')", logContext);
            }
            return this.GetAllRecord(sb.ToString(), "CreateDate desc");
        }
	}

}
