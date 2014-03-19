//================================================================================
// FileName: SecurityRoleRightPresenter.cs
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
namespace iPower.IRMP.Security.Engine.Service
{
	///<summary>
	/// ISecurityRoleRightView接口。
	///</summary>
	public interface ISecurityRoleRightView: IModuleView
	{

	}
    /// <summary>
    /// 列表界面接口。
    /// </summary>
    public interface ISecurityRoleRightListView : ISecurityRoleRightView
    { 
        /// <summary>
        /// 获取角色名称。
        /// </summary>
        string RoleName { get; }
        /// <summary>
        /// 获取权限名称。
        /// </summary>
        string RightName { get; }
    }
    /// <summary>
    /// 编辑界面借口。
    /// </summary>
    public interface ISecurityRoleRightEditView : ISecurityRoleRightView
    {
        /// <summary>
        /// 获取角色ID。
        /// </summary>
        GUIDEx RoleID { get; }
        
        /// <summary>
        /// 绑定数据。
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
	/// SecurityRoleRightPresenter行为类。
	///</summary>
	public class SecurityRoleRightPresenter: ModulePresenter<ISecurityRoleRightView>
	{
		#region 成员变量，构造函数。
        SecurityRoleRightEntity securityRoleRightEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SecurityRoleRightPresenter(ISecurityRoleRightView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.RoleRight_ModuleID;
            this.securityRoleRightEntity = new SecurityRoleRightEntity();
		}
		#endregion

		#region 数据操作函数。
        /// <summary>
        /// 获取列表数据。
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
        /// 批量删除数据。
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
        /// 角色改变。
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
        /// 重载加载。
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
        /// 批量保存角色权限。
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
