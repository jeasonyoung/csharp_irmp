//================================================================================
//  FileName: ModuleConfiguration.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/23
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
using System.Collections;
using System.Collections.Generic;
using System.Text;

using iPower.Data;
using iPower.Utility;
using iPower.Data.DataAccess;
using iPower.Configuration;
using iPower.WinService.Jobs;

using iPower.IRMP.Org;
using iPower.IRMP.Security;
namespace iPower.IRMP.Flow.WinService.Persistence
{
    /// <summary>
    /// 模块配置键名。
    /// </summary>
    internal class ModuleConfigurationKeys : JobConfigurationKey
    {
        /// 用户信息程序集配置键。
        /// </summary>
        public const string OrgFactoryAssemblyKey = "OrgFactoryAssembly";
        /// <summary>
        /// 安全管理应用系统注册程序集配置键。
        /// </summary>
        public const string SecurityFactoryAssemblyKey = "SecurityFactoryAssembly";
    }
    /// <summary>
    ///  模块配置类。
    /// </summary>
    public class ModuleConfiguration : JobConfiguration
    {
        #region 成员变量，构造函数。
        static Hashtable Cache = Hashtable.Synchronized(new Hashtable());
        /// <summary>
        /// 构造函数。
        /// </summary>
        public ModuleConfiguration()
            : base("WinFlowEngineService")
        {
           
        }
        #endregion

        #region 数据库连接。
        /// <summary>
        /// 获取默认数据库连接实体。
        /// </summary>
        public IDBAccess ModuleDefaultDatabase
        {
            get
            {
                ConnectionStringConfiguration conn = this.DefaultDataConnectionString;
                if (conn == null)
                    return null;
                EnumDbType dbType = EnumDbType.SqlServer;
                if (!string.IsNullOrEmpty(conn.ProviderName))
                    dbType = (EnumDbType)Enum.Parse(typeof(EnumDbType), conn.ProviderName, true);

                return DatabaseFactory.Instance(conn.ConnectionString, dbType);
            }
        }
        #endregion

        #region 组织架构。
        /// <summary>
        /// 获取组织架构接口。
        /// </summary>
        public IOrgFactory OrgFactory
        {
            get
            {
                lock (this)
                {
                    IOrgFactory factory = Cache[ModuleConfigurationKeys.OrgFactoryAssemblyKey] as IOrgFactory;
                    //if (factory == null)
                    //{
                    //    string path = this[ModuleConfigurationKeys.OrgFactoryAssemblyKey];
                    //    if (!string.IsNullOrEmpty(path))
                    //    {
                    //        if (path.EndsWith(".asmx", StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase))
                    //            factory = new OrgFactoryProvider(path);
                    //        else
                    //            factory = TypeHelper.Create(path) as IOrgFactory;
                    //    }
                    //    if (factory != null)
                    //        Cache[ModuleConfigurationKeys.OrgFactoryAssemblyKey] = factory;
                    //}
                    return factory;
                }
            }
        }
        #endregion

        #region 安全管理。
        /// <summary>
        /// 获取安全管理接口。
        /// </summary>
        public ISecurityFactory SecurityFactory
        {
            get
            {
                lock (this)
                {
                    ISecurityFactory factory = Cache[ModuleConfigurationKeys.OrgFactoryAssemblyKey] as ISecurityFactory;
                    //if (factory == null)
                    //{
                    //    string path = this[ModuleConfigurationKeys.OrgFactoryAssemblyKey];
                    //    if (!string.IsNullOrEmpty(path))
                    //    {
                    //        if (path.EndsWith(".asmx", StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase))
                    //            factory = new SecurityFactoryProvider(path);
                    //        else
                    //            factory = TypeHelper.Create(path) as ISecurityFactory;
                    //    }
                    //    if (factory != null)
                    //        Cache[ModuleConfigurationKeys.OrgFactoryAssemblyKey] = factory;
                    //}
                    return factory;
                }
            }
        }
        #endregion
    }

