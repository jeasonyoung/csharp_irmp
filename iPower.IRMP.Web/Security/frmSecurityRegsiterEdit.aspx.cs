//================================================================================
// FileName: frmSecurityRegsiterEdit.aspx.cs
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
	///frmSecurityRegsiterEdit列表页面后台代码。
	///</summary>
    public partial class frmSecurityRegsiterEdit : ModuleBasePage, ISecurityRegsiterEditView
    {
        #region 成员变量，构造函数。
        SecurityRegsiterPresenter presenter = null;
        ///<summary>
        ///构造函数。
        ///</summary>
        public frmSecurityRegsiterEdit()
        {
            this.presenter = new SecurityRegsiterPresenter(this);

        }
        #endregion

        #region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.presenter.InitializeComponent();
                //this.lbTitle.Text = base.CurrentModuleTitle;

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SecurityRegsiter data = new SecurityRegsiter();
            data.SystemID = this.txtSystemID.Text;
            data.ParentSystemID = this.ddlParentSystemID.SelectedValue;
            data.SystemSign = this.txtSystemSign.Text;
            data.SystemName = this.txtSystemName.Text;

            data.SystemURL = this.txtSystemURL.Text;
            data.SecurityURL = this.txtSecurityURL.Text;
            data.PatchURL = this.txtPatchURL.Text;
            data.ModuleConfigURL = this.txtModuleConfigURL.Text;

            data.SystemType = int.Parse(this.ddlSystemType.SelectedValue);
            data.SystemStatus = int.Parse(this.ddlSystemStatus.SelectedValue);
            data.SystemDescription = this.txtSystemDescription.Text;

            if (this.presenter.UpdateRegsiter(data))
                base.SaveData();
        }

        protected void btnBatch_Click(object sender, EventArgs e)
        {
            if (this.presenter.BatchInitAppModuleRight())
                base.SaveData();
        }
        #endregion

        #region 重载。
        public override void LoadData()
        {
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<SecurityRegsiter>>(delegate(object sender, EntityEventArgs<SecurityRegsiter> e)
            {
                if (e.Entity != null)
                {
                    //this.lbSystemID.Text = string.Format("(系统ID：{0})", e.Entity.SystemID);
                    this.txtSystemID.Text = e.Entity.SystemID;
                    this.txtSystemID.Enabled = false;
                    this.ddlParentSystemID.SelectedValue = e.Entity.ParentSystemID;
                    this.txtSystemSign.Text = e.Entity.SystemSign;
                    this.txtSystemName.Text = e.Entity.SystemName;

                    this.txtSystemURL.Text = e.Entity.SystemURL;
                    this.txtSecurityURL.Text = e.Entity.SecurityURL;
                    this.txtPatchURL.Text = e.Entity.PatchURL;
                    this.txtModuleConfigURL.Text = e.Entity.ModuleConfigURL;

                    this.ddlSystemType.SelectedValue = e.Entity.SystemType.ToString();
                    this.ddlSystemStatus.SelectedValue = e.Entity.SystemStatus.ToString();

                    this.txtSystemDescription.Text = e.Entity.SystemDescription;

                    this.btnBatch.Visible = true;
                }
            }));
        }

        public override bool DeleteData()
        {
            return false;

        }
        #endregion


        #region ISecurityRegsiterEditView 成员

        public GUIDEx SystemID
        {
            get { return this.RequestGUIEx("SystemID"); }
        }

        public void BindSystemType(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlSystemType, data);
        }

        public void BindSystemStatus(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlSystemStatus, data);
        }

        public void ShowMessage(string content)
        {
            this.errMsg.Message = content;
        }

        #endregion

        #region ISecurityRegsiterView 成员

        public void BindParentSystem(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlParentSystemID, data);
        }

        #endregion
    }
}
