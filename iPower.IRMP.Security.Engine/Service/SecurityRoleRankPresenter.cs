//================================================================================
// FileName: SecurityRoleRankPresenter.cs
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
using iPower.IRMP.Security.Engine.Domain;
using iPower.IRMP.Security.Engine.Persistence;

using iPower.IRMP.Org;
namespace iPower.IRMP.Security.Engine.Service
{
	///<summary>
	/// ISecurityRoleRankView接口。
	///</summary>
	public interface ISecurityRoleRankView: IModuleView
	{

	}
    /// <summary>
    /// 角色岗位级别列表接口。
    /// </summary>
    public interface ISecurityRoleRankListView : ISecurityRoleRankView
    {
        /// <summary>
        /// 获取角色名称。
        /// </summary>
        string RoleName { get; }
        /// <summary>
        /// 获取岗位级别名称。
        /// </summary>
        string RankName { get; }
    }
    /// <summary>
    /// 角色岗位级别编辑接口。
    /// </summary>
    public interface ISecurityRoleRankEditView : ISecurityRoleRankView
    {
        /// <summary>
        /// 获取角色ID。
        /// </summary>
        GUIDEx RoleID { get; }
        /// <summary>
        /// 绑定岗位级别数据。
        /// </summary>
        /// <param name="data">数据源。</param>
        void BindRank(IListControlsTreeViewData data);
        /// <summary>
        /// 选中。
        /// </summary>
        /// <param name="selected"></param>
        void RankSelected(StringCollection selected);
    }
		
	///<summary>
	/// SecurityRoleRankPresenter行为类。
	///</summary>
	public class SecurityRoleRankPresenter: ModulePresenter<ISecurityRoleRankView>
	{
		#region 成员变量，构造函数。
        SecurityRoleRankEntity securityRoleRankEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SecurityRoleRankPresenter(ISecurityRoleRankView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.RoleRank_ModuleID;
            this.securityRoleRankEntity = new SecurityRoleRankEntity();
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
                ISecurityRoleRankListView listView = this.View as ISecurityRoleRankListView;
                if (listView != null)
                {
                    return this.securityRoleRankEntity.ListDataSource(listView.RoleName, listView.RankName);
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
            ISecurityRoleRankEditView editView = this.View as ISecurityRoleRankEditView;
            if (editView != null)
            {
                IOrgFactory facotry = this.ModuleConfig.OrgFactory;
                if (facotry != null)
                {
                    editView.BindRank(new ListControlsTreeViewDataSource("RankName", "RankID", "ParentRankID", facotry.GetAllRank(null)));
                }
            }
        }
		///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<string[]>> handler)
		{
            ISecurityRoleRankEditView editView = this.View as ISecurityRoleRankEditView;
            if (editView != null && editView.RoleID.IsValid)
            {
                iPower.IRMP.Security.Engine.Domain.SecurityRole role = new iPower.IRMP.Security.Engine.Domain.SecurityRole();
                role.RoleID = editView.RoleID;
                SecurityRoleEntity securityRoleEntity = new SecurityRoleEntity();
                if (securityRoleEntity.LoadRecord(ref role))
                {
                    handler(this, new EntityEventArgs<string[]>(new string[] { role.RoleID, role.RoleName }));
                    this.ChangeRole(role.RoleID);
                }
            }
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleID"></param>
        public void ChangeRole(GUIDEx roleID)
        {
            ISecurityRoleRankEditView editView = this.View as ISecurityRoleRankEditView;
            if (editView != null)
            {
                editView.RankSelected(this.securityRoleRankEntity.GetRank(editView.RoleID));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <param name="ranks"></param>
        /// <returns></returns>
        public bool UpdateRoleRank(GUIDEx role, StringCollection ranks)
        {
            return this.securityRoleRankEntity.UpdateRoleRank(role, ranks);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool BatchDeleteRoleRank(StringCollection roles)
        {
            return this.securityRoleRankEntity.BatchDeleteRoleRank(roles);
        }
		#endregion

	}

}
