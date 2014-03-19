//================================================================================
// FileName: frmSysMgrSettingEdit.aspx.cs
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
	///frmSysMgrSettingEdit列表页面后台代码。
	///</summary>
    public partial class frmSysMgrSettingEdit : ModuleBasePage, ISysMgrSettingEditView
	{
		#region 成员变量，构造函数。
		SysMgrSettingPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmSysMgrSettingEdit()
		{
			this.presenter = new SysMgrSettingPresenter(this);

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
                SysMgrSetting data = new SysMgrSetting();
                data.SettingID = this.SettingID.IsValid ? this.SettingID : GUIDEx.New;
                data.SettingSign = this.txtSettingSign.Text.Trim();
                data.SettingType = int.Parse(this.ddlSettingType.SelectedValue);
                data.DefaultValue = this.txtDefaultValue.Text.Trim();
                data.Description = this.txtDescription.Text.Trim();
                data.AppAuthID = this.pbAppSystem.Value;
                if (this.presenter.UpdateSysMgrSetting(data))
                    this.SaveData();
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
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<SysMgrSetting>>(delegate(object sender, EntityEventArgs<SysMgrSetting> e)
            {
                if (e.Entity != null)
                {
                    this.ddlSettingType.SelectedValue = e.Entity.SettingType.ToString();
                    this.txtSettingSign.Text = e.Entity.SettingSign;
                    this.txtDefaultValue.Text = e.Entity.DefaultValue;
                    this.txtDescription.Text = e.Entity.Description;
                    this.pbAppSystem.Value = e.Entity.AppAuthID;
                    this.pbAppSystem.Text = e.Entity.SystemName;
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion

        #region ISysMgrSettingEditView 成员

        public GUIDEx SettingID
        {
            get { return this.RequestGUIEx("SettingID"); }
        }

        public string SystemName
        {
            get { return this.pbAppSystem.Text.Trim(); }
        }

        public void BindSettingType(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlSettingType, data);
        }

        public void ShowMessage(string message)
        {
            this.errMessage.Message = message;
        }
        #endregion
    }

}
