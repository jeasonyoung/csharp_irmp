//================================================================================
// FileName: SysMgrAppAuthorizationPresenter.cs
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

using iPower.IRMP.Security;
using iPower.IRMP.SysMgr.Engine.Domain;
using iPower.IRMP.SysMgr.Engine.Persistence;
namespace iPower.IRMP.SysMgr.Engine.Service
{
	///<summary>
	/// ISysMgrAppAuthorizationView接口。
	///</summary>
	public interface ISysMgrAppAuthorizationView: IModuleView
	{

	}
    /// <summary>
    /// 列表界面接口。
    /// </summary>
    public interface ISysMgrAppAuthorizationListView : ISysMgrAppAuthorizationView
    {
        /// <summary>
        /// 获取系统名称。
        /// </summary>
        string AppName { get; }
        /// <summary>
        /// 显示消息。
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
    }
    /// <summary>
    /// 编辑界面接口。
    /// </summary>
    public interface ISysMgrAppAuthorizationEditView : ISysMgrAppAuthorizationView
    {
        /// <summary>
        /// 获取系统授权ID。
        /// </summary>
        GUIDEx AppAuthID
        {
            get;
        }
        /// <summary>
        /// 绑定授权状态。
        /// </summary>
        /// <param name="data"></param>
        void BindAuthStatus(IListControlsData data);
        /// <summary>
        /// 显示消息。
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }
    /// <summary>
    /// Picker。
    /// </summary>
    public interface ISysMgrAppAuthorizationPickerView : ISysMgrAppAuthorizationView
    {
        /// <summary>
        /// 获取应用系统ID。
        /// </summary>
        GUIDEx AppID { get; }
        /// <summary>
        /// 获取应用系统名称。
        /// </summary>
        string AppName { get; }
        /// <summary>
        /// 是否使用本地数据源。
        /// </summary>
        bool IsLocal { get; }
        /// <summary>
        /// 绑定数据。
        /// </summary>
        /// <param name="data"></param>
        void BindApp(IListControlsData data);
    }
	///<summary>
	/// SysMgrAppAuthorizationPresenter行为类。
	///</summary>
	public class SysMgrAppAuthorizationPresenter: ModulePresenter<ISysMgrAppAuthorizationView>
	{
		#region 成员变量，构造函数。
        SysMgrAppAuthorizationEntity sysMgrAppAuthorizationEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrAppAuthorizationPresenter(ISysMgrAppAuthorizationView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.AppAuthorization_ModuleID;
            this.sysMgrAppAuthorizationEntity = new SysMgrAppAuthorizationEntity();
		}
		#endregion

		#region 数据操作函数。
        /// <summary>
        /// 获取列表数据源。
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
        /// 重载。
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
		///编辑页面加载数据。
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
        /// 更新数据。
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
        /// 批量删除授权系统。
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

        #region  Picker搜索
        /// <summary>
        /// Picker搜索。
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
