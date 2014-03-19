//================================================================================
// FileName: SecurityRolePostPresenter.cs
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
	/// ISecurityRolePostView�ӿڡ�
	///</summary>
	public interface ISecurityRolePostView: IModuleView
	{

	}
    /// <summary>
    /// �б����ӿڡ�
    /// </summary>
    public interface ISecurityRolePostListView : ISecurityRolePostView
    {
        /// <summary>
        /// ��ȡ��ɫ���ơ�
        /// </summary>
        string RoleName { get; }
        /// <summary>
        /// ��ȡ��λ���ơ�
        /// </summary>
        string PostName { get; }
    }
    /// <summary>
    /// �༭����ӿڡ�
    /// </summary>
    public interface ISecurityRolePostEditView : ISecurityRolePostView
    {
        /// <summary>
        /// ��ȡ��ɫID��
        /// </summary>
        GUIDEx RoleID { get; }
        /// <summary>
        /// �󶨸�λ���ݡ�
        /// </summary>
        /// <param name="data">����Դ��</param>
        void BindPost(IListControlsTreeViewData data);
        /// <summary>
        /// ѡ�С�
        /// </summary>
        /// <param name="selected"></param>
        void PostSelected(StringCollection selected);
    }
		
	///<summary>
	/// SecurityRolePostPresenter��Ϊ�ࡣ
	///</summary>
	public class SecurityRolePostPresenter: ModulePresenter<ISecurityRolePostView>
	{
		#region ��Ա���������캯����
        SecurityRolePostEntity securityRolePostEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SecurityRolePostPresenter(ISecurityRolePostView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.RolePost_ModuleID;
            this.securityRolePostEntity = new SecurityRolePostEntity();
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
                ISecurityRolePostListView listView = this.View as ISecurityRolePostListView;
                if (listView != null)
                {
                    return this.securityRolePostEntity.ListDataSource(listView.RoleName, listView.PostName);
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
            ISecurityRolePostEditView editView = this.View as ISecurityRolePostEditView;
            if (editView != null)
            {
                IOrgFactory facotry = this.ModuleConfig.OrgFactory;
                if (facotry != null)
                {
                    editView.BindPost(new ListControlsTreeViewDataSource("PostName", "PostID", "ParentPostID", facotry.GetAllPost(null)));
                }
            }
        }
		///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<string[]>> handler)
		{
            ISecurityRolePostEditView editView = this.View as ISecurityRolePostEditView;
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
            ISecurityRolePostEditView editView = this.View as ISecurityRolePostEditView;
            if (editView != null)
            {
                editView.PostSelected(this.securityRolePostEntity.GetPost(editView.RoleID));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <param name="posts"></param>
        /// <returns></returns>
        public bool UpdateRolePost(GUIDEx role, StringCollection posts)
        {
            return this.securityRolePostEntity.UpdateRolePost(role, posts);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool BatchDeleteRolePost(StringCollection roles)
        {
            return this.securityRolePostEntity.BatchDeleteRolePost(roles);
        }
		#endregion

	}

}
