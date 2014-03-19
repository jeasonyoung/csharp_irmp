//================================================================================
// FileName: frmResourcesEdit.aspx.cs
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
using iPower.Resources;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.IRMP.SysMgr.Engine.Domain;
using iPower.IRMP.SysMgr.Engine.Service;
namespace iPower.IRMP.SysMgr.Web
{
	///<summary>
	///frmResourcesEdit列表页面后台代码。
	///</summary>
    public partial class frmResourcesEdit : ModuleBasePage, IResourcesEditView
    {
        #region 成员变量，构造函数。
        ResourcesPresenter presenter = null;
        ///<summary>
        ///构造函数。
        ///</summary>
        public frmResourcesEdit()
        {
            this.presenter = new ResourcesPresenter(this);

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
                Resource data = new Resource();
                data.ResKey = this.txtResKey.Text.Trim();
                data.ResValue = this.txtResValue.Text.Trim();
                data.Description = this.txtDescription.Text.Trim();

                if (this.presenter.UpdateResources(data))
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
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<Resource>>(delegate(object sender, EntityEventArgs<Resource> e)
            {
                if (e.Entity != null)
                {
                    this.txtResKey.Text = e.Entity.ResKey;
                    this.txtResValue.Text = e.Entity.ResValue;
                    this.txtDescription.Text = e.Entity.Description;
                }
            }));
        }

        public override bool DeleteData()
        {
            return false;

        }
        #endregion
        
        #region IResourcesEditView 成员

        public string ResKey
        {
            get { return this.Request["ResKey"]; }
        }

        #endregion

        #region IResourcesView 成员

        public void ShowMessage(string message)
        {
            this.errMsg.Message = message;
        }

        #endregion
    }

}
