//================================================================================
//  FileName:frmFlowProcessImport.aspx.cs
//  Desc:
//
//  Called by
//
//  Auth:iPowerYoung
//  Date:2010-12-31 08:59:31
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
// ----  ------  -----------
//
//================================================================================
//  Copyright (C) 2009-2010 iPower Young Corporation
//================================================================================

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

using iPower.IRMP.Flow.Engine.Service;

namespace iPower.IRMP.Flow.Web
{
    public partial class frmFlowProcessImport : ModuleBasePage, IFlowProcessImportView
    {
        #region 成员变量，构造函数。
        FlowProcessPresenter presenter = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public frmFlowProcessImport()
        {
            this.presenter = new FlowProcessPresenter(this);
        }
        #endregion

        #region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                this.presenter.InitializeComponent();
        }

        protected void btnImportSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.uploadProcess.PostedFile != null && this.uploadProcess.PostedFile.InputStream != null)
                {
                    if (this.presenter.UploadPorcess(this.uploadProcess.PostedFile.InputStream))
                    {
                        this.importPanel.Visible = false;
                        this.drawChartPanel.Visible = true;

                        //if (!string.IsNullOrEmpty(this.ProcessXml))
                        //    this.WorkflowDisplay.DocumentContent = this.ProcessXml;
                    }
                }

            }
            catch (Exception ex)
            {
                this.ShowMesssage(ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.presenter.ImportProcess())
                    base.SaveData();
            }
            catch (Exception ex)
            {
                this.ShowMesssage(ex.Message);
            }
        }
        #endregion

        #region IFlowProcessImportView 成员

        public void ShowMesssage(string messge)
        {
            this.errMessage.Message = messge;
            this.errMessage.Alert = !string.IsNullOrEmpty(messge);
        }

        public string ProcessXml
        {
            get
            {
                return this.ViewState["ProcessXml"] as string;
            }
            set
            {
                this.ViewState["ProcessXml"] = value;
            }
        }

        #endregion
    }
}
