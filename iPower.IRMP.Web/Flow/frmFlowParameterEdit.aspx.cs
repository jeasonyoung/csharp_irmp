//================================================================================
// FileName: frmFlowParameterEdit.aspx.cs
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
	///frmFlowParameterEdit列表页面后台代码。
	///</summary>
    public partial class frmFlowParameterEdit : ModuleBasePage, IFlowParameterEditView
    {
        #region 成员变量，构造函数。
        FlowParameterPresenter presenter = null;
        ///<summary>
        ///构造函数。
        ///</summary>
        public frmFlowParameterEdit()
        {
            this.presenter = new FlowParameterPresenter(this);

        }
        #endregion

        #region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
                this.presenter.InitializeComponent();
        }

        protected void ddlProcess_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.presenter.BindStepData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                FlowParameter data = new FlowParameter();
                data.ParameterID = this.ParameterID.IsValid ? this.ParameterID : GUIDEx.New;
                data.StepID = this.ddlStep.SelectedValue;
                data.ParameterName = this.txtParameterName.Text;
                data.ParameterType = Convert.ToInt32(this.rdParameterType.SelectedValue);
                data.DefaultValue = this.txtDefaultValue.Text;
                data.ParameterDescription = this.txtParameterDescription.Text;

                if (this.presenter.UpdateData(data))
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
            GUIDEx processID = this.RequestGUIEx("ProcessID");
            if (processID.IsValid)
                this.SetProcess(processID);

            GUIDEx stepID = this.RequestGUIEx("StepID");
            if (stepID.IsValid)
            {
                this.ddlStep.SelectedValue = stepID;
                this.ddlStep.Enabled = false;
            }

            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<FlowParameter>>(delegate(object sender, EntityEventArgs<FlowParameter> e)
            {
                if (e != null && e.Entity != null)
                {
                    this.ddlStep.SelectedValue = e.Entity.StepID;
                    this.txtParameterName.Text = e.Entity.ParameterName;
                    this.rdParameterType.SelectedValue = e.Entity.ParameterType.ToString();
                    this.txtDefaultValue.Text = e.Entity.DefaultValue;
                    this.txtParameterDescription.Text = e.Entity.ParameterDescription;
                }
            }));
        }

        public override bool DeleteData()
        {
            return false;

        }
        #endregion

        #region IFlowParameterView 成员

        public string ProcessID
        {
            get { return this.ddlProcess.SelectedValue; }
        }

        public GUIDEx ParameterID
        {
            get { return this.RequestGUIEx("ParameterID"); }
        }

        public void BindProcess(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlProcess, data);
        }

        public void BindStep(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlStep, data);
        }

        public void BindEnumParameterType(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.rdParameterType, data);
        }

        public void SetProcess(GUIDEx processID)
        {
            this.ddlProcess.SelectedValue = processID;
            this.ddlProcess_OnSelectedIndexChanged(null, null);
            this.ddlProcess.Enabled = !processID.IsValid;

        }

        public void ShowMessage(string content)
        {
            this.errMessage.Message = content;
            this.errMessage.Alert = !string.IsNullOrEmpty(content);
        }

        #endregion
    }

}
