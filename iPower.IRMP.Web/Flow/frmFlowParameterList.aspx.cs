//================================================================================
// FileName: frmFlowParameterList.aspx.cs
// Desc:
// Called by
// Auth: 本代码由代码生成器自动生成。
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
	///frmFlowParameterList列表页面后台代码。
	///</summary>
	public partial class frmFlowParameterList:ModuleBasePage,IFlowParameterListView
	{
		#region 成员变量，构造函数。
		FlowParameterPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmFlowParameterList()
		{
			this.presenter = new FlowParameterPresenter(this);

		}
		#endregion

		#region 事件处理。
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.presenter.InitializeComponent();
				this.lbTitle.Text = base.NavigationContent;

			}

		}
        protected void ddlProcess_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.presenter.BindStepData();
        }
		protected void dgfrmFlowParameterList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmFlowParameterList.DataSource = this.presenter.ListDataSource;

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

		#region 重载。
		public override void LoadData()
		{
			this.dgfrmFlowParameterList.InvokeBuildDataSource();
		}
		public override bool DeleteData()
		{
            return this.presenter.BatchDeleteData(this.dgfrmFlowParameterList.CheckedValue);

		}
		#endregion
               
        #region IFlowParameterListView 成员

        public string ParameterName
        {
            get { return this.txtParameterName.Text; }
        }

        public string ProcessID
        {
            get { return this.ddlProcess.SelectedValue; }
        }

        public string StepID
        {
            get { return this.ddlStep.SelectedValue; }
        }

        public void BindProcess(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlProcess, data);
        }

        public void BindStep(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlStep, data);
        }

        public void ShowMessage(string content)
        {
            this.errMessage.Message = content;
            this.errMessage.Alert = !string.IsNullOrEmpty(content);
        }

        #endregion
    }

}
