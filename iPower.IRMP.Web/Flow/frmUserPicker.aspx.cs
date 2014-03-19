//================================================================================
//  FileName:frmUserPicker.aspx.cs
//  Desc:
//
//  Called by
//
//  Auth:iPowerYoung
//  Date:2010-12-15 10:28:05
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
// ----  ------  -----------
//
//================================================================================
//  Copyright (C) 2009-2010 iPower Young Corporation
//================================================================================

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using iPower.Web.Utility;
using iPower.Platform.Engine.DataSource;

using iPower.IRMP.Flow.Engine.Service;
namespace iPower.IRMP.Flow.Web
{
    public partial class frmUserPicker : ModuleBasePage, IUserPickerView
    {
        #region 成员变量，构造函数。
        UserPickerPresenter presenter = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public frmUserPicker()
        {
            this.presenter = new UserPickerPresenter(this);
        }
        #endregion

        #region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                this.presenter.InitializeComponent();
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
            List<string> listText = new List<string>(), listValue = new List<string>();
            switch (this.PickerType)
            {
                case EnumUserPickerType.Employee:
                    if (this.MultiSelect)
                    {
                        foreach (ListItem item in this.lbEmployeeSelect.Items)
                        {
                            listText.Add(item.Text);
                            listValue.Add(item.Value);
                        }
                    }
                    else
                    {
                        foreach (ListItem item in this.lbEmployeeSingleSelect.Items)
                        {
                            if (item.Selected)
                            {
                                listText.Add(item.Text);
                                listValue.Add(item.Value);
                            }
                        }
                    }
                    break;
                case EnumUserPickerType.Role:
                    foreach (ListItem item in this.chkRole.Items)
                    {
                        if (item.Selected)
                        {
                            listText.Add(item.Text);
                            listValue.Add(item.Value);
                        }
                    }
                    break;
                case EnumUserPickerType.Rank:
                    foreach (ListItem item in this.chkRank.Items)
                    {
                        if (item.Selected)
                        {
                            listText.Add(item.Text);
                            listValue.Add(item.Value);
                        }
                    }
                    break;
                case EnumUserPickerType.Post:
                    foreach (ListItem item in this.chkPost.Items)
                    {
                        if (item.Selected)
                        {
                            listText.Add(item.Text);
                            listValue.Add(item.Value);
                        }
                    }
                    break;
            }

            if (listText.Count > 0 && listValue.Count > 0)
            {
                string[] resultText = new string[listText.Count];
                string[] resultValue = new string[listValue.Count];
                
                listText.CopyTo(resultText, 0);
                listValue.CopyTo(resultValue, 0);

                base.SaveData(string.Join(",", resultText), string.Join(",", resultValue));
            }
        }
        
        protected void btnEmployeSearch_Click(object sender, EventArgs e)
        {
            this.presenter.SearchEmployee();
        }
        #endregion

        #region IUserPickerView 成员
        public string[] Values
        {
            get
            {
                string str = this.Request["Value"];
                if (string.IsNullOrEmpty(str))
                    return null;
                return str.Split(',');
            }
        }
        public bool MultiSelect
        {
            get
            {
                string s =this.Request["MultiSelect"];
                if (!string.IsNullOrEmpty(s))
                {
                    return bool.Parse(s);
                }
                return false;
            }
        }
        public EnumUserPickerType PickerType
        {
            get
            {
                string str = this.Request["t"];
                if (string.IsNullOrEmpty(str))
                    return EnumUserPickerType.Employee;
                return (EnumUserPickerType)Enum.Parse(typeof(EnumUserPickerType), str);
            }
        }

        public string DepartmentName
        {
            get { return this.txtDepartmentName.Text; }
        }

        public string EmployeeName
        {
            get { return this.txtKey.Text; }
        }

        public string EmployeeSexName
        {
            get { return this.ddlSex.SelectedValue; }
        }

        public void DisplayEmployeePanel(IListControlsData data)
        {
            if (this.PickerType == EnumUserPickerType.Employee)
            {
                if (this.MultiSelect)
                    this.ListControlsDataSourceBind(this.lbEmployeeMulti, data);
                else
                    this.ListControlsDataSourceBind(this.lbEmployeeSingleSelect, data);
            }
        }
        public void DisplayPanel(EnumUserPickerType type, IListControlsData data)
        {
            switch (type)
            {
                case EnumUserPickerType.Employee:
                    this.EmployeePanel.Visible = true;
                    base.CurrentModuleTitle = string.Format("选择用户({0})", this.MultiSelect ? "多选" : "单选");
                    if (this.MultiSelect)
                    {
                        this.EmployeePanelMultiSelect.Visible = true;
                        this.EmployeePanelSelect.Visible = false;
                        this.ListControlsDataSourceBind(this.lbEmployeeSelect, data);
                    }
                    else
                    {
                        this.EmployeePanelMultiSelect.Visible = false;
                        this.EmployeePanelSelect.Visible = true;
                        this.ListControlsDataSourceBind(this.lbEmployeeSingleSelect, data);
                    }
                    break;
                case EnumUserPickerType.Role:
                    this.RolePanel.Visible = true;
                    base.CurrentModuleTitle = "选择角色(多选)";
                    this.ListControlsDataSourceBind(this.chkRole, data);
                    this.SelectedCheckBoxData(ref this.chkRole, this.Values);
                    break;
                case EnumUserPickerType.Rank:
                    this.RankPanel.Visible = true;
                    base.CurrentModuleTitle = "选择岗位级别(多选)";
                    this.ListControlsDataSourceBind(this.chkRank, data);
                    this.SelectedCheckBoxData(ref this.chkRank, this.Values);
                    break;
                case EnumUserPickerType.Post:
                    this.PostPanel.Visible = true;
                    base.CurrentModuleTitle = "选择岗位(多选)";
                    this.ListControlsDataSourceBind(this.chkPost, data);
                    this.SelectedCheckBoxData(ref this.chkPost, this.Values);
                    break;
            }
        }
        #endregion

        #region 辅助函数。
        void SelectedCheckBoxData(ref CheckBoxList chkBoxList, string[] values)
        {
            if (values != null && chkBoxList.Items.Count > 0)
            {
                foreach (ListItem item in chkBoxList.Items)
                    item.Selected = (Array.BinarySearch<String>(values, item.Value) > -1);
            }
        }
        #endregion
    }
}
