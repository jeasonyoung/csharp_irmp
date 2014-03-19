//================================================================================
//  FileName: SecurityAppRegisterProvider.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/5/30
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
//  ----    ------  -----------------
//
//================================================================================
//  Copyright (C) 2004-2009 Jeason Young Corporation
//================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using iPower;
using iPower.IRMP.Org;
using iPower.IRMP.Security;
using iPower.IRMP.Security.Engine.Persistence;
namespace iPower.IRMP.Security.Engine
{
    /// <summary>
    /// 安全管理应用系统注册数据提供类。
    /// </summary>
    public class SecurityFactoryProvider : ISecurityFactory
    {
        #region 成员变量，构造函数。
        SecurityRegsiterEntity securityRegsiterEntity = null;
        SecurityRoleEntity securityRoleEntity = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public SecurityFactoryProvider()
        {
            this.securityRegsiterEntity = new SecurityRegsiterEntity();
            this.securityRoleEntity = new SecurityRoleEntity();
        }
        #endregion

        #region ISecurityFactory 成员
        /// <summary>
        /// 获取注册系统集合。
        /// </summary>
        /// <param name="appName">应用系统名称。</param>
        /// <returns></returns>
        public AppSystemCollection AppRegister(string appName)
        {
            lock (this)
            {
                AppSystemCollection collection = new AppSystemCollection();
                DataTable dtResult = new DataTable();
                dtResult.Columns.Add("AppID");
                dtResult.Columns.Add("AppName");

                DataTable dtSource = this.securityRegsiterEntity.GetAllRecord(string.Format("SystemName like '%{0}%'", appName));
                if (dtSource != null)
                {
                    DataRow dr = null;
                    foreach (DataRow row in dtSource.Rows)
                    {
                        dr = dtResult.NewRow();
                        dr[0] = row["SystemID"];
                        dr[1] = row["SystemName"];
                        dtResult.Rows.Add(dr);
                    }
                }

                if (dtResult.Rows.Count > 0)
                {
                    collection.InitAssignment(dtResult);
                }
                return collection;
            }
        }
        /// <summary>
        /// 获取角色集合。
        /// </summary>
        /// <returns></returns>
        public SecurityRoleCollection Roles()
        {
            lock (this)
            {
                SecurityRoleCollection collection = new SecurityRoleCollection();
                DataTable dtSource = this.securityRoleEntity.GetAllRecord();
                if (dtSource != null)
                {
                    collection.InitAssignment(dtSource);
                }
                return collection;
            }
        }
        /// <summary>
        /// 获取用户角色集合。
        /// </summary>
        /// <param name="systemID">系统ID。</param>
        /// <param name="employeeID">用户ID。</param>
        /// <returns></returns>
        public SecurityRoleCollection GetEmployeeRoles(string systemID, string employeeID)
        {
            lock (this)
            {
                SecurityRoleCollection collection = new SecurityRoleCollection();
                if (!string.IsNullOrEmpty(systemID) && !string.IsNullOrEmpty(employeeID))
                {
                    IOrgFactory factory = ModuleConfiguration.ModuleConfig.OrgFactory;
                    if (factory != null)
                    {
                        OrgEmployeeCollection employees = factory.GetAllEmployee(employeeID);
                        if (employees != null && employees.Count > 0)
                        {
                            string departmentID = employees[0].DepartmentID;
                            string postID = employees[0].PostID;
                            string rankID = string.Empty;
                            if (!string.IsNullOrEmpty(postID))
                            {
                                OrgPostCollection posts = factory.GetAllPost(postID);
                                if (posts != null && posts.Count > 0)
                                    rankID = posts[0].RankID;
                            }
                            DataTable dtSource = this.securityRoleEntity.GetEmployeeRoles(systemID, employeeID, departmentID, rankID, postID);
                            if (dtSource != null)
                                collection.InitAssignment(dtSource);
                        }
                    }
                }
                return collection;
            }
        }
        /// <summary>
        /// 根据角色ID获取用户信息。
        /// </summary>
        /// <param name="roleID">角色ID。</param>
        /// <returns></returns>
        public OrgEmployeeCollection GetAllEmployeeByRole(string roleID)
        {
            lock (this)
            {
                OrgEmployeeCollection collection = new OrgEmployeeCollection();
                IOrgFactory factory = ModuleConfiguration.ModuleConfig.OrgFactory;
                if (factory != null)
                {
                    //角色下用户。
                    List<GUIDEx> listEmployeeID = new SecurityRoleEmployeeEntity().GetAllEmployee(roleID);
                    //角色下岗位级别。
                    List<GUIDEx> listRankID = new SecurityRoleRankEntity().GetAllRank(roleID);
                    //角色下岗位。
                    List<GUIDEx> listPostID = new SecurityRolePostEntity().GetAllPost(roleID);
                    //角色下部门。
                    List<GUIDEx> listDepartmentID = new SecurityRoleDepartmentEntity().GetAllDeprtment(roleID);

                    #region 角色下用户。
                    if (listEmployeeID != null && listEmployeeID.Count > 0)
                    {
                        foreach (GUIDEx eid in listEmployeeID)
                        {
                            OrgEmployeeCollection employeeCollection = factory.GetAllEmployee(eid);
                            if (employeeCollection != null && employeeCollection.Count == 1)
                            {
                                collection.Add(employeeCollection[0]);
                            }
                        }
                    }
                    #endregion

                    #region 岗位级别。
                    if (listRankID != null && listRankID.Count > 0)
                    {
                        OrgPostCollection postCollection = factory.GetAllPost(null);
                        if (postCollection != null && postCollection.Count > 0)
                        {
                            if (listPostID == null)
                                listPostID = new List<GUIDEx>();
                            foreach (GUIDEx rankID in listRankID)
                            {
                                OrgPostCollection posts = postCollection.FindByRank(rankID);
                                if (posts != null && posts.Count > 0)
                                {
                                    foreach (OrgPost p in posts)
                                    {
                                        if (!listPostID.Contains(p.PostID))
                                            listPostID.Add(p.PostID);
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    OrgEmployeeCollection allEmployeeCollection = null;

                    #region 岗位下用户。
                    if (listPostID != null && listPostID.Count > 0)
                    {
                        if (allEmployeeCollection == null)
                            allEmployeeCollection = factory.GetAllEmployee(null);

                        foreach (GUIDEx postID in listPostID)
                        {
                            OrgEmployeeCollection employees = allEmployeeCollection.FindByPost(postID);
                            if (employees != null && employees.Count > 0)
                            {
                                collection.Add(employees);
                            }
                        }
                    }
                    #endregion

                    #region 部门下用户。
                    if (listDepartmentID != null && listDepartmentID.Count > 0)
                    {
                        if (allEmployeeCollection == null)
                            allEmployeeCollection = factory.GetAllEmployee(null);

                        foreach (GUIDEx deptID in listDepartmentID)
                        {
                            OrgEmployeeCollection employees = allEmployeeCollection.FindByDepartment(deptID);
                            if (employees != null && employees.Count > 0)
                            {
                                collection.Add(employees);
                            }
                        }
                    }
                    #endregion
                }
                return collection;
            }
        }

        #endregion
    }
}
