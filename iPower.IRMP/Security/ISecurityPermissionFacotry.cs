//================================================================================
//  FileName: ISecurityPermissionFacotry.cs
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
using iPower.Platform.Security;
namespace iPower.IRMP.Security
{
    /// <summary>
    /// 安全权限元操作工厂接口。
    /// </summary>
    public interface ISecurityPermissionFactory
    {
        /// <summary>
        /// 获取权限。
        /// </summary>
        /// <param name="systemID">系统ID。</param>
        /// <param name="moduleID">模块ID。</param>
        /// <param name="employeeID">用户ID。</param>
        /// <returns>模块权限集合。</returns>
        SecurityPermissionCollection ModulePermissions(string systemID, string moduleID, string employeeID);
    }
}
