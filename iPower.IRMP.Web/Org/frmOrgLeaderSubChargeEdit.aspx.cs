//================================================================================
// FileName: frmOrgLeaderSubChargeEdit.aspx.cs
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
// Copyright (C) 2009-2010 Jeason Young Corporation
//================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
	
using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.Platform.Engine.DataSource;
using iPower.IRMP.Org.Engine.Domain;
using iPower.IRMP.Org.Engine.Service;
namespace iPower.IRMP.Org.Web
{
	///<summary>
	///frmOrgLeaderSubChargeEdit列表页面后台代码。
	///</summary>
	public partial class frmOrgLeaderSubChargeEdit:ModuleBasePage,IOrgLeaderSubChargeEditView
	{
		#region 成员变量，构造函数。
		OrgLeaderSubChargePresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmOrgLeaderSubChargeEdit()
		{
			this.presenter = new OrgLeaderSubChargePresenter(this);
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
            if (this.presenter.UpdateLeaderSubCharge(this.txtEmployeeName.Value, this.txtSubDepartment.CheckedValue))
                base.SaveData();
		}
		#endregion

		#region 重载。
		public override void LoadData()
		{
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<string[]>>(delegate(object sender, EntityEventArgs<string[]> e)
            {
                string[] result = e.Entity;
                if (result != null)
                {
                    this.txtEmployeeName.Value = result[0];
                    this.txtEmployeeName.Text = result[1];

                    this.txtEmployeeName.Enabled = false;
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion

        #region IOrgLeaderSubChargeEditView 成员

        public GUIDEx EmployeeID
        {
            get { return this.RequestGUIEx("EmployeeID"); }
        }

        public void BindDepartment(IListControlsTreeViewData data, StringCollection depCollection)
        {
            this.txtSubDepartment.DataSource = data.DataSource as System.Data.DataTable;
            this.txtSubDepartment.IDField = data.DataValueField;
            this.txtSubDepartment.PIDField = data.ParentDataValueField;
            this.txtSubDepartment.TitleField = data.DataTextField;
            this.txtSubDepartment.OrderNoField = data.DataTextField;
            this.txtSubDepartment.CheckedValue = depCollection;
            this.txtSubDepartment.BuildTree();
        }

        #endregion
    }

}
