//================================================================================
// FileName: SysMgrEmployeeAuthorizationPresenter.cs
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

using iPower.IRMP.Org;
namespace iPower.IRMP.SysMgr.Engine.Service
{
	///<summary>
	/// ISysMgrEmployeeAuthorizationView接口。
	///</summary>
	public interface ISysMgrEmployeeAuthorizationView: IModuleView
	{
        /// <summary>
        /// 显示信息。
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
	}
    /// <summary>
    /// 列表界面接口。
    /// </summary>
    public interface ISysMgrEmployeeAuthorizationListView : ISysMgrEmployeeAuthorizationView
    {
        /// <summary>
        /// 获取用户名称。
        /// </summary>
        string EmployeeName { get; }
    }
    /// <summary>
    /// 编辑界面接口。
    /// </summary>
    public interface ISysMgrEmployeeAuthorizationEditView : ISysMgrEmployeeAuthorizationView
    {
        /// <summary>
        /// 获取用户ID。
        /// </summary>
        GUIDEx EmployeeID { get; }
        /// <summary>
        /// 绑定没有授权的系统。
        /// </summary>
        /// <param name="data"></param>
        void BindNotSelectedApp(IListControlsData data);
        /// <summary>
        /// 绑定授权的系统。
        /// </summary>
        /// <param name="data"></param>
        void BindSelectedApp(IListControlsData data);
    }
    /// <summary>
    /// OrgPicker接口。
    /// </summary>
    public interface ISysMgrEmployeeAuthorizationOrgPickerView : ISysMgrEmployeeAuthorizationView
    {
        /// <summary>
        /// 是否使用本地数据源。
        /// </summary>
        bool IsLocal { get; }
        /// <summary>
        /// 获取是否多选。
        /// </summary>
        bool MultiSelect { get; }
        /// <summary>
        /// 获取传递值。
        /// </summary>
        string[] Values { get; }
        /// <summary>
        /// 获取用户名称。
        /// </summary>
        string EmployeeName { get; }
        /// <summary>
        /// 绑定显示数据。
        /// </summary>
        /// <param name="data"></param>
        void DisplayEmployeePanel(IListControlsData data);
        /// <summary>
        /// 显示查询结果。
        /// </summary>
        /// <param name="data"></param>
        void SearchEmployeeResult(IListControlsData data);
    }
	///<summary>
	/// SysMgrEmployeeAuthorizationPresenter行为类。
	///</summary>
	public class SysMgrEmployeeAuthorizationPresenter: ModulePresenter<ISysMgrEmployeeAuthorizationView>
	{
		#region 成员变量，构造函数。
        SysMgrEmployeeAuthorizationEntity sysMgrEmployeeAuthorizationEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrEmployeeAuthorizationPresenter(ISysMgrEmployeeAuthorizationView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.EmployeeAuthorization_ModuleID;
            this.sysMgrEmployeeAuthorizationEntity = new SysMgrEmployeeAuthorizationEntity();
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
                ISysMgrEmployeeAuthorizationListView listView = this.View as ISysMgrEmployeeAuthorizationListView;
                if (listView != null)
                {
                    return this.sysMgrEmployeeAuthorizationEntity.ListDataSource(listView.EmployeeName);
                }
                return null;
            }
        }
        /// <summary>
        /// 加载数据。
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            #region Picker数据处理。
            ISysMgrEmployeeAuthorizationOrgPickerView orgPickerView = this.View as ISysMgrEmployeeAuthorizationOrgPickerView;
            if (orgPickerView != null && orgPickerView.Values != null)
            {
                string[] emps = orgPickerView.Values;
                if (emps != null && emps.Length > 0)
                {
                    //本地数据源。
                    if (orgPickerView.IsLocal)
                        orgPickerView.DisplayEmployeePanel(this.sysMgrEmployeeAuthorizationEntity.BindAuthorizationEmployees(emps));
                    else
                    {
                        IOrgFactory facotry = this.ModuleConfig.OrgFactory;
                        if (facotry != null)
                        {
                            OrgEmployeeCollection employees = new OrgEmployeeCollection();
                            foreach (string eid in emps)
                            {
                                OrgEmployeeCollection employeeCollection = facotry.GetAllEmployee(eid);
                                if (employeeCollection != null && employeeCollection.Count > 0)
                                    employees.Add(employeeCollection[0]);
                            }
                            if (employees != null && employees.Count > 0)
                            {
                                foreach (OrgEmployee item in employees)
                                {
                                    item.EmployeeName = string.Format("{0}[{1}]", item.EmployeeName, item.EmployeeSign);
                                }
                            }
                            orgPickerView.DisplayEmployeePanel(new ListControlsDataSource("EmployeeName", "EmployeeID", employees));
                        }
                    }
                }
            }
            #endregion
        }
		///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<string[]>> handler)
		{
            ISysMgrEmployeeAuthorizationEditView editView = this.View as ISysMgrEmployeeAuthorizationEditView;
            if (editView != null && editView.EmployeeID.IsValid)
            {
                string employeeID = editView.EmployeeID;
                string employeeName = this.sysMgrEmployeeAuthorizationEntity.GetEmployeeName(employeeID);
                if (!string.IsNullOrEmpty(employeeName))
                {
                    handler(this, new EntityEventArgs<string[]>(new string[] { employeeID, employeeName }));
                    this.SearchEmployeeApp(employeeID);
                }
            }
		}
        /// <summary>
        /// 更新数据。
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="employeeName"></param>
        /// <param name="appID"></param>
        /// <returns></returns>
        public bool UpdateEmployeeAuthorization(GUIDEx employeeID,string employeeName,  string[] appID)
        {
            bool result = false;
            try
            {
                if (!employeeID.IsValid || string.IsNullOrEmpty(employeeName))
                {
                    this.View.ShowMessage("用户不能为空！");
                }
                else if (appID == null || appID.Length == 0)
                {
                    this.View.ShowMessage("授权系统不能为空！");
                }
                else
                {
                    List<string> list = new List<string>();
                    SysMgrEmployeeAuthorization empAuth = new SysMgrEmployeeAuthorization();
                    foreach (string app in appID)
                    {
                        if (!string.IsNullOrEmpty(app))
                        {
                            list.Add(app);
                            empAuth.AppAuthID = app;
                            empAuth.EmployeeID = employeeID;
                            empAuth.EmployeeName = employeeName;

                            result = this.sysMgrEmployeeAuthorizationEntity.UpdateRecord(empAuth);
                        }
                    }

                    if (result)
                    {
                        this.sysMgrEmployeeAuthorizationEntity.DeleteEmployeeAuthApp(employeeID, list.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                this.View.ShowMessage(e.Message);
            }
            return result;
        }
        /// <summary>
        /// 批量删除数据。
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteEmployeeAuthorization(StringCollection priCollection)
        {
            bool result = false;
            if (priCollection != null && priCollection.Count > 0)
            {
                string err = null;
                foreach (string p in priCollection)
                {
                    result = this.sysMgrEmployeeAuthorizationEntity.DeleteAuthorizationEmployee(p, out err);
                    if (!result && !string.IsNullOrEmpty(err))
                    {
                        this.View.ShowMessage(err);
                        break;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        public void SearchEmployeeApp(GUIDEx employeeID)
        {
            ISysMgrEmployeeAuthorizationEditView editView = this.View as ISysMgrEmployeeAuthorizationEditView;
            if (editView != null && employeeID.IsValid)
            {
                editView.BindNotSelectedApp(this.sysMgrEmployeeAuthorizationEntity.GetAuthorizationEmployeeApp(employeeID, false));
                editView.BindSelectedApp(this.sysMgrEmployeeAuthorizationEntity.GetAuthorizationEmployeeApp(employeeID, true));
            }
        }
		#endregion

        #region Org Picker。
        /// <summary>
        /// 查询数据。
        /// </summary>
        public void SeachOrgPicker()
        {
            ISysMgrEmployeeAuthorizationOrgPickerView pickerView = this.View as ISysMgrEmployeeAuthorizationOrgPickerView;
            if (pickerView != null)
            {
                if (pickerView.IsLocal)
                {
                    #region 本地数据源。
                    DataTable dtResult = this.sysMgrEmployeeAuthorizationEntity.GetAllAuthorizationEmployee(pickerView.EmployeeName);
                    if (dtResult != null)
                    {
                        if (pickerView.MultiSelect && pickerView.Values != null && pickerView.Values.Length > 0)
                        {
                            DataRow[] rows = dtResult.Select(string.Format("EmployeeID not in ('{0}')", string.Join("','", pickerView.Values)));
                            DataTable dt = dtResult.Clone();
                            foreach (DataRow row in rows)
                            {
                                dt.ImportRow(row);
                            }
                            pickerView.SearchEmployeeResult(new ListControlsDataSource("EmployeeName", "EmployeeID", dt));
                        }
                        else
                            pickerView.SearchEmployeeResult(new ListControlsDataSource("EmployeeName", "EmployeeID", dtResult));
                    }
                    #endregion
                }
                else
                {
                    #region Org 系统数据源。
                    IOrgFactory facotry = this.ModuleConfig.OrgFactory;
                    if (facotry != null)
                    {
                        OrgEmployeeCollection collection = facotry.GetAllEmployee(pickerView.EmployeeName);
                        if (pickerView.MultiSelect && pickerView.Values != null && pickerView.Values.Length > 0)
                        {
                            string[] array = pickerView.Values;
                            if (array != null)
                            {
                                foreach (string eid in array)
                                {
                                    OrgEmployee employee = collection[eid];
                                    if (employee != null)
                                    {
                                        collection.Remove(employee);
                                        if (collection.Count == 0)
                                            break;
                                    }
                                }
                            }
                        }

                        if (collection != null && collection.Count > 0)
                        {
                            foreach (OrgEmployee item in collection)
                                item.EmployeeName = string.Format("{0}[{1}]", item.EmployeeName, item.EmployeeSign);
                        }

                        pickerView.SearchEmployeeResult(new ListControlsDataSource("EmployeeName", "EmployeeID", collection));
                    }
                    #endregion
                }
            }
        }
        #endregion
    }

}
