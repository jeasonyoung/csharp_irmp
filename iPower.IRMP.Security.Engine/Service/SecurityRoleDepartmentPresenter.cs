//================================================================================
// FileName: SecurityRoleDepartmentPresenter.cs
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
using iPower.IRMP.Org;
using iPower.IRMP.Security.Engine.Domain;
using iPower.IRMP.Security.Engine.Persistence;
namespace iPower.IRMP.Security.Engine.Service
{
	///<summary>
	/// ISecurityRoleDepartmentView�ӿڡ�
	///</summary>
	public interface ISecurityRoleDepartmentView: IModuleView
	{

	}
    /// <summary>
    /// �б����ӿڡ�
    /// </summary>
    public interface ISecurityRoleDepartmentListView : ISecurityRoleDepartmentView
    {
        /// <summary>
        /// ��ȡ��ɫ���ơ�
        /// </summary>
        string RoleName { get; }
        /// <summary>
        /// ��ȡ�������ơ�
        /// </summary>
        string DepartmentName { get; }
    }
    /// <summary>
    /// �༭����ӿڡ�
    /// </summary>
    public interface ISecurityRoleDepartmentEditView : ISecurityRoleDepartmentView
    {
        /// <summary>
        /// ��ȡ��ɫID��
        /// </summary>
        GUIDEx RoleID { get; }
        /// <summary>
        /// �󶨲������ݡ�
        /// </summary>
        /// <param name="data">����Դ��</param>
        void BindDepartment(IListControlsTreeViewData data);
        /// <summary>
        /// ѡ�С�
        /// </summary>
        /// <param name="selected"></param>
        void DepartmentSelected(StringCollection selected);
    }
	///<summary>
	/// SecurityRoleDepartmentPresenter��Ϊ�ࡣ
	///</summary>
	public class SecurityRoleDepartmentPresenter: ModulePresenter<ISecurityRoleDepartmentView>
	{
		#region ��Ա���������캯����
        SecurityRoleDepartmentEntity securityRoleDepartmentEntity = null;
        ///<summary>
		///���캯����
		///</summary>
		public SecurityRoleDepartmentPresenter(ISecurityRoleDepartmentView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.RoleDepartment_ModuleID;
            this.securityRoleDepartmentEntity = new SecurityRoleDepartmentEntity();
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
                ISecurityRoleDepartmentListView listView = this.View as ISecurityRoleDepartmentListView;
                if (listView != null)
                {
                    return this.securityRoleDepartmentEntity.ListDataSource(listView.RoleName, listView.DepartmentName);
                }
                return null;
            }
        }
        /// <summary>
        /// ���ء�
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            ISecurityRoleDepartmentEditView editView = this.View as ISecurityRoleDepartmentEditView;
            if (editView != null)
            {
                IOrgFactory facotry = this.ModuleConfig.OrgFactory;
                if (facotry != null)
                {
                    editView.BindDepartment(new ListControlsTreeViewDataSource("DepartmentName", "DepartmentID", "ParentDepartmentID", facotry.GetAllDepartment(null)));
                }
            }
        }
		///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<string[]>> handler)
		{
            ISecurityRoleDepartmentEditView editView = this.View as ISecurityRoleDepartmentEditView;
            if (editView != null)
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
            ISecurityRoleDepartmentEditView editView = this.View as ISecurityRoleDepartmentEditView;
            if (editView != null)
            {
                editView.DepartmentSelected(this.securityRoleDepartmentEntity.GetDepartment(editView.RoleID));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <param name="depts"></param>
        /// <returns></returns>
        public bool UpdateRoleDepartment(GUIDEx role, StringCollection depts)
        {
            return this.securityRoleDepartmentEntity.UpdateRoleDepartment(role, depts);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool BatchDeleteRoleDepartment(StringCollection roles)
        {
            return this.securityRoleDepartmentEntity.BatchDeleteRoleDepartment(roles);
        }
		#endregion

	}

}
