//================================================================================
//  FileName: frmSecurityActionImport.aspx.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/3/31
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
    public partial class frmSecurityActionImport : ModuleBasePage, ISecurityActionImportView
    {
        #region 成员变量，构造函数。
        SecurityActionPresenter presenter = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public frmSecurityActionImport()
        {
            this.presenter = new SecurityActionPresenter(this);
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

        protected void dgfrmSecurityActionImport_BuildDataSource(object sender, EventArgs e)
        {
            this.dgfrmSecurityActionImport.DataSource = this.presenter.ImportListViewDataSource;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.presenter.UploadSecurityActionCollection(this.dgfrmSecurityActionImport.CheckedValue))
                this.SaveData();
        }
        #endregion

        #region 重载。
        public override void LoadData()
        {
            this.dgfrmSecurityActionImport.InvokeBuildDataSource();
        }
        #endregion

        #region ISecurityActionImportView 成员

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
        public void ShowMessage(string message)
        {
            this.errMessage.Message = message;
        }

        #endregion
    }
}
