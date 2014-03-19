//================================================================================
// FileName: SecurityRolePresenter.cs
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
	/// ISecurityRoleView接口。
	///</summary>
	public interface ISecurityRoleView: IModuleView
	{

	}
    /// <summary>
    /// 列表界面接口。
    /// </summary>
    public interface ISecurityRoleListView : ISecurityRoleView
    {
        /// <summary>
        /// 获取角色名称。
        /// </summary>
        string RoleName { get; }
        /// <summary>
        /// 显示消息。
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }
    /// <summary>
    /// 编辑界面接口。
    /// </summary>
    public interface ISecurityRoleEditView : ISecurityRoleView
    {
        /// <summary>
        /// 获取角色ID。
        /// </summary>
        GUIDEx RoleID { get; }
        /// <summary>
        /// 绑定角色状态。
        /// </summary>
        /// <param name="data"></param>
        void BindRoleStatus(IListControlsData data);
        /// <summary>
        /// 绑定上级角色。
        /// </summary>
        /// <param name="data"></param>
        void BindParentRole(IListControlsTreeViewData data);
        /// <summary>
        /// 绑定所属系统数据。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="chkSelected"></param>
        void BindSystemTree(IListControlsTreeViewData data, StringCollection chkSelected);
    }
    /// <summary>
    /// Picker页面接口。
    /// </summary>
    public interface ISecurityRolePickerView : ISecurityRoleView
    {
        /// <summary>
        /// 获取角色名称。
        /// </summary>
        string RoleName { get; set; }
        /// <summary>
        /// 获取角色ID。
        /// </summary>
        GUIDEx RoleID { get; }
        /// <summary>
        /// 绑定角色数据。
        /// </summary>
        /// <param name="data"></param>
        void BindRole(IListControlsData data);
    }
	///<summary>
	/// SecurityRolePresenter行为类。
	///</summary>
	public class SecurityRolePresenter: ModulePresenter<ISecurityRoleView>
	{
		#region 成员变量，构造函数。
        SecurityRoleEntity securityRoleEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SecurityRolePresenter(ISecurityRoleView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Role_ModuleID;
            this.securityRoleEntity = new SecurityRoleEntity();
		}
		#endregion

        #region 重载。
        /// <summary>
        /// 加载数据。
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            ISecurityRoleEditView editView = this.View as ISecurityRoleEditView;
            if (editView != null)
            {
                editView.BindRoleStatus(this.EnumDataSource(typeof(EnumSystemStatus)));
                editView.BindParentRole(this.securityRoleEntity.Role);
                editView.BindSystemTree(new SecurityRegsiterEntity().RegsiterSystem, null);
            }
            ISecurityRolePickerView pickerView = this.View as ISecurityRolePickerView;
            if (pickerView != null)
            {
                if (pickerView.RoleID.IsValid)
                {
                    iPower.IRMP.Security.Engine.Domain.SecurityRole data = new iPower.IRMP.Security.Engine.Domain.SecurityRole();
                    data.RoleID = pickerView.RoleID;
                    if (this.securityRoleEntity.LoadRecord(ref data))
                        pickerView.RoleName = data.RoleName;
                }
                this.PickerSeach();
            }
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
                ISecurityRoleListView listView = this.View as ISecurityRoleListView;
                if (listView != null)
                {
                    DataTable dtSource = this.securityRoleEntity.ListDataSource(listView.RoleName);
                    dtSource.Columns.Add("RoleStatusName");
                    foreach (DataRow row in dtSource.Rows)
                    {
                        row["RoleStatusName"] = this.GetEnumMemberName(typeof(EnumSystemStatus), Convert.ToInt32(row["RoleStatus"]));
                    }
                    return dtSource.Copy();
                }
                return null;
            }
        }
		///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
        public void LoadEntityData(EventHandler<EntityEventArgs<iPower.IRMP.Security.Engine.Domain.SecurityRole>> handler)
		{
            ISecurityRoleEditView editView = this.View as ISecurityRoleEditView;
            if (editView != null)
            {
                iPower.IRMP.Security.Engine.Domain.SecurityRole data = new iPower.IRMP.Security.Engine.Domain.SecurityRole();
                data.RoleID = editView.RoleID;
                if (this.securityRoleEntity.LoadRecord(ref data))
                {
                    editView.BindParentRole(this.securityRoleEntity.NotSelfGetOffSprings(data.RoleID, null));
                    editView.BindSystemTree(new SecurityRegsiterEntity().RegsiterSystem, new SecurityRoleSystemEntity().LoadSystemData(data.RoleID));
                    handler(this, new EntityEventArgs<iPower.IRMP.Security.Engine.Domain.SecurityRole>(data));
                }
            }
		}
        /// <summary>
        /// 更新角色。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sysCollection"></param>
        /// <returns></returns>
        public bool UpdateRole(iPower.IRMP.Security.Engine.Domain.SecurityRole data, StringCollection sysCollection)
        {
            return this.securityRoleEntity.UpdateRole(data, sysCollection);
        }
        /// <summary>
        /// 批量删除角色。
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteRole(StringCollection priCollection)
        {
            bool result = false;
            if (priCollection != null)
            {
                string err = null;
                ISecurityRoleListView listView = this.View as ISecurityRoleListView;
                foreach (string id in priCollection)
                {
                    result = this.securityRoleEntity.DeleteRole(id, out err);
                    if (!result)
                    {
                        if (listView != null)
                            listView.ShowMessage(err);
                        break;
                    }
                }
            }
            return result;
        }
		#endregion
        
        #region Picker。
        /// <summary>
        /// Pick查询
        /// </summary>
        public void PickerSeach()
        {
            ISecurityRolePickerView pickerView = this.View as ISecurityRolePickerView;
            if (pickerView != null)
            {
                pickerView.BindRole(this.securityRoleEntity.GetRole(pickerView.RoleName));
            }
        }
        #endregion
    }

}
