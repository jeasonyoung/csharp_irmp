//================================================================================
// FileName: frmFlowStepList.aspx.cs
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
	///frmFlowStepList�б�ҳ���̨���롣
	///</summary>
	public partial class frmFlowStepList:ModuleBasePage,IFlowStepListView
	{
		#region ��Ա���������캯����
		FlowStepPresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmFlowStepList()
		{
			this.presenter = new FlowStepPresenter(this);

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
		protected void dgfrmFlowStepList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmFlowStepList.DataSource = this.presenter.ListDataSource;

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
			this.dgfrmFlowStepList.InvokeBuildDataSource();
		}
		public override bool DeleteData()
		{
			return this.presenter.BatchDeleteData(this.dgfrmFlowStepList.CheckedValue);
		}
		#endregion

        #region IFlowStepView ��Ա

        public void BindProcess(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlProcessID, data);
        }

        #endregion

        #region IFlowStepListView ��Ա

        public string StepName
        {
            get { return this.txtStepName.Text.Trim(); }
        }

        public string ProcessID
        {
            get { return this.ddlProcessID.SelectedValue; }
        }

        public void ShowMessage(string content)
        {
            this.errMessage.Message = content;
            this.errMessage.Alert = !string.IsNullOrEmpty(content);
        }

        #endregion
    }

}
