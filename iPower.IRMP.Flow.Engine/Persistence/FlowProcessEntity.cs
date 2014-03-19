//================================================================================
// FileName: FlowProcessEntity.cs
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
	///FlowProcessEntity实体类。
	///</summary>
	internal class FlowProcessEntity: DbModuleEntity<FlowProcess>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public FlowProcessEntity()
		{

		}
		#endregion

        #region 数据处理。
        /// <summary>
        /// 绑定流程数据。
        /// </summary>
        public IListControlsData BindProcess()
        {
            return new ListControlsDataSource("ProcessName", "ProcessID", this.GetAllRecord());
        }
        /// <summary>
        /// 根据流程标识获取流程ID。
        /// </summary>
        /// <param name="processSign"></param>
        /// <returns></returns>
        public GUIDEx FindProcessID(string processSign)
        {
            if (!string.IsNullOrEmpty(processSign))
            {
                const string sql = "select ProcessID from {0} where ProcessSign='{1}'";
                string strSql = string.Format(sql, this.TableName, processSign);

                object obj = this.DatabaseAccess.ExecuteScalar(strSql);
                if (obj != null)
                    return new GUIDEx(obj);
            }
            return GUIDEx.Null;
        }
        /// <summary>
        /// 根据流程ID获取流程名称。
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        public string FindProcessName(string processID)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(processID))
            {
                result = this.DatabaseAccess.ExecuteScalar(string.Format("select ProcessName from {0} where ProcessID='{1}'", this.TableName, processID)).ToString();
            }
            return result;
        }
        /// <summary>
        /// 列表数据源。
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string processName)
        {
            StringBuilder filter = new StringBuilder();
            if (!string.IsNullOrEmpty(processName))
            {
                filter.AppendFormat("((ProcessSign like '%{0}%') or (ProcessName like '%{0}%'))", processName);
            }
            return this.GetAllRecord(filter.ToString());
        }
        /// <summary>
        /// 检查是否可以启用流程。
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        public bool ChkEnableProcess(GUIDEx processID, out string message)
        {
            message = string.Empty;
            bool result = false;
            if (processID.IsValid)
            {
                string str = this.DatabaseAccess.ExecuteScalar(string.Format("exec spFlowEnableProcess '{0}'", processID)).ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    string[] arr = str.Split('|');
                    if (arr.Length == 2)
                    {
                        result = arr[0].IndexOf("0") > -1;
                        message = arr[1];
                    }
                }
            }
            else
                message = "流程ID为空！";
            return result;
        }
        #endregion

        #region 重载。
        public override bool DeleteRecord(StringCollection primaryValues)
        {
            if (primaryValues != null && primaryValues.Count > 0)
            {
                const string sql = "select count(*) from tblFlowStep where ProcessID in ('{0}')";
                string[] p = new string[primaryValues.Count];
                primaryValues.CopyTo(p, 0);

                int result = (int)this.DatabaseAccess.ExecuteScalar(string.Format(sql, string.Join("','", p)));
                if (result > 0)
                    throw new Exception("有未删除的步骤！");

                FlowProcessSerializationEntity flowProcessSerializationEntity = new FlowProcessSerializationEntity();
                flowProcessSerializationEntity.DeleteRecord(primaryValues);

                return base.DeleteRecord(primaryValues);
            }
            return false;
        }
        #endregion
    }

}
