//================================================================================
// FileName: OrgLeaderSubChargeEntity.cs
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
using iPower.IRMP.Org.Engine.Domain;
namespace iPower.IRMP.Org.Engine.Persistence
{
	///<summary>
	///OrgLeaderSubChargeEntityʵ���ࡣ
	///</summary>
	internal class OrgLeaderSubChargeEntity: DbModuleEntity<OrgLeaderSubCharge>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public OrgLeaderSubChargeEntity()
		{

		}
		#endregion

        /// <summary>
        /// �б����ݡ�
        /// </summary>
        /// <param name="departmentName"></param>
        /// <param name="employeeName"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string departmentName, string employeeName)
        {
            const string sql = "exec spOrgLeaderSubChargeListView '{0}','{1}'";
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("EmployeeID", typeof(string));
            dtResult.Columns.Add("EmployeeName", typeof(string));
            dtResult.Columns.Add("DepartmentName", typeof(string));

            DataTable dtSource = this.DatabaseAccess.ExecuteDataset(string.Format(sql, departmentName, employeeName)).Tables[0].Copy();
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                string strEmployeeID = string.Empty, strOldEmployeeID = string.Empty;
                string strEmployeeName = string.Empty;
                string strDepartmentName = string.Empty;
                DataRow dr = null;
                foreach (DataRow row in dtSource.Rows)
                {
                    strEmployeeID = Convert.ToString(row["EmployeeID"]);
                    if (!string.IsNullOrEmpty(strOldEmployeeID) && !string.Equals(strEmployeeID, strOldEmployeeID))
                    {
                        dr = dtResult.NewRow();
                        dr[0] = strOldEmployeeID;
                        dr[1] = strEmployeeName;
                        dr[2] = strDepartmentName.Substring(1);
                        dtResult.Rows.Add(dr);
                        strDepartmentName = string.Empty;
                    }
                    strEmployeeName = Convert.ToString(row["EmployeeName"]);
                    strDepartmentName += string.Format(",{0}", row["DepartmentName"]);
                    strOldEmployeeID = strEmployeeID;
                }
                dr = dtResult.NewRow();
                dr[0] = strEmployeeID;
                dr[1] = strEmployeeName;
                dr[2] = strDepartmentName.Substring(1);
                dtResult.Rows.Add(dr);
            }
            return dtResult;
        }

        /// <summary>
        /// ɾ���ֹܲ��š�
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public bool DeleteLeaderSubCharge(GUIDEx employeeID)
        {
            const string sql = "delete from {0} where EmployeeID='{1}'";
            return this.DatabaseAccess.ExecuteNonQuery(string.Format(sql, this.TableName, employeeID)) > 0;
        }
        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="deptCollection"></param>
        /// <returns></returns>
        public bool UpdateLeaderSubCharge(GUIDEx employeeID, StringCollection deptCollection)
        {
            bool result = false;
            if (employeeID.IsValid)
            {
                this.DeleteLeaderSubCharge(employeeID);
                if (deptCollection != null && deptCollection.Count > 0)
                {
                    foreach (string dpt in deptCollection)
                    {
                        OrgLeaderSubCharge data = new OrgLeaderSubCharge();
                        data.EmployeeID = employeeID;
                        data.DepartmentID = dpt;
                        this.UpdateRecord(data);
                    }
                }
                result = true;
            }
            return result;
        }

        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public StringCollection LoadLeaderSubCharge(GUIDEx employeeID)
        {
            StringCollection collection = new StringCollection();
            if (employeeID.IsValid)
            {
                DataTable dtSource = this.GetAllRecord(new string[] { "DepartmentID" }, string.Format("EmployeeID='{0}'", employeeID), null);
                if (dtSource != null)
                {
                    foreach (DataRow row in dtSource.Rows)
                    {
                        collection.Add(Convert.ToString(row["DepartmentID"]));
                    }
                }
            }
            return collection;
        }
	}

}
