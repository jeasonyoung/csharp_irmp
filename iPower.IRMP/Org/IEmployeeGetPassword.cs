//================================================================================
//  FileName: IEmployeeGetPassword.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/16
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

namespace iPower.IRMP.Org
{
    /// <summary>
    /// 用户找回密码接口。
    /// </summary>
    public interface IEmployeeGetPassword
    {
        /// <summary>
        /// 找回密码。
        /// </summary>
        /// <param name="employeeSign">用户登录帐号。</param>
        /// <param name="mode">找回密码方式枚举。</param>
        /// <param name="err">异常或错误信息。</param>
        /// <returns></returns>
        bool GetPassword(string employeeSign, EnumGetPasswordMode mode, out string err);
    }
    /// <summary>
    /// 找回密码方式枚举。
    /// </summary>
    public enum EnumGetPasswordMode
    {
        /// <summary>
        /// 短信。
        /// </summary>
        SMS = 0x00,
        /// <summary>
        /// 邮件。
        /// </summary>
        Email = 0x01
    }
}
