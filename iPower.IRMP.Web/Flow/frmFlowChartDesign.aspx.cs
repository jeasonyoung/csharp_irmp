//================================================================================
//  FileName:frmFlowChartDesign.aspx.cs
//  Desc:
//
//  Called by
//
//  Auth:iPowerYoung
//  Date:2010-12-23 11:55:47
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
using System.Xml.Xsl;
using System.Text;

using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.Platform.Engine.DataSource;

using iPower.IRMP.Flow.Engine.Domain;
using iPower.IRMP.Flow.Engine.Service;
namespace iPower.IRMP.Flow.Web
{
    public partial class frmFlowChartDesign : ModuleBasePage, IFlowProcessChartDesignView
    {
        #region 成员变量，构造函数。
        FlowProcessChartDesignPresenter presenter = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public frmFlowChartDesign()
        {
            this.presenter = new FlowProcessChartDesignPresenter(this);
        }
        #endregion

        #region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.presenter.InitializeComponent();

                this.btnAdd.PickerPage += string.Format("?ProcessID={0}", this.ProcessID);
                this.btnExport.PickerPage += string.Format("?ProcessID={0}", this.ProcessID);
            }
            else
                this.LoadData();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            this.btnExport.Enabled = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
        }
        #endregion

        #region 重载。
        public override void LoadData()
        {
            base.LoadData();
        }
        #endregion

        #region IFlowProcessChartDesignView 成员

        public GUIDEx ProcessID
        {
            get { return this.RequestGUIEx("ProcessID"); }
        }

        public void ShowMessage(string content)
        {
            this.errMessage.Message = content;
        }

        #endregion
    }
}
