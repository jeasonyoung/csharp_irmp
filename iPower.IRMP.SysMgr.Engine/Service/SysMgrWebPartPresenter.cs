//================================================================================
// FileName: SysMgrWebPartPresenter.cs
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
using iPower.IRMP.Security;
namespace iPower.IRMP.SysMgr.Engine.Service
{
	///<summary>
	/// ISysMgrWebPartView�ӿڡ�
	///</summary>
	public interface ISysMgrWebPartView: IModuleView
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
    public interface ISysMgrWebPartListView : ISysMgrWebPartView
    {
        /// <summary>
        /// ��������
        /// </summary>
        string WebPartName { get; }
    }
    /// <summary>
    /// �༭����ӿ�
    /// </summary>
    public interface ISysMgrWebPartEditView : ISysMgrWebPartView
    {
        /// <summary>
        /// ����ID
        /// </summary>
        GUIDEx WebPartID { get; }
        /// <summary>
        /// ��ȡ�����ñ༭����Դ��
        /// </summary>
        List<WebPartProperty> EditListDataSource { get; set; }
        /// <summary>
        /// ��״̬
        /// </summary>
        /// <param name="data"></param>
        void BindWebPartStatus(IListControlsData data);
    }
    /// <summary>
    /// ����Ӧ��ϵͳ
    /// </summary>
    public interface ISysMgrWebPartPickerView : ISysMgrWebPartView
    {
        /// <summary>
        /// ��ȡ����Ӧ��ϵͳID��
        /// </summary>
        GUIDEx WebPartID { get; }
        /// <summary>
        /// ��ȡ����Ӧ��ϵͳ���ơ�
        /// </summary>
        string WebPartName { get; }
        /// <summary>
        /// �����ݡ�
        /// </summary>
        /// <param name="data"></param>
        void BindWebPart(IListControlsData data);
    }
	///<summary>
	/// SysMgrWebPartPresenter��Ϊ�ࡣ
	///</summary>
	public class SysMgrWebPartPresenter: ModulePresenter<ISysMgrWebPartView>
	{
		#region ��Ա���������캯����
        SysMgrWebPartEntity sysMgrWebPartEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SysMgrWebPartPresenter(ISysMgrWebPartView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.WebPart_ModuleID;
            this.sysMgrWebPartEntity = new SysMgrWebPartEntity();
            this.sysMgrWebPartEntity.DbEntityDataChangeLogEvent += this.CreateCommonLog;
		}
		#endregion

