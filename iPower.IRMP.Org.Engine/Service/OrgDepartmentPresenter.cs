//================================================================================
// FileName: OrgDepartmentPresenter.cs
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
	/// IOrgDepartmentView接口。
	///</summary>
	public interface IOrgDepartmentView: IModuleView
	{
        /// <summary>
        /// 显示消息。
        /// </summary>
        /// <param name="content"></param>
        void ShowMessage(string content);
        /// <summary>
        /// 绑定上级部门。
        /// </summary>
        /// <param name="data"></param>
        void BindParentDepartment(IListControlsTreeViewData data);
	}

    /// <summary>
    /// 列表接口。
    /// </summary>
    public interface IOrgDepartmentListView : IOrgDepartmentView
    {
        /// <summary>
        /// 获取部门名称。
        /// </summary>
        string DepartmentName
        {
            get;
        }
        /// <summary>
        /// 获取上级部门ID
        /// </summary>
        GUIDEx ParentDepartmentID
        {
            get;
        }
    }
    /// <summary>
    /// 编辑接口。
    /// </summary>
    public interface IOrgDepartmentEditView : IOrgDepartmentView
    {
        /// <summary>
        /// 获取部门ID。
        /// </summary>
        GUIDEx DepartmentID { get; }
        /// <summary>
        /// 绑定部门状态。
        /// </summary>
        /// <param name="data"></param>
        void BindDepartmentStatus(IListControlsData data);
    }
		
	///<summary>
	/// OrgDepartmentPresenter行为类。
	///</summary>
	public class OrgDepartmentPresenter: ModulePresenter<IOrgDepartmentView>
	{
		#region 成员变量，构造函数。
        OrgDepartmentEntity orgDepartmentEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public OrgDepartmentPresenter(IOrgDepartmentView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Department_ModuleID;
            this.orgDepartmentEntity = new OrgDepartmentEntity();
		}
		#endregion

        #region 重载。
        /// <summary>
        /// 
        /// </summary>
        protected override void PreViewLoadData()
        {
            this.View.BindParentDepartment(this.orgDepartmentEntity.Department);
            IOrgDepartmentEditView editView = this.View as IOrgDepartmentEditView;
            if (editView != null)
            {
                editView.BindDepartmentStatus(this.EnumDataSource(typeof(EnumStatus)));
            }

            base.PreViewLoadData();
        }
        #endregion

        #region 数据操作函数。
        /// <summary>
        /// 列表数据接口。
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                IOrgDepartmentListView listView = this.View as IOrgDepartmentListView;
                if (listView != null)
                {
                    DataTable dtSource = this.orgDepartmentEntity.ListDataSource(listView.DepartmentName, listView.ParentDepartmentID);
                    if (dtSource != null)
                    {
                        dtSource.Columns.Add("DepartmentStatusName", typeof(string));
                        foreach (DataRow row in dtSource.Rows)
                        {
                            row["DepartmentStatusName"] = this.GetEnumMemberName(typeof(EnumStatus), Convert.ToInt32(row["DepartmentStatus"]));
                        }
                        return dtSource.Copy();
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// 批量删除数据。
        /// </summary>
        /// <param name="primaryCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteData(StringCollection primaryCollection)
        {
            bool result = false;
            string str = string.Empty;
            foreach (string pri in primaryCollection)
            {
                result = this.orgDepartmentEntity.DeleteDepartment(pri, out str);
                if (!result)
                {
                    if (!string.IsNullOrEmpty(str))
                        this.View.ShowMessage(str);
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// 保存部门信息数据。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SaveEntityData(Domain.OrgDepartment data)
        {
            return this.orgDepartmentEntity.UpdateRecord(data);
        }

		///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
        public void LoadEntityData(EventHandler<EntityEventArgs<Domain.OrgDepartment>> handler)
		{
            IOrgDepartmentEditView editView = this.View as IOrgDepartmentEditView;
            if (editView != null)
            {
                Domain.OrgDepartment data = new Domain.OrgDepartment();
                data.DepartmentID = editView.DepartmentID;
                if (this.orgDepartmentEntity.LoadRecord(ref data))
                {
                    editView.BindParentDepartment(this.orgDepartmentEntity.NotSelfGetOffSprings(data.DepartmentID));
                    handler(this, new EntityEventArgs<Domain.OrgDepartment>(data));
                }
            }
		}
		#endregion

	}

}
