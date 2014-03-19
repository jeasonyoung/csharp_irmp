//================================================================================
//  FileName: frmLogout.aspx.cs
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using iPower.IRMP.SysMgr.Engine.Service;
namespace iPower.IRMP.SysMgr.Web
{
    public partial class frmLogout : ModuleBasePage,ILogoutView
    {
        #region 成员变量，构造函数。
        SysMgrLogoutPresenter presenter = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public frmLogout()
        {
            this.presenter = new SysMgrLogoutPresenter(this);
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.presenter.InitializeComponent();
            }
        }

        #region ILogoutView 成员

        public void ShowMessage(string msg)
        {
            this.errMsg.Message = msg;
        }

        #endregion
    }
}
