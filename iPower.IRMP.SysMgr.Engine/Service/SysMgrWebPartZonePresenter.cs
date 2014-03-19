//================================================================================
// FileName: SysMgrWebPartZonePresenter.cs
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
	/// ISysMgrWebPartZoneView接口。
	///</summary>
	public interface ISysMgrWebPartZoneView: IModuleView
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
	public interface ISysMgrWebPartZoneListView : ISysMgrWebPartZoneView
    {
        /// <summary>
        /// 部件位置名称
        /// </summary>
        string ZoneName { get; }
    }
    /// <summary>
    /// 部件应用系统
    /// </summary>
    public interface ISysMgrWebPartZonePickerView : ISysMgrWebPartZoneView
    {
        /// <summary>
        /// 获取部件应用系统ID。
        /// </summary>
        GUIDEx ZoneID { get; }
        /// <summary>
        /// 获取部件应用系统名称。
        /// </summary>
        string ZoneName { get; }
        /// <summary>
        /// 绑定数据。
        /// </summary>
        /// <param name="data"></param>
        void BindWebPartZone(IListControlsData data);
    }
    /// <summary>
    /// 编辑界面接口
    /// </summary>
    public interface ISysMgrWebPartZoneEditView : ISysMgrWebPartZoneView
    {
        /// <summary>
        /// 部件ID
        /// </summary>
        GUIDEx ZoneID { get; }

        /// <summary>
        /// AppAuthID
        /// </summary>
        string AppAuthID { get; }

        /// <summary>
        /// 显示模式
        /// </summary>
        /// <param name="data"></param>
        void BindZoneMode(IListControlsData data);
    }
	///<summary>
	/// SysMgrWebPartZonePresenter行为类。
	///</summary>
	public class SysMgrWebPartZonePresenter: ModulePresenter<ISysMgrWebPartZoneView>
	{
		#region 成员变量，构造函数。
        SysMgrWebPartZoneEntity sysMgrWebPartZoneEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrWebPartZonePresenter(ISysMgrWebPartZoneView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.WebPartZone_ModuleID;
            this.sysMgrWebPartZoneEntity = new SysMgrWebPartZoneEntity();
            this.sysMgrWebPartZoneEntity.DbEntityDataChangeLogEvent += this.CreateCommonLog;
		}
		#endregion

		#region 数据操作函数。
        /// <summary>
        /// 列表数据源
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                ISysMgrWebPartZoneListView ListView = this.View as ISysMgrWebPartZoneListView;
                if (ListView != null)
                {
                    DataTable dtSource = this.sysMgrWebPartZoneEntity.ListDataSource(ListView.ZoneName);
                    dtSource.Columns.Add("ZoneModeName");
                    string strSystemName = string.Empty;
                    foreach (DataRow row in dtSource.Rows)
                    {
                        row["ZoneModeName"] = this.GetEnumMemberName(typeof(EnumZoneMode), Convert.ToInt32(row["ZoneMode"]));
                        strSystemName = Convert.ToString(row["SystemName"]);
                        if (string.IsNullOrEmpty(strSystemName))
                            row["SystemName"] = "[全局]";
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
            ISysMgrWebPartZoneEditView editview = this.View as ISysMgrWebPartZoneEditView;
            if (editview != null)
            {
                editview.BindZoneMode(this.EnumDataSource(typeof(EnumZoneMode)));
                return;
            }
            ISysMgrWebPartZonePickerView pickerView = this.View as ISysMgrWebPartZonePickerView;
            if (pickerView != null && pickerView.ZoneID.IsValid)
            {
                SysMgrWebPartZone data = new SysMgrWebPartZone();
                data.ZoneID = pickerView.ZoneID;
                if (this.sysMgrWebPartZoneEntity.LoadRecord(ref data))
                {
                    List<SysMgrWebPartZone> list = new List<SysMgrWebPartZone>();
                    list.Add(data);
                    pickerView.BindWebPartZone(new ListControlsDataSource("ZoneName", "ZoneID", list));
                }
            }
        }
		///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<SysMgrWebPartZone>> handler)
		{
            ISysMgrWebPartZoneEditView editview = this.View as ISysMgrWebPartZoneEditView;
            if (editview != null && editview.ZoneID.IsValid)
            {
                SysMgrWebPartZone data = new SysMgrWebPartZone();
                data.ZoneID = editview.ZoneID;
                if (this.sysMgrWebPartZoneEntity.LoadRecord(ref data))
                {
                    SysMgrAppAuthorization sysMgrAppAuthorization = new SysMgrAppAuthorization();
                    sysMgrAppAuthorization.AppAuthID = data.AppAuthID;
                    if (new SysMgrAppAuthorizationEntity().LoadRecord(ref sysMgrAppAuthorization))
                    {
                        data.SystemName = sysMgrAppAuthorization.SystemName;
                    }
                    handler(this, new EntityEventArgs<SysMgrWebPartZone>(data));
                }
            }
		}
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateSysMgrWebPartZone(SysMgrWebPartZone data)
        {
            ISysMgrWebPartZoneEditView editview = this.View as ISysMgrWebPartZoneEditView;
            if (editview != null && data != null)
            {
                try
                {
                    return this.sysMgrWebPartZoneEntity.UpdateRecord(data);
                }
                catch (Exception e)
                {
                    editview.ShowMessage(e.Message);
                }
            }
            return false;
        }

		#endregion

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteSysMgrWebPartZone(StringCollection priCollection)
        {
            bool result = false;
            if (priCollection != null && priCollection.Count > 0)
            {
                string err = null;
                foreach (string p in priCollection)
                {
                    result = this.sysMgrWebPartZoneEntity.DeleteSysMgrWebPartZone(p, out err);
                    if (!result && !string.IsNullOrEmpty(err))
                    {
                        this.View.ShowMessage(err);
                        break;
                    }
                }
            }
            return result;
        }
        #region Picker搜索
        /// <summary>
        /// Picker搜索。
        /// </summary>
        public void PickerSearch()
        {
            ISysMgrWebPartZonePickerView pickerView = this.View as ISysMgrWebPartZonePickerView;
            if (pickerView != null)
            {
                pickerView.BindWebPartZone((this.sysMgrWebPartZoneEntity.WebPartZonePicker(pickerView.ZoneName)));

            }
        }
        #endregion
	}

}
