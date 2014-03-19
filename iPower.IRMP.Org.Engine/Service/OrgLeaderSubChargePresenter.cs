//================================================================================
// FileName: OrgLeaderSubChargePresenter.cs
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
using Domain = iPower.IRMP.Org.Engine.Domain;
using iPower.IRMP.Org.Engine.Persistence;
namespace iPower.IRMP.Org.Engine.Service
{
	///<summary>
	/// IOrgLeaderSubChargeView接口。
	///</summary>
	public interface IOrgLeaderSubChargeView: IModuleView
	{

	}
    /// <summary>
    /// 列表界面接口。
    /// </summary>
    public interface IOrgLeaderSubChargeListView : IOrgLeaderSubChargeView
    {
        /// <summary>
        /// 获取用户姓名。
        /// </summary>
        string EmployeeName { get; }
        /// <summary>
        /// 获取部门名称。
        /// </summary>
        string DepartmentName { get; }
    }
    /// <summary>
    /// 编辑界面接口。
    /// </summary>
    public interface IOrgLeaderSubChargeEditView : IOrgLeaderSubChargeView
    {
        /// <summary>
        /// 获取用户ID。
        /// </summary>
        GUIDEx EmployeeID { get; }
        /// <summary>
        /// 绑定部门。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="depCollection"></param>
        void BindDepartment(IListControlsTreeViewData data, StringCollection depCollection);
    }
		
	///<summary>
	/// OrgLeaderSubChargePresenter行为类。
	///</summary>
	public class OrgLeaderSubChargePresenter: ModulePresenter<IOrgLeaderSubChargeView>
	{
		#region 成员变量，构造函数。
        OrgLeaderSubChargeEntity orgLeaderSubChargeEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public OrgLeaderSubChargePresenter(IOrgLeaderSubChargeView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.LeaderSubCharge_ModuleID;
            this.orgLeaderSubChargeEntity = new OrgLeaderSubChargeEntity();
		}
		#endregion

        /// <summary>
        /// 获取列表数据。
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                IOrgLeaderSubChargeListView listView = this.View as IOrgLeaderSubChargeListView;
                if (listView != null)
                    return this.orgLeaderSubChargeEntity.ListDataSource(listView.DepartmentName, listView.EmployeeName);
                return null;
            }
        }

		#region 数据操作函数。
        /// <summary>
        /// 批量删除。
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteLeaderSubCharge(StringCollection priCollection)
        {
            bool result = false;
            foreach (string emp in priCollection)
            {
                result = this.orgLeaderSubChargeEntity.DeleteLeaderSubCharge(emp);
                if (!result)
                    break;
            }
            return result;
        }
        /// <summary>
        ///  跟新数据。
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="deptCollection"></param>
        /// <returns></returns>
        public bool UpdateLeaderSubCharge(GUIDEx employeeID, StringCollection deptCollection)
        {
            return this.orgLeaderSubChargeEntity.UpdateLeaderSubCharge(employeeID, deptCollection);
        }
		///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<string[]>> handler)
		{
            IOrgLeaderSubChargeEditView editView = this.View as IOrgLeaderSubChargeEditView;
            if (editView != null && editView.EmployeeID.IsValid)
            {
                Domain.OrgEmployee data = new Domain.OrgEmployee();
                data.EmployeeID = editView.EmployeeID;
                OrgEmployeeEntity employeeEntity = new OrgEmployeeEntity();
                if (employeeEntity.LoadRecord(ref data))
                {
                    string[] emp = new string[] { data.EmployeeID, data.EmployeeName };
                    handler(this, new EntityEventArgs<string[]>(emp));
                    editView.BindDepartment(new OrgDepartmentEntity().Department,
                                            this.orgLeaderSubChargeEntity.LoadLeaderSubCharge(data.EmployeeID));
                }
            }
		}
        /// <summary>
        /// 重载加载数据。
        /// </summary>
        protected override void PreViewLoadData()
        {
            IOrgLeaderSubChargeEditView editView = this.View as IOrgLeaderSubChargeEditView;
            if (editView != null)
            {
                editView.BindDepartment(new OrgDepartmentEntity().Department, null);
            }
            base.PreViewLoadData();
        }
		#endregion

	}

}
