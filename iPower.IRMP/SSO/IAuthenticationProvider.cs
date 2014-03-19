//================================================================================
//  FileName: IAuthenticationProvider.cs
//  Desc:提供单点登录身份认证接口。
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/5/27
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
namespace iPower.IRMP.SSO
{
    /// <summary>
    /// 提供单点登录身份认证接口。
    /// </summary>
    public interface IAuthenticationProvider
    {
        /// <summary>
        /// 验证用户名和密码。
        /// </summary>
        /// <param name="userSign">用户账号。</param>
        /// <param name="password">用户密码。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>如果用户名和密码有效，则返回用户信息；否则返回null。</returns>
        IUser UserAuthorizationVerification(string userSign, string password, out string err);
        /// <summary>
        /// 修改密码。
        /// </summary>
        /// <param name="userSign">用户账号。</param>
        /// <param name="oldPassword">用户旧密码。</param>
        /// <param name="newPassword">用户新密码。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>如果修改成功，则返回True；否则返回false。</returns>
        bool ChangePassword(string userSign, string oldPassword, string newPassword, out string err);
    }
}