    #region 内置类。
    class OrgFactoryProvider : IOrgFactory
    {
        //#region 成员变量，构造函数。
        //Poxy.OrgFactoryProviderService providerService = null;
        ///// <summary>
        ///// 构造函数。
        ///// </summary>
        ///// <param name="url"></param>
        //public OrgFactoryProvider(string url)
        //{
        //    this.providerService = new iPower.IRMP.Flow.WinService.Poxy.OrgFactoryProviderService();
        //    this.providerService.Url = url;
        //}
        //#endregion

        //#region IOrgFactory 成员

        //public OrgDepartmentCollection GetAllDepartment()
        //{
        //    OrgDepartmentCollection collection = new OrgDepartmentCollection();
        //    Poxy.OrgDepartment[] depts = this.providerService.GetAllDepartment();
        //    if (depts != null && depts.Length > 0)
        //    {
        //        foreach (Poxy.OrgDepartment d in depts)
        //        {
        //            OrgDepartment deptment = new OrgDepartment();
        //            deptment.DepartmentID = d.DepartmentID;
        //            deptment.DepartmentName = d.DepartmentName;
        //            deptment.ParentDepartmentID = d.ParentDepartmentID;
        //            collection.Add(deptment);
        //        }
        //    }
        //    return collection;
        //}

        //public OrgDepartmentCollection GetSubCharge(string employeeID)
        //{
        //    OrgDepartmentCollection collection = new OrgDepartmentCollection();
        //    Poxy.OrgDepartment[] depts = this.providerService.GetSubCharge(employeeID);
        //    if (depts != null && depts.Length > 0)
        //    {
        //        foreach (Poxy.OrgDepartment d in depts)
        //        {
        //            OrgDepartment deptment = new OrgDepartment();
        //            deptment.DepartmentID = d.DepartmentID;
        //            deptment.DepartmentName = d.DepartmentName;
        //            deptment.ParentDepartmentID = d.ParentDepartmentID;
        //            collection.Add(deptment);
        //        }
        //    }
        //    return collection;
        //}

        //public OrgRankCollection GetAllRank()
        //{
        //    OrgRankCollection collection = new OrgRankCollection();
        //    Poxy.OrgRank[] ranks = this.providerService.GetAllRank();
        //    if (ranks != null && ranks.Length > 0)
        //    {
        //        foreach (Poxy.OrgRank r in ranks)
        //        {
        //            OrgRank rank = new OrgRank();
        //            rank.ParentRankID = r.ParentRankID;
        //            rank.RankID = r.RankID;
        //            rank.RankName = r.RankName;
        //            collection.Add(rank);
        //        }
        //    }
        //    return collection;
        //}

        //public OrgPostCollection GetAllPost()
        //{
        //    OrgPostCollection collection = new OrgPostCollection();
        //    Poxy.OrgPost[] posts = this.providerService.GetAllPost();
        //    if (posts != null && posts.Length > 0)
        //    {
        //        foreach (Poxy.OrgPost p in posts)
        //        {
        //            OrgPost post = new OrgPost();
        //            post.DepartmentID = p.DepartmentID;
        //            post.ParentPostID = p.ParentPostID;
        //            post.PostID = p.PostID;
        //            post.PostName = p.PostName;
        //            post.RankID = p.RankID;
        //            collection.Add(post);
        //        }
        //    }
        //    return collection;
        //}

        //public OrgEmployeeCollection GetAllEmployee()
        //{
        //    OrgEmployeeCollection collection = new OrgEmployeeCollection();
        //    Poxy.OrgEmployee[] emps = this.providerService.GetAllEmployee();
        //    if (emps != null && emps.Length > 0)
        //    {
        //        foreach (Poxy.OrgEmployee e in emps)
        //        {
        //            OrgEmployee employee = new OrgEmployee();
        //            employee.DepartmentID = e.DepartmentID;
        //            employee.EmployeeID = e.EmployeeID;
        //            employee.EmployeeName = e.EmployeeName;
        //            employee.PostID = e.PostID;
        //            collection.Add(employee);
        //        }
        //    }
        //    return collection;
        //}

