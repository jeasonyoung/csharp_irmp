//================================================================================
// FileName: frmSecurityRoleDepartmentList.aspx.cs
// Desc:
// Called by
// Auth: �������ɴ����������Զ����ɡ�
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
	///frmSecurityRoleDepartmentList�б�ҳ���̨���롣
	///</summary>
	public partial class frmSecurityRoleDepartmentList:ModuleBasePage,ISecurityRoleDepartmentListView
	{
		#region ��Ա���������캯����
		SecurityRoleDepartmentPresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmSecurityRoleDepartmentList()
		{
			this.presenter = new SecurityRoleDepartmentPresenter(this);

		}
		#endregion

		#region �¼�����
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.presenter.InitializeComponent();
				this.lbTitle.Text = base.NavigationContent;

			}

		}
		protected void dgfrmSecurityRoleDepartmentList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmSecurityRoleDepartmentList.DataSource = this.presenter.ListDataSource;

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

		#region ���ء�
		public override void LoadData()
		{
			this.dgfrmSecurityRoleDepartmentList.InvokeBuildDataSource();
				

		}
		public override bool DeleteData()
		{
			return this.presenter.BatchDeleteRoleDepartment(this.dgfrmSecurityRoleDepartmentList.CheckedValue);

		}
		#endregion


        #region ISecurityRoleDepartmentListView ��Ա

        public string RoleName
        {
            get { return this.txtRoleName.Text.Trim(); }
        }

        public string DepartmentName
        {
            get { return this.txtDepartmentName.Text.Trim(); }
        }

        #endregion
    }

}
