//================================================================================
// FileName: SecurityRegsiterPresenter.cs
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
	/// ISecurityRegsiterView接口。
	///</summary>
	public interface ISecurityRegsiterView: IModuleView
	{
        /// <summary>
        /// 显示数据。
        /// </summary>
        /// <param name="content"></param>
        void ShowMessage(string content);
	}
    /// <summary>
    /// 列表界面接口。
    /// </summary>
    public interface ISecurityRegsiterListView : ISecurityRegsiterView
    {
        /// <summary>
        /// 获取系统名称。
        /// </summary>
        string SystemName { get; }
        /// <summary>
        /// 获取上级系统ID。
        /// </summary>
        GUIDEx ParentSystemID { get; }
        /// <summary>
        /// 绑定上级系统。
        /// </summary>
        /// <param name="data"></param>
        void BindParentSystem(IListControlsTreeViewData data);
    }
    /// <summary>
    /// 编辑界面接口。
    /// </summary>
    public interface ISecurityRegsiterEditView : ISecurityRegsiterView
    {
        /// <summary>
        /// 获取系统ID。
        /// </summary>
        GUIDEx SystemID { get; }
        /// <summary>
        /// 绑定系统类型。
        /// </summary>
        /// <param name="data">数据源。</param>
        void BindSystemType(IListControlsData data);
        /// <summary>
        /// 绑定系统状态。
        /// </summary>
        /// <param name="data">数据源。</param>
        void BindSystemStatus(IListControlsData data);
        /// <summary>
        /// 绑定上级系统。
        /// </summary>
        /// <param name="data"></param>
        void BindParentSystem(IListControlsTreeViewData data);
    }
    /// <summary>
    /// 导入界面接口。
    /// </summary>
    public interface ISecurityRegsiterImportView : ISecurityRegsiterView
    {
        /// <summary>
        /// 获取上传文件对象。
        /// </summary>
        object UploadFile { get; set; }
        /// <summary>
        /// 获取系统状态。
        /// </summary>
        int SystemStatus { get; }
        /// <summary>
        /// 获取系统类型。
        /// </summary>
        int SystemType { get; }
        /// <summary>
        /// 绑定系统类型。
        /// </summary>
        /// <param name="data">数据源。</param>
        void BindSystemType(IListControlsData data);
        /// <summary>
        /// 绑定系统状态。
        /// </summary>
        /// <param name="data">数据源。</param>
        void BindSystemStatus(IListControlsData data);
    }
	///<summary>
	/// SecurityRegsiterPresenter行为类。
	///</summary>
	public class SecurityRegsiterPresenter: ModulePresenter<ISecurityRegsiterView>
	{
		#region 成员变量，构造函数。
        SecurityRegsiterEntity securityRegsiterEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SecurityRegsiterPresenter(ISecurityRegsiterView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Regsiter_ModuleID;
            this.securityRegsiterEntity = new SecurityRegsiterEntity();
		}
		#endregion

        #region 重载。
        /// <summary>
        /// 加载数据。
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            ISecurityRegsiterListView listView = this.View as ISecurityRegsiterListView;
            if (listView != null)
                 listView.BindParentSystem(this.securityRegsiterEntity.RegsiterSystem);
            ISecurityRegsiterEditView editView = this.View as ISecurityRegsiterEditView;
            if (editView != null)
            {
                editView.BindSystemStatus(this.EnumDataSource(typeof(EnumSystemStatus)));
                editView.BindSystemType(this.EnumDataSource(typeof(EnumSystemType)));
                if (!editView.SystemID.IsValid)
                    editView.BindParentSystem(this.securityRegsiterEntity.RegsiterSystem);
            }
            ISecurityRegsiterImportView importView = this.View as ISecurityRegsiterImportView;
            if (importView != null)
            {
                importView.BindSystemStatus(this.EnumDataSource(typeof(EnumSystemStatus)));
                importView.BindSystemType(this.EnumDataSource(typeof(EnumSystemType)));
            }
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
                ISecurityRegsiterListView listView = this.View as ISecurityRegsiterListView;
                if (listView != null)
                {
                    DataTable dtSource = this.securityRegsiterEntity.ListDataSource(listView.SystemName, listView.ParentSystemID);
                    if (dtSource != null)
                    {
                        dtSource.Columns.Add("SystemTypeName", typeof(string));
                        dtSource.Columns.Add("SystemStatusName", typeof(string));

                        foreach (DataRow row in dtSource.Rows)
                        {
                            row["SystemTypeName"] = this.GetEnumMemberName(typeof(EnumSystemType), Convert.ToInt32(row["SystemType"]));
                            row["SystemStatusName"] = this.GetEnumMemberName(typeof(EnumSystemStatus), Convert.ToInt32(row["SystemStatus"]));
                        }
                    }
                    return dtSource.Copy();
                }
                return null;
            }
        }
        /// <summary>
        /// 批量删除数据。
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteRegsiter(StringCollection priCollection)
        {
            bool result = false;
            string err = string.Empty;
            foreach (string p in priCollection)
            {
                result = this.securityRegsiterEntity.DeleteRegsiter(p, out err);
                if (!result)
                {
                    this.View.ShowMessage(err);
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// 批量初始化系统模块权限。
        /// </summary>
        /// <returns></returns>
        public bool BatchInitAppModuleRight()
        {
            bool result = false;
            ISecurityRegsiterEditView editView = this.View as ISecurityRegsiterEditView;
            if (editView != null && editView.SystemID.IsValid)
            {
                string err = null;
                result = this.securityRegsiterEntity.BatchInitAppModuleRight(editView.SystemID, out err);
                if (!result && !string.IsNullOrEmpty(err))
                    editView.ShowMessage(err);
            }
            return result;
        }
        ///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<SecurityRegsiter>> handler)
		{
            ISecurityRegsiterEditView editView = this.View as ISecurityRegsiterEditView;
            if (editView != null && editView.SystemID.IsValid)
            {
                SecurityRegsiter data = new SecurityRegsiter();
                data.SystemID = editView.SystemID;
                if (this.securityRegsiterEntity.LoadRecord(ref data))
                {
                    editView.BindParentSystem(this.securityRegsiterEntity.NotSelfGetOffSprings(data.SystemID, null));

                    handler(this, new EntityEventArgs<SecurityRegsiter>(data));
                }
            }
		}
        /// <summary>
        /// 更新数据。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateRegsiter(SecurityRegsiter data)
        {
            bool result = false;
            try
            {
                result = this.securityRegsiterEntity.UpdateRecord(data);
            }
            catch (Exception e)
            {
                this.View.ShowMessage(e.Message);
            }
            return result;
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
            ISecurityRegsiterImportView importView = this.View as ISecurityRegsiterImportView;
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
        public ModuleSystemDefineCollection ImportListViewDataSource
        {
            get
            {
                ISecurityRegsiterImportView importView = this.View as ISecurityRegsiterImportView;
                if (importView != null)
                     return importView.UploadFile as ModuleSystemDefineCollection;
                 return null;
            }
        }
        /// <summary>
        /// 保存上存
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool UploadSystemDefineSave(StringCollection priCollection)
        {
            bool result = false;
            ISecurityRegsiterImportView importView = this.View as ISecurityRegsiterImportView;
            if (priCollection != null && importView != null)
            {
                ModuleSystemDefineCollection sysCollection = importView.UploadFile as ModuleSystemDefineCollection;
                if (sysCollection == null)
                    importView.ShowMessage("上传的数据因意外无法获取，请重新上传！");
                else
                {
                    SecurityRegsiter data = null;
                    int status = importView.SystemStatus;
                    int type = importView.SystemType;
                    foreach (string sid in priCollection)
                    {
                        ModuleSystemDefine s = sysCollection[sid];
                        if (s != null)
                        {
                            data = new SecurityRegsiter();
                            data.SystemID = s.SystemID;
                            data.SystemSign = s.SystemSign;
                            data.SystemName = s.SystemName;
                            data.SystemDescription = s.SystemDescription;
                            data.SystemStatus = status;
                            data.SystemType = type;
                            result = this.UpdateRegsiter(data);
                        }
                    }
                }
            }
            return result;
        }
        #endregion

    }

}
