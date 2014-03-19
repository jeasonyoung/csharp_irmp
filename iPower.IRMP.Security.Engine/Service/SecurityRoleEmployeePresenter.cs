//================================================================================
// FileName: SecurityRoleEmployeePresenter.cs
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
using iPower.Data;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.Platform.Engine.DataSource;

using iPower.IRMP.Org;
using iPower.IRMP.Security.Engine.Domain;
using iPower.IRMP.Security.Engine.Persistence;
namespace iPower.IRMP.Security.Engine.Service
{
	///<summary>
	/// ISecurityRoleEmployeeView接口。
	///</summary>
	public interface ISecurityRoleEmployeeView: IModuleView
	{

	}
    /// <summary>
    /// 列表界面接口。
    /// </summary>
    public interface ISecurityRoleEmployeeListView : ISecurityRoleEmployeeView
    {
        /// <summary>
        /// 获取角色名称。
        /// </summary>
        string RoleName { get; }
        /// <summary>
        /// 获取用户名称。
        /// </summary>
        string EmployeeName { get; }
    }
    /// <summary>
    /// 编辑界面接口。
    /// </summary>
    public interface ISecurityRoleEmployeeEditView : ISecurityRoleEmployeeView
    {
        /// <summary>
        /// 获取角色ID。
        /// </summary>
        GUIDEx RoleID { get; }
        /// <summary>
        /// 绑定用户。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        void BindEmployee(string[] text, string[] value);
    }
    /// <summary>
    /// 
    /// </summary>
    public interface ISecurityRoleEmployeePickerView : ISecurityRoleEmployeeView
    {
        /// <summary>
        /// 获取是否多选。
        /// </summary>
        bool MultiSelect { get; }
        /// <summary>
        /// 获取数据值。
        /// </summary>
        string[] Values { get; }
        /// <summary>
        /// 获取部门名称。
        /// </summary>
        string DepartmentName { get; }
        /// <summary>
        /// 获取岗位名称。
        /// </summary>
        string PostName { get; }
        /// <summary>
        /// 获取用户名称。
        /// </summary>
        string EmployeeName { get; }
        /// <summary>
        /// 显示用户信息。
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
	/// SecurityRoleEmployeePresenter行为类。
	///</summary>
	public class SecurityRoleEmployeePresenter: ModulePresenter<ISecurityRoleEmployeeView>
	{
		#region 成员变量，构造函数。
        SecurityRoleEmployeeEntity securityRoleEmployeeEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SecurityRoleEmployeePresenter(ISecurityRoleEmployeeView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.RoleEmployee_ModuleID;
            this.securityRoleEmployeeEntity = new SecurityRoleEmployeeEntity();
		}
		#endregion

