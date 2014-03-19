//================================================================================
// FileName: SysMgrRegWebPartTemplatePresenter.cs
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
using System.Text;
using System.Data;
	
using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.Platform.Engine.DataSource;
using iPower.IRMP.SysMgr.Engine.Domain;
using iPower.IRMP.SysMgr.Engine.Persistence;
namespace iPower.IRMP.SysMgr.Engine.Service
{
	///<summary>
	/// ISysMgrRegWebPartTemplateView接口。
	///</summary>
	public interface ISysMgrRegWebPartTemplateView: IModuleView
	{
        /// <summary>
        /// 显示信息。
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
	}
    /// <summary>
    /// 列表界面接口
    /// </summary>
    public interface ISysMgrRegWebPartTemplateListView : ISysMgrRegWebPartTemplateView
    {
        /// <summary>
        /// 获取部件名称
        /// </summary>
        string WebPartTemplateName { get; }
    }
    /// <summary>
    /// 编辑界面接口
    /// </summary>
    public interface ISysMgrRegWebPartTemplateeEditView : ISysMgrRegWebPartTemplateView
    {
        /// <summary>
        /// 系统部件ID
        /// </summary>
        GUIDEx WebPartTemplateID { get; }
        /// <summary>
        /// 获取或设置编辑页面列表数据源。
        /// </summary>
        List<SysMgrRegWebPartTemplateProperty> EditListDataSource { get; set; }
    }
    /// <summary>
    /// Picker界面接口
    /// </summary>
    public interface ISysMgrRegWebPartTemplatePickerView : ISysMgrRegWebPartTemplateView
    {
        /// <summary>
        /// 部件模板ID
        /// </summary>
        GUIDEx WebPartTemplateID { get; }
        /// <summary>
        /// 部件名称
        /// </summary>
        string WebPartTemplateName { get; }
        /// <summary>
        /// 绑定数据。
        /// </summary>
        /// <param name="data"></param>
        void BindWebPartTemplate(IListControlsData data);
    }
	///<summary>
	/// SysMgrRegWebPartTemplatePresenter行为类。
	///</summary>
	public class SysMgrRegWebPartTemplatePresenter: ModulePresenter<ISysMgrRegWebPartTemplateView>
	{
		#region 成员变量，构造函数。
        SysMgrRegWebPartTemplateEntity sysMgrRegWebPartTemplateEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrRegWebPartTemplatePresenter(ISysMgrRegWebPartTemplateView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.RegWebPartTemplate_ModuleID;
            this.sysMgrRegWebPartTemplateEntity = new SysMgrRegWebPartTemplateEntity();
            this.sysMgrRegWebPartTemplateEntity.DbEntityDataChangeLogEvent += this.CreateCommonLog;
		}
		#endregion

		#region 数据操作函数。
        /// <summary>
        /// 获取列表数据源
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                ISysMgrRegWebPartTemplateListView ListView = this.View as ISysMgrRegWebPartTemplateListView;
                if (ListView != null)
                {
                    return this.sysMgrRegWebPartTemplateEntity.ListDataSource(ListView.WebPartTemplateName);
                }
                return null;
            }
        }
        /// <summary>
        /// 重载
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            ISysMgrRegWebPartTemplatePickerView pickerView = this.View as ISysMgrRegWebPartTemplatePickerView;
            if (pickerView != null && pickerView.WebPartTemplateID.IsValid)
            {
                SysMgrRegWebPartTemplate data = new SysMgrRegWebPartTemplate();
                data.WebPartTemplateID = pickerView.WebPartTemplateID;
                if (this.sysMgrRegWebPartTemplateEntity.LoadRecord(ref data))
                {
                    List<SysMgrRegWebPartTemplate> list = new List<SysMgrRegWebPartTemplate>();
                    list.Add(data);
                    pickerView.BindWebPartTemplate(new ListControlsDataSource("WebPartTemplateName", "WebPartTemplateID", list)); 
                }
            }
        }
		///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<SysMgrRegWebPartTemplate>> handler)
		{
            ISysMgrRegWebPartTemplateeEditView editView = this.View as ISysMgrRegWebPartTemplateeEditView;
            if (editView != null && editView.WebPartTemplateID.IsValid)
            {
                SysMgrRegWebPartTemplate data = new SysMgrRegWebPartTemplate();
                data.WebPartTemplateID = editView.WebPartTemplateID;
                if (this.sysMgrRegWebPartTemplateEntity.LoadRecord(ref data))
                {
                    editView.EditListDataSource = new SysMgrRegWebPartTemplatePropertyEntity().GetAllRecord(data.WebPartTemplateID);
                    handler(this, new EntityEventArgs<SysMgrRegWebPartTemplate>(data));
                }
            }
		}
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data">数据源</param>
        /// <param name="listTemplateProperty">属性集合。</param>
        /// <returns></returns>
        public bool UpdateMgrRegWebPartTemplate(SysMgrRegWebPartTemplate data, List<SysMgrRegWebPartTemplateProperty> listTemplateProperty)
        {
            bool result = false;
            if (data != null)
            {
                iPower.Data.IDBAccess oDBAccess = this.sysMgrRegWebPartTemplateEntity.DatabaseAccess;
                try
                {
                    if (oDBAccess != null)
                    {
                        SysMgrRegWebPartTemplatePropertyEntity sysMgrRegWebPartTemplatePropertyEntity = new SysMgrRegWebPartTemplatePropertyEntity();
                        sysMgrRegWebPartTemplatePropertyEntity.DatabaseAccess = oDBAccess;
                        if (result = oDBAccess.BeginTransaction())
                        {
                            if (result = this.sysMgrRegWebPartTemplateEntity.UpdateRecord(data))
                            {
                                sysMgrRegWebPartTemplatePropertyEntity.DeleteRecord(data.WebPartTemplateID);
                                if (listTemplateProperty != null && listTemplateProperty.Count > 0)
                                {
                                    foreach (SysMgrRegWebPartTemplateProperty oProperty in listTemplateProperty)
                                    {
                                        oProperty.WebPartTemplateID = data.WebPartTemplateID;
                                        result = sysMgrRegWebPartTemplatePropertyEntity.UpdateRecord(oProperty);
                                    }
                                }
                            }
                            result = oDBAccess.CommitTransaction();
                        }
                    }
                }
                catch (Exception e)
                {
                    result = false;
                    if (oDBAccess != null)
                        oDBAccess.RollbackTransaction();
                    this.View.ShowMessage(e.Message);
                }
            }
            return result;
        }
        
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteSysMgrRegWebPartTemplate(StringCollection priCollection)
        {
            bool result = false;
            if (priCollection != null && priCollection.Count > 0)
            {
                string err = null;
                foreach (string p in priCollection)
                {
                    result = this.sysMgrRegWebPartTemplateEntity.DeleteSysMgrRegWebPartTemplate(p, out err);
                    if (!result && !string.IsNullOrEmpty(err))
                    {
                        this.View.ShowMessage(err);
                        break;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Picker
        /// </summary>
        public void PickerSearch()
        {
            ISysMgrRegWebPartTemplatePickerView pickerView = this.View as ISysMgrRegWebPartTemplatePickerView;
            if (pickerView != null)
            {
                pickerView.BindWebPartTemplate(this.sysMgrRegWebPartTemplateEntity.WebPartTemplatePicker(pickerView.WebPartTemplateName));   
            }
        }
		#endregion
	}

}
