//================================================================================
// FileName: SysMgrAppAuthorizationPresenter.cs
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

using iPower.IRMP.Security;
using iPower.IRMP.SysMgr.Engine.Domain;
using iPower.IRMP.SysMgr.Engine.Persistence;
namespace iPower.IRMP.SysMgr.Engine.Service
{
	///<summary>
	/// ISysMgrAppAuthorizationView�ӿڡ�
	///</summary>
	public interface ISysMgrAppAuthorizationView: IModuleView
	{

	}
    /// <summary>
    /// �б����ӿڡ�
    /// </summary>
    public interface ISysMgrAppAuthorizationListView : ISysMgrAppAuthorizationView
    {
        /// <summary>
        /// ��ȡϵͳ���ơ�
        /// </summary>
        string AppName { get; }
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
    }
    /// <summary>
    /// �༭����ӿڡ�
    /// </summary>
    public interface ISysMgrAppAuthorizationEditView : ISysMgrAppAuthorizationView
    {
        /// <summary>
        /// ��ȡϵͳ��ȨID��
        /// </summary>
        GUIDEx AppAuthID
        {
            get;
        }
        /// <summary>
        /// ����Ȩ״̬��
        /// </summary>
        /// <param name="data"></param>
        void BindAuthStatus(IListControlsData data);
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }
    /// <summary>
    /// Picker��
    /// </summary>
    public interface ISysMgrAppAuthorizationPickerView : ISysMgrAppAuthorizationView
    {
        /// <summary>
        /// ��ȡӦ��ϵͳID��
        /// </summary>
        GUIDEx AppID { get; }
        /// <summary>
        /// ��ȡӦ��ϵͳ���ơ�
        /// </summary>
        string AppName { get; }
        /// <summary>
        /// �Ƿ�ʹ�ñ�������Դ��
        /// </summary>
        bool IsLocal { get; }
        /// <summary>
        /// �����ݡ�
        /// </summary>
        /// <param name="data"></param>
        void BindApp(IListControlsData data);
    }
	///<summary>
	/// SysMgrAppAuthorizationPresenter��Ϊ�ࡣ
	///</summary>
	public class SysMgrAppAuthorizationPresenter: ModulePresenter<ISysMgrAppAuthorizationView>
	{
		#region ��Ա���������캯����
        SysMgrAppAuthorizationEntity sysMgrAppAuthorizationEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SysMgrAppAuthorizationPresenter(ISysMgrAppAuthorizationView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.AppAuthorization_ModuleID;
            this.sysMgrAppAuthorizationEntity = new SysMgrAppAuthorizationEntity();
		}
		#endregion

		#region ���ݲ���������
        /// <summary>
        /// ��ȡ�б�����Դ��
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                ISysMgrAppAuthorizationListView listView = this.View as ISysMgrAppAuthorizationListView;
                if (listView != null)
                {
                    DataTable dtSource = this.sysMgrAppAuthorizationEntity.ListDataSource(listView.AppName);
                    if (dtSource != null)
                    {
                        dtSource.Columns.Add("AuthStatusName");
                        foreach (DataRow row in dtSource.Rows)
                        {
                            row["AuthStatusName"] = this.GetEnumMemberName(typeof(EnumAuthStatus), Convert.ToInt32(row["AuthStatus"]));
                        }
                    }
                    return dtSource;
                }
                return null;
            }
        }
        /// <summary>
        /// ���ء�
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            ISysMgrAppAuthorizationEditView editView = this.View as ISysMgrAppAuthorizationEditView;
            if (editView != null)
            {
                editView.BindAuthStatus(this.EnumDataSource(typeof(EnumAuthStatus)));
                return;
            }
            ISysMgrAppAuthorizationPickerView pickerView = this.View as ISysMgrAppAuthorizationPickerView;
            if (pickerView != null && pickerView.AppID.IsValid)
            {
                AppSystem app = null;
                if (pickerView.IsLocal)
                {
                    SysMgrAppAuthorization data = new SysMgrAppAuthorization();
                    data.AppAuthID = pickerView.AppID;
                    if (this.sysMgrAppAuthorizationEntity.LoadRecord(ref data))
                    {
                        app = new AppSystem();
                        app.AppID = data.AppAuthID;
                        app.AppName = data.SystemName;
                    }
                }
                else
                {
                    AppSystemCollection collection = this.ModuleConfig.SecurityFactory.AppRegister(string.Empty);
                    app = collection[pickerView.AppID];
                }
                if (app != null)
                {
                    AppSystemCollection appCollection = new AppSystemCollection();
                    appCollection.Add(app);
                    pickerView.BindApp(new ListControlsDataSource("AppName", "AppID", appCollection));
                }

            }
        }
		///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<SysMgrAppAuthorization>> handler)
		{
            ISysMgrAppAuthorizationEditView editView = this.View as ISysMgrAppAuthorizationEditView;
            if (editView != null && editView.AppAuthID.IsValid)
            {
                SysMgrAppAuthorization data = new SysMgrAppAuthorization();
                data.AppAuthID = editView.AppAuthID;
                if (this.sysMgrAppAuthorizationEntity.LoadRecord(ref data))
                    handler(this, new EntityEventArgs<SysMgrAppAuthorization>(data));
            }
		}
        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateSysMgrAppAuthorization(SysMgrAppAuthorization data)
        {
            ISysMgrAppAuthorizationEditView editView = this.View as ISysMgrAppAuthorizationEditView;
            if (editView != null && data != null)
            {
                try
                {
                    return this.sysMgrAppAuthorizationEntity.UpdateRecord(data);
                }
                catch (Exception e)
                {
                    editView.ShowMessage(e.Message);
                }
            }
            return false;
        }
        /// <summary>
        /// ����ɾ����Ȩϵͳ��
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteAppAuthorization(StringCollection priCollection)
        {
            bool result = false;
            ISysMgrAppAuthorizationListView listView = this.View as ISysMgrAppAuthorizationListView;
            if (listView != null && priCollection != null && priCollection.Count > 0)
            {
                string err = null;
                foreach (string p in priCollection)
                {
                    result = this.sysMgrAppAuthorizationEntity.DeleteRecord(p, out err);
                    if (!result && !string.IsNullOrEmpty(err))
                    {
                        listView.ShowMessage(err);
                        break;
                    }
                }
            }
            return result;
        }
		#endregion

        #region  Picker����
        /// <summary>
        /// Picker������
        /// </summary>
        public void PickerSearch()
        {
            ISysMgrAppAuthorizationPickerView pickerView = this.View as ISysMgrAppAuthorizationPickerView;
            if (pickerView != null)
            {
                if (pickerView.IsLocal)
                    pickerView.BindApp(this.sysMgrAppAuthorizationEntity.AppAuthorizationPicker(pickerView.AppName));
                else
                {
                    AppSystemCollection collection = this.ModuleConfig.SecurityFactory.AppRegister(pickerView.AppName);
                    if (collection != null)
                    {
                        pickerView.BindApp(new ListControlsDataSource("AppName", "AppID", collection));
                    }
                }
            }
        }
        #endregion
    }

}
