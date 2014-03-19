//================================================================================
// FileName: frmOrgDepartmentEdit.aspx.cs
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
	///frmOrgDepartmentEdit列表页面后台代码。
	///</summary>
    public partial class frmOrgDepartmentEdit : ModuleBasePage, IOrgDepartmentEditView
    {
        #region 成员变量，构造函数。
        OrgDepartmentPresenter presenter = null;
        ///<summary>
        ///构造函数。
        ///</summary>
        public frmOrgDepartmentEdit()
        {
            this.presenter = new OrgDepartmentPresenter(this);

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
                Domain.OrgDepartment data = new Domain.OrgDepartment();
                data.ParentDepartmentID = this.ddlParentDepartmentID.SelectedValue;
                data.DepartmentID = this.DepartmentID.IsValid ? this.DepartmentID : GUIDEx.New;
                data.DepartmentName = this.txtDepartmentName.Text;
                data.DepartmentSign = this.txtDepartmentSign.Text;
                data.DepartmentOrder = int.Parse(this.txtDepartmentOrder.Text);
                data.DepartmentDescription = this.txtDepartmentDescription.Text;

                data.DepartmentTel = this.txtDepartmentTel.Text;
                data.DepartmentFax = this.txtDepartmentFax.Text;
                data.DepartmentAddress = this.txtDepartmentAddress.Text;
                data.DepartmentLeader = this.txtDepartmentLeader.Text;
                data.DepartmentCapability = int.Parse(this.txtDepartmentCapability.Text);
                data.DepartmentLevel = int.Parse(this.txtDepartmentLevel.Text);
                data.DepartmentStatus = int.Parse(this.ddlDepartmentStatus.SelectedValue);

                data.DepartmentEx1 = this.txtDepartmentEx1.Text;
                data.DepartmentEx2 = this.txtDepartmentEx2.Text;
                data.DepartmentEx3 = this.txtDepartmentEx3.Text;
                data.DepartmentEx4 = this.txtDepartmentEx4.Text;

                if (this.presenter.SaveEntityData(data))
                    base.SaveData();
            }
            catch (Exception x)
            {
                this.ShowMessage(x.Message);
            }
        }
        #endregion

        #region 重载。
        public override void LoadData()
        {
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<Domain.OrgDepartment>>(delegate(object sender, EntityEventArgs<Domain.OrgDepartment> e)
            {
                if (e.Entity != null)
                {
                    this.ddlParentDepartmentID.SelectedValue = e.Entity.ParentDepartmentID;

                    this.txtDepartmentName.Text = e.Entity.DepartmentName;
                    this.txtDepartmentSign.Text = e.Entity.DepartmentSign;
                    this.txtDepartmentOrder.Text = e.Entity.DepartmentOrder.ToString();
                    this.txtDepartmentDescription.Text = e.Entity.DepartmentDescription;
                    this.txtDepartmentTel.Text = e.Entity.DepartmentTel;
                    this.txtDepartmentFax.Text = e.Entity.DepartmentFax;
                    this.txtDepartmentLeader.Text = e.Entity.DepartmentLeader;
                    this.txtDepartmentCapability.Text = e.Entity.DepartmentCapability.ToString();
                    this.txtDepartmentLevel.Text = e.Entity.DepartmentLevel.ToString();
                    this.ddlDepartmentStatus.SelectedValue = e.Entity.DepartmentStatus.ToString();
                    this.txtDepartmentEx1.Text = e.Entity.DepartmentEx1;
                    this.txtDepartmentEx2.Text = e.Entity.DepartmentEx2;
                    this.txtDepartmentEx3.Text = e.Entity.DepartmentEx3;
                    this.txtDepartmentEx4.Text = e.Entity.DepartmentEx4;
                }
            }));
        }

        public override bool DeleteData()
        {
            return false;

        }
        #endregion

        #region IOrgDepartmentView 成员

        public void ShowMessage(string content)
        {
            this.errMsg.Message = content;
            this.errMsg.Alert = !string.IsNullOrEmpty(content);
        }

        #endregion

        #region IOrgDepartmentEditView 成员

        public GUIDEx DepartmentID
        {
            get { return this.RequestGUIEx("DepartmentID"); }
        }

        public void BindDepartmentStatus(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlDepartmentStatus, data);
        }

        #endregion

        #region IOrgDepartmentView 成员


        public void BindParentDepartment(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlParentDepartmentID, data);
        }

        #endregion
    }

}
