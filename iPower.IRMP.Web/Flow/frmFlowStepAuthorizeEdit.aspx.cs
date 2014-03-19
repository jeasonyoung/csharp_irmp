//================================================================================
// FileName: frmFlowStepAuthorizeEdit.aspx.cs
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
	///frmFlowStepAuthorizeEdit列表页面后台代码。
	///</summary>
	public partial class frmFlowStepAuthorizeEdit:ModuleBasePage,IFlowStepAuthorizeEditView
	{
		#region 成员变量，构造函数。
		FlowStepAuthorizePresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmFlowStepAuthorizeEdit()
		{
			this.presenter = new FlowStepAuthorizePresenter(this);

		}
		#endregion

		#region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
                this.presenter.InitializeComponent();
        }

        protected void ddlProcessID_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.presenter.ChangeProcess();
        }
			
		protected void btnSave_Click(object sender, EventArgs e)
		{
            FlowStepAuthorize data = new FlowStepAuthorize();
            data.AuthorizeID = this.AuthorizeID.IsValid ? this.AuthorizeID : GUIDEx.New;
            data.StepID = new GUIDEx(this.ddlStepID.SelectedValue);
            data.EmployeeID = this.txtEmployeePickerBase.Value;
            data.EmployeeName = this.txtEmployeePickerBase.Text;
            data.TargetEmployeeID = this.txtTargetEmployeePickerBase.Value;
            data.TargetEmployeeName = this.txtTargetEmployeePickerBase.Text;
            data.BeginDate = DateTime.Parse(this.txtBeginDate.Text);
            data.EndDate = DateTime.Parse(this.txtEndDate.Text);

            if (this.presenter.UpdateData(data))
                base.SaveData();
		}
		#endregion

		#region 重载。
		public override void LoadData()
		{
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<FlowStepAuthorize>>(delegate(object sender, EntityEventArgs<FlowStepAuthorize> e)
            {
                if (e != null && e.Entity != null)
                {
                    this.ProcessID = this.presenter.GetProcessID(e.Entity.StepID);
                    this.presenter.ChangeProcess();

                    this.ddlStepID.SelectedValue = e.Entity.StepID;

                    this.txtEmployeePickerBase.Value = e.Entity.EmployeeID;
                    this.txtEmployeePickerBase.Text = e.Entity.EmployeeName;

                    this.txtTargetEmployeePickerBase.Value = e.Entity.TargetEmployeeID;
                    this.txtTargetEmployeePickerBase.Text = e.Entity.TargetEmployeeName;

                    this.txtBeginDate.Text = string.Format("{0:yyyy-MM-dd}", e.Entity.BeginDate);
                    this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", e.Entity.EndDate);
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion
        
        #region IFlowStepAuthorizeView 成员

        public void BindProcess(IListControlsData data)
        {
           this.ListControlsDataSourceBind(this.ddlProcessID, data);
        }

        #endregion

        #region IFlowStepAuthorizeEditView 成员

        public GUIDEx ProcessID
        {
            get
            {
                return this.ddlProcessID.SelectedValue;
            }
            set
            {
                this.ddlProcessID.SelectedValue = value;
            }
        }

        public void BindStep(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlStepID, data);
        }

        public GUIDEx AuthorizeID
        {
            get { return this.RequestGUIEx("AuthorizeID"); }
        }

        public void ShowMessage(string message)
        {
            this.errMessage.Message = message;
        }

        #endregion
    }

}
