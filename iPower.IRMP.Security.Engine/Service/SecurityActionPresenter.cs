//================================================================================
// FileName: SecurityActionPresenter.cs
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
using System.Xml;

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
	/// ISecurityActionView接口。
	///</summary>
	public interface ISecurityActionView: IModuleView
	{

	}
    /// <summary>
    /// 列表界面接口。
    /// </summary>
    public interface ISecurityActionListView : ISecurityActionView
    {
        /// <summary>
        /// 获取元操作名称。
        /// </summary>
        string ActionName { get; }
        /// <summary>
        /// 显示消息。
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }
    /// <summary>
    /// 编辑界面接口。
    /// </summary>
    public interface ISecurityActionEditView : ISecurityActionView
    {
        /// <summary>
        /// 获取元操作ID。
        /// </summary>
        GUIDEx ActionID { get; }
        /// <summary>
        /// 绑定元操作类型。
        /// </summary>
        /// <param name="data"></param>
        void BindActionType(IListControlsData data);
    }
    /// <summary>
    /// 导入界面接口。
    /// </summary>
    public interface ISecurityActionImportView : ISecurityActionView
    {
        /// <summary>
        /// 获取或设置上传数据。
        /// </summary>
        object UploadFile { get; set; }
        /// <summary>
        /// 显示消息。
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }
    /// <summary>
    /// 导出界面接口。
    /// </summary>
    public interface ISecurityActionExportView : ISecurityActionView
    {
        /// <summary>
        /// 导出数据。
        /// </summary>
        /// <param name="type"></param>
        /// <param name="className"></param>
        /// <param name="query"></param>
        void ExportActionCollection(string type, string className, string query);
        /// <summary>
        /// 显示消息。
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }

	///<summary>
	/// SecurityActionPresenter行为类。
	///</summary>
	public class SecurityActionPresenter: ModulePresenter<ISecurityActionView>
	{
		#region 成员变量，构造函数。
        SecurityActionEntity securityActionEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public SecurityActionPresenter(ISecurityActionView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Action_ModuleID;
            this.securityActionEntity = new SecurityActionEntity();
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
                ISecurityActionListView listView = this.View as ISecurityActionListView;
                if (listView != null)
                {
                    DataTable dtSource = this.securityActionEntity.ListDataSource(listView.ActionName);
                    if (dtSource != null)
                    {
                        dtSource.Columns.Add("ActionTypeName");
                        foreach (DataRow row in dtSource.Rows)
                        {
                            row["ActionTypeName"] = this.GetEnumMemberName(typeof(EnumActionType), Convert.ToInt32(row["ActionType"]));
                        }
                        return dtSource;
                    }
                }
                return null;
            }
        }

		///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<SecurityAction>> handler)
		{
            ISecurityActionEditView editView = this.View as ISecurityActionEditView;
            if (editView != null)
            {
                SecurityAction data = new SecurityAction();
                data.ActionID = editView.ActionID;
                if (this.securityActionEntity.LoadRecord(ref data))
                    handler(this, new EntityEventArgs<SecurityAction>(data));
            }
		}
        /// <summary>
        /// 更新数据。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateAction(SecurityAction data)
        {
            return this.securityActionEntity.UpdateRecord(data);
        }
        /// <summary>
        /// 批量删除数据。
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteAction(StringCollection priCollection)
        {
            bool result = false;
            if (priCollection != null)
            {
                ISecurityActionListView listView = this.View as ISecurityActionListView;
                string err= null;
                foreach (string p in priCollection)
                {
                    result = this.securityActionEntity.DeleteAction(p, out err);
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

        #region 重载。
        /// <summary>
        /// 
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            ISecurityActionEditView editView = this.View as ISecurityActionEditView;
            if (editView != null)
            {
                editView.BindActionType(this.EnumDataSource(typeof(EnumActionType)));
            }
        }
        #endregion

        #region 导出。
        /// <summary>
        /// 导出列表数据。
        /// </summary>
        public DataTable ExportListDataSource
        {
            get
            {
                DataTable dtSource = this.securityActionEntity.GetAllRecord();
                if (dtSource != null)
                {
                    dtSource.Columns.Add("ActionTypeName");
                    foreach (DataRow row in dtSource.Rows)
                    {
                        row["ActionTypeName"] = this.GetEnumMemberName(typeof(EnumActionType), Convert.ToInt32(row["ActionType"]));
                    }
                    return dtSource;
                }
                return null;
            }
        }
        /// <summary>
        /// 导出数据。
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool ExportAction(StringCollection priCollection)
        {
            if (priCollection != null)
            {
                ISecurityActionExportView exprotView = this.View as ISecurityActionExportView;
                if (exprotView != null)
                {
                    if (priCollection.Count == 0)
                    {
                        exprotView.ShowMessage("请选择导出的数据项！");
                        return false;
                    }
                    string[] q = new string[priCollection.Count];
                    priCollection.CopyTo(q, 0);
                    exprotView.ExportActionCollection("text/xml", "ExportAction", string.Join(",", q));
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 导出数据静态类。
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static XmlDocument DownloadSecurityAction(string query)
        {
            lock (typeof(SecurityActionPresenter))
            {
                string[] q = string.IsNullOrEmpty(query) ? null : query.Split(',');
                XmlDocument doc = new XmlDocument();
                SecurityActionCollection exportCollection = new SecurityActionCollection();
                if (q != null)
                {
                    SecurityActionEntity entity = new SecurityActionEntity();
                    foreach (string id in q)
                    {
                        SecurityAction data = new SecurityAction();
                        data.ActionID = id;
                        if (entity.LoadRecord(ref data))
                            exportCollection.Add(data);
                    }
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    exportCollection.Serializer(ms);
                    ms.Position = 0;
                    doc.Load(ms);
                    ms.Close();
                }
                return doc;
            }
        }
        #endregion

        #region 导入。
        /// <summary>
        /// 检查上传文件格式。
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        public bool CheckUploadFormat(Stream upload)
        {
            bool result = false;
            ISecurityActionImportView importView = this.View as ISecurityActionImportView;
            if (importView != null && upload != null)
            {
                try
                {
                    SecurityActionCollection o = SecurityActionCollection.DeSerializer(upload);
                    if (o != null)
                    {
                        importView.UploadFile = o;
                        result = true;
                    }
                }
                catch (Exception) { }
                if (!result)
                    importView.ShowMessage("上传的文件格式不正确！(请上传权限元操作导出文件)");
            }
            return result;
        }
         /// <summary>
        /// 获取导入列表数据。
        /// </summary>
        public SecurityActionCollection ImportListViewDataSource
        {
            get
            {
                ISecurityActionImportView importView = this.View as ISecurityActionImportView;
                if (importView != null)
                {
                    return importView.UploadFile as SecurityActionCollection;
                }
                return null;
            }
        }
          /// <summary>
        /// 保存上存
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool UploadSecurityActionCollection(StringCollection priCollection)
        {
            bool result = false;
            ISecurityActionImportView importView = this.View as ISecurityActionImportView;
            if (priCollection != null && importView != null)
            {
                SecurityActionCollection actionCollection = importView.UploadFile as SecurityActionCollection;
                if (actionCollection == null)
                {
                    importView.ShowMessage("上传的数据因意外无法获取，请重新上传！");
                    return result;
                }
                SecurityAction data = null;
                foreach (string id in priCollection)
                {
                    data = actionCollection[id];
                    if (data != null)
                        result = this.securityActionEntity.UpdateRecord(data);
                }
            }
            return result;
        }
        #endregion
    }

}
