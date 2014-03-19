//================================================================================
//  FileName:frmFlowProcessExport.aspx.cs
//  Desc:
//
//  Called by
//
//  Auth:iPowerYoung
//  Date:2011-01-04 09:48:17
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
// ----  ------  -----------
//
//================================================================================
//  Copyright (C) 2009-2011 iPower Young Corporation
//================================================================================

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;

using iPower;
using iPower.IRMP.Flow.Engine.Service;
namespace iPower.IRMP.Flow.Web
{
    public partial class frmFlowProcessExport : ModuleBasePage, IFlowProcessExportView
    {
        #region 成员变量，构造函数。
        FlowProcessPresenter presenter = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public frmFlowProcessExport()
        {
            this.presenter = new FlowProcessPresenter(this);
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    lock (this)
                    {
                        XmlDocument doc = this.presenter.ExportProcess();
                        if (doc != null)
                        {
                            string fileName = this.presenter.GetProcessName(this.ProcessID);
                            HttpResponse resp = this.Page.Response;
                            resp.Clear();
                            resp.Buffer = true;
                            resp.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.xml", Uri.EscapeDataString(fileName)));
                            resp.ContentEncoding = Encoding.UTF8;
                            resp.ContentType = "text/xml";
                            doc.Save(resp.OutputStream);
                            //resp.Flush();
                            resp.End();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.errMessage.Message = ex.Message;
            }
        }

        #region IFlowProcessExportView 成员

        public GUIDEx ProcessID
        {
            get { return this.RequestGUIEx("ProcessID"); }
        }

        #endregion
    }
}
