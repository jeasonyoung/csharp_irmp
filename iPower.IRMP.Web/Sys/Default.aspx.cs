﻿//================================================================================
//  FileName: Default.aspx.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/5/27
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
    /// <summary>
    /// 默认首页。
    /// </summary>
    public partial class Default : ModuleBasePage, IModuleView
    {
        #region 成员变量，构造函数。
        ModulePresenter<IModuleView> presenter = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public Default()
        {
            this.presenter = new ModulePresenter<IModuleView>(this);
        }
        #endregion

        #region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                this.presenter.InitializeComponent();
        }
        #endregion
    }
}
