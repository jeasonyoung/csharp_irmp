//================================================================================
// FileName: frmOrgPostList.aspx.cs
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
using iPower.Platform.Engine.DataSource;
using iPower.IRMP.Org.Engine.Service;
namespace iPower.IRMP.Org.Web
{
	///<summary>
	///frmOrgPostList列表页面后台代码。
	///</summary>
	public partial class frmOrgPostList:ModuleBasePage,IOrgPostListView
	{
		#region 成员变量，构造函数。
		OrgPostPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmOrgPostList()
		{
			this.presenter = new OrgPostPresenter(this);

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
		protected void dgfrmOrgPostList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmOrgPostList.DataSource = this.presenter.ListDataSource;

		}

        protected void tvDepartment_OnNodeClick(object sender, iPower.Web.TreeView.TreeViewNodeClickEventArgs e)
        {
            this.txtDepartmentName.Text = e.Node.Text;
            string url = this.btnAdd.PickerPage;
            if (url.IndexOf("?") > 0)
                url = url.Split('?')[0];
            this.btnAdd.PickerPage = string.Format("{0}?DepartmentID={1}", url, e.Node.Value);
            this.tvDepartment.CurrentFolderValue = e.Node.Value;
            e.Node.Expand = true;
            this.btnSearch_Click(null, EventArgs.Empty);
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
			this.dgfrmOrgPostList.InvokeBuildDataSource();
				

		}
		public override bool DeleteData()
		{
			return this.presenter.BatchDeletePost(this.dgfrmOrgPostList.CheckedValue);

		}
		#endregion


        #region IOrgPostListView 成员

        public string DepartmentName
        {
            get { return this.txtDepartmentName.Text.Trim(); }
        }

        public GUIDEx RankID
        {
            get { return this.ddlRankID.SelectedValue; }
        }

        #endregion

        #region IOrgPostView 成员

        public void BindRank(IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlRankID, data);
        }
         
        public void ShowMessage(string msg)
        {
            this.errMessage.Message = msg;
            this.errMessage.Alert = !string.IsNullOrEmpty(msg);
        }

        public void BuildDepartmentTreeView(IListControlsTreeViewData data)
        {
            if (data != null)
            {
                this.tvDepartment.DataSource = data.DataSource as System.Data.DataTable;
                this.tvDepartment.IDField = data.DataValueField;
                this.tvDepartment.PIDField = data.ParentDataValueField;
                this.tvDepartment.TitleField = data.DataTextField;
                this.tvDepartment.OrderNoField = data.DataTextField;
                this.tvDepartment.BuildTree();
            }
        }

        #endregion
    }

}
