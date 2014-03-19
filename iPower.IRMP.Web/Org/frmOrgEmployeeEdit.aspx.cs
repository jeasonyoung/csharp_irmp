//================================================================================
// FileName: frmOrgEmployeeEdit.aspx.cs
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
	///frmOrgEmployeeEdit列表页面后台代码。
	///</summary>
    public partial class frmOrgEmployeeEdit : ModuleBasePage, IOrgEmployeeEditView
    {
        #region 成员变量，构造函数。
        OrgEmployeePresenter presenter = null;
        ///<summary>
        ///构造函数。
        ///</summary>
        public frmOrgEmployeeEdit()
        {
            this.presenter = new OrgEmployeePresenter(this);

        }
        #endregion

        #region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
                this.presenter.InitializeComponent();
        }

        protected void ddlDepartmentID_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string strDepartment = this.ddlDepartmentID.SelectedValue;
            if (strDepartment.IndexOf('-') > 0)
                strDepartment = strDepartment.Split('-')[0];
            this.presenter.ChangeDepartmentToPost(strDepartment);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Domain.OrgEmployee data = new Domain.OrgEmployee();
            data.EmployeeID = this.EmployeeID.IsValid ? this.EmployeeID : GUIDEx.New;
            data.EmployeeSign = this.txtEmployeeSign.Text.Trim();
            data.EmployeeName = this.txtEmployeeName.Text.Trim();
            data.NickName = this.txtNickName.Text;
            data.DepartmentID = this.ddlDepartmentID.SelectedValue;
            data.EmployeePassword = this.txtEmployeePassword.Text.Trim();
            data.PostID = this.ddlPostID.SelectedValue;
            //data.EmployeePassword2 = this.txtEmployeePassword2.Text.Trim();

            data.Gender = int.Parse(this.ddlGender.SelectedValue);
            data.Birthday = this.txtBirthday.Text.Trim();
            data.IdentityCard = this.txtIdentityCard.Text.Trim();
            data.CardID = this.txtCardID.Text.Trim();
            data.Nation = this.txtNation.Text.Trim();

            data.WorkTelNo = this.txtWorkTelNo.Text.Trim();
            data.MobileNo = this.txtMobileNo.Text.Trim();
            data.MSNNO = this.txtMSNNO.Text.Trim();
            data.QQNO = this.txtQQNO.Text.Trim();
            data.Email = this.txtEmail.Text.Trim();
            data.Address = this.txtAddress.Text.Trim();

            data.EmployeeStatus = int.Parse(this.ddlEmployeeStatus.SelectedValue);
            data.OrderNo = int.Parse(this.txtOrderNo.Text);
            data.EmployeeDescription = this.txtEmployeeDescription.Text.Trim();

            data.EmployeeEx1 = this.txtEmployeeEx1.Text.Trim();
            data.EmployeeEx2 = this.txtEmployeeEx2.Text.Trim();
            data.EmployeeEx3 = this.txtEmployeeEx3.Text.Trim();
            data.EmployeeEx4 = this.txtEmployeeEx4.Text.Trim();

            if (this.presenter.UpdateEmployee(data))
                base.SaveData();
        }
        #endregion

        #region 重载。
        public override void LoadData()
        {
            string strDepartmentPost = this.DepartmentID;
            string strDepartmentID = strDepartmentPost;
            if (!string.IsNullOrEmpty(strDepartmentPost) && strDepartmentPost.IndexOf('-') > 0)
                strDepartmentID = strDepartmentPost.Split('-')[0];
            this.ddlDepartmentID.SelectedValue = strDepartmentID;

            this.ddlDepartmentID_OnSelectedIndexChanged(null, EventArgs.Empty);
            if (!string.IsNullOrEmpty(strDepartmentPost) && strDepartmentPost.IndexOf('-') > 0)
                this.ddlPostID.SelectedValue = strDepartmentPost.Split('-')[1];
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<Domain.OrgEmployee>>(delegate(object sender, EntityEventArgs<Domain.OrgEmployee> e)
            {
                if (e.Entity != null)
                {
                    this.txtEmployeeSign.Text = e.Entity.EmployeeSign;
                    this.txtEmployeeName.Text = e.Entity.EmployeeName;
                    this.txtNickName.Text = e.Entity.NickName;
                    this.ddlDepartmentID.SelectedValue = e.Entity.DepartmentID;

                    this.txtEmployeePassword.Text = e.Entity.EmployeePassword;
                    this.ddlPostID.SelectedValue = e.Entity.PostID;
                   // this.txtEmployeePassword2.Text = e.Entity.EmployeePassword2;

                    this.ddlGender.SelectedValue = e.Entity.Gender.ToString();
                    this.txtBirthday.Text = e.Entity.Birthday;
                    this.txtIdentityCard.Text = e.Entity.IdentityCard;
                    this.txtCardID.Text = e.Entity.CardID;
                    this.txtNation.Text = e.Entity.Nation;

                    this.txtWorkTelNo.Text = e.Entity.WorkTelNo;
                    this.txtMobileNo.Text = e.Entity.MobileNo;
                    this.txtMSNNO.Text = e.Entity.MSNNO;
                    this.txtQQNO.Text = e.Entity.QQNO;
                    this.txtEmail.Text = e.Entity.Email;
                    this.txtAddress.Text = e.Entity.Address;

                    this.ddlEmployeeStatus.SelectedValue = e.Entity.EmployeeStatus.ToString();
                    this.txtOrderNo.Text = e.Entity.OrderNo.ToString();
                    this.txtEmployeeDescription.Text = e.Entity.EmployeeDescription;

                    this.txtEmployeeEx1.Text = e.Entity.EmployeeEx1;
                    this.txtEmployeeEx2.Text = e.Entity.EmployeeEx2;
                    this.txtEmployeeEx3.Text = e.Entity.EmployeeEx3;
                    this.txtEmployeeEx4.Text = e.Entity.EmployeeEx4;
                }
            }));
        }

        public override bool DeleteData()
        {
            return false;

        }
        #endregion
        
        #region IOrgEmployeeView 成员

        public void BindDepartment(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlDepartmentID, data);
        }

        #endregion

        #region IOrgEmployeeEditView 成员

        public GUIDEx EmployeeID
        {
            get { return this.RequestGUIEx("EmployeeID"); }
        }

        public GUIDEx DepartmentID
        {
            get
            {
                return this.RequestGUIEx("DepartmentID");
            }
        }

        public void BindSex(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlGender, data);
        }

        public void BindEmployeeStatus(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlEmployeeStatus, data);
        }

        public void BindPost(iPower.Platform.Engine.DataSource.IListControlsTreeViewData data)
        {
            this.ListControlsDataSourceBind(this.ddlPostID, data);
        }

        #endregion
    }
}
