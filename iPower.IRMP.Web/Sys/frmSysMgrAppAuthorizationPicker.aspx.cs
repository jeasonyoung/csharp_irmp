//================================================================================
// FileName: frmSysMgrWebPartZoneList.aspx.cs
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
using iPower.Web.Utility;
using iPower.IRMP.SysMgr.Engine.Service;
namespace iPower.IRMP.SysMgr.Web
{
	///<summary>
	///
	///</summary>
    public partial class frmSysMgrAppAuthorizationPicker : ModuleBasePage, ISysMgrAppAuthorizationPickerView
	{
		#region 成员变量，构造函数。
        SysMgrAppAuthorizationPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
        public frmSysMgrAppAuthorizationPicker()
		{
            this.presenter = new SysMgrAppAuthorizationPresenter(this);

		}
		#endregion

		#region 事件处理。
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
                this.presenter.InitializeComponent();

			}

		}
        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            this.presenter.PickerSearch();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string[] text = new string[0], values = new string[0];
            ListBoxHelper.GetSelected(this.listSingleSelect, out text, out values);
            this.SaveData(string.Join(",", text), string.Join(",", values));
        }
		#endregion

        #region ISysMgrAppAuthorizationPickerView 成员

        public GUIDEx AppID
        {
            get { return this.RequestGUIEx("Value"); }
        }

        public string AppName
        {
            get { return this.txtAppName.Text.Trim(); }
        }

        public bool IsLocal
        {
            get
            {
                try
                {
                    string strIsLocal = this.Request["IsLocal"];
                    if (string.IsNullOrEmpty(strIsLocal))
                        return true;
                    return Convert.ToBoolean(strIsLocal);
                }
                catch (Exception) { }
                return true;
            }
        }

        public void BindApp(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.listSingleSelect, data);
            if (this.AppID.IsValid)
                this.listSingleSelect.SelectedValue = this.AppID;
        }

        #endregion
    }

}
