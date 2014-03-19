//================================================================================
// FileName: SysMgrSettingPresenter.cs
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
	/// ISysMgrSettingView�ӿڡ�
	///</summary>
	public interface ISysMgrSettingView: IModuleView
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
    public interface ISysMgrSettingListView : ISysMgrSettingView
    {
        /// <summary>
        /// ��ȡϵͳ����
        /// </summary>
        string SystemName { get; }
    }
    /// <summary>
    /// �༭����ӿ�
    /// </summary>
    public interface ISysMgrSettingEditView : ISysMgrSettingView
    {
        /// <summary>
        /// ��ȡϵͳ����ID
        /// </summary>
        GUIDEx SettingID { get; }

        /// <summary>
        /// ��ȡϵͳ����
        /// </summary>
        string SystemName { get; }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="data">����Դ</param>
        void BindSettingType(IListControlsData data);
    }
    /// <summary>
    /// ����Ӧ��ϵͳ
    /// </summary>
    public interface ISysMgrSettingPickerView : ISysMgrSettingView
    {
        /// <summary>
        /// ��ȡ����Ӧ��ϵͳID��
        /// </summary>
        GUIDEx SettingID { get; }
        /// <summary>
        /// ��ȡ����Ӧ��ϵͳ���ơ�
        /// </summary>
        string SettingSign { get; }
        /// <summary>
        /// �����ݡ�
        /// </summary>
        /// <param name="data"></param>
        void BindSetting(IListControlsData data);
    }
	///<summary>
	/// SysMgrSettingPresenter��Ϊ�ࡣ
	///</summary>
	public class SysMgrSettingPresenter: ModulePresenter<ISysMgrSettingView>
	{
		#region ��Ա���������캯����
        SysMgrSettingEntity sysMgrSettingEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SysMgrSettingPresenter(ISysMgrSettingView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Setting_ModuleID;
            this.sysMgrSettingEntity = new SysMgrSettingEntity();
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
                ISysMgrSettingListView ListView=this.View as ISysMgrSettingListView;
                if(ListView!=null)
                {

                    DataTable dtSource = this.sysMgrSettingEntity.ListDataSource(ListView.SystemName);
                    if (dtSource != null)
                    {
                        dtSource.Columns.Add("SettingTypeName");
                        foreach (DataRow row in dtSource.Rows)
                        {
                            row["SettingTypeName"] = this.GetEnumMemberName(typeof(EnumSettingType), Convert.ToInt32(row["SettingType"]));
                        }
                    }
                    return dtSource;
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
            ISysMgrSettingEditView editView = this.View as ISysMgrSettingEditView;
            if (editView != null)
            {
                editView.BindSettingType(this.EnumDataSource(typeof(EnumSettingType)));
                return;
            }
            ISysMgrSettingPickerView pickerView = this.View as ISysMgrSettingPickerView;
            if (pickerView != null && pickerView.SettingID.IsValid)
            {
                SysMgrSetting data = new SysMgrSetting();
                data.SettingID = pickerView.SettingID;
                if (this.sysMgrSettingEntity.LoadRecord(ref data))
                {
                    List<SysMgrSetting> list = new List<SysMgrSetting>();
                    list.Add(data);
                    pickerView.BindSetting(new ListControlsDataSource("SettingSign", "SettingID", list));
                }
            }
        }
		///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<SysMgrSetting>> handler)
		{
            ISysMgrSettingEditView editView = this.View as ISysMgrSettingEditView;
            if (editView != null && editView.SettingID.IsValid)
            {
                SysMgrSetting data = new SysMgrSetting();
                data.SettingID = editView.SettingID;
                if (this.sysMgrSettingEntity.LoadRecord(ref data))
                {
                    SysMgrAppAuthorization sysMgrAppAuthorization = new SysMgrAppAuthorization();
                    sysMgrAppAuthorization.AppAuthID = data.AppAuthID;
                    if (new SysMgrAppAuthorizationEntity().LoadRecord(ref sysMgrAppAuthorization))
                    {
                        data.SystemName = sysMgrAppAuthorization.SystemName;
                    }
                    handler(this, new EntityEventArgs<SysMgrSetting>(data));
                }
            }
		}

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateSysMgrSetting(SysMgrSetting data)
        {
            ISysMgrSettingEditView editView = this.View as ISysMgrSettingEditView;
            if (editView != null && data != null)
            {
                try
                {
                    return this.sysMgrSettingEntity.UpdateRecord(data);
                }
                catch (Exception e)
                {
                    editView.ShowMessage(e.Message);
                }
            }
            return false;
        }
        /// <summary>
        /// ����ɾ�����ݡ�
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteSysMgrSetting(StringCollection priCollection)
        {
            bool result = false;
            if (priCollection != null && priCollection.Count > 0)
            {
                string err = null;
                foreach (string p in priCollection)
                {
                    result = this.sysMgrSettingEntity.DeleteSysMgrSetting(p, out err);
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
            ISysMgrSettingPickerView pickerView = this.View as ISysMgrSettingPickerView;
            if (pickerView != null)
            {
                pickerView.BindSetting((this.sysMgrSettingEntity.SettingPicker(pickerView.SettingSign)));

            }
        }
        #endregion

	}

}
