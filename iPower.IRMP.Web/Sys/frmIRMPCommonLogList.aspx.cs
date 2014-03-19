//================================================================================
// FileName: frmIRMPCommonLogList.aspx.cs
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
	///frmIRMPCommonLogList列表页面后台代码。
	///</summary>
	public partial class frmIRMPCommonLogList:ModuleBasePage,IIRMPCommonLogListView
	{
		#region 成员变量，构造函数。
		IRMPCommonLogPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmIRMPCommonLogList()
		{
			this.presenter = new IRMPCommonLogPresenter(this);

		}
		#endregion

		#region 事件处理。
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.presenter.InitializeComponent();
				this.lbTitle.Text = base.NavigationContent;
                this.txtCreateDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
			}

		}
		protected void dgfrmIRMPCommonLogList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmIRMPCommonLogList.DataSource = this.presenter.ListDataSource;

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
			this.dgfrmIRMPCommonLogList.InvokeBuildDataSource();
				

		}
		public override bool DeleteData()
		{
			return this.presenter.BatchDeleteCommonLog(this.dgfrmIRMPCommonLogList.CheckedValue);

		}
		#endregion
        
        #region IIRMPCommonLogListView 成员

        public string SystemName
        {
            get { return this.txtSystemName.Text.Trim(); }
        }

        public string EmployeeName
        {
            get { return this.txtEmployeeName.Text.Trim(); }
        }

        public string CreateDate
        {
            get
            {
                string strDate = this.txtCreateDate.Text.Trim();
                if (!string.IsNullOrEmpty(strDate))
                {
                    try
                    {
                        DateTime dt = DateTime.Parse(strDate);
                        strDate = string.Format("{0:yyyy-MM-dd}", dt);
                    }
                    catch (Exception e)
                    {
                        this.ShowMessage(e.Message);
                    }
                }
                return strDate;
            }
        }

        public string LogContext
        {
            get { return this.txtLogContext.Text.Trim(); }
        }

        public void ShowMessage(string message)
        {
            this.errMessage.Message = message;
        }

        #endregion
    }

}