		#region 数据操作函数。
        /// <summary>
        /// 列表数据源。
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                ISecurityRoleEmployeeListView listView = this.View as ISecurityRoleEmployeeListView;
                if (listView != null)
                    return this.securityRoleEmployeeEntity.ListDataSource(listView.RoleName, listView.EmployeeName);
                return null;
            }
        }
        /// <summary>
        /// 加载数据。
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            ISecurityRoleEmployeePickerView pickerView = this.View as ISecurityRoleEmployeePickerView;
            if (pickerView != null && pickerView.Values != null)
            {
                pickerView.DisplayEmployeePanel(this.securityRoleEmployeeEntity.BindEmployees(pickerView.Values));
            }
        }
		///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<string[]>> handler)
		{
            ISecurityRoleEmployeeEditView editView = this.View as ISecurityRoleEmployeeEditView;
            if (editView != null && editView.RoleID.IsValid)
            {
                iPower.IRMP.Security.Engine.Domain.SecurityRole data = new iPower.IRMP.Security.Engine.Domain.SecurityRole();
                data.RoleID = editView.RoleID;
                SecurityRoleEntity securityRoleEntity = new SecurityRoleEntity();
                if (securityRoleEntity.LoadRecord(ref data))
                {
                    handler(this, new EntityEventArgs<string[]>(new string[] { data.RoleID, data.RoleName }));
                    this.ChangeRole(data.RoleID);
                }
            }
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleID"></param>
        public void ChangeRole(GUIDEx roleID)
        {
            ISecurityRoleEmployeeEditView editView = this.View as ISecurityRoleEmployeeEditView;
            if (editView != null)
            {
                string[] text = null, value = null;
                this.securityRoleEmployeeEntity.GetEmployees(roleID, out text, out value);
                editView.BindEmployee(text, value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <param name="emps"></param>
        /// <returns></returns>
        public bool UpdateRoleEmployee(GUIDEx role, string[] emps)
        {
            return this.securityRoleEmployeeEntity.UpdateRoleEmployee(role, emps);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool BatchDeleteRoleEmployee(StringCollection roles)
        {
            return this.securityRoleEmployeeEntity.BatchDeleteRoleEmployee(roles);
        }
		#endregion

        #region PickerView。
        /// <summary>
        /// 查询数据。
        /// </summary>
        public void SeachEmployee()
        {
            ISecurityRoleEmployeePickerView pickerView = this.View as ISecurityRoleEmployeePickerView;
            if (pickerView != null)
            {
                IOrgFactory facotry = this.ModuleConfig.OrgFactory;
                if (facotry != null)
                {
                    OrgEmployeeCollection employeeCollection = facotry.GetAllEmployee(pickerView.EmployeeName);

                    #region 部门检索。
                    if (!string.IsNullOrEmpty(pickerView.DepartmentName) && employeeCollection != null)
                    {
                        OrgEmployeeCollection resultCollection = new OrgEmployeeCollection();
                        OrgDepartmentCollection departmentCollection = facotry.GetAllDepartment(pickerView.DepartmentName);
                        if (departmentCollection != null && departmentCollection.Count > 0)
                        {
                            foreach (OrgDepartment d in departmentCollection)
                            {
                                OrgEmployeeCollection employees = employeeCollection.FindByDepartment(d.DepartmentID);
                                if (employees != null && employees.Count > 0)
                                {
                                    foreach (OrgEmployee e in employees)
                                        resultCollection.Add(e);
                                }
                            }
                        }
                        employeeCollection = new OrgEmployeeCollection();
                        if (resultCollection.Count > 0)
                        {
                            foreach (OrgEmployee e in resultCollection)
                                employeeCollection.Add(e);
                        }
                    }
                    #endregion

                    #region 岗位检索。
                    if (!string.IsNullOrEmpty(pickerView.PostName) && employeeCollection != null)
                    {
                        OrgEmployeeCollection resultCollection = new OrgEmployeeCollection();
                        OrgPostCollection postCollection = facotry.GetAllPost(pickerView.PostName);
                        if (postCollection != null && postCollection.Count > 0)
                        {
                            foreach (OrgPost p in postCollection)
                            {
                                OrgEmployeeCollection employees = employeeCollection.FindByPost(p.PostID);
                                if (employees != null && employees.Count > 0)
                                {
                                    foreach (OrgEmployee e in employees)
                                        resultCollection.Add(e);
                                }
                            }
                        }
                        employeeCollection = new OrgEmployeeCollection();
                        if (resultCollection.Count > 0)
                        {
                            foreach (OrgEmployee e in resultCollection)
                                employeeCollection.Add(e);
                        }
                    }
                    #endregion

                    #region 多选排除。
                    if (pickerView.MultiSelect && employeeCollection != null)
                    {
                        string[] array = pickerView.Values;
                        if (array != null && array.Length > 0 && employeeCollection.Count > 0)
                        {
                            foreach (string id in array)
                            {
                                OrgEmployee item = employeeCollection[id];
                                if (item != null)
                                {
                                    employeeCollection.Remove(item);
                                    if (employeeCollection.Count == 0)
                                        break;
                                }
                            }
                        }
                    }
                    #endregion

                    if (employeeCollection != null && employeeCollection.Count > 0)
                    {
                        foreach (OrgEmployee e in employeeCollection)
                        {
                            OrgDepartmentCollection depts = facotry.GetAllDepartment(e.DepartmentID);
                            e.EmployeeName = string.Format("{0}[{1}]({2})", e.EmployeeName, e.EmployeeSign, depts == null ? string.Empty : depts[0].DepartmentName);
                        }
                    }

                    pickerView.SearchEmployeeResult(new ListControlsDataSource("EmployeeName", "EmployeeID", employeeCollection));
                }
            }
        }
        #endregion
    }

}
