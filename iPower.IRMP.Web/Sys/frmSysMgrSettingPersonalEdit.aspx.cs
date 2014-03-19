//================================================================================
// FileName: frmSysMgrSettingPersonalEdit.aspx.cs
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
	///frmSysMgrSettingPersonalEdit�б�ҳ���̨���롣
	///</summary>
    public partial class frmSysMgrSettingPersonalEdit : ModuleBasePage, ISysMgrSettingPersonalEditView
	{
		#region ��Ա���������캯����
		SysMgrSettingPersonalPresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
		public frmSysMgrSettingPersonalEdit()
		{
			this.presenter = new SysMgrSettingPersonalPresenter(this);

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
                SysMgrSettingPersonal data = new SysMgrSettingPersonal();
                data.PersonalSettingID = this.PersonalSettingID.IsValid ? this.PersonalSettingID : GUIDEx.New;
                data.EmployeeID = this.pbEmployee.Value;
                data.EmployeeName = this.pbEmployee.Text;

                data.SettingID = this.pbSetting.Value;
                data.SettingSign = this.pbSetting.Text;

                data.SettingValue = this.txtSettingValue.Text;
                if (this.presenter.UpdateSysMgrSettingPersonal(data))
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
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<SysMgrSettingPersonal>>(delegate(object sender, EntityEventArgs<SysMgrSettingPersonal> e)
            {
                if (e.Entity != null)
                {
                    this.pbSetting.Value = e.Entity.SettingID;
                    this.pbSetting.Text = e.Entity.SettingSign;

                    this.pbEmployee.Text = e.Entity.EmployeeName;
                    this.pbEmployee.Value = e.Entity.EmployeeID;

                    this.txtSettingValue.Text = e.Entity.SettingValue;
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion

        #region ISysMgrSettingPersonalEditView ��Ա

        public GUIDEx PersonalSettingID
        {
            get { return this.RequestGUIEx("PersonalSettingID"); }
        }

        public void ShowMessage(string Msg)
        {
            this.errMessage.Message=Msg;
        }

        #endregion
    }

}
