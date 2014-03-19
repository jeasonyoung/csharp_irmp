//================================================================================
// FileName: SecurityRightPresenter.cs
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
	/// ISecurityRightView�ӿڡ�
	///</summary>
	public interface ISecurityRightView: IModuleView
	{
        
	}
    /// <summary>
    /// �б����ӿڡ�
    /// </summary>
    public interface ISecurityRightListView : ISecurityRightView
    {
        /// <summary>
        /// ��ȡ����ϵͳID��
        /// </summary>
        GUIDEx SystemID { get; }
        /// <summary>
        /// ��ȡģ�����ơ�
        /// </summary>
        string ModuleName { get; }
        /// <summary>
        /// ��ģ�����ṹ��
        /// </summary>
        /// <param name="data"></param>
        void BindModuleTree(IListControlsTreeViewData data);
        /// <summary>
        /// ������ϵͳ���ݡ�
        /// </summary>
        /// <param name="data"></param>
        void BindSystem(IListControlsTreeViewData data);
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }
    /// <summary>
    /// �༭����ӿڡ�
    /// </summary>
    public interface ISecurityRightEditView : ISecurityRightView
    {
        /// <summary>
        /// ��ȡȨ��ID��
        /// </summary>
        GUIDEx RightID { get; }
        /// <summary>
        /// ��ȡ����ϵͳID��
        /// </summary>
        GUIDEx SystemID { get; }
        
        /// <summary>
        /// ��ģ�顣
        /// </summary>
        /// <param name="data"></param>
        void BindModule(IListControlsTreeViewData data);
        /// <summary>
        /// ��Ԫ������
        /// </summary>
        /// <param name="data"></param>
        void BindAction(IListControlsData data);
        /// <summary>
        /// ������ϵͳ���ݡ�
        /// </summary>
        /// <param name="data"></param>
        /// <param name="value"></param>
        void BindSystem(IListControlsTreeViewData data, string value);
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
    }
	///<summary>
	/// SecurityRightPresenter��Ϊ�ࡣ
	///</summary>
	public class SecurityRightPresenter: ModulePresenter<ISecurityRightView>
	{
		#region ��Ա���������캯����
        SecurityRightEntity securityRightEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SecurityRightPresenter(ISecurityRightView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Right_ModuleID;
            this.securityRightEntity = new SecurityRightEntity();
		}
		#endregion

        #region ���ء�
        /// <summary>
        /// װ�����ݡ�
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

        #region ���ݲ���������
        /// <summary>
        /// ��ȡ�б����ݡ�
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
        /// �������ϵͳˢ��ģ����Ϣ��
        /// </summary>
        public void ChangeSystemRefershModuleTree()
        {
            ISecurityRightListView listView = this.View as ISecurityRightListView;
            if(listView != null)
                listView.BindModuleTree(new SecurityModuleEntity().ParentModule(listView.SystemID));
        }
        /// <summary>
        /// �������ϵͳ��
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
        /// ����ɾ����
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
		///�༭ҳ��������ݡ�
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
        /// ����Ȩ�ޡ�
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
