//================================================================================
// FileName: frmSecurityRegsiterList.aspx.cs
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
using iPower.IRMP.Security.Engine.Service;
namespace iPower.IRMP.Security.Web
{
	///<summary>
	///frmSecurityRegsiterList列表页面后台代码。
	///</summary>
	public partial class frmSecurityRegsiterList:ModuleBasePage,ISecurityRegsiterListView
	{
		#region 成员变量，构造函数。
		SecurityRegsiterPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmSecurityRegsiterList()
		{
			this.presenter = new SecurityRegsiterPresenter(this);

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
		protected void dgfrmSecurityRegsiterList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmSecurityRegsiterList.DataSource = this.presenter.ListDataSource;

		}

        protected void btnImport_Click(object sender, EventArgs e)
        {
            this.LoadData();
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
			this.dgfrmSecurityRegsiterList.InvokeBuildDataSource();
				

		}
		public override bool DeleteData()
		{
			return this.presenter.BatchDeleteRegsiter(this.dgfrmSecurityRegsiterList.CheckedValue);

		}
		#endregion


        #region ISecurityRegsiterListView 成员

        public string SystemName
        {
            get { return this.txtSystemName.Text.Trim(); }
        }

        public GUIDEx ParentSystemID
        {
            get { return this.ddlParentSystem.SelectedValue; }
        }

        #endregion

        #region ISecurityRegsiterView 成员

        public void BindParentSystem(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlParentSystem, data);
        }

        public void ShowMessage(string content)
        {
            this.errMessage.Message = content;
        }

        #endregion
    }

}
