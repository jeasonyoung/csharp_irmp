//================================================================================
//  FileName: frmOrgEmployeePicker.aspx.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/3/21
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

using iPower.Web.Utility;
using iPower.Platform.Engine.DataSource;
using iPower.IRMP.Org.Engine.Service;

namespace iPower.IRMP.Org.Web
{
    public partial class frmOrgEmployeePicker : ModuleBasePage, IEmployeePicker
    {
        #region 成员变量，构造函数。
        OrgEmployeePickerPresenter presenter = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public frmOrgEmployeePicker()
        {
            this.presenter = new OrgEmployeePickerPresenter(this);
        }
        #endregion

        #region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                this.presenter.InitializeComponent();
        }

        protected void btnEmployeSearch_Click(object sender, EventArgs e)
        {
            this.presenter.SearchEmployee();
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

        #region IEmployeePicker 成员

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
        public string Gender
        {
            get { return this.ddlSex.SelectedValue; }
        }


        public void BindGender(IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlSex, data);
        }

        public string DepartmentName
        {
            get { return this.txtDepartmentName.Text.Trim(); }
        }

        public string EmployeeName
        {
            get { return this.txtKey.Text.Trim(); }
        }

        public void DisplayEmployeePanel(IListControlsData data)
        {
            if (this.MultiSelect)
            {
                this.lbEmployeeMulti.Items.Clear();
                this.ListControlsDataSourceBind(this.lbEmployeeSelect, data);
            }
            else
                this.ListControlsDataSourceBind(this.lbEmployeeSelect, data);
        }

        public void SearchEmployeeResult(IListControlsData data)
        {
            if (this.MultiSelect)
            {
                this.lbEmployeeSelect.Items.Clear();
                this.ListControlsDataSourceBind(this.lbEmployeeMulti, data);
            }
            else
                this.ListControlsDataSourceBind(this.lbEmployeeSingleSelect, data);
        }
        #endregion
    }
}
