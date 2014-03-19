//================================================================================
// FileName: frmSysMgrWebPartList.aspx.cs
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
	///frmSysMgrWebPartList�б�ҳ���̨���롣
	///</summary>
    public partial class frmSysMgrWebPartList : ModuleBasePage, ISysMgrWebPartListView
	{
		#region ��Ա���������캯����
		SysMgrWebPartPresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmSysMgrWebPartList()
		{
			this.presenter = new SysMgrWebPartPresenter(this);

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
		protected void dgfrmSysMgrWebPartList_BuildDataSource(object sender, EventArgs e)
		{
            this.dgfrmSysMgrWebPartList.DataSource = this.presenter.ListDataSource;

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
			this.dgfrmSysMgrWebPartList.InvokeBuildDataSource();
				

		}
		public override bool DeleteData()
		{
            return this.presenter.BatchDeleteSysMgrWebPart(this.dgfrmSysMgrWebPartList.CheckedValue);

		}
		#endregion

        #region ISysMgrWebPartListView��Ա

        public string WebPartName
        {
            get { return this.txtWebPartName.Text.Trim(); }
        }
        public void ShowMessage(string Msg)
        {
            this.errMessage.Message = Msg;
        }

        #endregion
    }

}
