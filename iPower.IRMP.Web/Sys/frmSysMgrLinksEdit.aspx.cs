//================================================================================
// FileName: frmSysMgrLinksEdit.aspx.cs
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
	///frmSysMgrLinksEdit列表页面后台代码。
	///</summary>
    public partial class frmSysMgrLinksEdit : ModuleBasePage, ISysMgrLinksEditview
	{
		#region 成员变量，构造函数。
		SysMgrLinksPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmSysMgrLinksEdit()
		{
			this.presenter = new SysMgrLinksPresenter(this);

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
                SysMgrLinks data = new SysMgrLinks();
                data.LinkID = this.LinkID.IsValid ? this.LinkID : GUIDEx.New;
                data.LinkName = this.txtLinkName.Text;
                data.LinkUrl = this.txtLinkUrl.Text;
                data.LinkTarget = int.Parse(this.ddlLinkTarget.SelectedValue);
                data.LinkStatus = int.Parse(this.ddlLinkStatus.SelectedValue);
                data.EmployeeID = this.pbEmployee.Value;
                data.EmployeeName = this.pbEmployee.Text;
                data.Description = this.txtDescription.Text;
                data.OrderNo = int.Parse(this.txtOrderNo.Text);
                if (this.presenter.UpdateSysMgrLinks(data))
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
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<SysMgrLinks>>(delegate(object sender, EntityEventArgs<SysMgrLinks> e)
            {
                if (e.Entity != null)
                {
                    this.txtLinkName.Text = e.Entity.LinkName;
                    this.pbEmployee.Value = e.Entity.EmployeeID;
                    this.pbEmployee.Text = e.Entity.EmployeeName;
                    this.txtLinkUrl.Text = e.Entity.LinkUrl;
                    this.ddlLinkTarget.SelectedValue = e.Entity.LinkTarget.ToString();
                    this.ddlLinkStatus.SelectedValue = e.Entity.LinkStatus.ToString();
                    this.txtDescription.Text = e.Entity.Description;
                    this.txtOrderNo.Text = e.Entity.OrderNo.ToString();
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion

        #region ISysMgrLinksEditview 成员

        public void BindLinkTarget(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlLinkTarget, data);
        }

        public void BindLinkStatus(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlLinkStatus, data);
        }

        public GUIDEx LinkID
        {
            get { return this.RequestGUIEx("LinkID"); }
        }

        public void ShowMessage(string message)
        {
            this.errMessage.Message = message;
        }

        #endregion
    }

}
