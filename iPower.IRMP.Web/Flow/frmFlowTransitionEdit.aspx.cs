//================================================================================
// FileName: frmFlowTransitionEdit.aspx.cs
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
using System.Data;

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
	///frmFlowTransitionEdit列表页面后台代码。
	///</summary>
    public partial class frmFlowTransitionEdit : ModuleBasePage, IFlowTransitionEditView, IFlowTransitionConditionView
	{
		#region 成员变量，构造函数。
		FlowTransitionPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmFlowTransitionEdit()
		{
			this.presenter = new FlowTransitionPresenter(this);

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
            try
            {
                this.presenter.ChangeProcess();
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
            }
        }

        protected void ddlFromStepID_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.presenter.ChangeFromStep();
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
            }
        }

        protected void btnSaveCondition_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.presenter.AddTransitionCondition())
                    this.LoadTransitionCondition();
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
            }
        }

        protected void btnDeleteCondition_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.presenter.RemoveTransitionCondition(this.dgTransitionCondition.CheckedValue))
                    this.LoadTransitionCondition();
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
            }
        }
			
		protected void btnSave_Click(object sender, EventArgs e)
		{
            try
            {
                FlowTransition data = new FlowTransition();
                data.TransitionID = this.TransitionID.IsValid ? this.TransitionID : GUIDEx.New;
                data.TransitionRule = int.Parse(this.rdTransitionRule.SelectedValue);
                data.ProcessID = this.ProcessID;
                data.FromStepID = this.FromStepID;
                data.ToStepID = this.ddlToStepID.SelectedValue;

                if (this.presenter.UpdateData(data))
                    base.SaveData();
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
            }
		}

        protected void dgTransitionCondition_BuildDataSource(object sender, EventArgs e)
        {
            this.dgTransitionCondition.DataSource = this.TempTransitionCondition;
        }
		#endregion

		#region 重载。
		public override void LoadData()
		{
            GUIDEx processID = this.RequestGUIEx("ProcessID");
            if (processID.IsValid)
            {
                this.ProcessID = processID;
                this.ddlProcessID_OnSelectedIndexChanged(this, EventArgs.Empty);
                this.ddlProcessID.Enabled = false;
            }

            GUIDEx fromStepID = this.RequestGUIEx("FromStepID");
            if (fromStepID.IsValid)
            {
                this.FromStepID = fromStepID;
                this.ddlFromStepID_OnSelectedIndexChanged(this, EventArgs.Empty);
                this.ddlFromStepID.Enabled = false;
            }

            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<FlowTransition>>(delegate(object sender, EntityEventArgs<FlowTransition> e)
            {
                if (e != null && e.Entity != null)
                {
                    this.ProcessID = e.Entity.ProcessID;
                    this.ddlProcessID_OnSelectedIndexChanged(sender, EventArgs.Empty);
                    this.ddlProcessID.Enabled = false;

                    this.rdTransitionRule.SelectedValue = e.Entity.TransitionRule.ToString();
                    
                    this.FromStepID = e.Entity.FromStepID;
                    this.ddlFromStepID_OnSelectedIndexChanged(sender, EventArgs.Empty);
                    this.ddlFromStepID.Enabled = false;
                    this.ddlToStepID.SelectedValue = e.Entity.ToStepID;
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion
        
        #region IFlowTransitionView 成员

        public void BindProcess(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlProcessID, data);
        }

        #endregion

        #region IFlowTransitionEditView 成员

        public GUIDEx TransitionID
        {
            get { return this.RequestGUIEx("TransitionID"); }
        }

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

        public GUIDEx FromStepID
        {
            get
            {
                return this.ddlFromStepID.SelectedValue;
            }
            set
            {
                this.ddlFromStepID.SelectedValue = value;
            }
        }

        public DataTable TempTransitionCondition
        {
            get
            {
                return this.ViewState["TempTransitionCondition"] as DataTable;
            }
            set
            {
                this.ViewState["TempTransitionCondition"] = value;
            }
        }

        public void BindEnumTransitionRule(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.rdTransitionRule, data);
        }

        public void BindEnumCompareSign(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlCondition, data);
        }

        public void BindFromStep(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlFromStepID, data);
        }

        public void BindToStep(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlToStepID, data);
        }

        public void BindTransitionParameter(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlTransitionParameter, data);
        }

        public void LoadTransitionCondition()
        {
           this.dgTransitionCondition.InvokeBuildDataSource();
        }
        public void ShowMessage(string content)
        {
            this.errMessage.Message = content;
            this.errMessage.Alert = !string.IsNullOrEmpty(content);
        }
        #endregion

        #region IFlowTransitionConditionView 成员

        public GUIDEx TransitionParameterID
        {
            get { return this.ddlTransitionParameter.SelectedValue; }
        }

        public GUIDEx ConditionID
        {
            get { return this.ddlCondition.SelectedValue; }
        }

        public string CompareValue
        {
            get { return this.txtCompareValue.Text.Trim(); }
        }

        #endregion
    }

}
