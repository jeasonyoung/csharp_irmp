//================================================================================
// FileName: SecurityActionPresenter.cs
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
	/// ISecurityActionView�ӿڡ�
	///</summary>
	public interface ISecurityActionView: IModuleView
	{

	}
    /// <summary>
    /// �б����ӿڡ�
    /// </summary>
    public interface ISecurityActionListView : ISecurityActionView
    {
        /// <summary>
        /// ��ȡԪ�������ơ�
        /// </summary>
        string ActionName { get; }
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }
    /// <summary>
    /// �༭����ӿڡ�
    /// </summary>
    public interface ISecurityActionEditView : ISecurityActionView
    {
        /// <summary>
        /// ��ȡԪ����ID��
        /// </summary>
        GUIDEx ActionID { get; }
        /// <summary>
        /// ��Ԫ�������͡�
        /// </summary>
        /// <param name="data"></param>
        void BindActionType(IListControlsData data);
    }
    /// <summary>
    /// �������ӿڡ�
    /// </summary>
    public interface ISecurityActionImportView : ISecurityActionView
    {
        /// <summary>
        /// ��ȡ�������ϴ����ݡ�
        /// </summary>
        object UploadFile { get; set; }
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }
    /// <summary>
    /// ��������ӿڡ�
    /// </summary>
    public interface ISecurityActionExportView : ISecurityActionView
    {
        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="type"></param>
        /// <param name="className"></param>
        /// <param name="query"></param>
        void ExportActionCollection(string type, string className, string query);
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }

	///<summary>
	/// SecurityActionPresenter��Ϊ�ࡣ
	///</summary>
	public class SecurityActionPresenter: ModulePresenter<ISecurityActionView>
	{
		#region ��Ա���������캯����
        SecurityActionEntity securityActionEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SecurityActionPresenter(ISecurityActionView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Action_ModuleID;
            this.securityActionEntity = new SecurityActionEntity();
		}
		#endregion
        
		#region ���ݲ���������
        /// <summary>
        /// ��ȡ�б�����Դ��
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
		///�༭ҳ��������ݡ�
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
        /// �������ݡ�
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateAction(SecurityAction data)
        {
            return this.securityActionEntity.UpdateRecord(data);
        }
        /// <summary>
        /// ����ɾ�����ݡ�
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

        #region ���ء�
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

        #region ������
        /// <summary>
        /// �����б����ݡ�
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
        /// �������ݡ�
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
                        exprotView.ShowMessage("��ѡ�񵼳��������");
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
        /// �������ݾ�̬�ࡣ
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

        #region ���롣
        /// <summary>
        /// ����ϴ��ļ���ʽ��
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
                    importView.ShowMessage("�ϴ����ļ���ʽ����ȷ��(���ϴ�Ȩ��Ԫ���������ļ�)");
            }
            return result;
        }
         /// <summary>
        /// ��ȡ�����б����ݡ�
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
        /// �����ϴ�
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
                    importView.ShowMessage("�ϴ��������������޷���ȡ���������ϴ���");
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
