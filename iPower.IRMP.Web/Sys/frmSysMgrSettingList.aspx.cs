//================================================================================
// FileName: frmSysMgrSettingList.aspx.cs
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
	///frmSysMgrSettingList�б�ҳ���̨���롣
	///</summary>
    public partial class frmSysMgrSettingList : ModuleBasePage, ISysMgrSettingListView
	{
		#region ��Ա���������캯����
		SysMgrSettingPresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmSysMgrSettingList()
		{
			this.presenter = new SysMgrSettingPresenter(this);

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
		protected void dgfrmSysMgrSettingList_BuildDataSource(object sender, EventArgs e)
		{
            this.dgfrmSysMgrSettingList.DataSource = this.presenter.ListDataSource;

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
			this.dgfrmSysMgrSettingList.InvokeBuildDataSource();

		}
		public override bool DeleteData()
		{
            return this.presenter.BatchDeleteSysMgrSetting(this.dgfrmSysMgrSettingList.CheckedValue);

		}
		#endregion

        #region ISysMgrSettingListView ��Ա

        public string SystemName
        {
            get { return this.txtSystemName.Text.Trim(); }
        }

        public void ShowMessage(string msg)
        {
            this.errMessage.Message = msg;
        }

        #endregion
    }

}
