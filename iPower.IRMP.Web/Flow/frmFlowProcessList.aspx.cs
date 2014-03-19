//================================================================================
// FileName: frmFlowProcessList.aspx.cs
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
using System.Xml;
using System.IO;
using System.Text;

using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.IRMP.Flow.Engine.Service;
namespace iPower.IRMP.Flow.Web
{
	///<summary>
	///frmFlowProcessList列表页面后台代码。
	///</summary>
	public partial class frmFlowProcessList:ModuleBasePage,IFlowProcessListView
	{
		#region 成员变量，构造函数。
		FlowProcessPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmFlowProcessList()
		{
			this.presenter = new FlowProcessPresenter(this);

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
		protected void dgfrmFlowProcessList_BuildDataSource(object sender, EventArgs e)
		{
            this.dgfrmFlowProcessList.DataSource = this.presenter.ListDataSource;

		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			this.LoadData();

		}
        protected void btnImport_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (this.dgfrmFlowProcessList.CheckedValue.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("<script type=\"text/javascript\">");
                builder.AppendFormat("\twindow.open(\"frmFlowProcessExport.aspx?ProcessID={0}\",null,\"width=200px,height=100px,resizable=no,scrollbars=no,status=no,toolbar=no,menubar=no,location=no\");\r\n",
                     this.dgfrmFlowProcessList.CheckedValue[0]);
                builder.AppendLine("</script>");
                this.exportScript.Text = builder.ToString();
            }
            else
                this.ShowMessage("请选择流程！");
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
			this.dgfrmFlowProcessList.InvokeBuildDataSource();
		}
		public override bool DeleteData()
		{
            return this.presenter.BatchDeleteData(this.dgfrmFlowProcessList.CheckedValue);

		}
		#endregion
        
        #region IFlowProcessListView 成员

        public string ProcessName
        {
            get { return this.txtProcessName.Text.Trim(); }
        }

        public void ShowMessage(string content)
        {
            this.errMessage.Message = content;
            this.errMessage.Alert = !string.IsNullOrEmpty(content);
        }

        #endregion
    }

}
