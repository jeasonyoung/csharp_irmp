//================================================================================
// FileName: OrgPostPresenter.cs
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
using Domain = iPower.IRMP.Org.Engine.Domain;
using iPower.IRMP.Org.Engine.Persistence;
namespace iPower.IRMP.Org.Engine.Service
{
	///<summary>
	/// IOrgPostView�ӿڡ�
	///</summary>
	public interface IOrgPostView: IModuleView
	{
        /// <summary>
        /// �󶨸�λ�������ݡ�
        /// </summary>
        /// <param name="data"></param>
        void BindRank(IListControlsTreeViewData data);
	}
    /// <summary>
    /// �б�ӿڡ�
    /// </summary>
    public interface IOrgPostListView : IOrgPostView
    {
        /// <summary>
        /// ��ȡ�������ơ�
        /// </summary>
        string DepartmentName { get; }
        /// <summary>
        /// ��ȡ��λ����ID��
        /// </summary>
        GUIDEx RankID { get; }
        /// <summary>
        /// ������֯��Ԫ��
        /// </summary>
        /// <param name="data"></param>
        void BuildDepartmentTreeView(IListControlsTreeViewData data);
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
    }
    /// <summary>
    /// �༭ҳ��ӿڡ�
    /// </summary>
    public interface IOrgPostEditView : IOrgPostView
    {
        /// <summary>
        /// ��ȡ��λID��
        /// </summary>
        GUIDEx PostID { get; }
        /// <summary>
        /// ��ȡ����ID��
        /// </summary>
        GUIDEx DepartmentID { get; }
        /// <summary>
        /// �󶨲������ݡ�
        /// </summary>
        /// <param name="data"></param>
        void BindDepartment(IListControlsTreeViewData data);
        /// <summary>
        /// ���ϼ���λ��
        /// </summary>
        /// <param name="data"></param>
        void BindParentPost(IListControlsTreeViewData data);
    }
		
	///<summary>
	/// OrgPostPresenter��Ϊ�ࡣ
	///</summary>
	public class OrgPostPresenter: ModulePresenter<IOrgPostView>
	{
		#region ��Ա���������캯����
        OrgPostEntity orgPostEntity;
		///<summary>
		///���캯����
		///</summary>
		public OrgPostPresenter(IOrgPostView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Post_ModuleID;
            this.orgPostEntity = new OrgPostEntity();
		}
		#endregion

        /// <summary>
        /// �б�����Դ��
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                IOrgPostListView listView = this.View as IOrgPostListView;
                if (listView != null)
                    return this.orgPostEntity.ListDataSource(listView.DepartmentName, listView.RankID);
                return null;
            }
        }

        #region ���ء�
        /// <summary>
        /// ���ء�
        /// </summary>
        protected override void PreViewLoadData()
        {
            this.View.BindRank(new OrgRankEntity().RankData);
            IOrgPostEditView editView = this.View as IOrgPostEditView;
            if (editView != null)
            {
                editView.BindDepartment(new OrgDepartmentEntity().Department);
                this.ChangeDepartmentToParentPost(editView.DepartmentID);
            }
            IOrgPostListView listView = this.View as IOrgPostListView;
            if(listView != null)
                listView.BuildDepartmentTreeView(new OrgDepartmentEntity().Department);
            base.PreViewLoadData();
        }
        #endregion

        #region ���ݲ���������
        /// <summary>
        /// ������Ÿ��¸�λ��
        /// </summary>
        /// <param name="departmentID"></param>
        public void ChangeDepartmentToParentPost(string departmentID)
        {
            IOrgPostEditView editView = this.View as IOrgPostEditView;
            if (editView != null)
            {
                if (editView.PostID.IsValid)
                {
                    string strValue = string.Format("{0}-{1}", departmentID, editView.PostID);
                    editView.BindParentPost(this.orgPostEntity.NotSelfGetOffSprings(strValue));
                }
                else
                    editView.BindParentPost(this.orgPostEntity.PostData(departmentID));
            }
        }

        ///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
        public void LoadEntityData(EventHandler<EntityEventArgs<Domain.OrgPost>> handler)
		{
            IOrgPostEditView editView = this.View as IOrgPostEditView;
            if (editView != null)
            {
                Domain.OrgPost data = new Domain.OrgPost();
                data.PostID = editView.PostID;
                if (this.orgPostEntity.LoadRecord(ref data))
                {
                    this.ChangeDepartmentToParentPost(data.DepartmentID);
                    handler(this, new EntityEventArgs<Domain.OrgPost>(data));
                }
            }
		}
        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdatePostData(Domain.OrgPost data)
        {
            return this.orgPostEntity.UpdateRecord(data);
        }
        /// <summary>
        /// ����ɾ����λ��
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeletePost(StringCollection priCollection)
        {
            bool result = false;
            string err = string.Empty;
            IOrgPostListView listView = this.View as IOrgPostListView;
            foreach (string pid in priCollection)
            {
                result = this.orgPostEntity.DeletePost(pid, out err);
                if (!result)
                {
                    if (listView != null)
                        listView.ShowMessage(err);
                    break;
                }
            }
            return result;
        }
		#endregion

	}

}
