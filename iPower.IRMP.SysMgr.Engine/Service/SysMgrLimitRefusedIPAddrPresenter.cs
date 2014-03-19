//================================================================================
// FileName: SysMgrLimitRefusedIPAddrPresenter.cs
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
	/// ISysMgrLimitRefusedIPAddrView接口。
	///</summary>
	public interface ISysMgrLimitRefusedIPAddrView: IModuleView
	{

	}
    /// <summary>
    /// 列表界面接口。
    /// </summary>
    public interface ISysMgrLimitRefusedIPAddrListView : ISysMgrLimitRefusedIPAddrView
    {
        /// <summary>
        /// 获取用户名称。
        /// </summary>
        string EmployeeName
        {
            get;
        }
    }
    /// <summary>
    /// 编辑界面接口。
    /// </summary>
    public interface ISysMgrLimitRefusedIPAddrEditView : ISysMgrLimitRefusedIPAddrView
    {
        /// <summary>
        /// 获取拒绝ID。
        /// </summary>
        GUIDEx RefusedID { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
    }
		
	///<summary>
	/// SysMgrLimitRefusedIPAddrPresenter行为类。
	///</summary>
	public class SysMgrLimitRefusedIPAddrPresenter: ModulePresenter<ISysMgrLimitRefusedIPAddrView>
	{
		#region 成员变量，构造函数。
        SysMgrLimitRefusedIPAddrEntity sysMgrLimitRefusedIPAddrEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrLimitRefusedIPAddrPresenter(ISysMgrLimitRefusedIPAddrView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.LimitRefusedIPAddr_ModuleID;
            this.sysMgrLimitRefusedIPAddrEntity = new SysMgrLimitRefusedIPAddrEntity();
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
                ISysMgrLimitRefusedIPAddrListView listView = this.View as ISysMgrLimitRefusedIPAddrListView;
                if (listView != null)
                {
                    DataTable dtSource = this.sysMgrLimitRefusedIPAddrEntity.GetAllRecord(string.Format("EmployeeName like '%{0}%'", listView.EmployeeName));
                    if (dtSource != null)
                    {
                        foreach (DataRow row in dtSource.Rows)
                        {
                            string strEmp = Convert.ToString(row["EmployeeName"]);
                            if (string.IsNullOrEmpty(strEmp))
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
		///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<SysMgrLimitRefusedIPAddr>> handler)
		{
            ISysMgrLimitRefusedIPAddrEditView editView = this.View as ISysMgrLimitRefusedIPAddrEditView;
            if (editView != null && editView.RefusedID.IsValid)
            {
                SysMgrLimitRefusedIPAddr data = new SysMgrLimitRefusedIPAddr();
                data.RefusedID = editView.RefusedID;
                if (this.sysMgrLimitRefusedIPAddrEntity.LoadRecord(ref data))
                {
                    if (!data.EmployeeID.IsValid)
                        data.EmployeeName = "[全局]";
                    handler(this, new EntityEventArgs<SysMgrLimitRefusedIPAddr>(data));
                }
            }
		}
        /// <summary>
        /// 更新数据。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateLimitRefusedIPAddr(SysMgrLimitRefusedIPAddr data)
        {
            if (data != null)
            {
                try
                {
                    if (!data.EmployeeID.IsValid)
                        data.EmployeeName = string.Empty;
                    return this.sysMgrLimitRefusedIPAddrEntity.UpdateRecord(data);
                }
                catch (Exception e)
                {
                    ISysMgrLimitRefusedIPAddrEditView editView = this.View as ISysMgrLimitRefusedIPAddrEditView;
                    if (editView != null)
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
        public bool BatchDeleteLimitRefusedIPAddr(StringCollection priCollection)
        {
            return this.sysMgrLimitRefusedIPAddrEntity.DeleteRecord(priCollection);
        }
		#endregion

	}

}