        //#endregion

        #region IOrgFactory 成员

        public OrgDepartmentCollection GetAllDepartment(string departmentID)
        {
            throw new NotImplementedException();
        }

        public OrgRankCollection GetAllRank(string rankID)
        {
            throw new NotImplementedException();
        }

        public OrgPostCollection GetAllPost(string postID)
        {
            throw new NotImplementedException();
        }

        public OrgEmployeeCollection GetAllEmployee(string employeeID)
        {
            throw new NotImplementedException();
        }

        public OrgDepartmentCollection GetSubCharge(string employeeID)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    class SecurityFactoryProvider : ISecurityFactory
    {
        //#region 成员变量，构造函数。
        //Poxy.SecurityFactoryProviderService providerService = null;
        ///// <summary>
        ///// 构造函数。
        ///// </summary>
        ///// <param name="url"></param>
        //public SecurityFactoryProvider(string url)
        //{
        //    this.providerService = new Poxy.SecurityFactoryProviderService();
        //    this.providerService.Url = url;
        //}
        //#endregion

        //#region ISecurityFactory 成员

        //public AppSystemCollection AppRegister(string appName)
        //{
        //    AppSystemCollection collection = new AppSystemCollection();
        //    Poxy.AppSystem[] apps = this.providerService.AppRegister(appName);
        //    if (apps != null && apps.Length > 0)
        //    {
        //        foreach (Poxy.AppSystem app in apps)
        //        {
        //            AppSystem appSystem = new AppSystem();
        //            appSystem.AppID = app.AppID;
        //            appSystem.AppName = app.AppName;
        //            collection.Add(appSystem);
        //        }
        //    }
        //    return collection;
        //}

        //public SecurityRoleCollection Roles()
        //{
        //    SecurityRoleCollection collection = new SecurityRoleCollection();
        //    Poxy.SecurityRole[] roles = this.providerService.Roles();
        //    if (roles != null && roles.Length > 0)
        //    {
        //        foreach (Poxy.SecurityRole r in roles)
        //        {
        //            SecurityRole securityRole = new SecurityRole();
        //            securityRole.ParentRoleID = r.ParentRoleID;
        //            securityRole.RoleID = r.RoleID;
        //            securityRole.RoleName = r.RoleName;
        //            collection.Add(securityRole);
        //        }
        //    }
        //    return collection;
        //}

        //public OrgEmployeeCollection GetAllEmployeeByRole(string roleID)
        //{
        //    OrgEmployeeCollection collection = new OrgEmployeeCollection();
        //    Poxy.OrgEmployee[] emps = this.providerService.GetAllEmployeeByRole(roleID);
        //    if (emps != null && emps.Length > 0)
        //    {
        //        foreach (Poxy.OrgEmployee emp in emps)
        //        {
        //            OrgEmployee orgEmployee = new OrgEmployee();
        //            orgEmployee.DepartmentID = emp.DepartmentID;
        //            orgEmployee.EmployeeID = emp.EmployeeID;
        //            orgEmployee.EmployeeName = emp.EmployeeName;
        //            orgEmployee.PostID = emp.PostID;
        //            collection.Add(orgEmployee);
        //        }
        //    }
        //    return collection;
        //}

        //#endregion
        #region ISecurityFactory 成员

        public AppSystemCollection AppRegister(string appName)
        {
            throw new NotImplementedException();
        }

        public SecurityRoleCollection Roles()
        {
            throw new NotImplementedException();
        }

        public SecurityRoleCollection GetEmployeeRoles(string systemID, string employeeID)
        {
            throw new NotImplementedException();
        }

        public OrgEmployeeCollection GetAllEmployeeByRole(string roleID)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    #endregion
}
