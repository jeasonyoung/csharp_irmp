//================================================================================
// FileName: frmResourcesList.aspx.cs
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
using iPower.IRMP.SysMgr.Engine.Service;
namespace iPower.IRMP.SysMgr.Web
{
	///<summary>
	///frmResourcesList�б�ҳ���̨���롣
	///</summary>
    public partial class frmResourcesList : ModuleBasePage, IResourcesListView
    {
        #region ��Ա���������캯����
        ResourcesPresenter presenter = null;
        ///<summary>
        ///���캯����
        ///</summary>
        public frmResourcesList()
        {
            this.presenter = new ResourcesPresenter(this);

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
        protected void dgfrmResourcesList_BuildDataSource(object sender, EventArgs e)
        {
            this.dgfrmResourcesList.DataSource = this.presenter.ListDataSource;

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
            this.dgfrmResourcesList.InvokeBuildDataSource();


        }
        public override bool DeleteData()
        {
            return this.presenter.BatchDeleteResources(this.dgfrmResourcesList.CheckedValue);

        }
        #endregion


        #region IResourcesListView ��Ա

        public string ResKey
        {
            get { return this.txtResKey.Text.Trim(); }
        }

        #endregion

        #region IResourcesView ��Ա

        public void ShowMessage(string message)
        {
            this.errMessage.Message = message;
        }

        #endregion
    }

}
