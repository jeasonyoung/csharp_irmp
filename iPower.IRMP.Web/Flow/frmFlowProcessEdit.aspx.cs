//================================================================================
// FileName: frmFlowProcessEdit.aspx.cs
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
// Copyright (C) 2009-2010 iPower Young Corporation
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
using iPower.Platform.Engine.DataSource;

using iPower.IRMP.Flow.Engine.Domain;
using iPower.IRMP.Flow.Engine.Service;
namespace iPower.IRMP.Flow.Web
{
	///<summary>
	///frmFlowProcessEdit列表页面后台代码。
	///</summary>
    public partial class frmFlowProcessEdit : ModuleBasePage, IFlowProcessEditView
    {
        #region 成员变量，构造函数。
        FlowProcessPresenter presenter = null;
        ///<summary>
        ///构造函数。
        ///</summary>
        public frmFlowProcessEdit()
        {
            this.presenter = new FlowProcessPresenter(this);

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
            try
            {
                FlowProcess data = new FlowProcess();
                data.ProcessID = this.ProcessID.IsValid ? this.ProcessID : GUIDEx.New;
                data.ProcessName = this.txtProcessName.Text;
                data.ProcessSign = this.txtProcessSign.Text;
                data.BeginDate = Convert.ToDateTime(this.txtBeginDate.Text);
                data.EndDate = Convert.ToDateTime(this.txtEndDate.Text);
                data.ProcessStatus = Convert.ToInt32(this.ddlProcessStatus.SelectedValue);
                data.ProcessDescription = this.txtProcessDescription.Text;

                if (this.presenter.UpdateData(data))
                    base.SaveData();
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
            }
        }

        protected void btnProcessStatus_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (this.presenter.ChangeProcessStatus())
                    base.SaveData();
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
            }
        }
        #endregion

        #region 重载。
        public override void LoadData()
        {
            this.txtBeginDate.Text = string.Format("{0:yyyy-MM-dd 00:00:00}", DateTime.Now);
            this.txtEndDate.Text = "9999-12-31 23:59:59";

            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<FlowProcess>>(delegate(object sender, EntityEventArgs<FlowProcess> e)
            {
                if (e != null && e.Entity != null)
                {
                    this.txtProcessName.Text = e.Entity.ProcessName;
                    this.txtProcessSign.Text = e.Entity.ProcessSign;
                    this.txtBeginDate.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", e.Entity.BeginDate);
                    this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", e.Entity.EndDate);
                    this.ddlProcessStatus.SelectedValue = e.Entity.ProcessStatus.ToString();
                    this.txtProcessDescription.Text = e.Entity.ProcessDescription;

                    EnumProcessStatus pStatus = (EnumProcessStatus)Enum.Parse(typeof(EnumProcessStatus), e.Entity.ProcessStatus.ToString());

                    this.btnProcessStatus.MouseOutCssClass = (pStatus == EnumProcessStatus.Start) ? "BtnDisableMouseOut" : "BtnEnableMouseOut";
                    this.btnProcessStatus.MouseOverCssClass = (pStatus == EnumProcessStatus.Start) ? "BtnDisableMouseOver" : "BtnEnableMouseOver";
                    this.btnProcessStatus.Visible = true;
                }
            }));
        }

        public override bool DeleteData()
        {
            return false;

        }
        #endregion
        
        #region IFlowProcessEditView 成员

        public GUIDEx ProcessID
        {
            get { return this.RequestGUIEx("ProcessID"); }
        }

        public void BindEnumProcessStatus(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlProcessStatus, data);

            this.ddlProcessStatus.SelectedValue = ((int)EnumProcessStatus.Stop).ToString();
        }

        public void ShowMessage(string message)
        {
            this.errMessage.Message = message;
            this.errMessage.Alert = !string.IsNullOrEmpty(message);
        }

        #endregion
    }

}
