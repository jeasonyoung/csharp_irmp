//================================================================================
// FileName: frmSysMgrWebPartEdit.aspx.cs
// Desc:
// Called by
// Auth: �������ɴ����������Զ����ɡ�
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
    public partial class frmSysMgrRegWebPartTemplatePicker : ModuleBasePage, ISysMgrRegWebPartTemplatePickerView
	{
		#region ��Ա���������캯����
        SysMgrRegWebPartTemplatePresenter presenter = null;
		///<summary>
		///���캯����
		///</summary>
        public frmSysMgrRegWebPartTemplatePicker()
		{
			this.presenter = new SysMgrRegWebPartTemplatePresenter(this);

		}
		#endregion

		#region �¼�����
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

        #region ISysMgrRegWebPartTemplateePicker ��Ա

        public GUIDEx WebPartTemplateID
        {
            get { return this.RequestGUIEx("Value"); }
        }

        public string WebPartTemplateName
        {
            get { return this.txtWebPartTemplateName.Text.Trim();}
        }

        public void BindWebPartTemplate(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.listSingleSelect, data);
            if (this.WebPartTemplateID.IsValid)
                this.listSingleSelect.SelectedValue = this.WebPartTemplateID;
        }

        public void ShowMessage(string message)
        {
            this.errMessage.Message = message;
        }
        #endregion
    }

}
