//================================================================================
// FileName: frmSysMgrWebPartPersonalEdit.aspx.cs
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
	///frmSysMgrWebPartPersonalEdit列表页面后台代码。
	///</summary>
    public partial class frmSysMgrWebPartPersonalEdit : ModuleBasePage, ISysMgrWebPartPersonalEditView
	{
		#region 成员变量，构造函数。
		SysMgrWebPartPersonalPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmSysMgrWebPartPersonalEdit()
		{
			this.presenter = new SysMgrWebPartPersonalPresenter(this);

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
                SysMgrWebPartPersonal data = new SysMgrWebPartPersonal();
                data.PersonalWebPartID = this.PersonalWebPartID.IsValid ? this.PersonalWebPartID : GUIDEx.New;

                data.EmployeeID = this.pbEmployee.Value;
                data.EmployeeName = this.pbEmployee.Text;

                data.WebPartID = this.pbWebPart.Value;
                data.WebPartName = this.pbWebPart.Text;

                data.ZoneID = this.pbWebPartZone.Value;
                data.ZoneName = this.pbWebPartZone.Text;

                data.OrderNo = int.Parse(this.txtOrderNo.Text);
                if (this.presenter.UpdateSysMgrWebPartPersonal(data))
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
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<SysMgrWebPartPersonal>>(delegate(object sender, EntityEventArgs<SysMgrWebPartPersonal> e)
            {
                if (e.Entity != null)
                {
                    this.pbWebPart.Value = e.Entity.WebPartID;
                    this.pbWebPart.Text = e.Entity.WebPartName;

                    this.pbWebPartZone.Value = e.Entity.ZoneID;
                    this.pbWebPartZone.Text = e.Entity.ZoneName;

                    this.pbEmployee.Value = e.Entity.EmployeeID;
                    this.pbEmployee.Text = e.Entity.EmployeeName;

                    this.txtOrderNo.Text = e.Entity.OrderNo.ToString();
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion

        #region ISysMgrWebPartPersonalEditView 成员

        public GUIDEx PersonalWebPartID
        {
            get { return this.RequestGUIEx("PersonalWebPartID"); }
        }
        public void ShowMessage(string Msg)
        {
            this.errMessage.Message = Msg;
        }

        #endregion
    }

}
