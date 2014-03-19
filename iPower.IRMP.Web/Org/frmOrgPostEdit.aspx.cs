//================================================================================
// FileName: frmOrgPostEdit.aspx.cs
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
using Domain = iPower.IRMP.Org.Engine.Domain;
using iPower.IRMP.Org.Engine.Service;
namespace iPower.IRMP.Org.Web
{
	///<summary>
	///frmOrgPostEdit列表页面后台代码。
	///</summary>
    public partial class frmOrgPostEdit : ModuleBasePage, IOrgPostEditView
    {
        #region 成员变量，构造函数。
        OrgPostPresenter presenter = null;
        ///<summary>
        ///构造函数。
        ///</summary>
        public frmOrgPostEdit()
        {
            this.presenter = new OrgPostPresenter(this);

        }
        #endregion

        #region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.presenter.InitializeComponent();

                if (this.DepartmentID.IsValid)
                    this.ddlDepartmentID.SelectedValue = this.DepartmentID;
            }

        }
        protected void ddlDepartmentID_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.presenter.ChangeDepartmentToParentPost(this.ddlDepartmentID.SelectedValue);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Domain.OrgPost data = new Domain.OrgPost();
            data.ParentPostID = this.ddlParentPostID.SelectedValue;
            data.PostID = this.PostID.IsValid ? this.PostID : GUIDEx.New;
            data.PostName = this.txtPostName.Text.Trim();
            data.PostSign = this.txtPostSign.Text.Trim();
            data.DepartmentID = this.ddlDepartmentID.SelectedValue;
            data.RankID = this.ddlRankID.SelectedValue;
            data.PostDescription = this.txtPostDescription.Text.Trim();

            if (this.presenter.UpdatePostData(data))
                base.SaveData();
        }
        #endregion

        #region 重载。
        public override void LoadData()
        {
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<Domain.OrgPost>>(delegate(object sender, EntityEventArgs<Domain.OrgPost> e)
            {
                if (e.Entity != null)
                {
                    this.ddlDepartmentID.SelectedValue = e.Entity.DepartmentID;
                    this.ddlParentPostID.SelectedValue = e.Entity.ParentPostID;
                    this.txtPostName.Text = e.Entity.PostName;
                    this.txtPostSign.Text = e.Entity.PostSign;
                    this.ddlRankID.SelectedValue = e.Entity.RankID;
                    this.txtPostDescription.Text = e.Entity.PostDescription;
                }
            }));
        }

        public override bool DeleteData()
        {
            return false;

        }
        #endregion
        
        #region IOrgPostEditView 成员

        public GUIDEx PostID
        {
            get { return this.RequestGUIEx("PostID"); }
        }

        public GUIDEx DepartmentID
        {
            get { return this.RequestGUIEx("DepartmentID"); }
        }

        #endregion

        #region IOrgPostView 成员

        public void BindDepartment(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlDepartmentID, data);
        }

        public void BindRank(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlRankID, data);
        }

        public void BindParentPost(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlParentPostID, data);
        }

        #endregion
    }

}
