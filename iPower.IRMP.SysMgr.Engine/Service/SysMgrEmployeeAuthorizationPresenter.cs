//================================================================================
// FileName: SysMgrEmployeeAuthorizationPresenter.cs
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
	/// ISysMgrEmployeeAuthorizationView�ӿڡ�
	///</summary>
	public interface ISysMgrEmployeeAuthorizationView: IModuleView
	{
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
	}
    /// <summary>
    /// �б����ӿڡ�
    /// </summary>
    public interface ISysMgrEmployeeAuthorizationListView : ISysMgrEmployeeAuthorizationView
    {
        /// <summary>
        /// ��ȡ�û����ơ�
        /// </summary>
        string EmployeeName { get; }
    }
    /// <summary>
    /// �༭����ӿڡ�
    /// </summary>
    public interface ISysMgrEmployeeAuthorizationEditView : ISysMgrEmployeeAuthorizationView
    {
        /// <summary>
        /// ��ȡ�û�ID��
        /// </summary>
        GUIDEx EmployeeID { get; }
        /// <summary>
        /// ��û����Ȩ��ϵͳ��
        /// </summary>
        /// <param name="data"></param>
        void BindNotSelectedApp(IListControlsData data);
        /// <summary>
        /// ����Ȩ��ϵͳ��
        /// </summary>
        /// <param name="data"></param>
        void BindSelectedApp(IListControlsData data);
    }
    /// <summary>
    /// OrgPicker�ӿڡ�
    /// </summary>
    public interface ISysMgrEmployeeAuthorizationOrgPickerView : ISysMgrEmployeeAuthorizationView
    {
        /// <summary>
        /// �Ƿ�ʹ�ñ�������Դ��
        /// </summary>
        bool IsLocal { get; }
        /// <summary>
        /// ��ȡ�Ƿ��ѡ��
        /// </summary>
        bool MultiSelect { get; }
        /// <summary>
        /// ��ȡ����ֵ��
        /// </summary>
        string[] Values { get; }
        /// <summary>
        /// ��ȡ�û����ơ�
        /// </summary>
        string EmployeeName { get; }
        /// <summary>
        /// ����ʾ���ݡ�
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
	/// SysMgrEmployeeAuthorizationPresenter��Ϊ�ࡣ
	///</summary>
	public class SysMgrEmployeeAuthorizationPresenter: ModulePresenter<ISysMgrEmployeeAuthorizationView>
	{
		#region ��Ա���������캯����
        SysMgrEmployeeAuthorizationEntity sysMgrEmployeeAuthorizationEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SysMgrEmployeeAuthorizationPresenter(ISysMgrEmployeeAuthorizationView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.EmployeeAuthorization_ModuleID;
            this.sysMgrEmployeeAuthorizationEntity = new SysMgrEmployeeAuthorizationEntity();
		}
		#endregion

		#region ���ݲ���������
        /// <summary>
        /// ��ȡ�б�����Դ��
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
        /// �������ݡ�
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            #region Picker���ݴ���
            ISysMgrEmployeeAuthorizationOrgPickerView orgPickerView = this.View as ISysMgrEmployeeAuthorizationOrgPickerView;
            if (orgPickerView != null && orgPickerView.Values != null)
            {
                string[] emps = orgPickerView.Values;
                if (emps != null && emps.Length > 0)
                {
                    //��������Դ��
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
		///�༭ҳ��������ݡ�
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
        /// �������ݡ�
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
                    this.View.ShowMessage("�û�����Ϊ�գ�");
                }
                else if (appID == null || appID.Length == 0)
                {
                    this.View.ShowMessage("��Ȩϵͳ����Ϊ�գ�");
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
        /// ����ɾ�����ݡ�
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

        #region Org Picker��
        /// <summary>
        /// ��ѯ���ݡ�
        /// </summary>
        public void SeachOrgPicker()
        {
            ISysMgrEmployeeAuthorizationOrgPickerView pickerView = this.View as ISysMgrEmployeeAuthorizationOrgPickerView;
            if (pickerView != null)
            {
                if (pickerView.IsLocal)
                {
                    #region ��������Դ��
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
                    #region Org ϵͳ����Դ��
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
