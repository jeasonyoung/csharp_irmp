//================================================================================
// FileName: frmSysMgrLimitSpecifyTimeZoneEdit.aspx.cs
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
	///frmSysMgrLimitSpecifyTimeZoneEdit�б�ҳ���̨���롣
	///</summary>
	public partial class frmSysMgrLimitSpecifyTimeZoneEdit:ModuleBasePage,ISysMgrLimitSpecifyTimeZoneEditView
	{
		#region ��Ա���������캯����
		SysMgrLimitSpecifyTimeZonePresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmSysMgrLimitSpecifyTimeZoneEdit()
		{
			this.presenter = new SysMgrLimitSpecifyTimeZonePresenter(this);

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
            try
            {
                SysMgrLimitSpecifyTimeZone data = new SysMgrLimitSpecifyTimeZone();
                data.ZoneID = this.ZoneID.IsValid ? this.ZoneID : GUIDEx.New;

                data.EmployeeID = this.pbEmployee.Value;
                data.EmployeeName = this.pbEmployee.Text;

                data.StartTime = DateTime.Parse(this.txtStartTime.Text);
                data.EndTime = DateTime.Parse(this.txtEndTime.Text);

                data.AuthStatus = int.Parse(this.rdAuthStatus.SelectedValue);

                if (this.presenter.UpdateLimitSpecifyTimeZone(data))
                    this.SaveData();
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
            }
		}
		#endregion

		#region ���ء�
		public override void LoadData()
		{
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<SysMgrLimitSpecifyTimeZone>>(delegate(object sender, EntityEventArgs<SysMgrLimitSpecifyTimeZone> e)
            {
                if (e.Entity != null)
                {
                    this.pbEmployee.Value = e.Entity.EmployeeID;
                    this.pbEmployee.Text = e.Entity.EmployeeName;
                    this.pbEmployee.Enabled = false;

                    this.txtStartTime.Text = string.Format("{0:yyyy-MM-dd}", e.Entity.StartTime);
                    this.txtEndTime.Text = string.Format("{0:yyyy-MM-dd}", e.Entity.EndTime);

                    this.rdAuthStatus.SelectedValue = e.Entity.AuthStatus.ToString();
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion
        
        #region ISysMgrLimitSpecifyTimeZoneEditView ��Ա

        public GUIDEx ZoneID
        {
            get { return this.RequestGUIEx("ZoneID"); }
        }

        public void BindAuthStatus(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.rdAuthStatus, data);
        }

        public void ShowMessage(string msg)
        {
            this.errMsg.Message = msg;
        }

        #endregion
    }

}
