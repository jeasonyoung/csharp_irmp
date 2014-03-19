//================================================================================
// FileName: OrgEmployeePresenter.cs
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
using Domain = iPower.IRMP.Org.Engine.Domain;
using iPower.IRMP.Org.Engine.Persistence;
namespace iPower.IRMP.Org.Engine.Service
{
	///<summary>
	/// IOrgEmployeeView�ӿڡ�
	///</summary>
	public interface IOrgEmployeeView: IModuleView
	{
        /// <summary>
        /// �󶨲��š�
        /// </summary>
        /// <param name="data"></param>
        void BindDepartment(IListControlsTreeViewData data);
	}
    /// <summary>
    /// �б����ӿڡ�
    /// </summary>
    public interface IOrgEmployeeListView : IOrgEmployeeView
    {
        /// <summary>
        /// ��ȡ�û����ơ�
        /// </summary>
        string EmployeeName { get; }
        /// <summary>
        /// ��ȡ����ID��
        /// </summary>
        string DepartmentID { get; }
        /// <summary>
        /// �������Ÿ�λ����
        /// </summary>
        /// <param name="data"></param>
        void BuildDepartPostTreeView(IListControlsTreeViewData data);
        /// <summary>
        /// ��ʾ���ݡ�
        /// </summary>
        /// <param name="content"></param>
        void ShowMessage(string content);
    }
    /// <summary>
    /// �༭����ӿڡ�
    /// </summary>
    public interface IOrgEmployeeEditView : IOrgEmployeeView
    {
        /// <summary>
        /// ��ȡ�û�ID��
        /// </summary>
        GUIDEx EmployeeID { get; }
        /// <summary>
        /// ��ȡ����ID��
        /// </summary>
        GUIDEx DepartmentID { get; }
        /// <summary>
        /// ���Ա�
        /// </summary>
        /// <param name="data"></param>
        void BindSex(IListControlsData data);
        /// <summary>
        /// ���û�״̬��
        /// </summary>
        /// <param name="data"></param>
        void BindEmployeeStatus(IListControlsData data);
        /// <summary>
        /// �󶨸�λ��
        /// </summary>
        /// <param name="data"></param>
        void BindPost(IListControlsTreeViewData data);
    }
		
	///<summary>
	/// OrgEmployeePresenter��Ϊ�ࡣ
	///</summary>
	public class OrgEmployeePresenter: ModulePresenter<IOrgEmployeeView>
	{
		#region ��Ա���������캯����
        OrgEmployeeEntity orgEmployeeEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public OrgEmployeePresenter(IOrgEmployeeView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Employee_ModuleID;
            this.orgEmployeeEntity = new OrgEmployeeEntity();
		}
		#endregion

        /// <summary>
        /// ��ȡ�б����ݡ�
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                IOrgEmployeeListView listView = this.View as IOrgEmployeeListView;
                if (listView != null)
                {
                    DataTable dtSouce = this.orgEmployeeEntity.ListViewDataSource(listView.EmployeeName, listView.DepartmentID);
                    if (dtSouce != null)
                    {
                        dtSouce.Columns.Add("GenderName", typeof(string));
                        dtSouce.Columns.Add("EmployeeStatusName", typeof(string));

                        foreach (DataRow row in dtSouce.Rows)
                        {
                            row["GenderName"] = this.GetEnumMemberName(typeof(EnumGender), Convert.ToInt32(row["Gender"]));
                            row["EmployeeStatusName"] = this.GetEnumMemberName(typeof(EnumStatus), Convert.ToInt32(row["EmployeeStatus"]));
                        }

                        return dtSouce.Copy();
                    }
                }
                return null;
            }
        }

        #region ���ء�
        /// <summary>
        /// �������ݡ�
        /// </summary>
        protected override void PreViewLoadData()
        {
            IOrgEmployeeListView listView = this.View as IOrgEmployeeListView;
            if (listView != null)
            {
                this.View.BindDepartment(new OrgPostEntity().DepartmentPost(null));
                listView.BuildDepartPostTreeView(new OrgPostEntity().DepartmentPost(null));
            }
            IOrgEmployeeEditView editView = this.View as IOrgEmployeeEditView;
            if (editView != null)
            {
                editView.BindDepartment(new OrgDepartmentEntity().Department);
                editView.BindEmployeeStatus(this.EnumDataSource(typeof(EnumStatus)));
                editView.BindSex(this.EnumDataSource(typeof(EnumGender)));
                this.ChangeDepartmentToPost(editView.DepartmentID);
            }
            base.PreViewLoadData();
        }
        #endregion

        #region ���ݲ���������
        /// <summary>
        /// 
        /// </summary>
        public void ChangeDepartmentToPost(string departmentID)
        {
            IOrgEmployeeEditView editView = this.View as IOrgEmployeeEditView;
            if(editView != null)
                editView.BindPost(new OrgPostEntity().Post(departmentID));
        }
        ///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
        public void LoadEntityData(EventHandler<EntityEventArgs<Domain.OrgEmployee>> handler)
		{
            IOrgEmployeeEditView editView = this.View as IOrgEmployeeEditView;
            if (editView != null)
            {
                Domain.OrgEmployee data = new Domain.OrgEmployee();
                data.EmployeeID = editView.EmployeeID;
                if (this.orgEmployeeEntity.LoadRecord(ref data))
                {
                    this.ChangeDepartmentToPost(data.DepartmentID);
                    handler(this, new EntityEventArgs<Domain.OrgEmployee>(data));
                }
            }
		}
        /// <summary>
        /// �����û����ݡ�
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateEmployee(Domain.OrgEmployee data)
        {
            if (data != null)
            {
                if (!string.IsNullOrEmpty(data.EmployeePassword))
                    data.PasswordDate = DateTime.Now;
                if (!string.IsNullOrEmpty(data.EmployeePassword2))
                    data.PasswordDate2 = DateTime.Now;
            }
            return this.orgEmployeeEntity.UpdateRecord(data);
        }
        /// <summary>
        /// ����ɾ���û����ݡ�
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteEmployee(StringCollection priCollection)
        {
            bool result = false;
            string err = string.Empty;
            IOrgEmployeeListView listView = this.View as IOrgEmployeeListView;
            foreach (string emp in priCollection)
            {
                result = this.orgEmployeeEntity.DeleteEmployee(emp, out err);
                if (!result)
                {
                    if (listView != null)
                        listView.ShowMessage(err);
                    break;
                }
            }
            return result;
        }
		#endregion

	}

}
