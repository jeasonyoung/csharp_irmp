//================================================================================
// FileName: OrgRankPresenter.cs
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
using Domain = iPower.IRMP.Org.Engine.Domain;
using iPower.IRMP.Org.Engine.Persistence;
namespace iPower.IRMP.Org.Engine.Service
{
	///<summary>
	/// IOrgRankView接口。
	///</summary>
	public interface IOrgRankView: IModuleView
	{
        /// <summary>
        /// 绑定上级岗位级别。
        /// </summary>
        /// <param name="data"></param>
        void BindParentRank(IListControlsTreeViewData data);
	}
    /// <summary>
    /// 列表页面接口。
    /// </summary>
    public interface IOrgRankListView : IOrgRankView
    {
        /// <summary>
        /// 获取岗位级别名称。
        /// </summary>
        string RankName { get; }
        /// <summary>
        /// 获取父岗位级别ID。
        /// </summary>
        GUIDEx ParentRankID { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
    }
    /// <summary>
    /// 编辑页面接口。
    /// </summary>
    public interface IOrgRankEditView : IOrgRankView
    {
        /// <summary>
        /// 获取岗位级别ID。
        /// </summary>
        GUIDEx RankID { get; }
    }
	///<summary>
	/// OrgRankPresenter行为类。
	///</summary>
	public class OrgRankPresenter: ModulePresenter<IOrgRankView>
    {
        #region 成员变量，构造函数。
        OrgRankEntity orgRankEntity;
        ///<summary>
		///构造函数。
		///</summary>
		public OrgRankPresenter(IOrgRankView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Rank_ModuleID;
            this.orgRankEntity = new OrgRankEntity();
		}
		#endregion

        /// <summary>
        /// 列表数据源。
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

        #region 重载。
        /// <summary>
        /// 加载数据。
        /// </summary>
        protected override void PreViewLoadData()
        {
            this.View.BindParentRank(this.orgRankEntity.RankData);
            base.PreViewLoadData();
        }
        #endregion

        #region 数据操作函数。
        ///<summary>
		///编辑页面加载数据。
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
        /// 更新岗位级别数据。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateRankData(Domain.OrgRank data)
        {
            return this.orgRankEntity.UpdateRecord(data);
        }
        /// <summary>
        /// 批量删除岗位级别数据。
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
