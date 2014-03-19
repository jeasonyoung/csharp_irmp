//================================================================================
//  FileName: SecurityFactory.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/8/5
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

using iPower.Logs;
using iPower.IRMP.Org;
using iPower.IRMP.Security;
namespace iPower.IRMP.Security.Poxy
{
    /// <summary>
    /// 安全模块代理工厂。
    /// </summary>
    public class SecurityFactory : ISecurityFactory
    {
        #region 成员变量，构造函数。
        LogContainer log = null;
        ModuleConfiguration config = null;
        SecurityFactoryProviderServicePoxy service = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public SecurityFactory()
        {
            this.config = ModuleConfiguration.ModuleConfig;
            this.log = new LogContainer(this.config);

            this.service = new SecurityFactoryProviderServicePoxy();
            this.service.Url = this.config.SecurityFactoryUrl;
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
            AppSystemCollection collection = new AppSystemCollection();
            try
            {
                Poxy.AppSystem[] apps = this.service.AppRegister(appName);
                if (apps != null)
                {
                    foreach (Poxy.AppSystem app in apps)
                    {
                        Security.AppSystem appSystem = new Security.AppSystem();
                        appSystem.AppID = app.AppID;
                        appSystem.AppName = app.AppName;
                        collection.Add(appSystem);
                    }
                }
            }
            catch (Exception e)
            {
                this.log.CreateErrorLog(e.Message);
                throw e;
            }
            return collection;
        }
        /// <summary>
        /// 获取角色集合。
        /// </summary>
        /// <returns></returns>
        public SecurityRoleCollection Roles()
        {
            SecurityRoleCollection collection = new SecurityRoleCollection();
            try
            {
                Poxy.SecurityRole[] roles = this.service.Roles();
                if (roles != null)
                {
                    foreach (Poxy.SecurityRole sr in roles)
                    {
                        Security.SecurityRole role = new Security.SecurityRole();
                        role.ParentRoleID = sr.ParentRoleID;
                        role.RoleID = sr.RoleID;
                        role.RoleName = sr.RoleName;
                        collection.Add(role);
                    }
                }
            }
            catch (Exception e)
            {
                this.log.CreateErrorLog(e.Message);
                throw e;
            }
            return collection;
        }
        /// <summary>
        /// 获取用户角色集合。
        /// </summary>
        /// <param name="systemID"></param>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public SecurityRoleCollection GetEmployeeRoles(string systemID, string employeeID)
        {
            SecurityRoleCollection collection = new SecurityRoleCollection();
            try
            {
                if (!string.IsNullOrEmpty(employeeID))
                {
                    Poxy.SecurityRole[] roles = this.service.GetEmployeeRoles(systemID, employeeID);
                    if (roles != null && roles.Length > 0)
                    {
                        foreach (Poxy.SecurityRole sr in roles)
                        {
                            Security.SecurityRole role = new Security.SecurityRole();
                            role.RoleID = sr.RoleID;
                            role.RoleName = sr.RoleName;
                            role.ParentRoleID = sr.ParentRoleID;
                            collection.Add(role);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                this.log.CreateErrorLog(e.Message);
                throw e;
            }
            return collection;
        }
        /// <summary>
        /// 根据角色ID获取用户信息。
        /// </summary>
        /// <param name="roleID">角色ID。</param>
        /// <returns></returns>
        public OrgEmployeeCollection GetAllEmployeeByRole(string roleID)
        {
            OrgEmployeeCollection collection = new OrgEmployeeCollection();
            try
            {
                Poxy.OrgEmployee[] employees = this.service.GetAllEmployeeByRole(roleID);
                if (employees != null)
                {
                    foreach (Poxy.OrgEmployee employee in employees)
                    {
                        Org.OrgEmployee oe = new Org.OrgEmployee();
                        oe.DepartmentID = employee.DepartmentID;
                        oe.EmployeeID = employee.EmployeeID;
                        oe.EmployeeName = employee.EmployeeName;
                        oe.PostID = employee.PostID;
                        collection.Add(oe);
                    }
                }
            }
            catch (Exception e)
            {
                this.log.CreateErrorLog(e.Message);
                throw e;
            }
            return collection;
        }

        #endregion
    }
}
