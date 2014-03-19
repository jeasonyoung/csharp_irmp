//================================================================================
//  FileName: PermissionModule.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/4/13
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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;

using iPower;
using iPower.Logs;
using iPower.Platform;
using iPower.Platform.Security;
namespace iPower.IRMP.Security.Client
{
    /// <summary>
    /// 权限模块。
    /// </summary>
    public class PermissionModule : IHttpModule
    {
        #region 成员变量，构造函数。
        AuthenticateUserModulesProvider authenticateUserModules = null;
        LogContainer log = null;
        static Hashtable Cache = Hashtable.Synchronized(new Hashtable());
        /// <summary>
        /// 构造函数。
        /// </summary>
        public PermissionModule()
        {

            this.authenticateUserModules = Cache[typeof(AuthenticateUserModulesProvider)] as AuthenticateUserModulesProvider;
            if (this.authenticateUserModules == null)
            {
                ISecurityPermissionFactory permissionFactory = ModuleConfiguration.ModuleConfigurationInstance.SecurityPermissionFactory;
                if (permissionFactory != null)
                {
                    this.authenticateUserModules = new AuthenticateUserModulesProvider(permissionFactory);
                    Cache[typeof(AuthenticateUserModulesProvider)] = this.authenticateUserModules;
                }
            }

            this.log = Cache[typeof(LogContainer)] as LogContainer;
            if (this.log == null)
            {
                this.log = new LogContainer(ModuleConfiguration.ModuleConfigurationInstance);
                Cache[typeof(LogContainer)] = this.log;
            }
        }
        #endregion

        #region IHttpModule 成员
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += new EventHandler(delegate(object sender, EventArgs e)
            {
                lock (this)
                {
                    HttpApplication app = sender as HttpApplication;
                    if (app != null && app.Context != null && app.Context.Handler != null)
                    {
                        System.Web.UI.Page page = app.Context.Handler as System.Web.UI.Page;
                        if (page != null)
                        {
                            page.LoadComplete += new EventHandler(delegate(object o, EventArgs ex)
                            {
                                try
                                {
                                    ISystem sys = o as ISystem;
                                    IUser user = o as IUser;
                                    ISecurity sec = o as ISecurity;
                                    IMenuData menu = o as IMenuData;
                                    if (sys != null && user != null && sec != null && menu != null)
                                    {
                                        //控制菜单。
                                        if (this.authenticateUserModules != null && menu.MenuData != null && menu.MenuData.Count > 0 &&
                                            sys.CurrentSystemID.IsValid && user.CurrentUserID.IsValid)
                                        {
                                            ModuleDefineCollection old = menu.MenuData;
                                            string key = string.Format("MenuData_{0}_{1}", sys.CurrentSystemID, user.CurrentUserID);
                                            try
                                            {
                                                menu.MenuData = this.authenticateUserModules.AuthenticateUserModules(menu.MenuData, sys.CurrentSystemID, user.CurrentUserID);
                                                Cache[key] = menu.MenuData;
                                            }
                                            catch (Exception x)
                                            {
                                                ModuleDefineCollection mc = Cache[key] as ModuleDefineCollection;
                                                if (mc == null)
                                                {
                                                    mc = old;
                                                }
                                                menu.MenuData = mc;
                                                string err = string.Format("Message:{0}\r\nSource:{1}\r\nStackTrace:{2}", x.Message, x.Source, x.StackTrace);
                                                this.log.CreateErrorLog(err);
                                            }
                                        }
                                        //控制按钮权限。
                                        if (this.authenticateUserModules != null && sec.SecurityID.IsValid && sys.CurrentSystemID.IsValid && user.CurrentUserID.IsValid)
                                        {
                                            this.authenticateUserModules.SetPagePermissions(sec, sys.CurrentSystemID, user.CurrentUserID);
                                        }
                                    }
                                }
                                catch (Exception exc)
                                {
                                    string err = string.Format("Message:{0}\r\nSource:{1}\r\nStackTrace:{2}", exc.Message, exc.Source, exc.StackTrace);
                                    this.log.CreateErrorLog(err);
                                    throw exc;
                                }
                            });

                        }
                    }
                }
            });
        }

        #endregion
    }
}