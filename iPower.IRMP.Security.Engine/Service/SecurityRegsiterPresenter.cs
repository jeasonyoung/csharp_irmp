//================================================================================
// FileName: SecurityRegsiterPresenter.cs
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
	/// ISecurityRegsiterView�ӿڡ�
	///</summary>
	public interface ISecurityRegsiterView: IModuleView
	{
        /// <summary>
        /// ��ʾ���ݡ�
        /// </summary>
        /// <param name="content"></param>
        void ShowMessage(string content);
	}
    /// <summary>
    /// �б����ӿڡ�
    /// </summary>
    public interface ISecurityRegsiterListView : ISecurityRegsiterView
    {
        /// <summary>
        /// ��ȡϵͳ���ơ�
        /// </summary>
        string SystemName { get; }
        /// <summary>
        /// ��ȡ�ϼ�ϵͳID��
        /// </summary>
        GUIDEx ParentSystemID { get; }
        /// <summary>
        /// ���ϼ�ϵͳ��
        /// </summary>
        /// <param name="data"></param>
        void BindParentSystem(IListControlsTreeViewData data);
    }
    /// <summary>
    /// �༭����ӿڡ�
    /// </summary>
    public interface ISecurityRegsiterEditView : ISecurityRegsiterView
    {
        /// <summary>
        /// ��ȡϵͳID��
        /// </summary>
        GUIDEx SystemID { get; }
        /// <summary>
        /// ��ϵͳ���͡�
        /// </summary>
        /// <param name="data">����Դ��</param>
        void BindSystemType(IListControlsData data);
        /// <summary>
        /// ��ϵͳ״̬��
        /// </summary>
        /// <param name="data">����Դ��</param>
        void BindSystemStatus(IListControlsData data);
        /// <summary>
        /// ���ϼ�ϵͳ��
        /// </summary>
        /// <param name="data"></param>
        void BindParentSystem(IListControlsTreeViewData data);
    }
    /// <summary>
    /// �������ӿڡ�
    /// </summary>
    public interface ISecurityRegsiterImportView : ISecurityRegsiterView
    {
        /// <summary>
        /// ��ȡ�ϴ��ļ�����
        /// </summary>
        object UploadFile { get; set; }
        /// <summary>
        /// ��ȡϵͳ״̬��
        /// </summary>
        int SystemStatus { get; }
        /// <summary>
        /// ��ȡϵͳ���͡�
        /// </summary>
        int SystemType { get; }
        /// <summary>
        /// ��ϵͳ���͡�
        /// </summary>
        /// <param name="data">����Դ��</param>
        void BindSystemType(IListControlsData data);
        /// <summary>
        /// ��ϵͳ״̬��
        /// </summary>
        /// <param name="data">����Դ��</param>
        void BindSystemStatus(IListControlsData data);
    }
	///<summary>
	/// SecurityRegsiterPresenter��Ϊ�ࡣ
	///</summary>
	public class SecurityRegsiterPresenter: ModulePresenter<ISecurityRegsiterView>
	{
		#region ��Ա���������캯����
        SecurityRegsiterEntity securityRegsiterEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SecurityRegsiterPresenter(ISecurityRegsiterView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Regsiter_ModuleID;
            this.securityRegsiterEntity = new SecurityRegsiterEntity();
		}
		#endregion

        #region ���ء�
        /// <summary>
        /// �������ݡ�
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

        #region ���ݲ���������
        /// <summary>
        /// �б�����Դ��
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
        /// ����ɾ�����ݡ�
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
        /// ������ʼ��ϵͳģ��Ȩ�ޡ�
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
		///�༭ҳ��������ݡ�
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
        /// �������ݡ�
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
        
        #region ���봦��
        /// <summary>
        /// ����ϴ��ļ���ʽ��
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
                    importView.ShowMessage("�ϴ����ļ���ʽ����ȷ��(���ϴ��˵������ļ�)");
            }
            return result;
        }
        /// <summary>
        /// ��ȡ�����б����ݡ�
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
        /// �����ϴ�
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
                    importView.ShowMessage("�ϴ��������������޷���ȡ���������ϴ���");
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
