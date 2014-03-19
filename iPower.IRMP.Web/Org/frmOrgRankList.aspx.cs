//================================================================================
// FileName: frmOrgRankList.aspx.cs
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
	///frmOrgRankList�б�ҳ���̨���롣
	///</summary>
	public partial class frmOrgRankList:ModuleBasePage,IOrgRankListView
	{
		#region ��Ա���������캯����
		OrgRankPresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmOrgRankList()
		{
			this.presenter = new OrgRankPresenter(this);

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
		#region ���ء�
		public override void LoadData()
		{
			this.dgfrmOrgRankList.InvokeBuildDataSource();
				
		}
		public override bool DeleteData()
		{
			return this.presenter.BatchDeleteRank(this.dgfrmOrgRankList.CheckedValue);

		}
		#endregion


        #region IOrgRankListView ��Ա

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

        #region IOrgRankView ��Ա

        public void BindParentRank(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlParentRankID, data);

        }

        #endregion
    }

}
