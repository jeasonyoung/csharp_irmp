//================================================================================
//  FileName: frmSecurityEmployeePicker.aspx.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/4/12
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

using iPower;
using iPower.Web.Utility;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.IRMP.Security.Engine.Service;
namespace iPower.IRMP.Security.Web
{
    public partial class frmSecurityEmployeePicker : ModuleBasePage, ISecurityRoleEmployeePickerView
    {
        #region 成员变量，构造函数。
		SecurityRoleEmployeePresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
        public frmSecurityEmployeePicker()
		{
			this.presenter = new SecurityRoleEmployeePresenter(this);

		}
		#endregion

        #region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
                this.presenter.InitializeComponent();
        }

        protected void ListBox_OnPreRender(object sender, EventArgs e)
        {
            ListBox obj = sender as ListBox;
            if (obj != null)
            {
                foreach (ListItem item in obj.Items)
                    item.Attributes.Add("title", item.Text);
            }
        }

        protected void btnEmployeSearch_Click(object sender, EventArgs e)
        {
            this.presenter.SeachEmployee();
        }

        protected void btnEmployeeSelectAll_OnClick(object sender, EventArgs e)
        {
            ListBoxHelper.AddAll(this.lbEmployeeMulti, this.lbEmployeeSelect);
        }

        protected void btnEmployeeSelect_OnClick(object sender, EventArgs e)
        {
            ListBoxHelper.AddSelected(this.lbEmployeeMulti, this.lbEmployeeSelect);
        }

        protected void btnEmployeeRemove_OnClick(object sender, EventArgs e)
        {
            ListBoxHelper.RemoveSelected(this.lbEmployeeMulti, this.lbEmployeeSelect);
        }

        protected void btnEmployeeRemoveAll_OnClick(object sender, EventArgs e)
        {
            ListBoxHelper.RemoveAll(this.lbEmployeeMulti, this.lbEmployeeSelect);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string[] text = new string[0], values = new string[0];
            if (this.MultiSelect)
                ListBoxHelper.GetAll(this.lbEmployeeSelect, out text, out values);
            else
                ListBoxHelper.GetSelected(this.lbEmployeeSingleSelect, out text, out values);
            this.SaveData(string.Join(",", text), string.Join(",", values));
        }

        #endregion

        #region 重载。
        public override void LoadData()
        {
            this.EmployeePanelMultiSelect.Visible = this.MultiSelect;
            this.EmployeePanelSelect.Visible = !this.MultiSelect;
        }
        public override bool DeleteData()
        {
            return false;

        }
        #endregion

        #region ISecurityRoleEmployeePickerView 成员

        public bool MultiSelect
        {
            get
            {
                string strMultiSelect = this.Request["MultiSelect"];
                return (!string.IsNullOrEmpty(strMultiSelect)) && Convert.ToBoolean(strMultiSelect);
            }
        }

        public string[] Values
        {
            get
            {
                string strValue = this.Request["Value"];
                if (!string.IsNullOrEmpty(strValue))
                    return strValue.Split(',');
                return null;
            }
        }

        public string DepartmentName
        {
            get { return this.txtDepartmentName.Text.Trim(); }
        }

        public string PostName
        {
            get { return this.txtPostName.Text.Trim(); }
        }

        public string EmployeeName
        {
            get { return this.txtEmployeeName.Text.Trim(); }
        }

        public void DisplayEmployeePanel(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            if (this.MultiSelect)
                this.ListControlsDataSourceBind(this.lbEmployeeSelect, data);
            else
            {
                this.ListControlsDataSourceBind(this.lbEmployeeSingleSelect, data);
                if (this.Values != null && this.Values.Length > 0)
                    this.lbEmployeeSingleSelect.SelectedValue = this.Values[0];
            }
        }

        public void SearchEmployeeResult(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            if (this.MultiSelect)
                this.ListControlsDataSourceBind(this.lbEmployeeMulti, data);
            else
            {
                this.ListControlsDataSourceBind(this.lbEmployeeSingleSelect, data);
                if (this.Values != null && this.Values.Length > 0)
                    this.lbEmployeeSingleSelect.SelectedValue = this.Values[0];
            }
        }
        #endregion
    }
}
