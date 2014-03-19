//================================================================================
// FileName: frmSysMgrLimitRefusedIPAddrEdit.aspx.cs
// Desc:
// Called by
// Auth: �������ɴ����������Զ����ɡ�
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
	///frmSysMgrLimitRefusedIPAddrEdit�б�ҳ���̨���롣
	///</summary>
	public partial class frmSysMgrLimitRefusedIPAddrEdit:ModuleBasePage,ISysMgrLimitRefusedIPAddrEditView
	{
		#region ��Ա���������캯����
		SysMgrLimitRefusedIPAddrPresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmSysMgrLimitRefusedIPAddrEdit()
		{
			this.presenter = new SysMgrLimitRefusedIPAddrPresenter(this);

		}
		#endregion

		#region �¼�����
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
                this.presenter.InitializeComponent();
        }
			
		protected void btnSave_Click(object sender, EventArgs e)
		{
            SysMgrLimitRefusedIPAddr data = new SysMgrLimitRefusedIPAddr();
            data.RefusedID = this.RefusedID.IsValid ? this.RefusedID : GUIDEx.New;
            data.EmployeeID = this.pbEmployee.Value;
            data.EmployeeName = this.pbEmployee.Text;
            data.RefusedIPAddr = this.txtRefusedIPAddr.Text.Trim();

            if (this.presenter.UpdateLimitRefusedIPAddr(data))
                this.SaveData();
		}
		#endregion

		#region ���ء�
		public override void LoadData()
		{
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<SysMgrLimitRefusedIPAddr>>(delegate(object sender, EntityEventArgs<SysMgrLimitRefusedIPAddr> e)
            {
                if (e.Entity != null)
                {
                    this.pbEmployee.Value = e.Entity.EmployeeID;
                    this.pbEmployee.Text = e.Entity.EmployeeName;

                    this.pbEmployee.Enabled = false;

                    this.txtRefusedIPAddr.Text = e.Entity.RefusedIPAddr;
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion
        
        #region ISysMgrLimitRefusedIPAddrEditView ��Ա

        public GUIDEx RefusedID
        {
            get { return this.RequestGUIEx("RefusedID"); }
        }

        public void ShowMessage(string msg)
        {
            this.errMsg.Message = msg;
        }

        #endregion
    }

}
