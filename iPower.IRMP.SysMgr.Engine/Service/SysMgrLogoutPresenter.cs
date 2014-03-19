//================================================================================
//  FileName: SysMgrLogoutPresenter.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/7/1
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

using iPower.IRMP.SSO;
namespace iPower.IRMP.SysMgr.Engine.Service
{
    /// <summary>
    /// 注销界面接口。
    /// </summary>
    public interface ILogoutView : IModuleView
    {
        /// <summary>
        /// 显示信息。
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
    }
    /// <summary>
    /// 注销行为类。
    /// </summary>
    public class SysMgrLogoutPresenter : ModulePresenter<ILogoutView>
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="view"></param>
        public SysMgrLogoutPresenter(ILogoutView view)
            : base(view)
        {
        }
        #endregion

        #region 重载。
        /// <summary>
        /// 
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            ILogoutView logoutView = this.View as ILogoutView;
            if (logoutView != null)
            {
                string err = null;
                IUserLogin login = this.ModuleConfig.SSOClientUserLoginAssembly;
                if (login != null)
                {
                    bool result = login.SignOut(out err);
                    if (!result)
                        logoutView.ShowMessage(err);
                }
            }
        }
        #endregion
    }
}
