//================================================================================
// FileName: frmFlowProcessInstanceEdit.aspx.cs
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
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
	
using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.IRMP.Flow.Engine.Domain;
using iPower.IRMP.Flow.Engine.Service;
namespace iPower.IRMP.Flow.Web
{
	///<summary>
	///frmFlowProcessInstanceEdit列表页面后台代码。
	///</summary>
	public partial class frmFlowProcessInstanceEdit:ModuleBasePage,IFlowProcessInstanceEditView
	{
		#region 成员变量，构造函数。
		FlowProcessInstancePresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmFlowProcessInstanceEdit()
		{
			this.presenter = new FlowProcessInstancePresenter(this);

		}
		#endregion

		#region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
                this.presenter.InitializeComponent();
        }

        protected void dgStepInstance_BuildDataSource(object sender, EventArgs e)
        {
            this.dgStepInstance.DataSource = this.presenter.StepInstanceDataSource;
        }

        protected void dgStepInstanceRunError_BuildDataSource(object sender, EventArgs e)
        {
            this.dgStepInstanceRunError.DataSource = this.presenter.StepInstanceRunErrorDataSource;
        }

        protected void dgStepInstanceTask_BuildDataSource(object sender, EventArgs e)
        {
            this.dgStepInstanceTask.DataSource = this.presenter.StepInstanceTaskDataSource;
        }
			
		protected void btnSave_Click(object sender, EventArgs e)
		{
            if (this.ProcessInstanceID.IsValid)
            {
                EnumInstanceProcessStatus status = (EnumInstanceProcessStatus)Enum.Parse(typeof(EnumInstanceProcessStatus), this.ddlFlowInstanceStatus.SelectedValue);
                if (this.presenter.ChangeFlowInstanceStatus(this.ProcessInstanceID, status))
                    this.SaveData();
            }
		}

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                StringCollection collection = this.dgStepInstanceTask.CheckedValue;
                if (collection != null && collection.Count > 0)
                {
                    if (this.presenter.BatchDeleteStepInstanceTask(collection))
                        this.SaveData();
                }
                else
                {
                    throw new Exception("未选择数据！");
                }
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
            this.btnSave.Visible = false;
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<FlowProcessInstance>>(delegate(object sender, EntityEventArgs<FlowProcessInstance> e)
            {
                if (e != null && e.Entity != null)
                {
                    this.txtProcessName.Text = e.Entity.ProcessName;
                    this.txtProcessInstanceName.Text = e.Entity.ProcessInstanceName;
                    this.txtCreateDate.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", e.Entity.CreateDate);
                    if (e.Entity.EndDate != null && e.Entity.EndDate != DateTime.MinValue)
                    {
                        this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", e.Entity.EndDate);
                    }
                    this.ddlFlowInstanceStatus.SelectedValue = e.Entity.InstanceProcessStatus.ToString();

                    this.btnSave.Visible = (e.Entity.InstanceProcessStatus != (int)EnumInstanceProcessStatus.Complete);

                    this.dgStepInstance.InvokeBuildDataSource();
                    this.dgStepInstanceRunError.InvokeBuildDataSource();
                    this.dgStepInstanceTask.InvokeBuildDataSource();
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion
        
        #region IFlowProcessInstanceEditView 成员

        public GUIDEx ProcessInstanceID
        {
            get { return this.RequestGUIEx("ProcessInstanceID"); }
        }

        public void BindFlowInstanceStatus(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlFlowInstanceStatus, data);
        }

        public void ShowMessage(string message)
        {
            this.errMsg.Message = message;
        }

        public void BindProcessResumes(object dataSource)
        {
            this.rpProcessResumes.DataSource = dataSource;
            this.rpProcessResumes.DataBind();
        }
        #endregion
    }

}
