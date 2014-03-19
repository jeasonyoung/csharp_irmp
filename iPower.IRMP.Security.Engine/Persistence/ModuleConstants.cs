//================================================================================
//  FileName: ModuleConstants.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/2/24
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

namespace iPower.IRMP.Security.Engine.Persistence
{
    /// <summary>
    /// 模块常量。
    /// </summary>
    public static class ModuleConstants
    {
        /// <summary>
        /// 应用系统注册模块ID。
        /// </summary>
        public const string Regsiter_ModuleID = "AH000000000000000000000000000101";
        /// <summary>
        /// 系统模块注册模块ID。
        /// </summary>
        public const string Module_ModuleID = "AH000000000000000000000000000201";
        /// <summary>
        /// 权限元数据模块ID。
        /// </summary>
        public const string Action_ModuleID = "AH000000000000000000000000000301";
        /// <summary>
        /// 模块权限模块ID。
        /// </summary>
        public const string Right_ModuleID = "AH000000000000000000000000000401";
        /// <summary>
        /// 角色模块ID。
        /// </summary>
        public const string Role_ModuleID = "AH000000000000000000000000000102";
        /// <summary>
        /// 角色权限模块ID。
        /// </summary>
        public const string RoleRight_ModuleID = "AH000000000000000000000000000202";
        /// <summary>
        /// 角色岗位级别模块ID。
        /// </summary>
        public const string RoleRank_ModuleID = "AH000000000000000000000000000302";
        /// <summary>
        /// 角色岗位模块ID。
        /// </summary>
        public const string RolePost_ModuleID = "AH000000000000000000000000000402";
        /// <summary>
        /// 角色部门模块ID。
        /// </summary>
        public const string RoleDepartment_ModuleID = "AH000000000000000000000000000502";
        /// <summary>
        /// 角色用户模块ID。
        /// </summary>
        public const string RoleEmployee_ModuleID = "AH000000000000000000000000000602";
    }
}
