//================================================================================
// FileName: SysMgrSettingPresenter.cs
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
	/// ISysMgrSettingView接口。
	///</summary>
	public interface ISysMgrSettingView: IModuleView
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
    public interface ISysMgrSettingListView : ISysMgrSettingView
    {
        /// <summary>
        /// 获取系统名称
        /// </summary>
        string SystemName { get; }
    }
    /// <summary>
    /// 编辑界面接口
    /// </summary>
    public interface ISysMgrSettingEditView : ISysMgrSettingView
    {
        /// <summary>
        /// 获取系统配置ID
        /// </summary>
        GUIDEx SettingID { get; }

        /// <summary>
        /// 获取系统名称
        /// </summary>
        string SystemName { get; }

        /// <summary>
        /// 绑定配置类型
        /// </summary>
        /// <param name="data">数据源</param>
        void BindSettingType(IListControlsData data);
    }
    /// <summary>
    /// 部件应用系统
    /// </summary>
    public interface ISysMgrSettingPickerView : ISysMgrSettingView
    {
        /// <summary>
        /// 获取部件应用系统ID。
        /// </summary>
        GUIDEx SettingID { get; }
        /// <summary>
        /// 获取部件应用系统名称。
        /// </summary>
        string SettingSign { get; }
        /// <summary>
        /// 绑定数据。
        /// </summary>
        /// <param name="data"></param>
        void BindSetting(IListControlsData data);
    }
	///<summary>
	/// SysMgrSettingPresenter行为类。
	///</summary>
	public class SysMgrSettingPresenter: ModulePresenter<ISysMgrSettingView>
	{
		#region 成员变量，构造函数。
        SysMgrSettingEntity sysMgrSettingEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrSettingPresenter(ISysMgrSettingView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Setting_ModuleID;
            this.sysMgrSettingEntity = new SysMgrSettingEntity();
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
        /// 重载
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
		///编辑页面加载数据。
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
        /// 更新
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
        /// 批量删除数据。
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

        #region Picker搜索
        /// <summary>
        /// Picker搜索。
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
