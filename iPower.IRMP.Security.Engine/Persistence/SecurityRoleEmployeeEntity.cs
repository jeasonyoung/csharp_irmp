//================================================================================
// FileName: SecurityRoleEmployeeEntity.cs
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
using iPower.IRMP.Org;
using iPower.IRMP.Security.Engine.Domain;
namespace iPower.IRMP.Security.Engine.Persistence
{
	///<summary>
	///SecurityRoleEmployeeEntity实体类。
	///</summary>
	internal class SecurityRoleEmployeeEntity: DbModuleEntity<SecurityRoleEmployee>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public SecurityRoleEmployeeEntity()
		{

		}
		#endregion

        #region 数据操作。
        /// <summary>
        /// 获取角色下的用户ID。
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<GUIDEx> GetAllEmployee(GUIDEx roleID)
        {
            List<GUIDEx> list = new List<GUIDEx>();
            if (roleID.IsValid)
            {
                DataTable dtSource = this.GetAllRecord(string.Format("RoleID='{0}'", roleID));
                foreach (DataRow row in dtSource.Rows)
                {
                    list.Add(new GUIDEx(row["EmployeeID"]));
                }
            }
            return list;
        }
        /// <summary>
        /// 获取角色下的用户。
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void GetEmployees(GUIDEx roleID, out string[] text, out string [] value)
        {
            text = value = null;
            if (roleID.IsValid)
            {
                DataTable dtSource = this.GetAllRecord(string.Format("RoleID='{0}'", roleID));
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    List<string> listText = new List<string>(), listValue = new List<string>();
                    foreach (DataRow row in dtSource.Rows)
                    {
                        listText.Add(Convert.ToString(row["EmployeeName"]));
                        listValue.Add(Convert.ToString(row["EmployeeID"]));
                    }
                    text = new string[listText.Count];
                    listText.CopyTo(text, 0);
                    value = new string[listValue.Count];
                    listValue.CopyTo(value, 0);
                }
            }
        }
        /// <summary>
        /// 绑定用户数据。
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public IListControlsData BindEmployees(params string[] employeeID)
        {
            const string sql = "select distinct EmployeeID,EmployeeName from {0} where (EmployeeID in ('{1}'))";
            DataTable dtSource = this.DatabaseAccess.ExecuteDataset(string.Format(sql, this.TableName, string.Join("','", employeeID))).Tables[0];
            if (dtSource != null)
            {
                return new ListControlsDataSource("EmployeeName", "EmployeeID", dtSource);
            }
            return null;
        }
        /// <summary>
        ///  列表数据源。
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="employeeName"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string roleName, string employeeName)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("RoleID", typeof(string));
            dtResult.Columns.Add("RoleName", typeof(string));
            dtResult.Columns.Add("EmployeeNames", typeof(string));

            const string sql = "exec spSecurityRoleEmployeeListView '{0}','{1}'";
            DataTable dtSource = this.DatabaseAccess.ExecuteDataset(string.Format(sql, roleName, employeeName)).Tables[0];
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                string strRoleID = null, strRoleName = null, strEmps = string.Empty;
                string oldStrRoleID = null;
                foreach (DataRow row in dtSource.Rows)
                {
                    strRoleID = Convert.ToString(row["RoleID"]);
                    if (!string.IsNullOrEmpty(oldStrRoleID) && (oldStrRoleID != strRoleID))
                    {
                        DataRow dr = dtResult.NewRow();
                        dr["RoleID"] = oldStrRoleID;
                        dr["RoleName"] = strRoleName;
                        dr["EmployeeNames"] = strEmps;
                        dtResult.Rows.Add(dr);
                        strEmps = string.Empty;
                    }
                    strRoleName = Convert.ToString(row["RoleName"]);
                    if (!string.IsNullOrEmpty(strEmps))
                        strEmps += ",";
                    strEmps += Convert.ToString(row["EmployeeName"]);
                    oldStrRoleID = strRoleID;
                }
                if (!string.IsNullOrEmpty(oldStrRoleID))
                {
                    DataRow dr = dtResult.NewRow();
                    dr["RoleID"] = oldStrRoleID;
                    dr["RoleName"] = strRoleName;
                    dr["EmployeeNames"] = strEmps;
                    dtResult.Rows.Add(dr);
                }
                dtResult.AcceptChanges();
            }
            return dtResult.Copy();
        }
        /// <summary>
        /// 更新角色用户。
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="emps"></param>
        /// <returns></returns>
        public bool UpdateRoleEmployee(GUIDEx roleID, string[] emps)
        {
            bool result = false;
            if (roleID.IsValid)
            {
                const string del_sql = "delete from {0} where RoleID='{1}'";
                result = this.DatabaseAccess.ExecuteNonQuery(string.Format(del_sql, this.TableName, roleID)) > 0;
                if (emps != null && emps.Length > 0)
                {
                    IOrgFactory factory = this.ModuleConfig.OrgFactory;
                    if (factory != null)
                    {                        
                        foreach (string eid in emps)
                        {
                            OrgEmployeeCollection employees = factory.GetAllEmployee(eid);
                            if (employees != null && employees.Count > 0)
                            {
                                SecurityRoleEmployee data = new SecurityRoleEmployee();
                                data.RoleID = roleID;
                                data.EmployeeID = employees[0].EmployeeID;
                                data.EmployeeName = string.Format("{0}[{1}]", employees[0].EmployeeName, employees[0].EmployeeSign);
                                result = this.UpdateRecord(data);
                            }
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 批量删除。
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool BatchDeleteRoleEmployee(StringCollection roles)
        {
            const string del_sql = "delete from {0} where RoleID in ('{1}')";
            if (roles != null && roles.Count > 0)
            {
                string[] roleIDs = new string[roles.Count];
                roles.CopyTo(roleIDs, 0);
                return this.DatabaseAccess.ExecuteNonQuery(string.Format(del_sql, this.TableName, string.Join("','", roleIDs))) > 0;
            }
            return false;
        }
        #endregion
    }

}
