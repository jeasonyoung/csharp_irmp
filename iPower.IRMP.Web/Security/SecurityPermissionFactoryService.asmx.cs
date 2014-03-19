//================================================================================
//  FileName: SecurityPermissionFacotryService.asmx.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/29
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

using iPower.Platform.Security;
using iPower.IRMP.Security;
using iPower.IRMP.Security.Engine;
using iPower.IRMP.Security.Engine.Persistence;
namespace iPower.IRMP.Security.Web
{
    /// <summary>
    /// SecurityPermissionFacotryService。
    /// </summary>
    [WebService(Namespace = "http://ipower.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SecurityPermissionFactoryService : System.Web.Services.WebService
    {
        #region 成员变量，构造函数。
        ISecurityPermissionFactory securityPermissionFactory = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public SecurityPermissionFactoryService()
        {
            this.securityPermissionFactory = new SecurityPermissionFactory();
        }
        #endregion
        
        /// <summary>
        /// 获取权限。
        /// </summary>
        /// <param name="systemID">系统ID。</param>
        /// <param name="moduleID">模块ID。</param>
        /// <param name="employeeID">用户ID。</param>
        /// <returns>模块权限集合。</returns>
        [WebMethod(Description = "获取权限。")]
        public SecurityPermissionCollection ModulePermissions(string systemID, string moduleID, string employeeID)
        {
            return this.securityPermissionFactory.ModulePermissions(systemID, moduleID, employeeID) as SecurityPermissionCollection;
        }
    }
}