		#region ���ݲ���������
        /// <summary>
        /// �б�����Դ
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                ISysMgrWebPartListView ListView = this.View as ISysMgrWebPartListView;
                if (ListView != null)
                {
                    DataTable dtScuore = this.sysMgrWebPartEntity.ListDataSource(ListView.WebPartName);
                    dtScuore.Columns.Add("WebPartStatusName");
                    foreach (DataRow row in dtScuore.Rows)
                    {
                        row["WebPartStatusName"] = this.GetEnumMemberName(typeof(EnumWebPartStatus), Convert.ToInt32(row["WebPartStatus"]));
                    }
                    return dtScuore;
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
            ISysMgrWebPartEditView editView = this.View as ISysMgrWebPartEditView;
            if (editView != null)
            {
                editView.BindWebPartStatus(this.EnumDataSource(typeof(EnumWebPartStatus)));
                return;
            }
            ISysMgrWebPartPickerView pickerView = this.View as ISysMgrWebPartPickerView;
            if (pickerView != null && pickerView.WebPartID.IsValid)
            {
                SysMgrWebPart data = new SysMgrWebPart();
                data.WebPartID = pickerView.WebPartID;
                if (this.sysMgrWebPartEntity.LoadRecord(ref data))
                {
                    List<SysMgrWebPart> list = new List<SysMgrWebPart>();
                    list.Add(data);
                    pickerView.BindWebPart(new ListControlsDataSource("WebPartName", "WebPartID", list));
                }
            }
        }
		///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<SysMgrWebPart>> handler)
		{
            ISysMgrWebPartEditView editView = this.View as ISysMgrWebPartEditView;
            if (editView != null && editView.WebPartID.IsValid)
            {
                SysMgrWebPart data = new SysMgrWebPart();
                data.WebPartID = editView.WebPartID;
                if (this.sysMgrWebPartEntity.LoadRecord(ref data))
                {
                    SysMgrRegWebPartTemplate sysMgrRegWebPartTemplate = new SysMgrRegWebPartTemplate();
                    sysMgrRegWebPartTemplate.WebPartTemplateID = data.WebPartTemplateID;
                    if (new SysMgrRegWebPartTemplateEntity().LoadRecord(ref sysMgrRegWebPartTemplate))
                        data.WebPartTemplateName = sysMgrRegWebPartTemplate.WebPartTemplateName;
                    SysMgrWebPartPropertyEntity sysMgrWebPartPropertyEntity = new SysMgrWebPartPropertyEntity();
                    editView.EditListDataSource = sysMgrWebPartPropertyEntity.GetWebPartProperty(data.WebPartID);
                    handler(this, new EntityEventArgs<SysMgrWebPart>(data));
                }
            }
		}
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="webPartTemplateID"></param>
        public bool LoadTemplateProperties(GUIDEx webPartTemplateID)
        {
            bool result = false;
            ISysMgrWebPartEditView editView = this.View as ISysMgrWebPartEditView;
            if (webPartTemplateID.IsValid)
            {
                SysMgrRegWebPartTemplatePropertyEntity sysMgrRegWebPartTemplatePropertyEntity = new SysMgrRegWebPartTemplatePropertyEntity();
                List<SysMgrRegWebPartTemplateProperty> listTemplateProperty = sysMgrRegWebPartTemplatePropertyEntity.GetAllRecord(webPartTemplateID);
                if (listTemplateProperty != null && listTemplateProperty.Count > 0)
                {
                    List<WebPartProperty> list = new List<WebPartProperty>();
                    foreach (SysMgrRegWebPartTemplateProperty swtp in listTemplateProperty)
                    {
                        WebPartProperty p = new WebPartProperty();
                        p.PropertyID = swtp.TemplatePropertyID;
                        p.PropertyName = swtp.TemplatePropertyName;
                        p.PropertyValue = swtp.TemplateDefaultValue;
                        p.PropertyDescription = swtp.Description;
                        list.Add(p);
                    }
                    if (list.Count > 0)
                    {
                        result = true;
                        editView.EditListDataSource = list;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="data"></param>
        /// <param name="listWebPartProperty"></param>
        /// <returns></returns>
        public bool UpdateSysMgrWebPart(SysMgrWebPart data, List<WebPartProperty> listWebPartProperty)
        {
            bool result = false;
            if (data != null)
            {
                iPower.Data.IDBAccess oDBAccess = this.sysMgrWebPartEntity.DatabaseAccess;
                try
                {
                    if (oDBAccess != null)
                    {
                        SysMgrWebPartPropertyEntity sysMgrWebPartPropertyEntity = new SysMgrWebPartPropertyEntity();
                        sysMgrWebPartPropertyEntity.DatabaseAccess = oDBAccess;
                        if (result = oDBAccess.BeginTransaction())
                        {
                            if (result = this.sysMgrWebPartEntity.UpdateRecord(data))
                            {
                                sysMgrWebPartPropertyEntity.DeleteRecord(data.WebPartID);
                                if (listWebPartProperty != null && listWebPartProperty.Count > 0)
                                {
                                    SysMgrWebPartProperty p = null;
                                    foreach (WebPartProperty wp in listWebPartProperty)
                                    {
                                        p = new SysMgrWebPartProperty();
                                        p.WebPartID = data.WebPartID;
                                        p.TemplatePropertyID = wp.PropertyID;
                                        p.PropertyValue = wp.PropertyValue;
                                        result = sysMgrWebPartPropertyEntity.UpdateRecord(p);
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
        public bool BatchDeleteSysMgrWebPart(StringCollection priCollection)
        {
            bool result = false;
            if (priCollection != null && priCollection.Count > 0)
            {
                string err = null;
                foreach(string p in priCollection)
                {
                    result = this.sysMgrWebPartEntity.DeleteWebPart(p, out err);
                    if (!result && !string.IsNullOrEmpty(err))
                    {
                        this.View.ShowMessage(err);
                        break;
                    }
                }
            }
            return result;
        }
        #endregion

        #region Picker����
        /// <summary>
        /// Picker������
        /// </summary>
        public void PickerSearch()
        {
            ISysMgrWebPartPickerView pickerView = this.View as ISysMgrWebPartPickerView;
            if (pickerView != null)
            {
                pickerView.BindWebPart((this.sysMgrWebPartEntity.WebPartPicker(pickerView.WebPartName)));
            }
        }
        #endregion
    }

}
