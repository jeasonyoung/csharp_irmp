//================================================================================
//  FileName: frmLogin.aspx.cs
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
using System.Web.UI;
using System.Web.UI.WebControls;

using iPower.IRMP.SysMgr.Engine.Service;
namespace iPower.IRMP.SysMgr.Web
{
    public partial class frmLogin : ModuleBasePage, ILoginView
    {
        #region 成员变量，构造函数。
        SysMgrLoginPresenter presenter = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public frmLogin()
        {
            this.presenter = new SysMgrLoginPresenter(this);
        }
        #endregion

        //#region 属性。
        ///// <summary>
        ///// 获取版权信息。
        ///// </summary>
        //protected override string CopyRight
        //{
        //    get
        //    {
        //        return this.presenter.CopyRight;
        //    }
        //}
        //#endregion

        #region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.presenter.InitializeComponent();
                this.CurrentPageTile = "用户登录";
            }
        }

        protected void btnLogin_OnClick(object sender, EventArgs e)
        {
            this.presenter.Login();
        }
        #endregion

        #region ILoginView 成员

        public string EmployeeSign
        {
            get { return this.txtLoginSign.Text.Trim(); }
        }

        public string EmployeePassword
        {
            get { return this.txtLoginPassword.Text.Trim(); }
        }

        public void ShowMessage(string message)
        {
            this.errMsg.Message = message;
        }

        #endregion
    }
}
