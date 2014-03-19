//================================================================================
// FileName: frmSecurityRoleEdit.aspx.cs
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
using Domain = iPower.IRMP.Security.Engine.Domain;
using iPower.IRMP.Security.Engine.Service;
namespace iPower.IRMP.Security.Web
{
	///<summary>
	///frmSecurityRoleEdit列表页面后台代码。
	///</summary>
	public partial class frmSecurityRoleEdit:ModuleBasePage,ISecurityRoleEditView
	{
		#region 成员变量，构造函数。
		SecurityRolePresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmSecurityRoleEdit()
		{
			this.presenter = new SecurityRolePresenter(this);

		}
		#endregion

		#region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
                this.presenter.InitializeComponent();
        }
			
		protected void btnSave_Click(object sender, EventArgs e)
		{
            Domain.SecurityRole data = new Domain.SecurityRole();
            data.RoleID = this.RoleID.IsValid ? this.RoleID : GUIDEx.New;
            data.RoleName = this.txtRoleName.Text.Trim();
            data.ParentRoleID = this.ddlParentRoleID.SelectedValue;
            data.RoleStatus = int.Parse(this.ddlRoleStatus.SelectedValue);
            data.RoleDescription = this.txtRoleDescription.Text.Trim();

            if (this.presenter.UpdateRole(data, this.tvSystem.CheckedValue))
                this.SaveData();
		}
		#endregion

		#region 重载。
		public override void LoadData()
		{
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<Domain.SecurityRole>>(delegate(object sender, EntityEventArgs<Domain.SecurityRole> e)
            {
                if (e.Entity != null)
                {
                    this.txtRoleName.Text = e.Entity.RoleName;
                    this.ddlParentRoleID.SelectedValue = e.Entity.ParentRoleID;
                    this.ddlRoleStatus.SelectedValue = e.Entity.RoleStatus.ToString();
                    this.txtRoleDescription.Text = e.Entity.RoleDescription;
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion
        
        #region ISecurityRoleEditView 成员

        public GUIDEx RoleID
        {
            get { return this.RequestGUIEx("RoleID"); }
        }

        public void BindParentRole(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlParentRoleID, data);
        }

        public void BindSystemTree(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data, System.Collections.Specialized.StringCollection chkSelected)
        {
            this.ListControlsDataSourceBind(this.tvSystem, data);
            if (chkSelected != null)
                this.tvSystem.CheckedValue = chkSelected;
        }

        public void BindRoleStatus(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlRoleStatus, data);
        }

        #endregion
    }

}
