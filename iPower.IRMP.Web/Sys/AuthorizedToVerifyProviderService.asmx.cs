//================================================================================
//  FileName: AuthorizedToVerifyProviderService.asmx.cs
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
using System.Web;
using System.Web.Services;

using iPower;
using iPower.IRMP.SysMgr.Engine;
namespace iPower.IRMP.SysMgr.Web
{
    /// <summary>
    /// 验证授权服务。
    /// </summary>
    [WebService(Namespace = "http://ipower.org/",
                Name = "验证授权服务。",
                Description = "验证授权服务。")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AuthorizedToVerifyProviderService : System.Web.Services.WebService
    {
        #region 成员变量，构造函数。
        AuthorizedToVerifyProvider provider = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public AuthorizedToVerifyProviderService()
        {
            this.provider = new AuthorizedToVerifyProvider();
        }
        #endregion

        /// <summary>
        /// 验证系统授权。
        /// </summary>
        /// <param name="systemID">系统ID。</param>
        /// <param name="authPassword">授权密码。</param>
        /// <returns></returns>
        [WebMethod(Description="验证系统授权。")]
        public CallResult AppAuthorization(string systemID, string authPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(systemID))
                    return new CallResult(-1, "系统ID为空。");
                else if (string.IsNullOrEmpty(authPassword))
                    return new CallResult(-1, "授权密码为空。");
                else
                {
                    string err= null;
                    bool result = this.provider.AppAuthorization(systemID, authPassword, out err);
                    return new CallResult(result ? 0 : -1, err);
                }
            }
            catch (Exception e)
            {
                return new CallResult(-1, e.Message);
            }
        }

         /// <summary>
        /// 验证用户授权。
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <param name="systemID">系统ID。</param>
        /// <param name="clientIP">用户登录IP地址。</param>
        /// <returns></returns>
        [WebMethod(Description = "验证用户授权。")]
        public CallResult UserAuthorizationVerification(string employeeID, string systemID, string clientIP)
        {
            try
            {
                if (string.IsNullOrEmpty(employeeID))
                    return new CallResult(-1, "用户ID为空。");
                else if (string.IsNullOrEmpty(systemID))
                    return new CallResult(-1, "系统ID为空。");
                else if (string.IsNullOrEmpty(clientIP))
                    return new CallResult(-1, "用户登录IP地址为空。");
                else
                {
                    string err = null;
                    bool result = this.provider.UserAuthorizationVerification(employeeID, systemID, clientIP, out err);
                    return new CallResult(result ? 0 : -1, err);
                }
            }
            catch (Exception e)
            {
                return new CallResult(-1, e.Message);
            }
        }
    }
}
