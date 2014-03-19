//================================================================================
//  FileName: ISecurityFactory.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/27
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

using iPower.IRMP.Org;
namespace iPower.IRMP.Security
{
    /// <summary>
    /// 安全模块接口工厂。
    /// </summary>
    public interface ISecurityFactory
    {
        /// <summary>
        /// 获取注册系统集合。
        /// </summary>
        /// <param name="appName">应用系统名称。</param>
        /// <returns></returns>
        AppSystemCollection AppRegister(string appName);
        /// <summary>
        /// 获取角色集合。
        /// </summary>
        /// <returns></returns>
        SecurityRoleCollection Roles();
        /// <summary>
        /// 获取用户角色集合。
        /// </summary>
        /// <param name="systemID">系统ID。</param>
        /// <param name="employeeID">用户ID。</param>
        /// <returns></returns>
        SecurityRoleCollection GetEmployeeRoles(string systemID,string employeeID);
        /// <summary>
        /// 根据角色ID获取用户信息。
        /// </summary>
        /// <param name="roleID">角色ID。</param>
        /// <returns></returns>
        OrgEmployeeCollection GetAllEmployeeByRole(string roleID);
    }
}
