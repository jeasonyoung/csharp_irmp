//================================================================================
//  FileName: CredentialSoapHeader.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/5/26
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
using System.Web.Services.Protocols;
namespace iPower.IRMP.SSOService
{
    /// <summary>
    /// 授权访问单点登录的验证。
    /// </summary>
    public class CredentialSoapHeader : SoapHeader
    {
        #region 成员变量，构造函数。
        string appSystemID, appSystemPwd;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public CredentialSoapHeader()
        {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="appSystemID">应用系统注册ID。</param>
        /// <param name="appSystemPwd">应用系统注册密码。</param>
        public CredentialSoapHeader(string appSystemID, string appSystemPwd)
        {
            this.appSystemID = appSystemID;
            this.appSystemPwd = appSystemPwd;
        }
        #endregion

        #region 属性。
        /// <summary>
        /// 获取或设置应用系统注册ID。
        /// </summary>
        public string AppSystemID
        {
            get { return this.appSystemID; }
            set { this.appSystemID = value; }
        }
        /// <summary>
        /// 获取或设置应用系统注册密码。
        /// </summary>
        public string AppSystemPwd
        {
            get { return this.appSystemPwd;}
            set { this.appSystemPwd = value; }
        }
        #endregion
    }
}
