//================================================================================
// FileName: SysMgrRegWebPartTemplatePresenter.cs
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
	/// ISysMgrRegWebPartTemplateView�ӿڡ�
	///</summary>
	public interface ISysMgrRegWebPartTemplateView: IModuleView
	{
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
	}
    /// <summary>
    /// �б����ӿ�
    /// </summary>
    public interface ISysMgrRegWebPartTemplateListView : ISysMgrRegWebPartTemplateView
    {
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        string WebPartTemplateName { get; }
    }
    /// <summary>
    /// �༭����ӿ�
    /// </summary>
    public interface ISysMgrRegWebPartTemplateeEditView : ISysMgrRegWebPartTemplateView
    {
        /// <summary>
        /// ϵͳ����ID
        /// </summary>
        GUIDEx WebPartTemplateID { get; }
        /// <summary>
        /// ��ȡ�����ñ༭ҳ���б�����Դ��
        /// </summary>
        List<SysMgrRegWebPartTemplateProperty> EditListDataSource { get; set; }
    }
    /// <summary>
    /// Picker����ӿ�
    /// </summary>
    public interface ISysMgrRegWebPartTemplatePickerView : ISysMgrRegWebPartTemplateView
    {
        /// <summary>
        /// ����ģ��ID
        /// </summary>
        GUIDEx WebPartTemplateID { get; }
        /// <summary>
        /// ��������
        /// </summary>
        string WebPartTemplateName { get; }
        /// <summary>
        /// �����ݡ�
        /// </summary>
        /// <param name="data"></param>
        void BindWebPartTemplate(IListControlsData data);
    }
	///<summary>
	/// SysMgrRegWebPartTemplatePresenter��Ϊ�ࡣ
	///</summary>
	public class SysMgrRegWebPartTemplatePresenter: ModulePresenter<ISysMgrRegWebPartTemplateView>
	{
		#region ��Ա���������캯����
        SysMgrRegWebPartTemplateEntity sysMgrRegWebPartTemplateEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SysMgrRegWebPartTemplatePresenter(ISysMgrRegWebPartTemplateView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.RegWebPartTemplate_ModuleID;
            this.sysMgrRegWebPartTemplateEntity = new SysMgrRegWebPartTemplateEntity();
            this.sysMgrRegWebPartTemplateEntity.DbEntityDataChangeLogEvent += this.CreateCommonLog;
		}
		#endregion

		#region ���ݲ���������
        /// <summary>
        /// ��ȡ�б�����Դ
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
        /// ����
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
		///�༭ҳ��������ݡ�
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
        /// ��������
        /// </summary>
        /// <param name="data">����Դ</param>
        /// <param name="listTemplateProperty">���Լ��ϡ�</param>
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
        /// ����ɾ��
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
