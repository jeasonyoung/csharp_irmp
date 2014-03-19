//================================================================================
// FileName: frmSecurityRightList.aspx.cs
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
	///frmSecurityRightList�б�ҳ���̨���롣
	///</summary>
	public partial class frmSecurityRightList:ModuleBasePage,ISecurityRightListView
	{
		#region ��Ա���������캯����
		SecurityRightPresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmSecurityRightList()
		{
			this.presenter = new SecurityRightPresenter(this);

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
		protected void dgfrmSecurityRightList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmSecurityRightList.DataSource = this.presenter.ListDataSource;

		}
        protected void ddlSystemID_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtModuleName.Text = string.Empty;
            this.BuildAddUrl();
            this.presenter.ChangeSystemRefershModuleTree();
            this.btnSearch_Click(sender, e);
        }
        protected void tvModule_OnNodeClick(object sender, iPower.Web.TreeView.TreeViewNodeClickEventArgs e)
        {
            if (e.Node != null)
            {
                this.txtModuleName.Text = e.Node.Text;
                this.tvModule.CurrentFolderValue = e.Node.Value;
                e.Node.Expand = true;

                this.LoadData();
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

        #region ����������
        void BuildAddUrl()
        {
            string id = this.ddlSystemID.SelectedValue;
            if (!string.IsNullOrEmpty(id))
            {
                string url = this.btnAdd.PickerPage;
                if (!string.IsNullOrEmpty(url))
                {
                    url = url.Split('?')[0];
                    this.btnAdd.PickerPage = string.Format("{0}?SystemID={1}", url, id);
                }
            }
        }
        #endregion

        #region ���ء�
        public override void LoadData()
		{
			this.dgfrmSecurityRightList.InvokeBuildDataSource();
            this.BuildAddUrl();
		}
		public override bool DeleteData()
		{
			return this.presenter.BatchDeleteRight(this.dgfrmSecurityRightList.CheckedValue);

		}
		#endregion


        #region ISecurityRightListView ��Ա

        public GUIDEx SystemID
        {
            get { return this.ddlSystemID.SelectedValue; }
        }

        public string ModuleName
        {
            get { return this.txtModuleName.Text; }
        }

        public void BindSystem(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlSystemID, data);
        }

        public void BindModuleTree(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.tvModule, data);
        }
 
        public void ShowMessage(string message)
        {
            this.errMessage.Message = message;
        }

        #endregion
    }

}
