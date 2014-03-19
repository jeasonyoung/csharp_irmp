//================================================================================
// FileName: frmFlowStepEdit.aspx.cs
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

using iPower.IRMP.Flow;
using iPower.IRMP.Flow.Engine.Domain;
using iPower.IRMP.Flow.Engine.Service;
namespace iPower.IRMP.Flow.Web
{
	///<summary>
	///frmFlowStepEdit列表页面后台代码。
	///</summary>
    public partial class frmFlowStepEdit : ModuleBasePage, IFlowStepEditView
    {
        #region 成员变量，构造函数。
        FlowStepPresenter presenter = null;
        ///<summary>
        ///构造函数。
        ///</summary>
        public frmFlowStepEdit()
        {
            this.presenter = new FlowStepPresenter(this);
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
                FlowStep data = new FlowStep();
                data.StepID = this.StepID.IsValid ? this.StepID : GUIDEx.New;

                data.ProcessID = this.ddlProcessID.SelectedValue;
                data.StepDuration = Convert.ToInt32(this.txtStepDuration.Text);

                data.StepName = this.txtStepName.Text;
                data.StepSign = this.txtStepSign.Text;

                data.StepType = Convert.ToInt32(this.rdStepType.SelectedValue);
                data.StepMode = Convert.ToInt32(this.rdStepMode.SelectedValue);

                data.EntryAction = this.txtEntryAction.Text;
                data.EntryQuery = this.txtEntryQuery.Text;

                data.OrderNo = Convert.ToInt32(this.txtStepOrderNo.Text);

                EnumStepWarning enumStepWarning = EnumStepWarning.None;
                foreach (ListItem item in this.chkStepWarning.Items)
                {
                    if (item.Selected)
                    {
                        if (enumStepWarning == EnumStepWarning.None)
                            enumStepWarning = (EnumStepWarning)Convert.ToInt32(item.Value);
                        else
                            enumStepWarning |= (EnumStepWarning)Convert.ToInt32(item.Value);
                    }
                }

                data.StepWarning = (int)enumStepWarning;
                data.StepDescription = this.txtStepDescription.Text;

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
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<FlowStep>>(delegate(object sender, EntityEventArgs<FlowStep> e)
            {
                if (e != null && e.Entity != null)
                {
                    this.ddlProcessID.SelectedValue = e.Entity.ProcessID;
                    this.txtStepDuration.Text = e.Entity.StepDuration.ToString();
                    this.txtStepName.Text = e.Entity.StepName;
                    this.txtStepSign.Text = e.Entity.StepSign;

                    this.rdStepType.SelectedValue = e.Entity.StepType.ToString();
                    this.rdStepMode.SelectedValue = e.Entity.StepMode.ToString();

                    this.txtEntryAction.Text = e.Entity.EntryAction;
                    this.txtEntryQuery.Text = e.Entity.EntryQuery;

                    this.txtStepOrderNo.Text = e.Entity.OrderNo.ToString();

                    EnumStepWarning enumStepWarning = (EnumStepWarning)e.Entity.StepWarning;

                    foreach (ListItem item in this.chkStepWarning.Items)
                    {
                        if (item.Value == ((int)(enumStepWarning & EnumStepWarning.SMS)).ToString())
                        {
                            item.Selected = true;
                        }
                        else if (item.Value == ((int)(enumStepWarning & EnumStepWarning.Email)).ToString())
                        {
                            item.Selected = true;
                        }
                        else if (item.Value == ((int)(enumStepWarning & EnumStepWarning.IAMS)).ToString())
                        {
                            item.Selected = true;
                        }
                    }

                    this.txtStepDescription.Text = e.Entity.StepDescription;
                }
            }));

            if (this.ProcessID.IsValid)
            {
                this.ddlProcessID.SelectedValue = this.ProcessID;
                this.ddlProcessID.Enabled = false;
            }
        }

        public override bool DeleteData()
        {
            return false;

        }
        #endregion

        #region 属性。
        /// <summary>
        /// 获取流程ID。
        /// </summary>
        protected GUIDEx ProcessID
        {
            get
            {
                return this.RequestGUIEx("ProcessID");
            }
        }
        #endregion

        #region IFlowStepEditView 成员

        public GUIDEx StepID
        {
            get { return this.RequestGUIEx("StepID"); }
        }

        public void BindProcess(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlProcessID, data);
        }

        public void BindEnumStepType(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.rdStepType, data);
        }

        public void BindEnumStepMode(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.rdStepMode, data);
        }

        public void BindEnumStepWarning(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.chkStepWarning, data);
        }

        public string[] GetEmployeeID
        {
            get { return this.txtEmployeePickerBase.Value.Split(','); }
        }
        public string[] GetEmployeeName
        {
            get { return this.txtEmployeePickerBase.Text.Split(','); }
        }

        public string[] GetRoleID
        {
            get { return this.txtRolePickerBase.Value.Split(','); }
        }
        public string[] GetRoleName
        {
            get { return this.txtRolePickerBase.Text.Split(','); }
        }

        public string[] GetRankID
        {
            get { return this.txtRankPickerBase.Value.Split(','); }
        }
        public string[] GetRankName
        {
            get { return this.txtRankPickerBase.Text.Split(','); }
        }

        public string[] GetPostID
        {
            get { return this.txtPostPickerBase.Value.Split(','); }
        }
        public string[] GetPostName
        {
            get { return this.txtPostPickerBase.Text.Split(','); }
        }

        public void SetEmployee(string[] employeeID, string[] employeeName)
        {
            if (employeeID != null && employeeName != null)
            {
                this.txtEmployeePickerBase.Value = string.Join(",", employeeID);
                this.txtEmployeePickerBase.Text = string.Join(",", employeeName);
            }
        }

        public void SetRole(string[] roleID, string[] roleName)
        {
            if (roleID != null && roleName != null)
            {
                this.txtRolePickerBase.Value = string.Join(",", roleID);
                this.txtRolePickerBase.Text = string.Join(",", roleName);
            }
        }

        public void SetRank(string[] rankID, string[] rankName)
        {
            if (rankID != null && rankName != null)
            {
                this.txtRankPickerBase.Value = string.Join(",", rankID);
                this.txtRankPickerBase.Text = string.Join(",", rankName);
            }
        }

        public void SetPost(string[] postID, string[] postName)
        {
            if (postID != null && postName != null)
            {
                this.txtPostPickerBase.Value = string.Join(",", postID);
                this.txtPostPickerBase.Text = string.Join(",", postName);
            }
        }

        public void ShowMessage(string content)
        {
            this.errMessage.Message = content;
            this.errMessage.Alert = !string.IsNullOrEmpty(content);
        }

        #endregion
    }
}
