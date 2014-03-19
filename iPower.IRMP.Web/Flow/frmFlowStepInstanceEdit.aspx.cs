//================================================================================
//  FileName: frmFlowStepInstanceEdit.aspx.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/8/17
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

using iPower.Platform.Engine.Service;
using iPower.IRMP.Flow.Engine.Domain;
using iPower.IRMP.Flow.Engine.Service;
namespace iPower.IRMP.Flow.Web
{
    public partial class frmFlowStepInstanceEdit : ModuleBasePage, IFlowStepInstanceEditView
    {
        #region 成员变量，构造函数。
        FlowStepInstancePresenter presenter = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public frmFlowStepInstanceEdit()
        {
            this.presenter = new FlowStepInstancePresenter(this);
        }
        #endregion

        #region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                this.presenter.InitializeComponent();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                EnumInstanceStepStatus status = (EnumInstanceStepStatus)Enum.Parse(typeof(EnumInstanceStepStatus), this.ddlInstanceStepStatus.SelectedValue);
                if (this.presenter.UpdateStepInstanceStatus(status))
                    this.SaveData();
            }
            catch (Exception ex)
            {
                this.errMessage.Message = ex.Message;
            }
        }
        #endregion

        #region 重载。
        public override void LoadData()
        {
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<FlowStepInstance>>(delegate(object sender, EntityEventArgs<FlowStepInstance> e)
            {
                if (e != null && e.Entity != null)
                {
                    this.txtStepName.Text = e.Entity.StepName;
                    
                    this.pbFromEmployee.Value = e.Entity.FromEmployeeID;
                    this.pbFromEmployee.Text = e.Entity.FromEmployeeName;

                    this.txtCreateDate.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}",e.Entity.CreateDate);
                    this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", e.Entity.EndDate);

                    this.ddlInstanceStepStatus.SelectedValue = e.Entity.InstanceStepStatus.ToString();
                }
            }));
        }
        #endregion

        #region IFlowStepInstanceEditView 成员

        public GUIDEx StepInstanceID
        {
            get { return this.RequestGUIEx("StepInstanceID"); }
        }

        public void BindInstanceStepStatus(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlInstanceStepStatus, data);
        }

        #endregion
    }
}
