//================================================================================
//  FileName: frmSecurityModuleImport.aspx.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/3/30
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
//  ----    ------  -----------------
//
//================================================================================
//  Copyright (C) 2004-2009 Jeason Young Corporation
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
using iPower.IRMP.Security.Engine.Service;
namespace iPower.IRMP.Security.Web
{
    public partial class frmSecurityModuleImport : ModuleBasePage, ISecurityModuleImportView
    {
        #region 成员变量，构造函数。
        SecurityModulePresenter presenter = null;
        /// <summary>
        /// 构造哦函数。
        /// </summary>
        public frmSecurityModuleImport()
        {
            this.presenter = new SecurityModulePresenter(this);
        }
        #endregion

        #region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
                this.presenter.InitializeComponent();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string fileName = this.txtFileUpload.FileName;
            if (string.IsNullOrEmpty(fileName))
            {
                this.ShowMessage("请选择文件上传！");
                return;
            }
            if (this.presenter.CheckUploadFormat(this.txtFileUpload.FileContent))
                this.LoadData();
        }

        protected void dgfrmSecurityModuleImport_BuildDataSource(object sender, EventArgs e)
        {
            this.dgfrmSecurityModuleImport.DataSource = this.presenter.ImportListViewDataSource;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.presenter.UploadModuleDefineSave(this.dgfrmSecurityModuleImport.CheckedValue))
                this.SaveData();
        }
        #endregion

        #region 重载。
        public override void LoadData()
        {
            this.dgfrmSecurityModuleImport.InvokeBuildDataSource();
        }
        #endregion

        #region ISecurityModuleView 成员

        public void ShowMessage(string message)
        {
            this.errMessage.Message = message;
        }

        public void BindSystem(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlSystemID, data);
        }

        #endregion

        #region ISecurityModuleImportView 成员

        public GUIDEx ImportSystemID
        {
            get { return this.ddlSystemID.SelectedValue; }
        }

        public object UploadFile
        {
            get
            {
                return this.ViewState["UploadFile"];
            }
            set
            {
                this.ViewState["UploadFile"] = value;
                this.btnSave.Enabled = (value != null);
            }
        }

        #endregion
    }
}
