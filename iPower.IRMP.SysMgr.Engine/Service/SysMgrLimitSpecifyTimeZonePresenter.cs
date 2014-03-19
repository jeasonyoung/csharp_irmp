//================================================================================
// FileName: SysMgrLimitSpecifyTimeZonePresenter.cs
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
	/// ISysMgrLimitSpecifyTimeZoneView接口。
	///</summary>
	public interface ISysMgrLimitSpecifyTimeZoneView: IModuleView
	{

	}
    /// <summary>
    /// 列表界面接口。
    /// </summary>
    public interface ISysMgrLimitSpecifyTimeZoneListView : ISysMgrLimitSpecifyTimeZoneView
    {
        /// <summary>
        /// 获取用户名称。
        /// </summary>
        string EmployeeName { get; }
    }
    /// <summary>
    /// 边界界面接口。
    /// </summary>
    public interface ISysMgrLimitSpecifyTimeZoneEditView : ISysMgrLimitSpecifyTimeZoneView
    {
        /// <summary>
        /// 
        /// </summary>
        GUIDEx ZoneID { get; }
        /// <summary>
        /// 绑定授权状态。
        /// </summary>
        /// <param name="data"></param>
        void BindAuthStatus(IListControlsData data);
        /// <summary>
        /// 显示消息。
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
    }
		
	///<summary>
	/// SysMgrLimitSpecifyTimeZonePresenter行为类。
	///</summary>
	public class SysMgrLimitSpecifyTimeZonePresenter: ModulePresenter<ISysMgrLimitSpecifyTimeZoneView>
	{
		#region 成员变量，构造函数。
        SysMgrLimitSpecifyTimeZoneEntity sysMgrLimitSpecifyTimeZoneEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrLimitSpecifyTimeZonePresenter(ISysMgrLimitSpecifyTimeZoneView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.LimitSpecifyTimeZone_ModuleID;
            this.sysMgrLimitSpecifyTimeZoneEntity = new SysMgrLimitSpecifyTimeZoneEntity();
		}
		#endregion

		#region 数据操作函数。
        /// <summary>
        /// 获取数据源。
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                ISysMgrLimitSpecifyTimeZoneListView listView = this.View as ISysMgrLimitSpecifyTimeZoneListView;
                if (listView != null)
                {
                    DataTable dtSource = this.sysMgrLimitSpecifyTimeZoneEntity.GetAllRecord(string.Format("EmployeeName like '%{0}%'", listView.EmployeeName));
                    if (dtSource != null)
                    {
                        dtSource.Columns.Add("AuthStatusName");
                        foreach (DataRow row in dtSource.Rows)
                        {
                            row["AuthStatusName"] = this.GetEnumMemberName(typeof(EnumAuthStatus), Convert.ToInt32(row["AuthStatus"]));
                            string empId = Convert.ToString(row["EmployeeID"]);
                            if (string.IsNullOrEmpty(empId))
                            {
                                row["EmployeeName"] = "[全局]";
                            }
                        }
                    }
                    return dtSource;
                }
                return null;
            }
        }
        /// <summary>
        /// 重载加载。
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            ISysMgrLimitSpecifyTimeZoneEditView editView = this.View as ISysMgrLimitSpecifyTimeZoneEditView;
            if (editView != null)
            {
                editView.BindAuthStatus(this.EnumDataSource(typeof(EnumAuthStatus)));
            }
        }
		///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<SysMgrLimitSpecifyTimeZone>> handler)
		{
            ISysMgrLimitSpecifyTimeZoneEditView editView = this.View as ISysMgrLimitSpecifyTimeZoneEditView;
            if (editView != null && editView.ZoneID.IsValid)
            {
                SysMgrLimitSpecifyTimeZone data = new SysMgrLimitSpecifyTimeZone();
                data.ZoneID = editView.ZoneID;
                if (this.sysMgrLimitSpecifyTimeZoneEntity.LoadRecord(ref data))
                {
                    if (!data.EmployeeID.IsValid)
                        data.EmployeeName = "[全局]";
                    handler(this, new EntityEventArgs<SysMgrLimitSpecifyTimeZone>(data));
                }
            }
		}
        /// <summary>
        /// 更新数据。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateLimitSpecifyTimeZone(SysMgrLimitSpecifyTimeZone data)
        {
            bool result = false;
            if (data != null)
            {
                try
                {
                    if (!data.EmployeeID.IsValid)
                        data.EmployeeName = string.Empty;

                    if (data.EndTime <= data.StartTime)
                        throw new Exception("起始时间应该早于结束时间!");
                    else
                        result = this.sysMgrLimitSpecifyTimeZoneEntity.UpdateRecord(data);
                }
                catch (Exception e)
                {
                    ISysMgrLimitSpecifyTimeZoneEditView editView = this.View as ISysMgrLimitSpecifyTimeZoneEditView;
                    if (editView != null)
                        editView.ShowMessage(e.Message);
                }
            }
            return result;
        }
        /// <summary>
        /// 批量删除数据。
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteLimitSpecifyTimeZone(StringCollection priCollection)
        {
            return this.sysMgrLimitSpecifyTimeZoneEntity.DeleteRecord(priCollection);
        }
		#endregion

	}

}
