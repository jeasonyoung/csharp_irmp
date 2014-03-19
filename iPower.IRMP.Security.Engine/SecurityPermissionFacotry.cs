//================================================================================
//  FileName: SecurityPermissionFacotry.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/4/13
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

using iPower;
using iPower.Data;
using iPower.Platform.Security;

using iPower.IRMP.Security;
using iPower.IRMP.Security.Engine.Persistence;
namespace iPower.IRMP.Security.Engine
{
    /// <summary>
    /// 安全权限元操作工厂类。
    /// </summary>
    public class SecurityPermissionFactory : ISecurityPermissionFactory
    {
        #region 成员变量，构造函数。
        SecurityRightEntity securityRightEntity = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public SecurityPermissionFactory()
        {
            this.securityRightEntity = new SecurityRightEntity();
        }
        #endregion

        #region ISecurityPermissionFacotry 成员
        /// <summary>
        /// 获取权限。
        /// </summary>
        /// <param name="systemID">系统ID。</param>
        /// <param name="moduleID">模块ID。</param>
        /// <param name="employeeID">用户ID。</param>
        /// <returns>模块权限集合。</returns>
        public SecurityPermissionCollection ModulePermissions(string systemID, string moduleID, string employeeID)
        {
            return this.securityRightEntity.GetModulePermissions(systemID, moduleID, employeeID);
        }

        #endregion
    }
}
