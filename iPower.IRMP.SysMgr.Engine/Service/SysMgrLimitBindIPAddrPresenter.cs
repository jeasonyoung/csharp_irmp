//================================================================================
// FileName: SysMgrLimitBindIPAddrPresenter.cs
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
	/// ISysMgrLimitBindIPAddrView接口。
	///</summary>
	public interface ISysMgrLimitBindIPAddrView: IModuleView
	{

	}
    /// <summary>
    /// 列表界面接口。
    /// </summary>
    public interface ISysMgrLimitBindIPAddrListView : ISysMgrLimitBindIPAddrView
    {
        /// <summary>
        /// 获取用户名称。
        /// </summary>
        string EmployeeName { get; }
    }
    /// <summary>
    /// 编辑界面接口。
    /// </summary>
    public interface ISysMgrLimitBindIPAddrEditView : ISysMgrLimitBindIPAddrView
    {
        /// <summary>
        ///获取绑定ID。
        /// </summary>
        GUIDEx BindID { get; }
        /// <summary>
        /// 显示数据。
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
    }
	///<summary>
	/// SysMgrLimitBindIPAddrPresenter行为类。
	///</summary>
	public class SysMgrLimitBindIPAddrPresenter: ModulePresenter<ISysMgrLimitBindIPAddrView>
	{
		#region 成员变量，构造函数。
        SysMgrLimitBindIPAddrEntity sysMgrLimitBindIPAddrEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrLimitBindIPAddrPresenter(ISysMgrLimitBindIPAddrView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.LimitBindIPAddr_ModuleID;
            this.sysMgrLimitBindIPAddrEntity = new SysMgrLimitBindIPAddrEntity();
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
                ISysMgrLimitBindIPAddrListView listView = this.View as ISysMgrLimitBindIPAddrListView;
                if (listView != null)
                {
                    DataTable dtSource = this.sysMgrLimitBindIPAddrEntity.GetAllRecord(string.Format("EmployeeName like '%{0}%'", listView.EmployeeName));
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
		public void LoadEntityData(EventHandler<EntityEventArgs<SysMgrLimitBindIPAddr>> handler)
		{
            ISysMgrLimitBindIPAddrEditView editView = this.View as ISysMgrLimitBindIPAddrEditView;
            if (editView != null && editView.BindID.IsValid)
            {
                SysMgrLimitBindIPAddr data = new SysMgrLimitBindIPAddr();
                data.BindID = editView.BindID;
                if (this.sysMgrLimitBindIPAddrEntity.LoadRecord(ref data))
                {
                    if (!data.EmployeeID.IsValid)
                        data.EmployeeName = "[全局]";
                    handler(this, new EntityEventArgs<SysMgrLimitBindIPAddr>(data));
                }
            }
		}
        /// <summary>
        /// 更新数据。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateLimitBindIPAddr(SysMgrLimitBindIPAddr data)
        {
            bool result = false;
            try
            {
                if (data != null)
                {
                    if (!data.EmployeeID.IsValid)
                        data.EmployeeName = string.Empty;
                    return this.sysMgrLimitBindIPAddrEntity.UpdateRecord(data);
                }
            }
            catch (Exception e)
            {
                ISysMgrLimitBindIPAddrEditView editView = this.View as ISysMgrLimitBindIPAddrEditView;
                if (editView != null)
                    editView.ShowMessage(e.Message);
            }
            return result;
        }
        /// <summary>
        /// 批量删除数据。
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteLimitBindIPAddr(StringCollection priCollection)
        {
            if (priCollection != null && priCollection.Count > 0)
            {
                return this.sysMgrLimitBindIPAddrEntity.DeleteRecord(priCollection);
            }
            return false;
        }
		#endregion

	}

}
