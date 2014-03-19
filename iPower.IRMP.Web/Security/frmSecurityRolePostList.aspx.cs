//================================================================================
// FileName: frmSecurityRolePostList.aspx.cs
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
	///frmSecurityRolePostList�б�ҳ���̨���롣
	///</summary>
	public partial class frmSecurityRolePostList:ModuleBasePage,ISecurityRolePostListView
	{
		#region ��Ա���������캯����
		SecurityRolePostPresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmSecurityRolePostList()
		{
			this.presenter = new SecurityRolePostPresenter(this);

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
		protected void dgfrmSecurityRolePostList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmSecurityRolePostList.DataSource = this.presenter.ListDataSource;

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
			this.dgfrmSecurityRolePostList.InvokeBuildDataSource();
				

		}
		public override bool DeleteData()
		{
			return this.presenter.BatchDeleteRolePost(this.dgfrmSecurityRolePostList.CheckedValue);

		}
		#endregion


        #region ISecurityRolePostListView ��Ա

        public string RoleName
        {
            get { return this.txtRoleName.Text.Trim(); }
        }

        public string PostName
        {
            get { return this.txtPostName.Text.Trim(); }
        }

        #endregion
    }

}
