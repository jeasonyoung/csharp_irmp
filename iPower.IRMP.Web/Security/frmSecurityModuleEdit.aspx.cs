//================================================================================
// FileName: frmSecurityModuleEdit.aspx.cs
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
	///frmSecurityModuleEdit�б�ҳ���̨���롣
	///</summary>
	public partial class frmSecurityModuleEdit:ModuleBasePage,ISecurityModuleEditView
	{
		#region ��Ա���������캯����
		SecurityModulePresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmSecurityModuleEdit()
		{
			this.presenter = new SecurityModulePresenter(this);

		}
		#endregion

		#region �¼�����
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
                this.presenter.InitializeComponent();
        }

        protected void ddlSystemID_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.presenter.ChangeSystemSelected();
        }
			
		protected void btnSave_Click(object sender, EventArgs e)
		{
            SecurityModule data = new SecurityModule();
            data.ModuleID = this.txtModuleID.Text.Trim();
            data.SystemID = this.ddlSystemID.SelectedValue;
            data.ModuleName = this.txtModuleName.Text.Trim();
            data.ParentModuleID = this.ddlParentModuleID.SelectedValue;
            data.ModuleStatus = int.Parse(this.ddlModuleStatus.SelectedValue);
            data.OrderNo = !string.IsNullOrEmpty(this.txtOrderNo.Text) ? int.Parse(this.txtOrderNo.Text) : 0;
            data.ModuleDescription = this.txtModuleDescription.Text.Trim();

            if (this.presenter.UpdateModule(data))
                base.SaveData();
		}
		#endregion

		#region ���ء�
		public override void LoadData()
		{
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<SecurityModule>>(delegate(object sender, EntityEventArgs<SecurityModule> e)
            {
                if (e.Entity != null)
                {
                    //this.lbModuleID.Text = string.Format("(ģ��ID��{0})", e.Entity.ModuleID);

                    this.ddlSystemID.SelectedValue = e.Entity.SystemID;
                    this.txtModuleID.Text = e.Entity.ModuleID;
                    this.txtModuleID.Enabled = false;

                    this.txtModuleName.Text = e.Entity.ModuleName;
                    this.ddlParentModuleID.SelectedValue = e.Entity.ParentModuleID;

                    this.ddlModuleStatus.SelectedValue = e.Entity.ModuleStatus.ToString();
                    this.txtOrderNo.Text = e.Entity.OrderNo.ToString();

                    this.txtModuleDescription.Text = e.Entity.ModuleDescription;
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion
        
        #region ISecurityModuleEditView ��Ա

        public GUIDEx ModuleID
        {
            get { return this.RequestGUIEx("ModuleID"); }
        }

        public GUIDEx SystemID
        {
            get { return this.ddlSystemID.SelectedValue; }
        }
        public void BindParentModule(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlParentModuleID, data);
        }

        public void BindModuleStatus(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlModuleStatus, data);
        }

        #endregion

        #region ISecurityModuleView ��Ա

        public void ShowMessage(string message)
        {
            this.errMessage.Message = message;
        }

        public void BindSystem(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlSystemID, data);
        }

        #endregion
    }
}
