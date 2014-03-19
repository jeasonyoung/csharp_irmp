//================================================================================
// FileName: frmSecurityRegsiterList.aspx.cs
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
	///frmSecurityRegsiterList�б�ҳ���̨���롣
	///</summary>
	public partial class frmSecurityRegsiterList:ModuleBasePage,ISecurityRegsiterListView
	{
		#region ��Ա���������캯����
		SecurityRegsiterPresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmSecurityRegsiterList()
		{
			this.presenter = new SecurityRegsiterPresenter(this);

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

		#region ���ء�
		public override void LoadData()
		{
			this.dgfrmSecurityRegsiterList.InvokeBuildDataSource();
				

		}
		public override bool DeleteData()
		{
			return this.presenter.BatchDeleteRegsiter(this.dgfrmSecurityRegsiterList.CheckedValue);

		}
		#endregion


        #region ISecurityRegsiterListView ��Ա

        public string SystemName
        {
            get { return this.txtSystemName.Text.Trim(); }
        }

        public GUIDEx ParentSystemID
        {
            get { return this.ddlParentSystem.SelectedValue; }
        }

        #endregion

        #region ISecurityRegsiterView ��Ա

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
