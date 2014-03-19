//================================================================================
// FileName: IRMPCommonLogPresenter.cs
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
	/// IIRMPCommonLogView接口。
	///</summary>
	public interface IIRMPCommonLogView: IModuleView
	{

	}
    /// <summary>
    /// 列表界面接口。
    /// </summary>
    public interface IIRMPCommonLogListView : IIRMPCommonLogView
    {
        /// <summary>
        /// 获取系统名称。
        /// </summary>
        string SystemName { get; }
        /// <summary>
        /// 获取用户名称。
        /// </summary>
        string EmployeeName { get; }
        /// <summary>
        /// 获取创建时间。
        /// </summary>
        string CreateDate { get; }
        /// <summary>
        /// 获取日志内容。
        /// </summary>
        string LogContext { get; }
        /// <summary>
        /// 显示消息。
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }
    /// <summary>
    /// 编辑界面接口。
    /// </summary>
    public interface IIRMPCommonLogEditView : IIRMPCommonLogView
    {
        /// <summary>
        /// 获取日志ID。
        /// </summary>
        GUIDEx LogID { get; }
    }
		
	///<summary>
	/// IRMPCommonLogPresenter行为类。
	///</summary>
	public class IRMPCommonLogPresenter: ModulePresenter<IIRMPCommonLogView>
	{
		#region 成员变量，构造函数。
        IRMPCommonLogEntity iRMPCommonLogEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public IRMPCommonLogPresenter(IIRMPCommonLogView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.CommonLog_ModuleID;
            this.iRMPCommonLogEntity = new IRMPCommonLogEntity();
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
                IIRMPCommonLogListView listView = this.View as IIRMPCommonLogListView;
                if (listView != null)
                {
                    return this.iRMPCommonLogEntity.ListDataSource(listView.SystemName, listView.EmployeeName, listView.CreateDate, listView.LogContext);
                }
                return null;
            }
        }
		///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<IRMPCommonLog>> handler)
		{
            IIRMPCommonLogEditView editView = this.View as IIRMPCommonLogEditView;
            if (editView != null && editView.LogID.IsValid && handler != null)
            {
                IRMPCommonLog data = new IRMPCommonLog();
                data.LogID = editView.LogID;
                if (this.iRMPCommonLogEntity.LoadRecord(ref data))
                    handler(this, new EntityEventArgs<IRMPCommonLog>(data));
            }
		}
        /// <summary>
        /// 批量删除数据。
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteCommonLog(StringCollection priCollection)
        {
            return this.iRMPCommonLogEntity.DeleteRecord(priCollection);
        }
		#endregion

	}

}
