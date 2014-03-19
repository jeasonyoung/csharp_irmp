//================================================================================
// FileName: frmOrgLeaderSubChargeList.aspx.cs
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
using iPower.IRMP.Org.Engine.Service;
namespace iPower.IRMP.Org.Web
{
	///<summary>
	///frmOrgLeaderSubChargeList�б�ҳ���̨���롣
	///</summary>
	public partial class frmOrgLeaderSubChargeList:ModuleBasePage,IOrgLeaderSubChargeListView
	{
		#region ��Ա���������캯����
		OrgLeaderSubChargePresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmOrgLeaderSubChargeList()
		{
			this.presenter = new OrgLeaderSubChargePresenter(this);

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

		#region ���ء�
		public override void LoadData()
		{
			this.dgfrmOrgLeaderSubChargeList.InvokeBuildDataSource();
				

		}
		public override bool DeleteData()
		{
            return this.presenter.BatchDeleteLeaderSubCharge(this.dgfrmOrgLeaderSubChargeList.CheckedValue);

		}
		#endregion
        
        #region IOrgLeaderSubChargeListView ��Ա

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
