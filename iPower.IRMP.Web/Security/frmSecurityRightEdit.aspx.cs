//================================================================================
// FileName: frmSecurityRightEdit.aspx.cs
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
using iPower.IRMP.Security.Engine.Domain;
using iPower.IRMP.Security.Engine.Service;
namespace iPower.IRMP.Security.Web
{
	///<summary>
	///frmSecurityRightEdit列表页面后台代码。
	///</summary>
	public partial class frmSecurityRightEdit:ModuleBasePage,ISecurityRightEditView
	{
		#region 成员变量，构造函数。
		SecurityRightPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmSecurityRightEdit()
		{
			this.presenter = new SecurityRightPresenter(this);

		}
		#endregion

		#region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
                this.presenter.InitializeComponent();
        }

        protected void ddlSystemID_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.presenter.ChangeSystemRefershModule(this.ddlSystemID.SelectedValue);
        }

        protected void ddlModuleID_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem itemModule = this.ddlModuleID.SelectedItem;
            if (itemModule != null )
            {
                this.txtRightName.Text = itemModule.Text.Trim();
            }
            this.ddlActionID.SelectedIndex = -1;

        }

        protected void ddlActionID_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem itemModule = this.ddlModuleID.SelectedItem;
            ListItem itemActioin = this.ddlActionID.SelectedItem;

            if (itemModule != null && itemActioin != null)
            {
                this.txtRightName.Text = string.Format("{0}-{1}", itemModule.Text.Trim(), itemActioin.Text.Trim());
            }
        }
			
		protected void btnSave_Click(object sender, EventArgs e)
		{
            SecurityRight data = new SecurityRight();
            data.SystemID = this.ddlSystemID.SelectedValue;
            data.RightID = this.RightID.IsValid ? this.RightID : GUIDEx.New;
            data.ModuleID = this.ddlModuleID.SelectedValue;
            data.ActionID = this.ddlActionID.SelectedValue;
            data.RightName = this.txtRightName.Text.Trim();

            if (this.presenter.UpdateRight(data))
                this.SaveData();
		}
		#endregion

		#region 重载。
		public override void LoadData()
		{
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<SecurityRight>>(delegate(object sender, EntityEventArgs<SecurityRight> e)
            {
                if (e.Entity != null)
                {
                    this.ddlSystemID.SelectedValue = e.Entity.SystemID;
                    this.ddlSystemID_OnSelectedIndexChanged(null, EventArgs.Empty);
                    this.ddlActionID.Enabled = false;

                    this.ddlModuleID.SelectedValue = e.Entity.ModuleID;
                    this.ddlModuleID.Enabled = false;

                    this.ddlActionID.SelectedValue = e.Entity.ActionID;

                    this.txtRightName.Text = e.Entity.RightName;
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion
        
        #region ISecurityRightEditView 成员

        public GUIDEx RightID
        {
            get { return this.RequestGUIEx("RightID"); }
        }
        public GUIDEx SystemID
        {
            get { return this.RequestGUIEx("SystemID"); }
        }
        public void BindModule(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlModuleID, data);
        }

        public void BindAction(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlActionID, data);
        }

        public void BindSystem(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data, string value)
        {
            this.ListControlsDataSourceBind(this.ddlSystemID, data);
            if (!string.IsNullOrEmpty(value))
            {
                this.ddlSystemID.SelectedValue = value;
                if (this.ddlSystemID.SelectedIndex > -1)
                {
                    this.ddlSystemID.Enabled = false;
                }
            }
        }
        public void ShowMessage(string message)
        {
            this.errMessage.Message = message;
        }

        #endregion
    }

}
