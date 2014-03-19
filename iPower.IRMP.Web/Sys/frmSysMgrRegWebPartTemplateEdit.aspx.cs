//================================================================================
// FileName: frmSysMgrRegWebPartTemplateEdit.aspx.cs
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
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
	
using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.IRMP.SysMgr.Engine.Domain;
using iPower.IRMP.SysMgr.Engine.Service;
namespace iPower.IRMP.SysMgr.Web
{
	///<summary>
	///frmSysMgrRegWebPartTemplateEdit列表页面后台代码。
	///</summary>
    public partial class frmSysMgrRegWebPartTemplateEdit : ModuleBasePage, ISysMgrRegWebPartTemplateeEditView
	{
		#region 成员变量，构造函数。
		SysMgrRegWebPartTemplatePresenter presenter = null;
		///<summary>
		///构造函数。
		///</summary>
		public frmSysMgrRegWebPartTemplateEdit()
		{
			this.presenter = new SysMgrRegWebPartTemplatePresenter(this);

		}
		#endregion
        
        #region 属性。
        GUIDEx TemplatePropertyID
        {
            get
            {
                object obj = this.ViewState["TemplatePropertyID"];
                return obj == null ? GUIDEx.Null : new GUIDEx(obj);
            }
            set
            {
                this.ViewState["TemplatePropertyID"] = value;
            }
        }
        #endregion

        #region 事件处理。
        protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
				this.presenter.InitializeComponent();
		}

        protected void dgfrmSysMgrRegWebPartTemplateEdit_BuildDataSource(object sender, EventArgs e)
        {
            this.dgfrmSysMgrRegWebPartTemplateEdit.DataSource = this.EditListDataSource;
        }

        protected void dgfrmSysMgrRegWebPartTemplateEdit_OnRowSelecting(object sender, EventArgs e)
        {
            SysMgrRegWebPartTemplateProperty data = sender as SysMgrRegWebPartTemplateProperty;
            if (data != null)
            {
                this.TemplatePropertyID = data.TemplatePropertyID;
                this.txtTemplatePropertyName.Text = data.TemplatePropertyName;
                this.txtTemplateDefaultValue.Text = data.TemplateDefaultValue;
                this.txtTemplatePropertyDescription.Text = data.Description;

                this.btnAdd.ButtonType = "Save";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SysMgrRegWebPartTemplateProperty data = new SysMgrRegWebPartTemplateProperty();
                data.TemplatePropertyID = this.TemplatePropertyID.IsValid ? this.TemplatePropertyID : GUIDEx.New;
                data.TemplatePropertyName = this.txtTemplatePropertyName.Text;
                data.TemplateDefaultValue = this.txtTemplateDefaultValue.Text;
                data.Description = this.txtTemplatePropertyDescription.Text;
                if (string.IsNullOrEmpty(data.TemplatePropertyName))
                    throw new Exception("属性名不能为空！");
                else if (string.IsNullOrEmpty(data.TemplateDefaultValue))
                    throw new Exception("默认值不能为空！");
                else
                {
                    List<SysMgrRegWebPartTemplateProperty> dataSource = this.EditListDataSource;
                    if (dataSource == null)
                        dataSource = new List<SysMgrRegWebPartTemplateProperty>();
                    else
                    {
                        SysMgrRegWebPartTemplateProperty oProperty = dataSource.Find(new Predicate<SysMgrRegWebPartTemplateProperty>(delegate(SysMgrRegWebPartTemplateProperty o)
                        {
                            return (o != null) && (o.TemplatePropertyID == data.TemplatePropertyID);
                        }));
                        if (oProperty != null)
                            dataSource.Remove(oProperty);
                        SysMgrRegWebPartTemplateProperty property = dataSource.Find(new Predicate<SysMgrRegWebPartTemplateProperty>(delegate(SysMgrRegWebPartTemplateProperty o)
                        {
                            return (o != null) && (o.TemplatePropertyName == data.TemplatePropertyName);
                        }));
                        if (property != null)
                            throw new Exception("属性名已经存在！");
                    }
                    dataSource.Add(data);
                    this.EditListDataSource = dataSource;
                    this.txtTemplatePropertyName.Text = this.txtTemplateDefaultValue.Text = this.txtTemplatePropertyDescription.Text = string.Empty;
                    this.TemplatePropertyID = GUIDEx.Null;
                    this.btnAdd.ButtonType = "Add";
                    this.dgfrmSysMgrRegWebPartTemplateEdit.InvokeBuildDataSource();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                StringCollection templatePropertyIDCollection = this.dgfrmSysMgrRegWebPartTemplateEdit.CheckedValue;
                if (templatePropertyIDCollection != null && templatePropertyIDCollection.Count > 0)
                {
                    List<SysMgrRegWebPartTemplateProperty> dataSource = this.EditListDataSource;
                    if (dataSource != null && dataSource.Count > 0)
                    {
                        bool result = false;
                        foreach (string templatePropertyID in templatePropertyIDCollection)
                        {
                            if (!string.IsNullOrEmpty(templatePropertyID))
                            {
                                SysMgrRegWebPartTemplateProperty oProperty = dataSource.Find(new Predicate<SysMgrRegWebPartTemplateProperty>(delegate(SysMgrRegWebPartTemplateProperty data)
                                {
                                    return (data != null) && (data.TemplatePropertyID == templatePropertyID);
                                }));
                                if (oProperty != null)
                                {
                                    result = dataSource.Remove(oProperty);
                                }
                            }
                        }
                        if (result)
                        {
                            this.EditListDataSource = dataSource;
                            this.dgfrmSysMgrRegWebPartTemplateEdit.InvokeBuildDataSource();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
            }
        }
			
		protected void btnSave_Click(object sender, EventArgs e)
		{
            try
            {
                SysMgrRegWebPartTemplate data = new SysMgrRegWebPartTemplate();
                data.WebPartTemplateID = this.WebPartTemplateID.IsValid ? this.WebPartTemplateID : GUIDEx.New;
                data.WebPartTemplateName = this.txtWebPartTemplateName.Text.Trim();
                data.WebPartTemplatePath = this.txtWebPartTemplatePath.Text.Trim();
                data.Description = this.txtDescription.Text.Trim();
                if (this.presenter.UpdateMgrRegWebPartTemplate(data, this.EditListDataSource))
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
            this.presenter.LoadEntityData(new EventHandler<EntityEventArgs<SysMgrRegWebPartTemplate>>(delegate(object sender, EntityEventArgs<SysMgrRegWebPartTemplate> e)
            {
                if (e.Entity != null)
                {
                    this.txtDescription.Text = e.Entity.Description;
                    this.txtWebPartTemplateName.Text = e.Entity.WebPartTemplateName;
                    this.txtWebPartTemplatePath.Text = e.Entity.WebPartTemplatePath;

                    this.dgfrmSysMgrRegWebPartTemplateEdit.InvokeBuildDataSource();
                }
            }));
		}
			
		public override bool DeleteData()
		{
			return false;

		}
		#endregion

        #region ISysMgrRegWebPartTemplateeEditView 成员

        public GUIDEx WebPartTemplateID
        {
            get { return this.RequestGUIEx("WebPartTemplateID"); }
        }

        public void ShowMessage(string msg)
        {
            this.errMessage.Message = msg;
        }

        public List<SysMgrRegWebPartTemplateProperty> EditListDataSource
        {
            get
            {
                return this.ViewState["EditListDataSource"] as List<SysMgrRegWebPartTemplateProperty>;
            }
            set
            {
                this.ViewState["EditListDataSource"] = value;
            }
        }

        #endregion
    }

}
