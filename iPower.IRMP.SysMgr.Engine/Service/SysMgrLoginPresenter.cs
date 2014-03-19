//================================================================================
//  FileName: SysMgrLoginPresenter.cs
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

using iPower.IRMP.SSO;
namespace iPower.IRMP.SysMgr.Engine.Service
{
    /// <summary>
    /// 登录界面接口。
    /// </summary>
    public interface ILoginView : IModuleView
    {
        /// <summary>
        /// 获取用户登录帐号。
        /// </summary>
        string EmployeeSign
        {
            get;
        }
        /// <summary>
        /// 获取用户登录密码。
        /// </summary>
        string EmployeePassword
        {
            get;
        }
        /// <summary>
        /// 显示信息。
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }
    /// <summary>
    /// 登录行为类。
    /// </summary>
    public class SysMgrLoginPresenter : ModulePresenter<ILoginView>
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="view"></param>
        public SysMgrLoginPresenter(ILoginView view)
            : base(view)
        {
        }
        #endregion

        #region 属性。
        /// <summary>
        /// 获取版权信息。
        /// </summary>
        public string CopyRight
        {
            get
            {
                return this.ModuleConfig.CopyRight;
            }
        }
        #endregion

        /// <summary>
        /// 登录。
        /// </summary>
        public void Login()
        {
            try
            {
                string err = null;
                IUserLogin login = this.ModuleConfig.SSOClientUserLoginAssembly;
                if (login != null)
                {
                    bool result = login.SignIn(this.View.EmployeeSign, this.View.EmployeePassword, out err);
                    if (!result)
                        this.View.ShowMessage(err);
                }
            }
            catch (Exception e)
            {
                this.View.ShowMessage(e.Message);
            }
        }
    }
}
