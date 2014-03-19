//================================================================================
// FileName: SecurityRolePostPresenter.cs
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
using iPower.IRMP.Org;
using iPower.IRMP.Security.Engine.Domain;
using iPower.IRMP.Security.Engine.Persistence;
namespace iPower.IRMP.Security.Engine.Service
{
	///<summary>
	/// ISecurityRolePostView接口。
	///</summary>
	public interface ISecurityRolePostView: IModuleView
	{

	}
    /// <summary>
    /// 列表界面接口。
    /// </summary>
    public interface ISecurityRolePostListView : ISecurityRolePostView
    {
        /// <summary>
        /// 获取角色名称。
        /// </summary>
        string RoleName { get; }
        /// <summary>
        /// 获取岗位名称。
        /// </summary>
        string PostName { get; }
    }
    /// <summary>
    /// 编辑界面接口。
    /// </summary>
    public interface ISecurityRolePostEditView : ISecurityRolePostView
    {
        /// <summary>
        /// 获取角色ID。
        /// </summary>
        GUIDEx RoleID { get; }
        /// <summary>
        /// 绑定岗位数据。
        /// </summary>
        /// <param name="data">数据源。</param>
        void BindPost(IListControlsTreeViewData data);
        /// <summary>
        /// 选中。
        /// </summary>
        /// <param name="selected"></param>
        void PostSelected(StringCollection selected);
    }
		
	///<summary>
	/// SecurityRolePostPresenter行为类。
	///</summary>
	public class SecurityRolePostPresenter: ModulePresenter<ISecurityRolePostView>
	{
		#region 成员变量，构造函数。
        SecurityRolePostEntity securityRolePostEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SecurityRolePostPresenter(ISecurityRolePostView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.RolePost_ModuleID;
            this.securityRolePostEntity = new SecurityRolePostEntity();
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
                ISecurityRolePostListView listView = this.View as ISecurityRolePostListView;
                if (listView != null)
                {
                    return this.securityRolePostEntity.ListDataSource(listView.RoleName, listView.PostName);
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
		///编辑页面加载数据。
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
