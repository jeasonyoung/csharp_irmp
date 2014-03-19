//================================================================================
// FileName: frmSecurityModuleList.aspx.cs
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
	///frmSecurityModuleList�б�ҳ���̨���롣
	///</summary>
	public partial class frmSecurityModuleList:ModuleBasePage,ISecurityModuleListView
	{
		#region ��Ա���������캯����
		SecurityModulePresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmSecurityModuleList()
		{
			this.presenter = new SecurityModulePresenter(this);

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
		protected void dgfrmSecurityModuleList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmSecurityModuleList.DataSource = this.presenter.ListDataSource;

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
			this.dgfrmSecurityModuleList.InvokeBuildDataSource();
				

		}
		public override bool DeleteData()
		{
			return this.presenter.BatchDeleteModule(this.dgfrmSecurityModuleList.CheckedValue);

		}
		#endregion


        #region ISecurityModuleListView ��Ա

        public string ModuleName
        {
            get { return this.txtModuleName.Text.Trim(); }
        }

        public GUIDEx SystemID
        {
            get { return this.ddlSystemID.SelectedValue; }
        }

        #endregion

        #region ISecurityModuleView ��Ա

        public void ShowMessage(string message)
        {
            this.errMessage.Message = message;
        }

        public void BindSystem(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlSystemID, data);
        }

        #endregion
    }

}
