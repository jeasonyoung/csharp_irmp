//================================================================================
//  FileName: TaskView.ascx.cs
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

using iPower.Platform.UI;
using iPower.Platform.WebPart;
using iPower.IRMP.Engine.Service;
namespace iPower.IRMP.Web.WebPart
{
    /// <summary>
    /// 待办、待阅、已办、已阅部件。
    /// </summary>
    public partial class TaskView : WebPartBase, IModuleView
    {
        #region 成员变量，构造函数。
        ModulePresenter<IModuleView> presenter = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public TaskView()
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
        protected void dgTaskViewList_OnBuildDataSource(object sender, EventArgs e)
        {
            IWebPartData oWebPartData = this.WebPartData;
            if (oWebPartData != null)
            {
                this.dgTaskViewList.DataSource = oWebPartData.DataSource(this.CurrentUserID, string.Empty);
            }
        }
        #endregion

        #region 重载。
        public override void LoadData()
        {
            try
            {
                string strPageSize = this.QueryPropertyValue("PageSize");
                if (!string.IsNullOrEmpty(strPageSize))
                    this.dgTaskViewList.PageSize = int.Parse(strPageSize);
            }
            catch (Exception) { }
            this.dgTaskViewList.InvokeBuildDataSource();
        }
        #endregion
    }
}