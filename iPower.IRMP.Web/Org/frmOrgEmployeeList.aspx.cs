//================================================================================
// FileName: frmOrgEmployeeList.aspx.cs
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
	///frmOrgEmployeeList列表页面后台代码。
	///</summary>
	public partial class frmOrgEmployeeList:ModuleBasePage,IOrgEmployeeListView
	{
		#region 成员变量，构造函数。
		OrgEmployeePresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmOrgEmployeeList()
		{
			this.presenter = new OrgEmployeePresenter(this);

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
		protected void dgfrmOrgEmployeeList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmOrgEmployeeList.DataSource = this.presenter.ListDataSource;

		}

        protected void tvDepartment_OnNodeClick(object sender, iPower.Web.TreeView.TreeViewNodeClickEventArgs e)
        {
            if (e != null)
            {
                string url = this.btnAdd.PickerPage;
                if (url.IndexOf("?") > 0)
                    url = url.Split('?')[0];
                this.btnAdd.PickerPage = string.Format("{0}?DepartmentID={1}", url, e.Node.Value);

                this.ddlDepartmentID.SelectedValue = e.Node.Value;

                this.btnSearch_Click(null, EventArgs.Empty);
                this.tvDepartment.CurrentFolderValue = e.Node.Value;
                e.Node.Expand = true;
            }
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
			this.dgfrmOrgEmployeeList.InvokeBuildDataSource();
				

		}
		public override bool DeleteData()
		{
			return this.presenter.BatchDeleteEmployee(this.dgfrmOrgEmployeeList.CheckedValue);

		}
		#endregion


        #region IOrgEmployeeListView 成员

        public string EmployeeName
        {
            get { return this.txtEmployeeName.Text.Trim(); }
        }

        public string DepartmentID
        {
            get { return this.ddlDepartmentID.SelectedValue; }
        }

        #endregion

        #region IOrgEmployeeView 成员

        public void BindDepartment(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlDepartmentID, data);
        }

        #endregion

        #region IOrgEmployeeListView 成员


        public void ShowMessage(string content)
        {
            this.errMessage.Message = content;
            this.errMessage.Alert = !string.IsNullOrEmpty(content);
        }

        public void BuildDepartPostTreeView(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.tvDepartment.DataSource = data.DataSource as System.Data.DataTable;
            this.tvDepartment.IDField = data.DataValueField;
            this.tvDepartment.PIDField = data.ParentDataValueField;
            this.tvDepartment.TitleField = data.DataTextField;
            this.tvDepartment.OrderNoField = data.DataTextField;
            this.tvDepartment.BuildTree();
        }

        #endregion
    }

}
