//================================================================================
// FileName: frmFlowParameterMapEdit.aspx.cs
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
	///frmFlowParameterMapEdit列表页面后台代码。
	///</summary>
	public partial class frmFlowParameterMapEdit:ModuleBasePage,IFlowParameterMapEditView
	{
		#region 成员变量，构造函数。
		FlowParameterMapPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmFlowParameterMapEdit()
		{
			this.presenter = new FlowParameterMapPresenter(this);

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
            this.presenter.ChangeProcess();
        }

        protected void ddlTransitionID_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.presenter.ChangeTransition();
        }
			
		protected void btnSave_Click(object sender, EventArgs e)
		{
            try
            {
                FlowParameterMap data = new FlowParameterMap();
                data.TransitionID = this.TransitionID;
                data.ParameterID = this.ddlParameterID.SelectedValue;
                data.MapParameterID = this.ddlMapParameterID.SelectedValue;
                data.MapMode = int.Parse(this.rdMapMode.SelectedValue);
                data.AssemblyName = this.txtAssemblyName.Text;
                data.ClassName = this.txtClassName.Text;
                data.EntryName = this.txtEntryName.Text;

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
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<FlowParameterMap>>(delegate(object sender, EntityEventArgs<FlowParameterMap> e)
            {
                if (sender != null && e != null && e.Entity != null)
                {
                    this.ddlProcess.SelectedValue = new GUIDEx(sender);
                    this.presenter.ChangeProcess();

                    this.TransitionID = e.Entity.TransitionID;
                    this.presenter.ChangeTransition();

                    this.ddlParameterID.SelectedValue = e.Entity.ParameterID;
                    this.ddlMapParameterID.SelectedValue = e.Entity.MapParameterID;

                    this.rdMapMode.SelectedValue = e.Entity.MapMode.ToString();

                    this.txtAssemblyName.Text = e.Entity.AssemblyName;
                    this.txtClassName.Text = e.Entity.ClassName;
                    this.txtEntryName.Text = e.Entity.EntryName;
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion


        #region IFlowParameterMapView 成员
        public GUIDEx TransitionID
        {
            get { return this.ddlTransitionID.SelectedValue; }
            set { this.ddlTransitionID.SelectedValue = value; }
        }

        public void BindTransition(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlTransitionID, data);
        }

        public GUIDEx ProcessID
        {
            get { return this.ddlProcess.SelectedValue; }
        }

        public void BindProcess(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlProcess, data);
        }
        #endregion

        #region IFlowParameterMapEditView 成员

        public void BindEnumMapMode(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.rdMapMode, data);
        }

        public GUIDEx[] ParameterMapID
        {
            get
            {
                string str = this.Request["ParameterMapID"];
                if (!string.IsNullOrEmpty(str))
                {
                    string[] arr = str.Split('_');
                    if (arr != null && arr.Length == 3)
                    {
                        return new GUIDEx[] { new GUIDEx(arr[0]),
                                              new GUIDEx(arr[1]),
                                              new GUIDEx(arr[2])};
                    }
                }
                return null;
            }
        }

        public void BindParameterData(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlParameterID, data);
        }

        public void BindMapParameterData(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlMapParameterID, data);
        }

        public void ShowMessage(string content)
        {
            this.errMessage.Message = content;
            this.errMessage.Alert = !string.IsNullOrEmpty(content);
        }

        #endregion
    }

}
