//================================================================================
// FileName: frmSysMgrWebPartEdit.aspx.cs
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
using iPower.Web.UI;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.IRMP.SysMgr.Engine.Domain;
using iPower.IRMP.SysMgr.Engine.Persistence;
using iPower.IRMP.SysMgr.Engine.Service;
namespace iPower.IRMP.SysMgr.Web
{
	///<summary>
	///frmSysMgrWebPartEdit列表页面后台代码。
	///</summary>
    public partial class frmSysMgrWebPartEdit : ModuleBasePage, ISysMgrWebPartEditView
	{
		#region 成员变量，构造函数。
		SysMgrWebPartPresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmSysMgrWebPartEdit()
		{
			this.presenter = new SysMgrWebPartPresenter(this);

		}
		#endregion

		#region 事件处理。
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
				this.presenter.InitializeComponent();
		}

        protected void pbWebPartTemplate_OnTextChanged(object sender, EventArgs e)
        {
            GUIDEx webPartTemplateID = new GUIDEx(this.pbWebPartTemplate.Value);
            if (webPartTemplateID.IsValid && this.presenter.LoadTemplateProperties(webPartTemplateID))
            {
                this.dgfrmSysMgrWebPartEdit.InvokeBuildDataSource();
            }
        }

        protected void txtPropertyValue_OnTextChanged(object sender, EventArgs e)
        {
            TextBoxEx tb = (sender as TextBoxEx);
            if (tb != null)
            {
                object parent = tb.Parent;
                if (parent != null)
                {
                    DataControlFieldCellEx fieldCellEx = parent as DataControlFieldCellEx;
                    if (fieldCellEx != null)
                    {
                        DataGridViewRow row = fieldCellEx.Parent as DataGridViewRow;
                        if (row != null)
                        {
                            List<WebPartProperty> dataSource = this.EditListDataSource;
                            if (dataSource != null && dataSource.Count > 0)
                            {
                                WebPartProperty p = dataSource[row.RowIndex];
                                if (p != null)
                                {
                                    p.PropertyValue = tb.Text;
                                    this.EditListDataSource = dataSource;
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void dgfrmSysMgrWebPartEdit_BuildDataSource(object sender, EventArgs e)
        {
            this.dgfrmSysMgrWebPartEdit.DataSource = this.EditListDataSource;
        }

		protected void btnSave_Click(object sender, EventArgs e)
		{
            try
            {
                SysMgrWebPart data = new SysMgrWebPart();
                data.WebPartID = this.WebPartID.IsValid ? this.WebPartID : GUIDEx.New;
                data.WebPartName = this.txtWebPartName.Text;

                data.DataAssemblyName = this.txtDataAssemblyName.Text;
                data.DataClassName = this.txtDataClassName.Text;

                data.WebPartStatus = int.Parse(this.ddlWebPartStatus.SelectedValue);
                data.Description = this.txtDescription.Text;

                data.WebPartTemplateID = this.pbWebPartTemplate.Value;
                data.WebPartTemplateName = this.pbWebPartTemplate.Text;
                
                if (this.presenter.UpdateSysMgrWebPart(data, this.EditListDataSource))
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
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<SysMgrWebPart>>(delegate(object sender, EntityEventArgs<SysMgrWebPart> e)
            {
                if (e.Entity != null)
                {
                    this.txtWebPartName.Text = e.Entity.WebPartName;

                    this.pbWebPartTemplate.Value = e.Entity.WebPartTemplateID;
                    this.pbWebPartTemplate.Text = e.Entity.WebPartTemplateName;
                    this.pbWebPartTemplate.Enabled = false;

                    this.ddlWebPartStatus.SelectedValue = e.Entity.WebPartStatus.ToString();

                    this.txtDataAssemblyName.Text = e.Entity.DataAssemblyName;
                    this.txtDataClassName.Text = e.Entity.DataClassName;

                    this.txtDescription.Text = e.Entity.Description;

                    this.dgfrmSysMgrWebPartEdit.InvokeBuildDataSource();
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion

        #region ISysMgrWebPartEditView成员

        public void BindWebPartStatus(iPower.Platform.Engine.DataSource.IListControlsData data)
        {
            this.ListControlsDataSourceBind(this.ddlWebPartStatus, data);
        }
        public GUIDEx WebPartID
        {
            get { return this.RequestGUIEx("WebPartID"); }
        }
        public void ShowMessage(string message)
        {
            this.errMessage.Message = message;
        }
        public List<WebPartProperty> EditListDataSource
        {
            get
            {
                return this.ViewState["EditListDataSource"] as List<WebPartProperty>;
            }
            set
            {
                this.ViewState["EditListDataSource"] = value;
            }
        }

        #endregion
    }

}
