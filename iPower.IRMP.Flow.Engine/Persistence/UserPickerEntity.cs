//================================================================================
//  FileName:UserPickerEntity.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-16 09:37:02
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
// ----  ------  -----------
//
//================================================================================
//  Copyright (C) 2009-2010 Jeason Young Corporation
//================================================================================

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Data;

using iPower;
using iPower.Platform.Engine.DataSource;
using iPower.IRMP.Org;
using iPower.IRMP.Security;
using iPower.IRMP.Flow;
namespace iPower.IRMP.Flow.Engine.Persistence
{
    /// <summary>
    /// 用户信息实体类。
    /// </summary>
    internal class UserPickerEntity
    {
        #region 成员变量，构造函数。
        IOrgFactory orgFactory = null;
        ISecurityFactory securityFacotry = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public UserPickerEntity()
        {
            this.orgFactory = ModuleConfiguration.ModuleConfig.OrgFacotry;
            this.securityFacotry = ModuleConfiguration.ModuleConfig.SecurityFactory;
        }
        #endregion

        /// <summary>
        /// 绑定用户。
        /// </summary>
        /// <returns></returns>
        public IListControlsData BindEmployees(string departmentName, string sexName, string employeeName, params string[] ignoreEmployeeID)
        {
            if (this.orgFactory != null)
            {
                OrgEmployeeCollection collection = this.orgFactory.GetAllEmployee(employeeName);
                if (collection != null && collection.Count > 0)
                {
                    OrgDepartmentCollection departmentCollection = this.orgFactory.GetAllDepartment(departmentName);
                    if (departmentCollection != null && departmentCollection.Count > 0)
                    {
                        OrgEmployeeCollection employeeCollection = new OrgEmployeeCollection();
                        foreach (OrgDepartment d in departmentCollection)
                        {
                            OrgEmployeeCollection employees = collection.FindByDepartment(d.DepartmentID);
                            if (employees != null && employees.Count > 0)
                                employeeCollection.Add(employees);
                        }
                        collection = new OrgEmployeeCollection();
                        collection.Add(employeeCollection);
                    }

                    if (collection.Count > 0 && ignoreEmployeeID != null && ignoreEmployeeID.Length > 0)
                    {
                        foreach (string eid in ignoreEmployeeID)
                        {
                            OrgEmployee item = collection[eid];
                            if (item != null)
                            {
                                collection.Remove(item);
                                if (collection.Count == 0)
                                    break;
                            }
                        }
                    }

                    if (collection.Count > 0)
                    {
                        foreach (OrgEmployee item in collection)
                            item.EmployeeName = item.ToString();
                    }
                }
                return new ListControlsDataSource("EmployeeName", "EmployeeID", collection);
            }
            return null;
        }
        /// <summary>
        /// 绑定角色。
        /// </summary>
        /// <returns></returns>
        public IListControlsData BindRoles()
        {
            if (this.securityFacotry != null)
                return new ListControlsDataSource("RoleName", "RoleID", this.securityFacotry.Roles());
            return null;
        }
        /// <summary>
        /// 绑定岗位级别。
        /// </summary>
        /// <returns></returns>
        public IListControlsData BindRanks()
        {
            if (this.orgFactory != null)
                return new ListControlsDataSource("RankName", "RankID", this.orgFactory.GetAllRank(null));
            return null;
        }
        /// <summary>
        /// 绑定岗位。
        /// </summary>
        /// <returns></returns>
        public IListControlsData BindPosts()
        {
            if (this.orgFactory != null)
                return new ListControlsDataSource("PostName", "PostID", this.orgFactory.GetAllPost(null));
            return null;
        }


        /// <summary>
        /// 根据用户ID查找用户信息.
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <returns>用户信息集合(用户ID，用户姓名)。</returns>
        public IListControlsData FindByEmployeeID(string[] employeeID)
        {
            if (employeeID != null && employeeID.Length > 0)
            {
                OrgEmployeeCollection collection = new OrgEmployeeCollection();
                foreach (string eid in employeeID)
                {
                    OrgEmployeeCollection employeeCollection = this.orgFactory.GetAllEmployee(eid);
                    if (employeeCollection != null && employeeCollection.Count == 1)
                        collection.Add(employeeCollection);
                }
                if (collection.Count > 0)
                {
                    foreach (OrgEmployee item in collection)
                        item.EmployeeName = item.ToString();
                }
                return new ListControlsDataSource("EmployeeName", "EmployeeID", collection);
            }
            return null;
        }
    }
}
