//================================================================================
// FileName: OrgPostPresenter.cs
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
	/// IOrgPostView接口。
	///</summary>
	public interface IOrgPostView: IModuleView
	{
        /// <summary>
        /// 绑定岗位级别数据。
        /// </summary>
        /// <param name="data"></param>
        void BindRank(IListControlsTreeViewData data);
	}
    /// <summary>
    /// 列表接口。
    /// </summary>
    public interface IOrgPostListView : IOrgPostView
    {
        /// <summary>
        /// 获取部门名称。
        /// </summary>
        string DepartmentName { get; }
        /// <summary>
        /// 获取岗位级别ID。
        /// </summary>
        GUIDEx RankID { get; }
        /// <summary>
        /// 构建组织单元数
        /// </summary>
        /// <param name="data"></param>
        void BuildDepartmentTreeView(IListControlsTreeViewData data);
        /// <summary>
        /// 显示消息。
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
    }
    /// <summary>
    /// 编辑页面接口。
    /// </summary>
    public interface IOrgPostEditView : IOrgPostView
    {
        /// <summary>
        /// 获取岗位ID。
        /// </summary>
        GUIDEx PostID { get; }
        /// <summary>
        /// 获取部门ID。
        /// </summary>
        GUIDEx DepartmentID { get; }
        /// <summary>
        /// 绑定部门数据。
        /// </summary>
        /// <param name="data"></param>
        void BindDepartment(IListControlsTreeViewData data);
        /// <summary>
        /// 绑定上级岗位。
        /// </summary>
        /// <param name="data"></param>
        void BindParentPost(IListControlsTreeViewData data);
    }
		
	///<summary>
	/// OrgPostPresenter行为类。
	///</summary>
	public class OrgPostPresenter: ModulePresenter<IOrgPostView>
	{
		#region 成员变量，构造函数。
        OrgPostEntity orgPostEntity;
		///<summary>
		///构造函数。
		///</summary>
		public OrgPostPresenter(IOrgPostView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Post_ModuleID;
            this.orgPostEntity = new OrgPostEntity();
		}
		#endregion

        /// <summary>
        /// 列表数据源。
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

        #region 重载。
        /// <summary>
        /// 加载。
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

        #region 数据操作函数。
        /// <summary>
        /// 变更部门更新岗位。
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
		///编辑页面加载数据。
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
        /// 更新数据。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdatePostData(Domain.OrgPost data)
        {
            return this.orgPostEntity.UpdateRecord(data);
        }
        /// <summary>
        /// 批量删除岗位。
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
