//================================================================================
// FileName: frmOrgLeaderSubChargeList.aspx.cs
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
	///frmOrgLeaderSubChargeList列表页面后台代码。
	///</summary>
	public partial class frmOrgLeaderSubChargeList:ModuleBasePage,IOrgLeaderSubChargeListView
	{
		#region 成员变量，构造函数。
		OrgLeaderSubChargePresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmOrgLeaderSubChargeList()
		{
			this.presenter = new OrgLeaderSubChargePresenter(this);

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
		protected void dgfrmOrgLeaderSubChargeList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmOrgLeaderSubChargeList.DataSource = this.presenter.ListDataSource;

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
			this.dgfrmOrgLeaderSubChargeList.InvokeBuildDataSource();
				

		}
		public override bool DeleteData()
		{
            return this.presenter.BatchDeleteLeaderSubCharge(this.dgfrmOrgLeaderSubChargeList.CheckedValue);

		}
		#endregion
        
        #region IOrgLeaderSubChargeListView 成员

        public string EmployeeName
        {
            get { return this.txtEmployeeName.Text.Trim(); }
        }

        public string DepartmentName
        {
            get { return this.txtDepartmentName.Text.Trim(); }
        }

        #endregion
    }

}
