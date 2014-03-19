//================================================================================
// FileName: frmFlowStepAuthorizeList.aspx.cs
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
// Copyright (C) 2009-2010 iPower Young Corporation
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
using iPower.Platform.Engine.DataSource;

using iPower.IRMP.Flow.Engine.Service;
namespace iPower.IRMP.Flow.Web
{
	///<summary>
	///frmFlowStepAuthorizeList�б�ҳ���̨���롣
	///</summary>
	public partial class frmFlowStepAuthorizeList:ModuleBasePage,IFlowStepAuthorizeView
	{
		#region ��Ա���������캯����
		FlowStepAuthorizePresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmFlowStepAuthorizeList()
		{
			this.presenter = new FlowStepAuthorizePresenter(this);

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
		protected void dgfrmFlowStepAuthorizeList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmFlowStepAuthorizeList.DataSource = null;

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
			this.dgfrmFlowStepAuthorizeList.InvokeBuildDataSource();
		}
		public override bool DeleteData()
		{
			return false;

		}
		#endregion


        #region IFlowStepAuthorizeView ��Ա

        public void BindProcess(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlProcessID, data);
        }

        #endregion
    }

}
