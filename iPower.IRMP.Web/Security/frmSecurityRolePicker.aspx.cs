//================================================================================
//  FileName: frmSecurityRolePicker.aspx.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/4/6
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
//  ----    ------  -----------------
//
//================================================================================
//  Copyright (C) 2004-2009 Jeason Young Corporation
//================================================================================
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using iPower;
using iPower.Web.Utility;
using iPower.Platform.Engine.DataSource;
using iPower.IRMP.Security.Engine.Service;
namespace iPower.IRMP.Security.Web
{

    public partial class frmSecurityRolePicker : ModuleBasePage, ISecurityRolePickerView
    {
        #region 成员变量，构造函数。
        SecurityRolePresenter presenter = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public frmSecurityRolePicker()
        {
            this.presenter = new SecurityRolePresenter(this);
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.presenter.InitializeComponent();
                //this.lbTitle.Text = this.CurrentModuleTitle;
            }
        }

        protected void btnRoleSearch_OnClick(object sender, EventArgs e)
        {
            this.presenter.PickerSeach();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string[] text = new string[0], values = new string[0];
            ListBoxHelper.GetSelected(this.listRoleSingleSelect, out text, out values);
            this.SaveData(string.Join(",", text), string.Join(",", values));
        }

        #region ISecurityRolePickerView 成员

        public string RoleName
        {
            get
            {
                return this.txtRoleName.Text.Trim();
            }
            set
            {
                this.txtRoleName.Text = value;
            }
        }

        public GUIDEx RoleID
        {
            get { return this.RequestGUIEx("Value"); }
        }

        public void BindRole(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.listRoleSingleSelect, data);
            if (this.RoleID.IsValid)
                this.listRoleSingleSelect.SelectedValue = this.RoleID;
        }

        #endregion
    }
}
