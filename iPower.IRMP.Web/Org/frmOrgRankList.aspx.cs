//================================================================================
// FileName: frmOrgRankList.aspx.cs
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
// Copyright (C) 2009-2010 Jeason Young Corporation
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
using iPower.IRMP.Org.Engine.Service;
namespace iPower.IRMP.Org.Web
{
	///<summary>
	///frmOrgRankList列表页面后台代码。
	///</summary>
	public partial class frmOrgRankList:ModuleBasePage,IOrgRankListView
	{
		#region 成员变量，构造函数。
		OrgRankPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmOrgRankList()
		{
			this.presenter = new OrgRankPresenter(this);

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
		protected void dgfrmOrgRankList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmOrgRankList.DataSource = this.presenter.ListDataSource;

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
			this.dgfrmOrgRankList.InvokeBuildDataSource();
				
		}
		public override bool DeleteData()
		{
			return this.presenter.BatchDeleteRank(this.dgfrmOrgRankList.CheckedValue);

		}
		#endregion


        #region IOrgRankListView 成员

        public string RankName
        {
            get { return this.txtRankName.Text.Trim(); }
        }

        public GUIDEx ParentRankID
        {
            get { return this.ddlParentRankID.SelectedValue; }
        }

        public void ShowMessage(string msg)
        {
            this.errMessage.Message = msg;
            this.errMessage.Alert = !string.IsNullOrEmpty(msg);
        }

        #endregion

        #region IOrgRankView 成员

        public void BindParentRank(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlParentRankID, data);

        }

        #endregion
    }

}
