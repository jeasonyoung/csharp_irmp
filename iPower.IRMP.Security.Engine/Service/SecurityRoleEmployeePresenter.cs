//================================================================================
// FileName: SecurityRoleEmployeePresenter.cs
// Desc:
// Called by
// Auth: �������ɴ����������Զ����ɡ�
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
	/// ISecurityRoleEmployeeView�ӿڡ�
	///</summary>
	public interface ISecurityRoleEmployeeView: IModuleView
	{

	}
    /// <summary>
    /// �б����ӿڡ�
    /// </summary>
    public interface ISecurityRoleEmployeeListView : ISecurityRoleEmployeeView
    {
        /// <summary>
        /// ��ȡ��ɫ���ơ�
        /// </summary>
        string RoleName { get; }
        /// <summary>
        /// ��ȡ�û����ơ�
        /// </summary>
        string EmployeeName { get; }
    }
    /// <summary>
    /// �༭����ӿڡ�
    /// </summary>
    public interface ISecurityRoleEmployeeEditView : ISecurityRoleEmployeeView
    {
        /// <summary>
        /// ��ȡ��ɫID��
        /// </summary>
        GUIDEx RoleID { get; }
        /// <summary>
        /// ���û���
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
        /// ��ȡ�Ƿ��ѡ��
        /// </summary>
        bool MultiSelect { get; }
        /// <summary>
        /// ��ȡ����ֵ��
        /// </summary>
        string[] Values { get; }
        /// <summary>
        /// ��ȡ�������ơ�
        /// </summary>
        string DepartmentName { get; }
        /// <summary>
        /// ��ȡ��λ���ơ�
        /// </summary>
        string PostName { get; }
        /// <summary>
        /// ��ȡ�û����ơ�
        /// </summary>
        string EmployeeName { get; }
        /// <summary>
        /// ��ʾ�û���Ϣ��
        /// </summary>
        /// <param name="data"></param>
        void DisplayEmployeePanel(IListControlsData data);
        /// <summary>
        /// ��ʾ��ѯ�����
        /// </summary>
        /// <param name="data"></param>
        void SearchEmployeeResult(IListControlsData data);
    }
	///<summary>
	/// SecurityRoleEmployeePresenter��Ϊ�ࡣ
	///</summary>
	public class SecurityRoleEmployeePresenter: ModulePresenter<ISecurityRoleEmployeeView>
	{
		#region ��Ա���������캯����
        SecurityRoleEmployeeEntity securityRoleEmployeeEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SecurityRoleEmployeePresenter(ISecurityRoleEmployeeView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.RoleEmployee_ModuleID;
            this.securityRoleEmployeeEntity = new SecurityRoleEmployeeEntity();
		}
		#endregion

		#region ���ݲ���������
        /// <summary>
        /// �б�����Դ��
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
        /// �������ݡ�
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
		///�༭ҳ��������ݡ�
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

        #region PickerView��
        /// <summary>
        /// ��ѯ���ݡ�
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

                    #region ���ż�����
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

                    #region ��λ������
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

                    #region ��ѡ�ų���
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
