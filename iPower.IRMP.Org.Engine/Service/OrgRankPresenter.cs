//================================================================================
// FileName: OrgRankPresenter.cs
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
	/// IOrgRankView�ӿڡ�
	///</summary>
	public interface IOrgRankView: IModuleView
	{
        /// <summary>
        /// ���ϼ���λ����
        /// </summary>
        /// <param name="data"></param>
        void BindParentRank(IListControlsTreeViewData data);
	}
    /// <summary>
    /// �б�ҳ��ӿڡ�
    /// </summary>
    public interface IOrgRankListView : IOrgRankView
    {
        /// <summary>
        /// ��ȡ��λ�������ơ�
        /// </summary>
        string RankName { get; }
        /// <summary>
        /// ��ȡ����λ����ID��
        /// </summary>
        GUIDEx ParentRankID { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
    }
    /// <summary>
    /// �༭ҳ��ӿڡ�
    /// </summary>
    public interface IOrgRankEditView : IOrgRankView
    {
        /// <summary>
        /// ��ȡ��λ����ID��
        /// </summary>
        GUIDEx RankID { get; }
    }
	///<summary>
	/// OrgRankPresenter��Ϊ�ࡣ
	///</summary>
	public class OrgRankPresenter: ModulePresenter<IOrgRankView>
    {
        #region ��Ա���������캯����
        OrgRankEntity orgRankEntity;
        ///<summary>
		///���캯����
		///</summary>
		public OrgRankPresenter(IOrgRankView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Rank_ModuleID;
            this.orgRankEntity = new OrgRankEntity();
		}
		#endregion

        /// <summary>
        /// �б�����Դ��
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                IOrgRankListView listView = this.View as IOrgRankListView;
                if (listView != null)
                {
                    return this.orgRankEntity.ListDataSource(listView.RankName, listView.ParentRankID);
                }
                return null;
            }
        }

        #region ���ء�
        /// <summary>
        /// �������ݡ�
        /// </summary>
        protected override void PreViewLoadData()
        {
            this.View.BindParentRank(this.orgRankEntity.RankData);
            base.PreViewLoadData();
        }
        #endregion

        #region ���ݲ���������
        ///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
        public void LoadEntityData(EventHandler<EntityEventArgs<Domain.OrgRank>> handler)
		{
            IOrgRankEditView editView = this.View as IOrgRankEditView;
            if (editView != null)
            {
                Domain.OrgRank data = new Domain.OrgRank();
                data.RankID = editView.RankID;
                if (this.orgRankEntity.LoadRecord(ref data))
                {
                    editView.BindParentRank(this.orgRankEntity.NotSelfGetOffSprings(data.RankID));
                    handler(this, new EntityEventArgs<Domain.OrgRank>(data));
                }
            }
		}
        /// <summary>
        /// ���¸�λ�������ݡ�
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateRankData(Domain.OrgRank data)
        {
            return this.orgRankEntity.UpdateRecord(data);
        }
        /// <summary>
        /// ����ɾ����λ�������ݡ�
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteRank(StringCollection priCollection)
        {
            bool result = false;
            string err = string.Empty;
            IOrgRankListView listView = this.View as IOrgRankListView;
            foreach (string rankId in priCollection)
            {
                result = this.orgRankEntity.DeleteRank(rankId, out err);
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
