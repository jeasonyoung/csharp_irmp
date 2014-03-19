//================================================================================
// FileName: SecurityRightPresenter.cs
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
	/// ISecurityRightView接口。
	///</summary>
	public interface ISecurityRightView: IModuleView
	{
        
	}
    /// <summary>
    /// 列表界面接口。
    /// </summary>
    public interface ISecurityRightListView : ISecurityRightView
    {
        /// <summary>
        /// 获取所属系统ID。
        /// </summary>
        GUIDEx SystemID { get; }
        /// <summary>
        /// 获取模块名称。
        /// </summary>
        string ModuleName { get; }
        /// <summary>
        /// 绑定模块树结构。
        /// </summary>
        /// <param name="data"></param>
        void BindModuleTree(IListControlsTreeViewData data);
        /// <summary>
        /// 绑定所属系统数据。
        /// </summary>
        /// <param name="data"></param>
        void BindSystem(IListControlsTreeViewData data);
        /// <summary>
        /// 显示消息。
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }
    /// <summary>
    /// 编辑界面接口。
    /// </summary>
    public interface ISecurityRightEditView : ISecurityRightView
    {
        /// <summary>
        /// 获取权限ID。
        /// </summary>
        GUIDEx RightID { get; }
        /// <summary>
        /// 获取所属系统ID。
        /// </summary>
        GUIDEx SystemID { get; }
        
        /// <summary>
        /// 绑定模块。
        /// </summary>
        /// <param name="data"></param>
        void BindModule(IListControlsTreeViewData data);
        /// <summary>
        /// 绑定元操作。
        /// </summary>
        /// <param name="data"></param>
        void BindAction(IListControlsData data);
        /// <summary>
        /// 绑定所属系统数据。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="value"></param>
        void BindSystem(IListControlsTreeViewData data, string value);
        /// <summary>
        /// 显示消息。
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }
	///<summary>
	/// SecurityRightPresenter行为类。
	///</summary>
	public class SecurityRightPresenter: ModulePresenter<ISecurityRightView>
	{
		#region 成员变量，构造函数。
        SecurityRightEntity securityRightEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SecurityRightPresenter(ISecurityRightView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Right_ModuleID;
            this.securityRightEntity = new SecurityRightEntity();
		}
		#endregion

        #region 重载。
        /// <summary>
        /// 装载数据。
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();

            ISecurityRightListView listView = this.View as ISecurityRightListView;
            if (listView != null)
            {
                listView.BindSystem(new SecurityRegsiterEntity().RegsiterSystem);
                this.ChangeSystemRefershModuleTree();
            }
            ISecurityRightEditView editView = this.View as ISecurityRightEditView;
            if (editView != null)
            {
                editView.BindSystem(new SecurityRegsiterEntity().RegsiterSystem, editView.SystemID);
                this.ChangeSystemRefershModule(editView.SystemID);
                editView.BindAction(new SecurityActionEntity().Action);
            }
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
                ISecurityRightListView listView = this.View as ISecurityRightListView;
                if (listView != null)
                {
                    return this.securityRightEntity.ListDataSource(listView.SystemID, listView.ModuleName);
                }
                return null;
            }
        }
        /// <summary>
        /// 变更所属系统刷新模块信息。
        /// </summary>
        public void ChangeSystemRefershModuleTree()
        {
            ISecurityRightListView listView = this.View as ISecurityRightListView;
            if(listView != null)
                listView.BindModuleTree(new SecurityModuleEntity().ParentModule(listView.SystemID));
        }
        /// <summary>
        /// 变更所属系统。
        /// </summary>
        public void ChangeSystemRefershModule(string systemID)
        {
            ISecurityRightEditView editView = this.View as ISecurityRightEditView;
            if (editView != null)
            {
                editView.BindModule(new SecurityModuleEntity().ParentModule(systemID));
            }
        }
       
        /// <summary>
        /// 批量删除。
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteRight(StringCollection priCollection)
        {
            bool result = false;
            if (priCollection != null)
            {
                string err = null;
                ISecurityRightListView listView = this.View as ISecurityRightListView;
                foreach (string id in priCollection)
                {
                    result = this.securityRightEntity.DeleteRight(id, out err);
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
        ///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<SecurityRight>> handler)
		{
            ISecurityRightEditView editView = this.View as ISecurityRightEditView;
            if (editView != null && editView.RightID.IsValid)
            {
                SecurityModuleEntity securityModuleEntity = new SecurityModuleEntity();
                SecurityRight data = new SecurityRight();
                data.RightID = editView.RightID;
                if (this.securityRightEntity.LoadRecord(ref data))
                {
                    SecurityModule module = new SecurityModule();
                    module.ModuleID = data.ModuleID;
                    if (securityModuleEntity.LoadRecord(ref module))
                        data.SystemID = module.SystemID;
                    handler(this, new EntityEventArgs<SecurityRight>(data));
                }
            }
		}
        /// <summary>
        /// 更新权限。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateRight(SecurityRight data)
        {
            bool result = false;
            string err = null;
            ISecurityRightEditView editView = this.View as ISecurityRightEditView;
            result = this.securityRightEntity.UpdateRecord(data, out err);
            if (!result && editView != null && !string.IsNullOrEmpty(err))
                editView.ShowMessage(err);
            return result;
        }
		#endregion

	}

}
