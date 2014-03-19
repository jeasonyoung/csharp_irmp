//================================================================================
// FileName: frmSysMgrLimitLoginEdit.aspx.cs
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
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
	
using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.IRMP.SysMgr.Engine.Domain;
using iPower.IRMP.SysMgr.Engine.Service;
namespace iPower.IRMP.SysMgr.Web
{
	///<summary>
	///frmSysMgrLimitLoginEdit列表页面后台代码。
	///</summary>
	public partial class frmSysMgrLimitLoginEdit:ModuleBasePage,ISysMgrLimitLoginEditView
	{
		#region 成员变量，构造函数。
		SysMgrLimitLoginPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmSysMgrLimitLoginEdit()
		{
			this.presenter = new SysMgrLimitLoginPresenter(this);

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
            SysMgrLimitLogin data = new SysMgrLimitLogin();
            data.LimitID = this.LimitID.IsValid ? this.LimitID : GUIDEx.New;
            data.EmployeeID = this.pbEmployee.Value;
            data.EmployeeName = this.pbEmployee.Text;

            if (this.presenter.UpdateLimitLogin(data))
                this.SaveData();
		}
		#endregion

		#region 重载。
		public override void LoadData()
		{
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<SysMgrLimitLogin>>(delegate(object sender, EntityEventArgs<SysMgrLimitLogin> e)
            {
                if (e.Entity != null)
                {
                    this.pbEmployee.Value = e.Entity.EmployeeID;
                    this.pbEmployee.Text = e.Entity.EmployeeName;
                    this.pbEmployee.Enabled = false;
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion
        
        #region ISysMgrLimitLoginEditView 成员

        public GUIDEx LimitID
        {
            get { return this.RequestGUIEx("LimitID"); }
        }

        public void ShowMessage(string msg)
        {
            this.errMsg.Message = msg;
        }

        #endregion
    }

}
