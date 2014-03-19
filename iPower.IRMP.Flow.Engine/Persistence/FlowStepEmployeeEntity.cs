//================================================================================
// FileName: FlowStepEmployeeEntity.cs
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
	///FlowStepEmployeeEntity实体类。
	///</summary>
	internal class FlowStepEmployeeEntity: DbModuleEntity<FlowStepEmployee>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public FlowStepEmployeeEntity()
		{
		}
		#endregion

        #region 数据处理。
        /// <summary>
        /// 加载数据。
        /// </summary>
        /// <param name="stepID">步骤ID。</param>
        /// <param name="employeeID">用户ID。</param>
        /// <param name="employeeName">用户名称。</param>
        /// <returns></returns>
        public bool LoadFlowStepEmployee(string stepID, out string[] employeeID, out string[] employeeName)
        {
            employeeID = employeeName = new string[0];
            bool result = false;
            if (!string.IsNullOrEmpty(stepID))
            {
                List<string> listEmployeeID = new List<string>(), listEmployeeName = new List<string>();
                DataTable dtSource = this.GetAllRecord(string.Format("StepID='{0}'", stepID));
                foreach (DataRow row in dtSource.Rows)
                {
                    listEmployeeID.Add(Convert.ToString(row["EmployeeID"]));
                    listEmployeeName.Add(Convert.ToString(row["EmployeeName"]));
                }
                employeeID = new string[listEmployeeID.Count];
                listEmployeeID.CopyTo(employeeID);

                employeeName = new string[listEmployeeName.Count];
                listEmployeeName.CopyTo(employeeName);
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 清楚并添加。
        /// </summary>
        /// <param name="stepID">步骤ID。</param>
        /// <param name="employeeID">用户ID。</param>
        /// <param name="employeeName">用户名称。</param>
        /// <returns></returns>
        public bool DeleteAllAndInsert(string stepID, string[] employeeID, string[] employeeName)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(stepID))
            {
                result = this.DeleteRecord(string.Format("StepID='{0}'", stepID));

                if (employeeID != null && employeeName != null && (employeeID.Length == employeeName.Length))
                {
                    for (int i = 0; i < employeeID.Length; i++)
                    {
                        string empID = employeeID[i];
                        if (!string.IsNullOrEmpty(empID))
                        {
                            FlowStepEmployee flowStepEmployee = new FlowStepEmployee();
                            flowStepEmployee.StepID = stepID;
                            flowStepEmployee.EmployeeID = empID;
                            flowStepEmployee.EmployeeName = employeeName[i];

                            result = this.UpdateRecord(flowStepEmployee);
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 加载数据。
        /// </summary>
        /// <param name="stepID">步骤ID。</param>
        /// <returns></returns>
        public StepEmployeeCollection LoadStepEmployeeCollection(GUIDEx stepID)
        {
            StepEmployeeCollection collection = new StepEmployeeCollection();
            if (stepID.IsValid)
            {
                DataTable dtSource = this.GetAllRecord(string.Format("StepID='{0}'", stepID));
                List<FlowStepEmployee> list = this.ConvertDataSource(dtSource);
                if (list != null)
                {
                    foreach (FlowStepEmployee fse in list)
                    {
                        StepEmployee se = new StepEmployee();
                        se.EmployeeID = fse.EmployeeID;
                        se.EmployeeName = fse.EmployeeName;
                        collection.Add(se);
                    }
                }
            }
            return collection;
        }
        #endregion

        #region 重载。
        public override bool DeleteRecord(StringCollection primaryValues)
        {
            if (primaryValues != null && primaryValues.Count > 0)
            {
                string[] p = new string[primaryValues.Count];
                primaryValues.CopyTo(p, 0);

                const string sql = "delete from {0} where StepID in ('{1}')";

                return this.DatabaseAccess.ExecuteNonQuery(string.Format(sql, this.TableName, string.Join("','", p))) > 0;
                //return base.DeleteRecord(primaryValues);
            }
            return false;
        }
        #endregion
    }

}
