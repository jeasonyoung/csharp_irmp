//================================================================================
//  FileName: IAuthorizedToVerify.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/9
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
namespace iPower.IRMP.SysMgr
{
    /// <summary>
    /// 授权验证接口。
    /// </summary>
    public interface IAuthorizedToVerify
    {
        /// <summary>
        /// 验证系统授权。
        /// </summary>
        /// <param name="systemID">系统ID。</param>
        /// <param name="authPassword">授权密码。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>获得授权返回true,否则返回false。</returns>
        bool AppAuthorization(GUIDEx systemID, string authPassword, out string err);
        /// <summary>
        /// 验证用户授权。
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <param name="systemID">系统ID。</param>
        /// <param name="clientIP">用户登录IP地址。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>获得授权返回true,否则返回false。</returns>
        bool UserAuthorizationVerification(GUIDEx employeeID, GUIDEx systemID, string clientIP, out string err);
    }
}
