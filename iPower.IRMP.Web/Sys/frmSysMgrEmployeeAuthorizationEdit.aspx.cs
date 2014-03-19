//================================================================================
// FileName: frmSysMgrEmployeeAuthorizationEdit.aspx.cs
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

using iPower.Web.Utility;

using iPower.IRMP.SysMgr.Engine.Domain;
using iPower.IRMP.SysMgr.Engine.Service;
namespace iPower.IRMP.SysMgr.Web
{
	///<summary>
	///frmSysMgrEmployeeAuthorizationEdit列表页面后台代码。
	///</summary>
    public partial class frmSysMgrEmployeeAuthorizationEdit : ModuleBasePage, ISysMgrEmployeeAuthorizationEditView
	{
		#region 成员变量，构造函数。
		SysMgrEmployeeAuthorizationPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmSysMgrEmployeeAuthorizationEdit()
		{
			this.presenter = new SysMgrEmployeeAuthorizationPresenter(this);

		}
		#endregion

		#region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
                this.presenter.InitializeComponent();
        }

        protected void btnSelectAll_OnClick(object sender, EventArgs e)
        {
            ListBoxHelper.AddAll(this.lbAllAppAuthMulti, this.lbSelectAppAuthMulti);
        }

        protected void btnSelect_OnClick(object sender, EventArgs e)
        {
            ListBoxHelper.AddSelected(this.lbAllAppAuthMulti, this.lbSelectAppAuthMulti);
        }

        protected void btnRemove_OnClick(object sender, EventArgs e)
        {
            ListBoxHelper.RemoveSelected(this.lbAllAppAuthMulti, this.lbSelectAppAuthMulti);
        }

        protected void btnRemoveAll_OnClick(object sender, EventArgs e)
        {
            ListBoxHelper.RemoveAll(this.lbAllAppAuthMulti, this.lbSelectAppAuthMulti);
        }

        protected void pbEmployee_OnTextChanged(object sender, EventArgs e)
        {
            this.presenter.SearchEmployeeApp(this.pbEmployee.Value);
        }
	
		protected void btnSave_Click(object sender, EventArgs e)
		{
            string[] appName =null, appID = null;
            ListBoxHelper.GetAll(this.lbSelectAppAuthMulti, out appName, out appID);
            if (this.presenter.UpdateEmployeeAuthorization(this.pbEmployee.Value, this.pbEmployee.Text, appID))
                this.SaveData();
		}
		#endregion

		#region 重载。
		public override void LoadData()
		{
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<string[]>>(delegate(object sender, EntityEventArgs<string[]> e)
            {
                if (e.Entity != null)
                {
                    this.pbEmployee.Value = e.Entity[0];
                    this.pbEmployee.Text = e.Entity[1];
                    this.pbEmployee.Enabled = false;
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion
        
        #region ISysMgrEmployeeAuthorizationView 成员

        public void ShowMessage(string msg)
        {
            this.errMsg.Message = msg;
        }

        #endregion

        #region ISysMgrEmployeeAuthorizationEditView 成员

        public GUIDEx EmployeeID
        {
            get { return this.RequestGUIEx("EmployeeID"); }
        }

        public void BindNotSelectedApp(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.lbAllAppAuthMulti, data);
        }

        public void BindSelectedApp(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.lbSelectAppAuthMulti, data);
        }

        #endregion
    }

}
