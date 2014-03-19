//================================================================================
// FileName: frmOrgDepartmentList.aspx.cs
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
	///frmOrgDepartmentList列表页面后台代码。
	///</summary>
	public partial class frmOrgDepartmentList:ModuleBasePage,IOrgDepartmentListView
	{
		#region 成员变量，构造函数。
		OrgDepartmentPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmOrgDepartmentList()
		{
			this.presenter = new OrgDepartmentPresenter(this);

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
		protected void dgfrmOrgDepartmentList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmOrgDepartmentList.DataSource = this.presenter.ListDataSource;

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
			this.dgfrmOrgDepartmentList.InvokeBuildDataSource();
		}
		public override bool DeleteData()
		{
			return this.presenter.BatchDeleteData(this.dgfrmOrgDepartmentList.CheckedValue);

		}
		#endregion


        #region IOrgDepartmentListView 成员
        /// <summary>
        /// 获取部门名称。
        /// </summary>
        public string DepartmentName
        {
            get { return this.txtDepartmentName.Text.Trim(); }
        }
        /// <summary>
        /// 获取上级部门ID。
        /// </summary>
        public GUIDEx ParentDepartmentID
        {
            get { return this.ddlDepartmentID.SelectedValue; }
        }
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="content"></param>
        public void ShowMessage(string content)
        {
            this.errMessage.Message = content;
            this.errMessage.Alert = !string.IsNullOrEmpty(content);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void BindParentDepartment(IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlDepartmentID, data);
        }

        #endregion
    }

}
