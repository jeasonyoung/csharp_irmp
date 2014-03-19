//================================================================================
// FileName: OrgEmployeeEntity.cs
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
using iPower.Data;
using iPower.Cryptography;

using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.DataSource;
using iPower.Platform.Engine.Persistence;
using iPower.IRMP.Org;
using Domain = iPower.IRMP.Org.Engine.Domain;
namespace iPower.IRMP.Org.Engine.Persistence
{
	///<summary>
	///OrgEmployeeEntity实体类。
	///</summary>
    internal class OrgEmployeeEntity : DbModuleEntity<Domain.OrgEmployee>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public OrgEmployeeEntity()
		{

		}
		#endregion

        /// <summary>
        /// 根据用户标识获取用户数据。
        /// </summary>
        /// <param name="employeeSign"></param>
        /// <returns></returns>
        public Domain.OrgEmployee GetEmployee(string employeeSign)
        {
            Domain.OrgEmployee employee = null;
            if (!string.IsNullOrEmpty(employeeSign))
            {
                using (IDataReader reader = this.GetReaderRecord(string.Format("EmployeeSign = '{0}'", employeeSign)))
                {
                    if (reader != null && reader.Read())
                    {
                        employee = this.Assignment(reader);
                        reader.Close();
                    }
                }
            }
            return employee;
        }
        /// <summary>
        /// 重载。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool UpdateRecord(Domain.OrgEmployee entity)
        {
            if (entity != null)
            {
                string history = entity.EmployeePassword;
                Domain.OrgEmployee orgEmployee = entity;
                if (this.LoadRecord(ref orgEmployee))
                {
                    string strHistory = orgEmployee.PasswordHistory;
                    if (!string.IsNullOrEmpty(strHistory))
                    {
                        history = strHistory;
                        string[] hisArray = strHistory.Split(',');
                        if (hisArray.Length >= 5)
                        {
                            hisArray[0] = null;
                            history = string.Join(",", hisArray);
                        }
                        history += string.Concat(",", entity.EmployeePassword);
                    }
                }
                entity.PasswordHistory = history;
            }
            return base.UpdateRecord(entity);
        }
        /// <summary>
        /// 获取列表数据。
        /// </summary>
        /// <param name="employeeName"></param>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public DataTable ListViewDataSource(string employeeName, string departmentID)
        {
            const string sql = "exec spOrgEmployeeListView '{0}','{1}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, employeeName, departmentID)).Tables[0].Copy();
        }
        /// <summary>
        /// 删除用户数据。
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool DeleteEmployee(string employeeID, out string err)
        {
            const string sql = "exec spOrgDeleteEmployee '{0}'";
            string result = this.DatabaseAccess.ExecuteScalar(string.Format(sql, employeeID)).ToString();
            string[] array = result.Split('|');
            err = array[1];

            return array[0] == "0";
        }
        /// <summary>
        /// 获取数据。
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <returns></returns>
        public IListControlsData Employee(string[] employeeID)
        {
            DataTable dtSource = null;
            if (employeeID == null)
                dtSource = this.GetAllRecord();
            else
            {
                string filter = string.Format("EmployeeID in ('{0}')", string.Join("','", employeeID));
                dtSource = this.GetAllRecord(filter);
            }
            return new ListControlsDataSource("EmployeeName", "EmployeeID", dtSource);
        }
        /// <summary>
        /// 获取用户数据。
        /// </summary>
        /// <param name="departmentName"></param>
        /// <param name="employeeName"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public IListControlsData Employee(string departmentName, string employeeName, string gender)
        {
            const string sql = "exec spOrgEmployeePicker '{0}','{1}','{2}'";
            string strSQL = string.Format(sql, departmentName, employeeName, gender);
            DataSet dsSet = this.DatabaseAccess.ExecuteDataset(strSQL);
            return new ListControlsDataSource("EmployeeName", "EmployeeID", dsSet);
        }
        /// <summary>
        /// 获取所有有效用户数据。
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <returns></returns>
        public OrgEmployeeCollection GetAllEmployee(string employeeID)
        {
            OrgEmployeeCollection collection = new OrgEmployeeCollection();
            DataTable dtSource = null;
            if (string.IsNullOrEmpty(employeeID))
                dtSource = this.GetAllRecord(string.Format("EmployeeStatus='{0}'", (int)EnumStatus.Start));
            else
            {
                dtSource = this.GetAllRecord(string.Format("EmployeeStatus='{0}' and EmployeeID = '{1}'", (int)EnumStatus.Start, employeeID));
                if(dtSource != null && dtSource.Rows.Count == 0)
                    dtSource = this.GetAllRecord(string.Format("EmployeeStatus='{0}' and (EmployeeName like '%{1}%' or EmployeeSign like '%{1}%')", (int)EnumStatus.Start, employeeID));
            }

            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                foreach (DataRow row in dtSource.Rows)
                {
                    OrgEmployee item = new OrgEmployee();
                    item.EmployeeID = Convert.ToString(row["EmployeeID"]);
                    item.EmployeeName = Convert.ToString(row["EmployeeName"]);
                    item.EmployeeSign = Convert.ToString(row["EmployeeSign"]);

                    item.DepartmentID = Convert.ToString(row["DepartmentID"]);
                    item.PostID = Convert.ToString(row["PostID"]);

                    item.Order = Convert.ToInt32(row["OrderNo"]);

                    collection.Add(item);
                }
            }
            return collection;
        }
        /// <summary>
        /// 获取用户下的部门数据。
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public OrgDepartmentCollection GetSubCharge(GUIDEx employeeID)
        {
            OrgDepartmentCollection collection = new OrgDepartmentCollection();
            if (employeeID.IsValid)
            {
                const string sql = "exec spOrgEmployeeSubChargeDepartment '{0}'";
                DataTable dtSource = this.DatabaseAccess.ExecuteDataset(string.Format(sql, employeeID)).Tables[0].Copy();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    foreach (DataRow row in dtSource.Rows)
                    {
                        OrgDepartment item = new OrgDepartment();
                        item.DepartmentID = Convert.ToString(row["DepartmentID"]);
                        item.DepartmentName = Convert.ToString(row["DepartmentName"]);
                        item.ParentDepartmentID = Convert.ToString(row["ParentDepartmentID"]);
                        item.Order = Convert.ToInt32(row["DepartmentOrder"]);
                        collection.Add(item);
                    }
                }
            }
            return collection;
        }
    }
}
