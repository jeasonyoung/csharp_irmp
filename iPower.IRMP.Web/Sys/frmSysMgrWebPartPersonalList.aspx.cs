//================================================================================
// FileName: frmSysMgrWebPartPersonalList.aspx.cs
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
	///frmSysMgrWebPartPersonalList列表页面后台代码。
	///</summary>
    public partial class frmSysMgrWebPartPersonalList : ModuleBasePage, ISysMgrWebPartPersonalListView
	{
		#region 成员变量，构造函数。
		SysMgrWebPartPersonalPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmSysMgrWebPartPersonalList()
		{
			this.presenter = new SysMgrWebPartPersonalPresenter(this);

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
		protected void dgfrmSysMgrWebPartPersonalList_BuildDataSource(object sender, EventArgs e)
		{
			this.dgfrmSysMgrWebPartPersonalList.DataSource = this.presenter.ListDataSource;

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
			this.dgfrmSysMgrWebPartPersonalList.InvokeBuildDataSource();
		}

        public override bool DeleteData()
        {
            return this.presenter.BatchDeleteSysMgrWebPartPersonal(dgfrmSysMgrWebPartPersonalList.CheckedValue);
        }
		#endregion

        #region ISysMgrWebPartPersonalListView 成员

        public string WebPartName
        {
            get { return this.txtWebPartName.Text.Trim(); }
        }

        public void ShowMessage(string msg)
        {
            this.errMessage.Message=msg;
        }

        #endregion
    }

}
