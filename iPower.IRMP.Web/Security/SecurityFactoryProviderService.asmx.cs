//================================================================================
//  FileName: SecurityFactoryProviderService.asmx.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/28
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
using System.Web;
using System.Web.Services;

using iPower.IRMP.Org;
using iPower.IRMP.Security;
using iPower.IRMP.Security.Engine;
namespace iPower.IRMP.Security.Web
{
    /// <summary>
    /// SecurityFactoryProviderService。
    /// </summary>
    [WebService(Namespace = "http://ipower.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SecurityFactoryProviderService : System.Web.Services.WebService, ISecurityFactory
    {
        #region 成员变量，构造函数。
        ISecurityFactory factory = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public SecurityFactoryProviderService()
        {
            this.factory = new SecurityFactoryProvider();
        }
        #endregion


        #region ISecurityFactory 成员
        /// <summary>
        /// 获取注册系统集合。
        /// </summary>
        /// <param name="appName">应用系统名称。</param>
        /// <returns></returns>
        [WebMethod(Description = "获取注册系统集合。")]
        public AppSystemCollection AppRegister(string appName)
        {
            return this.factory.AppRegister(appName);
        }
        /// <summary>
        /// 获取角色集合。
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "获取角色集合。")]
        public SecurityRoleCollection Roles()
        {
            return this.factory.Roles();
        }
        /// <summary>
        /// 获取用户角色集合。
        /// </summary>
        /// <param name="systemID">系统ID。</param>
        /// <param name="employeeID">用户ID。</param>
        /// <returns></returns>
        [WebMethod(Description="获取用户角色集合。")]
        public SecurityRoleCollection GetEmployeeRoles(string systemID, string employeeID)
        {
            return this.factory.GetEmployeeRoles(systemID, employeeID);
        }
        /// <summary>
        /// 根据角色ID获取用户信息。
        /// </summary>
        /// <param name="roleID">角色ID。</param>
        /// <returns></returns>
        [WebMethod(Description = "根据角色ID获取用户信息。")]
        public OrgEmployeeCollection GetAllEmployeeByRole(string roleID)
        {
            return this.factory.GetAllEmployeeByRole(roleID);
        }

        #endregion

    }
}
