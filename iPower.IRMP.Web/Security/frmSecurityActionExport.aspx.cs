//================================================================================
//  FileName: frmSecurityActionExport.aspx.cs
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
using System.IO;
using System.Text;
using System.Xml;

using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.IRMP.Security.Engine.Service;
namespace iPower.IRMP.Security.Web
{
    public partial class frmSecurityActionExport : ModuleBasePage, ISecurityActionExportView
    {
        #region 成员变量，构造函数。
        SecurityActionPresenter presenter = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public frmSecurityActionExport()
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

        protected void frmSecurityActionImport_BuildDataSource(object sender, EventArgs e)
        {
            this.dgfrmSecurityActionImport.DataSource = this.presenter.ExportListDataSource;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {          
            this.presenter.ExportAction(this.dgfrmSecurityActionImport.CheckedValue);
        }
        #endregion

        #region 重载。
        public override void LoadData()
        {
            this.dgfrmSecurityActionImport.InvokeBuildDataSource();
        }
        #endregion

        #region ISecurityActionExportView 成员

        public void ExportActionCollection(string type, string className, string query)
        {
            string url = "Download.ashx" + Download.CreateDownloadQueryString(type, className, query);
            this.DownloadiFrame.Text = string.Format("<iframe id=\"download\" name=\"download\" height=\"0px\" width=\"0px\" src=\"{0}\"></iframe>", url);
        }
 
        public void ShowMessage(string message)
        {
            this.errMessage.Message = message;
        }

        #endregion
    }
}
