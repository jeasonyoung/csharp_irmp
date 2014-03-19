//================================================================================
// FileName: SysMgrLimitLoginPresenter.cs
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
	/// ISysMgrLimitLoginView接口。
	///</summary>
	public interface ISysMgrLimitLoginView: IModuleView
	{

	}
    /// <summary>
    /// 列表界面接口。
    /// </summary>
    public interface ISysMgrLimitLoginListView : ISysMgrLimitLoginView
    {
        /// <summary>
        /// 获取用户名称。
        /// </summary>
        string EmployeeName { get; }
    }
    /// <summary>
    /// 编辑界面接口。
    /// </summary>
    public interface ISysMgrLimitLoginEditView : ISysMgrLimitLoginView
    {
        /// <summary>
        /// 获取限制ID。
        /// </summary>
        GUIDEx LimitID { get; }
        /// <summary>
        /// 显示消息。
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
    }
		
	///<summary>
	/// SysMgrLimitLoginPresenter行为类。
	///</summary>
	public class SysMgrLimitLoginPresenter: ModulePresenter<ISysMgrLimitLoginView>
	{
		#region 成员变量，构造函数。
        SysMgrLimitLoginEntity sysMgrLimitLoginEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrLimitLoginPresenter(ISysMgrLimitLoginView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.LimitLogin_ModuleID;
            this.sysMgrLimitLoginEntity = new SysMgrLimitLoginEntity();
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
                ISysMgrLimitLoginListView listView = this.View as ISysMgrLimitLoginListView;
                if (listView != null)
                {
                    return this.sysMgrLimitLoginEntity.GetAllRecord(string.Format("EmployeeName like '%{0}%'", listView.EmployeeName));
                }
                return null;
            }
        }
		///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<SysMgrLimitLogin>> handler)
		{
            ISysMgrLimitLoginEditView editView = this.View as ISysMgrLimitLoginEditView;
            if (editView != null&& editView.LimitID.IsValid)
            {
                SysMgrLimitLogin data = new SysMgrLimitLogin();
                data.LimitID = editView.LimitID;
                if (this.sysMgrLimitLoginEntity.LoadRecord(ref data))
                    handler(this, new EntityEventArgs<SysMgrLimitLogin>(data));
            }
		}
        /// <summary>
        /// 更新数据。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateLimitLogin(SysMgrLimitLogin data)
        {
            bool result = false;
            if (data != null)
            {
                try
                {
                    result = this.sysMgrLimitLoginEntity.UpdateRecord(data);
                }
                catch (Exception e)
                {
                    ISysMgrLimitLoginEditView editView = this.View as ISysMgrLimitLoginEditView;
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
        public bool BatchDeleteLimitLogin(StringCollection priCollection)
        {
            return this.sysMgrLimitLoginEntity.DeleteRecord(priCollection);
        }
		#endregion

	}

}
