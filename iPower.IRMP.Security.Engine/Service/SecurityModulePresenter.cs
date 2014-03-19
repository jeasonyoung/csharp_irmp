//================================================================================
// FileName: SecurityModulePresenter.cs
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
using System.IO;
	
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
	/// ISecurityModuleView接口。
	///</summary>
	public interface ISecurityModuleView: IModuleView
	{
        /// <summary>
        /// 展示消息。
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
        /// <summary>
        /// 绑定所属系统。
        /// </summary>
        /// <param name="data"></param>
        void BindSystem(IListControlsTreeViewData data);
	}
    /// <summary>
    /// 列表界面接口。
    /// </summary>
    public interface ISecurityModuleListView : ISecurityModuleView
    {
        /// <summary>
        /// 获取模块名称。
        /// </summary>
        string ModuleName { get; }
        /// <summary>
        /// 获取所属系统ID。
        /// </summary>
        GUIDEx SystemID { get; }
    }
    /// <summary>
    /// 编辑界面接口。
    /// </summary>
    public interface ISecurityModuleEditView : ISecurityModuleView
    {
        /// <summary>
        /// 获取模块ID。
        /// </summary>
        GUIDEx ModuleID { get; }
        /// <summary>
        /// 获取所属系统ID。
        /// </summary>
        GUIDEx SystemID { get; }
        /// <summary>
        /// 绑定上级模块。
        /// </summary>
        /// <param name="data"></param>
        void BindParentModule(IListControlsTreeViewData data);
        /// <summary>
        /// 绑定模块状态。
        /// </summary>
        /// <param name="data"></param>
        void BindModuleStatus(IListControlsData data);
    }
    /// <summary>
    /// 模块导入界面接口。
    /// </summary>
    public interface ISecurityModuleImportView : ISecurityModuleView
    {
        /// <summary>
        /// 获取导入的系统ID。
        /// </summary>
        GUIDEx ImportSystemID { get; }
        /// <summary>
        /// 获取上传文件对象。
        /// </summary>
        object UploadFile { get; set; }

    }
	///<summary>
	/// SecurityModulePresenter行为类。
	///</summary>
	public class SecurityModulePresenter: ModulePresenter<ISecurityModuleView>
	{
		#region 成员变量，构造函数。
        SecurityModuleEntity securityModuleEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SecurityModulePresenter(ISecurityModuleView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Module_ModuleID;
            this.securityModuleEntity = new SecurityModuleEntity();
		}
		#endregion

        #region 重载。
        /// <summary>
        /// 
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            if (this.View != null)
                this.View.BindSystem(new SecurityRegsiterEntity().RegsiterSystem);

            ISecurityModuleEditView editView = this.View as ISecurityModuleEditView;
            if (editView != null)
            {
                editView.BindModuleStatus(this.EnumDataSource(typeof(EnumSystemStatus)));
                this.ChangeSystemSelected();
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
                ISecurityModuleListView listView = this.View as ISecurityModuleListView;
                if (listView != null)
                {
                    DataTable dtSource = this.securityModuleEntity.ListDataSource(listView.ModuleName, listView.SystemID);
                    dtSource.Columns.Add("ModuleStatusName");
                    foreach (DataRow row in dtSource.Rows)
                    {
                        row["ModuleStatusName"] = this.GetEnumMemberName(typeof(EnumSystemStatus), Convert.ToInt32(row["ModuleStatus"]));
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
		public void LoadEntityData(EventHandler<EntityEventArgs<SecurityModule>> handler)
		{
            ISecurityModuleEditView editView = this.View as ISecurityModuleEditView;
            if (editView != null)
            {
                SecurityModule data = new SecurityModule();
                data.ModuleID = editView.ModuleID;
                if (this.securityModuleEntity.LoadRecord(ref data))
                {
                    editView.BindParentModule(this.securityModuleEntity.NotSelfGetOffSprings(data.ModuleID, data.SystemID));
                    handler(this, new EntityEventArgs<SecurityModule>(data));
                }
            }
		}
        /// <summary>
        /// 批量删除数据。
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteModule(StringCollection priCollection)
        {
            bool result = false;
            string err = null;
            foreach (string p in priCollection)
            {
                result = this.securityModuleEntity.DeleteModule(p, out err);
                if (!result)
                {
                    ISecurityModuleListView listView = this.View as ISecurityModuleListView;
                    if (listView != null)
                        listView.ShowMessage(err);
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// 更新数据。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateModule(SecurityModule data)
        {
            return this.securityModuleEntity.UpdateRecord(data);
        }
        /// <summary>
        /// 变更所属系统选项。
        /// </summary>
        public void ChangeSystemSelected()
        {
            ISecurityModuleEditView editView = this.View as ISecurityModuleEditView;
            if (editView != null)
            {
                if (editView.ModuleID.IsValid)
                    editView.BindParentModule(this.securityModuleEntity.NotSelfGetOffSprings(editView.ModuleID, editView.SystemID));
                else
                    editView.BindParentModule(this.securityModuleEntity.ParentModule(editView.SystemID));
            }
        }
		#endregion

        #region 导入处理。
        /// <summary>
        /// 检查上传文件格式。
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        public bool CheckUploadFormat(Stream upload)
        {
            bool result = false;
            ISecurityModuleImportView importView = this.View as ISecurityModuleImportView;
            if (upload != null && importView != null)
            {
                try
                {
                    ModuleDefineFactory o = ModuleDefineFactory.DeSerializer(upload);
                    if (o != null)
                    {
                        importView.UploadFile = o.System;
                        result = true;
                    }
                }
                catch (Exception) { }
                if (!result)
                    importView.ShowMessage("上传的文件格式不正确！(请上传菜单定义文件)");
            }
            return result;
        }
        /// <summary>
        /// 获取导入列表数据。
        /// </summary>
        public DataTable ImportListViewDataSource
        {
            get
            {
                ISecurityModuleImportView importView = this.View as ISecurityModuleImportView;
                if (importView != null && importView.ImportSystemID.IsValid)
                {
                    ModuleSystemDefineCollection sysCollection = importView.UploadFile as ModuleSystemDefineCollection;
                    if (sysCollection == null)
                        return null;
                    ModuleSystemDefine s = sysCollection[importView.ImportSystemID];
                    if (s == null)
                    {
                        importView.ShowMessage("没有所属系统下的模块数据！");
                        return null;
                    }
                    DataTable dtSource = new DataTable();
                    dtSource.Columns.Add("ModuleID");
                    dtSource.Columns.Add("ModuleName");
                    dtSource.Columns.Add("ModuleUri");
                    dtSource.Columns.Add("OrderNo");
                    this.CreateModuleDefine(ref dtSource, s.Modules);
                    return dtSource.Copy();
                }
                return null;
            }
        }
         /// <summary>
        /// 保存上存
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool UploadModuleDefineSave(StringCollection priCollection)
        {
            bool result = false;
            ISecurityModuleImportView importView = this.View as ISecurityModuleImportView;
            if (priCollection != null && importView != null)
            {
                 ModuleSystemDefineCollection sysCollection  = importView.UploadFile as ModuleSystemDefineCollection;
                 if (sysCollection == null)
                 {
                     importView.ShowMessage("上传的数据因意外无法获取，请重新上传！");
                     return result;
                 }
                 string systemID = importView.ImportSystemID;
                 ModuleSystemDefine s = sysCollection[systemID];
                 if (s == null)
                 {
                     importView.ShowMessage("没有所属系统下的模块数据！");
                     return result;
                 }
                 SecurityModule data = null;
                 ModuleDefine d = null;
                 foreach (string id in priCollection)
                 {
                     d = s.Modules[id];
                     if (d != null)
                     {
                         data = new SecurityModule();
                         data.SystemID = systemID;
                         data.ModuleID = d.ModuleID;
                         data.ModuleName = d.ModuleName;
                         data.OrderNo = d.OrderNo;
                         data.ModuleDescription = d.ModuleUri;
                         data.ModuleStatus = (int)EnumSystemStatus.Start;
                         if (d.Parent != null && priCollection.Contains(d.Parent.ModuleID))
                             data.ParentModuleID = d.Parent.ModuleID;
                         result = this.securityModuleEntity.UpdateRecord(data);
                     }
                 }
            }
            return result;
        }
        #endregion

        #region 辅助函数。
        void CreateModuleDefine(ref DataTable dtSource, ModuleDefineCollection dCollection)
        {
            if (dCollection != null)
            {
                DataRow dr = null;
                foreach (ModuleDefine d in dCollection)
                {
                    dr = dtSource.NewRow();
                    dr["ModuleID"] = d.ModuleID;
                    dr["ModuleName"] = d.ModuleName;
                    dr["ModuleUri"] = d.ModuleUri;
                    dr["OrderNo"] = d.OrderNo;
                    dtSource.Rows.Add(dr);

                    if (d.Modules != null)
                        this.CreateModuleDefine(ref dtSource, d.Modules);
                }
            }
        }
        #endregion
    }

}
