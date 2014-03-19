//================================================================================
//  FileName: IUserLogin.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/14
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

namespace iPower.IRMP.SSO
{
    /// <summary>
    /// 用户登录接口。
    /// </summary>
    public interface IUserLogin
    {
        /// <summary>
        /// 用户登录。
        /// </summary>
        /// <param name="userSign">用户帐号。</param>
        /// <param name="password">用户密码。</param>
        /// <param name="msg">返回信息。</param>
        /// <returns>登录失败返回false,成功返回true并自动跳转页面。</returns>
        bool SignIn(string userSign, string password, out string msg);
        /// <summary>
        /// 用户注销。
        /// </summary>
        /// <param name="msg">返回信息。</param>
        /// <returns>失败返回false,成功返回true并自动跳转页面。</returns>
        bool SignOut(out string msg);
        /// <summary>
        /// 修改密码。
        /// </summary>
        /// <param name="userSign">用户帐号。</param>
        /// <param name="oldPassword">用户旧密码。</param>
        /// <param name="newPassword">用户新密码。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>如果修改成功，则返回True；否则返回false。</returns>
        bool ChangePassword(string userSign, string oldPassword, string newPassword, out string err); 
    }
}
