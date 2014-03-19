//================================================================================
// FileName: frmSecurityRoleEmployeeList.aspx.cs
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
	///frmSecurityRoleEmployeeList列表页面后台代码。
	///</summary>
	public partial class frmSecurityRoleEmployeeList:ModuleBasePage,ISecurityRoleEmployeeListView
	{
		#region 成员变量，构造函数。
		SecurityRoleEmployeePresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmSecurityRoleEmployeeList()
		{
			this.presenter = new SecurityRoleEmployeePresenter(this);

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
		protected void dgfrmSecurityRoleEmployeeList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmSecurityRoleEmployeeList.DataSource = this.presenter.ListDataSource;

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
			this.dgfrmSecurityRoleEmployeeList.InvokeBuildDataSource();
				

		}
		public override bool DeleteData()
		{
            return this.presenter.BatchDeleteRoleEmployee(this.dgfrmSecurityRoleEmployeeList.CheckedValue);

		}
		#endregion


        #region ISecurityRoleEmployeeListView 成员

        public string RoleName
        {
            get { return this.txtRole.Text.Trim(); }
        }

        public string EmployeeName
        {
            get { return this.txtEmployeeName.Text.Trim(); }
        }

        #endregion
    }

}
