//================================================================================
// FileName: FlowStepInstanceDataEntity.cs
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
using System.Text.RegularExpressions;
using System.Data;
	
using iPower;
using iPower.IRMP.Flow;
using iPower.IRMP.Flow.WinService.Domain;
namespace iPower.IRMP.Flow.WinService.Persistence
{
	///<summary>
	///FlowStepInstanceDataEntity实体类。
	///</summary>
	internal class FlowStepInstanceDataEntity: DbModuleEntity<FlowStepInstanceData>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public FlowStepInstanceDataEntity()
		{

		}
		#endregion

        /// <summary>
        /// 加载数据。
        /// </summary>
        /// <param name="stepInstanceID"></param>
        /// <param name="dataCategory"></param>
        /// <returns></returns>
        public DataTable LoadStepInstanceData(GUIDEx stepInstanceID, EnumDataCategory dataCategory)
        {
            return this.GetAllRecord(string.Format("StepInstanceID='{0}' and DataCategory={1}", stepInstanceID, (int)dataCategory));
        }

        /// <summary>
        /// 获取步骤待办人员。
        /// </summary>
        /// <param name="stepInstanceID"></param>
        /// <returns></returns>
        public Dictionary<GUIDEx, string> GetStepInstanceToDoEmployees(GUIDEx stepInstanceID)
        {
            DataTable dtSource = this.LoadStepInstanceData(stepInstanceID, EnumDataCategory.AddData);
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                Dictionary<GUIDEx, string> result = new Dictionary<GUIDEx, string>();
                string strDataText = null;
                Regex regex = new Regex("^toDoEmployees:\\{(\"<?EmployeeID>[A-Z|a-z|0-9]+\":\"<?EmployeeName>.+?\"[;])+\\}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                foreach (DataRow row in dtSource.Rows)
                {
                    strDataText = Convert.ToString(row["DataText"]);
                    if (!string.IsNullOrEmpty(strDataText) && regex.IsMatch(strDataText))
                    {
                        MatchCollection matchs = regex.Matches(strDataText);
                        foreach (Match m in matchs)
                        {
                            if (m.Success)
                            {
                                result.Add(new GUIDEx(m.Groups["EmployeeID"].Value), m.Groups["EmployeeName"].Value);
                            }
                        }
                        break;
                    }
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// 获取步骤待阅人员。
        /// </summary>
        /// <param name="stepInstanceID"></param>
        /// <returns></returns>
        public Dictionary<GUIDEx, string> GetStepInstanceToViewEmployees(GUIDEx stepInstanceID)
        {
            DataTable dtSource = this.LoadStepInstanceData(stepInstanceID, EnumDataCategory.AddData);
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                Dictionary<GUIDEx, string> result = new Dictionary<GUIDEx, string>();
                string strDataText = null;
                Regex regex = new Regex("^toViewEmployees:\\{(\"<?EmployeeID>[A-Z|a-z|0-9]+\":\"<?EmployeeName>.+?\"[;])+\\}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                foreach (DataRow row in dtSource.Rows)
                {
                    strDataText = Convert.ToString(row["DataText"]);
                    if (!string.IsNullOrEmpty(strDataText) && regex.IsMatch(strDataText))
                    {
                        MatchCollection matchs = regex.Matches(strDataText);
                        foreach (Match m in matchs)
                        {
                            if (m.Success)
                            {
                                result.Add(new GUIDEx(m.Groups["EmployeeID"].Value), m.Groups["EmployeeName"].Value);
                            }
                        }
                        break;
                    }
                }
                return result;
            }
            return null;
        }
	}

}
