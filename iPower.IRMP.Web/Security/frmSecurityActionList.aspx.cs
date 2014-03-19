//================================================================================
// FileName: frmSecurityActionList.aspx.cs
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
	///frmSecurityActionList�б�ҳ���̨���롣
	///</summary>
	public partial class frmSecurityActionList:ModuleBasePage,ISecurityActionListView
	{
		#region ��Ա���������캯����
		SecurityActionPresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmSecurityActionList()
		{
			this.presenter = new SecurityActionPresenter(this);

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
		protected void dgfrmSecurityActionList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmSecurityActionList.DataSource = this.presenter.ListDataSource;

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
			this.dgfrmSecurityActionList.InvokeBuildDataSource();
				

		}
		public override bool DeleteData()
		{
			return this.presenter.BatchDeleteAction(this.dgfrmSecurityActionList.CheckedValue);

		}
		#endregion
        
        #region ISecurityActionListView ��Ա

        public string ActionName
        {
            get { return this.txtActionName.Text.Trim(); }
        }
 
        public void ShowMessage(string message)
        {
            this.errMessage.Message = message;
        }

        #endregion
    }

}
