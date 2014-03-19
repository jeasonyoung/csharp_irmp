//================================================================================
// FileName: SecurityRoleRightPresenter.cs
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
using iPower.IRMP.Security.Engine.Domain;
using iPower.IRMP.Security.Engine.Persistence;
namespace iPower.IRMP.Security.Engine.Service
{
	///<summary>
	/// ISecurityRoleRightView�ӿڡ�
	///</summary>
	public interface ISecurityRoleRightView: IModuleView
	{

	}
    /// <summary>
    /// �б����ӿڡ�
    /// </summary>
    public interface ISecurityRoleRightListView : ISecurityRoleRightView
    { 
        /// <summary>
        /// ��ȡ��ɫ���ơ�
        /// </summary>
        string RoleName { get; }
        /// <summary>
        /// ��ȡȨ�����ơ�
        /// </summary>
        string RightName { get; }
    }
    /// <summary>
    /// �༭�����ڡ�
    /// </summary>
    public interface ISecurityRoleRightEditView : ISecurityRoleRightView
    {
        /// <summary>
        /// ��ȡ��ɫID��
        /// </summary>
        GUIDEx RoleID { get; }
        
        /// <summary>
        /// �����ݡ�
        /// </summary>
        /// <param name="data"></param>
        void BindModuleRight(IListControlsTreeViewData data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rightCollection"></param>
        void SelectedRight(StringCollection rightCollection);
    }
		
	///<summary>
	/// SecurityRoleRightPresenter��Ϊ�ࡣ
	///</summary>
	public class SecurityRoleRightPresenter: ModulePresenter<ISecurityRoleRightView>
	{
		#region ��Ա���������캯����
        SecurityRoleRightEntity securityRoleRightEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SecurityRoleRightPresenter(ISecurityRoleRightView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.RoleRight_ModuleID;
            this.securityRoleRightEntity = new SecurityRoleRightEntity();
		}
		#endregion

		#region ���ݲ���������
        /// <summary>
        /// ��ȡ�б����ݡ�
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                ISecurityRoleRightListView listView = this.View as ISecurityRoleRightListView;
                if (listView != null)
                    return this.securityRoleRightEntity.ListDataSource(listView.RoleName, listView.RightName);
                return null;
            }
        }
        /// <summary>
        /// ����ɾ�����ݡ�
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteRoleRight(StringCollection priCollection)
        {
            bool result = false;
            if (priCollection != null)
            {
                foreach (string id in priCollection)
                {
                    result = this.securityRoleRightEntity.DeleteRecord(id);
                }
            }
            return result;
        }
        /// <summary>
        /// ��ɫ�ı䡣
        /// </summary>
        /// <param name="roleID"></param>
        public void ChangeRole(GUIDEx roleID)
        {
            ISecurityRoleRightEditView editView = this.View as ISecurityRoleRightEditView;
            if (editView != null)
            {
                if (!roleID.IsValid)
                    editView.BindModuleRight(null);
                else
                {
                    editView.BindModuleRight(this.securityRoleRightEntity.GetRoleSystemAllRightData(roleID));
                    editView.SelectedRight(this.securityRoleRightEntity.GetRoleRight(roleID));
                }
            }
        }
        /// <summary>
        /// ���ؼ��ء�
        /// </summary>
        public void LoadEntityData(EventHandler<EntityEventArgs<string[]>> handler)
        {
            ISecurityRoleRightEditView editView = this.View as ISecurityRoleRightEditView;
            if (editView != null && editView.RoleID.IsValid)
            {
                iPower.IRMP.Security.Engine.Domain.SecurityRole data = new iPower.IRMP.Security.Engine.Domain.SecurityRole();
                data.RoleID = editView.RoleID;
                SecurityRoleEntity entity = new SecurityRoleEntity();
                if (entity.LoadRecord(ref data))
                {
                    handler(this, new EntityEventArgs<string[]>(new string[] { data.RoleID, data.RoleName }));
                    this.ChangeRole(data.RoleID);
                }
            }
        }
        /// <summary>
        /// ���������ɫȨ�ޡ�
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="rightCollection"></param>
        /// <returns></returns>
        public bool BatchSaveRoleRight(GUIDEx roleID, StringCollection rightCollection)
        {
            if (roleID.IsValid)
            {
                List<string> list = new List<string>();
                int index = 0;
                foreach (string id in rightCollection)
                {
                    index = id.IndexOf("-R-");
                    if (index > 0)
                    {
                        list.Add(id.Substring(index + 3));
                    }
                }
                this.securityRoleRightEntity.DeleteRecord(roleID);
                if (list.Count > 0)
                {
                    SecurityRoleRight data = new SecurityRoleRight();
                    foreach (string rightID in list)
                    {
                        data.RoleID = roleID;
                        data.RightID = rightID;
                        this.securityRoleRightEntity.UpdateRecord(data);
                    }
                }
                return true;
            }
            return false;
        }
		#endregion

	}

}
