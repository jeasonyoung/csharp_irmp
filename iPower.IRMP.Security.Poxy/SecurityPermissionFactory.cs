//================================================================================
//  FileName: SecurityPermissionFacotry.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/9/28
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
using iPower.Logs;
using iPower.IRMP.Security;
using iPower.Platform.Security;
namespace iPower.IRMP.Security.Poxy
{
    /// <summary>
    /// 安全权限元操作工厂类。
    /// </summary>
    public class SecurityPermissionFacotry : ISecurityPermissionFactory
    {
        #region 成员变量，构造函数。
        LogContainer log = null;
        ModuleConfiguration config = null;
        SecurityPermissionFactoryServicePoxy service = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public SecurityPermissionFacotry()
        {
            this.config = ModuleConfiguration.ModuleConfig;
            this.log = new LogContainer(this.config);

            this.service = new SecurityPermissionFactoryServicePoxy();
            this.service.Url = this.config.SecurityPermissionUrl;
        }
        #endregion

        #region ISecurityPermissionFacotry 成员
        /// <summary>
        /// 获取权限。
        /// </summary>
        /// <param name="systemID">系统ID。</param>
        /// <param name="moduleID">模块ID。</param>
        /// <param name="employeeID">用户ID。</param>
        /// <returns>模块权限集合。</returns>
        public SecurityPermissionCollection ModulePermissions(string systemID, string moduleID, string employeeID)
        {
            SecurityPermissionCollection collection = new SecurityPermissionCollection();
            try
            {
                Poxy.SecurityPermission[] sps = this.service.ModulePermissions(systemID, moduleID, employeeID);
                if (sps != null && sps.Length > 0)
                {
                    foreach (Poxy.SecurityPermission p in sps)
                    {
                        iPower.Platform.Security.SecurityPermission sp = new iPower.Platform.Security.SecurityPermission();
                        sp.PermissionID = p.PermissionID;
                        sp.PermissionName = p.PermissionName;
                        collection.Add(sp);
                    }
                }
            }
            catch (Exception e)
            {
                this.log.CreateErrorLog(e.Message);
                throw e;
            }
            return collection;
        }

        #endregion
    }
}
