//================================================================================
//  FileName:Default.aspx.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-13 09:07:45
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
// ----  ------  -----------
//
//================================================================================
//  Copyright (C) 2009-2010 Jeason Young Corporation
//================================================================================

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using iPower.Platform;
using iPower.IRMP.Flow.Engine.Service;
namespace iPower.IRMP.Flow.Web
{
    public partial class _Default : ModuleBasePage,IModuleView
    {
        #region 成员变量，构造函数。
        ModulePresenter<IModuleView> presenter = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public _Default()
        {
            this.presenter = new ModulePresenter<IModuleView>(this);
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                this.presenter.InitializeComponent();
        }
    }
}
