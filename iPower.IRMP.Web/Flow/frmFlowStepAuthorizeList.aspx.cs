//================================================================================
// FileName: frmFlowStepAuthorizeList.aspx.cs
// Desc:
// Called by
// Auth: 本代码由代码生成器自动生成。
// Date:
//================================================================================
// Change History
//================================================================================
// Date  Author  Description
// ----  ------  -----------
//
//================================================================================
// Copyright (C) 2009-2010 iPower Young Corporation
//================================================================================
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
	
using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.Platform.Engine.DataSource;

using iPower.IRMP.Flow.Engine.Service;
namespace iPower.IRMP.Flow.Web
{
	///<summary>
	///frmFlowStepAuthorizeList列表页面后台代码。
	///</summary>
	public partial class frmFlowStepAuthorizeList:ModuleBasePage,IFlowStepAuthorizeView
	{
		#region 成员变量，构造函数。
		FlowStepAuthorizePresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmFlowStepAuthorizeList()
		{
			this.presenter = new FlowStepAuthorizePresenter(this);

		}
		#endregion

		#region 事件处理。
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.presenter.InitializeComponent();
				this.lbTitle.Text = base.NavigationContent;

			}

		}
		protected void dgfrmFlowStepAuthorizeList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmFlowStepAuthorizeList.DataSource = null;

		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			this.LoadData();

		}
		protected void btnDelete_Click(object sender, EventArgs e)
		{
			if (this.DeleteData())
			this.LoadData();

		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			this.LoadData();

		}
		#endregion

		#region 重载。
		public override void LoadData()
		{
			this.dgfrmFlowStepAuthorizeList.InvokeBuildDataSource();
		}
		public override bool DeleteData()
		{
			return false;

		}
		#endregion


        #region IFlowStepAuthorizeView 成员

        public void BindProcess(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlProcessID, data);
        }

        #endregion
    }

}
