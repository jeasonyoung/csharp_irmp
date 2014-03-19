//================================================================================
//  FileName: AuthorizedToVerifyService.cs
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
using iPower.IRMP;
using iPower.IRMP.SysMgr;
using iPower.IRMP.SysMgr.Engine.Persistence;
namespace iPower.IRMP.SysMgr.Engine
{
    /// <summary>
    /// 验证授权服务。
    /// </summary>
    public class AuthorizedToVerifyProvider : IAuthorizedToVerify
    {
        #region 成员变量，构造函数。
        SysMgrAppAuthorizationEntity sysMgrAppAuthorizationEntity = null;
        SysMgrEmployeeAuthorizationEntity sysMgrEmployeeAuthorizationEntity = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public AuthorizedToVerifyProvider()
        {
            this.sysMgrAppAuthorizationEntity = new SysMgrAppAuthorizationEntity();
            this.sysMgrEmployeeAuthorizationEntity = new SysMgrEmployeeAuthorizationEntity();
        }
        #endregion

        #region IAuthorizedToVerify 成员
        /// <summary>
        /// 验证系统授权。
        /// </summary>
        /// <param name="systemID">系统ID。</param>
        /// <param name="authPassword">授权密码。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>获得授权返回true,否则返回false。</returns>
        public bool AppAuthorization(GUIDEx systemID, string authPassword, out string err)
        {
            try
            {
                return this.sysMgrAppAuthorizationEntity.AppAuthorization(systemID, authPassword, out err);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return false;
        }
        /// <summary>
        /// 验证用户授权。
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <param name="systemID">系统ID。</param>
        /// <param name="clientIP">用户登录IP地址。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>获得授权返回true,否则返回false。</returns>
        public bool UserAuthorizationVerification(GUIDEx employeeID, GUIDEx systemID, string clientIP, out string err)
        {
            try
            {
                return this.sysMgrEmployeeAuthorizationEntity.UserAuthorizationVerification(employeeID, systemID, clientIP, out err);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return false;
        }

        #endregion
    }
}
