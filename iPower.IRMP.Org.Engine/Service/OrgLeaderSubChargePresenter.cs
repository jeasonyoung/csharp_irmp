//================================================================================
// FileName: OrgLeaderSubChargePresenter.cs
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
	/// IOrgLeaderSubChargeView�ӿڡ�
	///</summary>
	public interface IOrgLeaderSubChargeView: IModuleView
	{

	}
    /// <summary>
    /// �б����ӿڡ�
    /// </summary>
    public interface IOrgLeaderSubChargeListView : IOrgLeaderSubChargeView
    {
        /// <summary>
        /// ��ȡ�û�������
        /// </summary>
        string EmployeeName { get; }
        /// <summary>
        /// ��ȡ�������ơ�
        /// </summary>
        string DepartmentName { get; }
    }
    /// <summary>
    /// �༭����ӿڡ�
    /// </summary>
    public interface IOrgLeaderSubChargeEditView : IOrgLeaderSubChargeView
    {
        /// <summary>
        /// ��ȡ�û�ID��
        /// </summary>
        GUIDEx EmployeeID { get; }
        /// <summary>
        /// �󶨲��š�
        /// </summary>
        /// <param name="data"></param>
        /// <param name="depCollection"></param>
        void BindDepartment(IListControlsTreeViewData data, StringCollection depCollection);
    }
		
	///<summary>
	/// OrgLeaderSubChargePresenter��Ϊ�ࡣ
	///</summary>
	public class OrgLeaderSubChargePresenter: ModulePresenter<IOrgLeaderSubChargeView>
	{
		#region ��Ա���������캯����
        OrgLeaderSubChargeEntity orgLeaderSubChargeEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public OrgLeaderSubChargePresenter(IOrgLeaderSubChargeView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.LeaderSubCharge_ModuleID;
            this.orgLeaderSubChargeEntity = new OrgLeaderSubChargeEntity();
		}
		#endregion

        /// <summary>
        /// ��ȡ�б����ݡ�
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

		#region ���ݲ���������
        /// <summary>
        /// ����ɾ����
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
        ///  �������ݡ�
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="deptCollection"></param>
        /// <returns></returns>
        public bool UpdateLeaderSubCharge(GUIDEx employeeID, StringCollection deptCollection)
        {
            return this.orgLeaderSubChargeEntity.UpdateLeaderSubCharge(employeeID, deptCollection);
        }
		///<summary>
		///�༭ҳ��������ݡ�
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
        /// ���ؼ������ݡ�
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
