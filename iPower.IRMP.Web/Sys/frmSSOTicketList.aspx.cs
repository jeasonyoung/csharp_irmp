//================================================================================
// FileName: frmSSOTicketList.aspx.cs
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
	///frmSSOTicketList�б�ҳ���̨���롣
	///</summary>
    public partial class frmSSOTicketList : ModuleBasePage, ISSOTicketListView
    {
        #region ��Ա���������캯����
        SSOTicketPresenter presenter = null;
        ///<summary>
        ///���캯����
        ///</summary>
        public frmSSOTicketList()
        {
            this.presenter = new SSOTicketPresenter(this);

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
        protected void dgfrmSSOTicketList_BuildDataSource(object sender, EventArgs e)
        {
            this.dgfrmSSOTicketList.DataSource = this.presenter.ListDataSource;

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
            this.dgfrmSSOTicketList.InvokeBuildDataSource();


        }
        public override bool DeleteData()
        {
            return this.presenter.BatchDeleteSSOTicket(this.dgfrmSSOTicketList.CheckedValue);

        }
        #endregion
        
        #region ISSOTicketListView ��Ա

        public string UserData
        {
            get { return this.txtUserData.Text; }
        }

        #endregion

        #region ISSOTicketView ��Ա

        public void ShowMessage(string msg)
        {
            this.errMessage.Message = msg;
        }

        #endregion
    }

}
