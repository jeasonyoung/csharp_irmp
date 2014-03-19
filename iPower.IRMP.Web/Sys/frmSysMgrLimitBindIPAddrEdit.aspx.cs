//================================================================================
// FileName: frmSysMgrLimitBindIPAddrEdit.aspx.cs
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
	///frmSysMgrLimitBindIPAddrEdit列表页面后台代码。
	///</summary>
	public partial class frmSysMgrLimitBindIPAddrEdit:ModuleBasePage,ISysMgrLimitBindIPAddrEditView
	{
		#region 成员变量，构造函数。
		SysMgrLimitBindIPAddrPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmSysMgrLimitBindIPAddrEdit()
		{
			this.presenter = new SysMgrLimitBindIPAddrPresenter(this);

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
            SysMgrLimitBindIPAddr data = new SysMgrLimitBindIPAddr();
            data.BindID = this.BindID.IsValid ? this.BindID : GUIDEx.New;
            data.EmployeeID = this.pbEmployee.Value;
            data.EmployeeName = this.pbEmployee.Text;

            data.BindIPAddr = this.txtBindIPAddr.Text.Trim();

            if (this.presenter.UpdateLimitBindIPAddr(data))
                this.SaveData();
		}
		#endregion

		#region 重载。
		public override void LoadData()
		{
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<SysMgrLimitBindIPAddr>>(delegate(object sender, EntityEventArgs<SysMgrLimitBindIPAddr> e)
            {
                if (e.Entity != null)
                {
                    this.pbEmployee.Value = e.Entity.EmployeeID;
                    this.pbEmployee.Text = e.Entity.EmployeeName;
                    this.pbEmployee.Enabled = false;

                    this.txtBindIPAddr.Text = e.Entity.BindIPAddr;
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion
        
        #region ISysMgrLimitBindIPAddrEditView 成员

        public GUIDEx BindID
        {
            get { return this.RequestGUIEx("BindID"); }
        }

        public void ShowMessage(string msg)
        {
            this.errMsg.Message = msg;
        }

        #endregion
    }

}
