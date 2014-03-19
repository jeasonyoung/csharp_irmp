//================================================================================
//  FileName: frmSecurityRegsiterImport.aspx.cs
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
using System.IO;

using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.IRMP.Security.Engine.Service;
namespace iPower.IRMP.Security.Web
{
    public partial class frmSecurityRegsiterImport : ModuleBasePage, ISecurityRegsiterImportView
    {
        #region 成员变量，构造函数。
        SecurityRegsiterPresenter presenter = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public frmSecurityRegsiterImport()
        {
            this.presenter = new SecurityRegsiterPresenter(this);
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

        protected void dgfrmSecurityRegsiterImport_BuildDataSource(object sender, EventArgs e)
        {
            this.dgfrmSecurityRegsiterImport.DataSource = this.presenter.ImportListViewDataSource;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.presenter.UploadSystemDefineSave(this.dgfrmSecurityRegsiterImport.CheckedValue))
                this.SaveData();
        }
        #endregion

        #region 重载。
        public override void LoadData()
        {
            this.dgfrmSecurityRegsiterImport.InvokeBuildDataSource();
        }
        #endregion

        #region ISecurityRegsiterView 成员

        public void ShowMessage(string content)
        {
            this.errMessage.Message = content;
        }

        #endregion

        #region ISecurityRegsiterImportView 成员

        public object UploadFile
        {
            get
            {
                return this.ViewState["UploadFileUri"];
             }
            set
            {
                this.ViewState["UploadFileUri"] = value;
                this.btnSave.Enabled = (value != null);
            }
        }

        public void BindSystemType(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlSystemType, data);
        }

        public void BindSystemStatus(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlSystemStatus, data);
        }

        #endregion

        #region ISecurityRegsiterImportView 成员


        public int SystemStatus
        {
            get
            {
                try
                {
                    return int.Parse(this.ddlSystemStatus.SelectedValue);
                }
                catch (Exception) { }
                return 0;
            }
        }

        public int SystemType
        {
            get
            {
                try
                {
                    return int.Parse(this.ddlSystemType.SelectedValue);
                }
                catch (Exception) { }
                return 0;
            }
        }

        #endregion
    }
}
