//================================================================================
// FileName: frmFlowProcessInstanceList.aspx.cs
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
using iPower.IRMP.Flow.Engine.Service;
namespace iPower.IRMP.Flow.Web
{
	///<summary>
	///frmFlowProcessInstanceList列表页面后台代码。
	///</summary>
	public partial class frmFlowProcessInstanceList:ModuleBasePage,IFlowProcessInstanceListView
	{
		#region 成员变量，构造函数。
		FlowProcessInstancePresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmFlowProcessInstanceList()
		{
			this.presenter = new FlowProcessInstancePresenter(this);

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
		protected void dgfrmFlowProcessInstanceList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmFlowProcessInstanceList.DataSource = this.presenter.ListDataSource;

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
			this.dgfrmFlowProcessInstanceList.InvokeBuildDataSource();
		}
		public override bool DeleteData()
		{
			return this.presenter.BatchDeleteFlowProcessInstance(this.dgfrmFlowProcessInstanceList.CheckedValue);

		}
		#endregion


        #region IFlowProcessInstanceListView 成员

        public string ProcessInstanceName
        {
            get { return this.txtProcessInstanceName.Text.Trim(); }
        }

        public void ShowMessage(string message)
        {
            this.errMessage.Message = message;
        }

        #endregion
    }

}
