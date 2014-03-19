//================================================================================
// FileName: OrgEmployeePresenter.cs
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
	/// IOrgEmployeeView接口。
	///</summary>
	public interface IOrgEmployeeView: IModuleView
	{
        /// <summary>
        /// 绑定部门。
        /// </summary>
        /// <param name="data"></param>
        void BindDepartment(IListControlsTreeViewData data);
	}
    /// <summary>
    /// 列表界面接口。
    /// </summary>
    public interface IOrgEmployeeListView : IOrgEmployeeView
    {
        /// <summary>
        /// 获取用户名称。
        /// </summary>
        string EmployeeName { get; }
        /// <summary>
        /// 获取部门ID。
        /// </summary>
        string DepartmentID { get; }
        /// <summary>
        /// 构建部门岗位树。
        /// </summary>
        /// <param name="data"></param>
        void BuildDepartPostTreeView(IListControlsTreeViewData data);
        /// <summary>
        /// 显示数据。
        /// </summary>
        /// <param name="content"></param>
        void ShowMessage(string content);
    }
    /// <summary>
    /// 编辑界面接口。
    /// </summary>
    public interface IOrgEmployeeEditView : IOrgEmployeeView
    {
        /// <summary>
        /// 获取用户ID。
        /// </summary>
        GUIDEx EmployeeID { get; }
        /// <summary>
        /// 获取部门ID。
        /// </summary>
        GUIDEx DepartmentID { get; }
        /// <summary>
        /// 绑定性别。
        /// </summary>
        /// <param name="data"></param>
        void BindSex(IListControlsData data);
        /// <summary>
        /// 绑定用户状态。
        /// </summary>
        /// <param name="data"></param>
        void BindEmployeeStatus(IListControlsData data);
        /// <summary>
        /// 绑定岗位。
        /// </summary>
        /// <param name="data"></param>
        void BindPost(IListControlsTreeViewData data);
    }
		
	///<summary>
	/// OrgEmployeePresenter行为类。
	///</summary>
	public class OrgEmployeePresenter: ModulePresenter<IOrgEmployeeView>
	{
		#region 成员变量，构造函数。
        OrgEmployeeEntity orgEmployeeEntity = null;
		///<summary>
		///构造函数。
		///</summary>
		public OrgEmployeePresenter(IOrgEmployeeView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Employee_ModuleID;
            this.orgEmployeeEntity = new OrgEmployeeEntity();
		}
		#endregion

        /// <summary>
        /// 获取列表数据。
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                IOrgEmployeeListView listView = this.View as IOrgEmployeeListView;
                if (listView != null)
                {
                    DataTable dtSouce = this.orgEmployeeEntity.ListViewDataSource(listView.EmployeeName, listView.DepartmentID);
                    if (dtSouce != null)
                    {
                        dtSouce.Columns.Add("GenderName", typeof(string));
                        dtSouce.Columns.Add("EmployeeStatusName", typeof(string));

                        foreach (DataRow row in dtSouce.Rows)
                        {
                            row["GenderName"] = this.GetEnumMemberName(typeof(EnumGender), Convert.ToInt32(row["Gender"]));
                            row["EmployeeStatusName"] = this.GetEnumMemberName(typeof(EnumStatus), Convert.ToInt32(row["EmployeeStatus"]));
                        }

                        return dtSouce.Copy();
                    }
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
            IOrgEmployeeListView listView = this.View as IOrgEmployeeListView;
            if (listView != null)
            {
                this.View.BindDepartment(new OrgPostEntity().DepartmentPost(null));
                listView.BuildDepartPostTreeView(new OrgPostEntity().DepartmentPost(null));
            }
            IOrgEmployeeEditView editView = this.View as IOrgEmployeeEditView;
            if (editView != null)
            {
                editView.BindDepartment(new OrgDepartmentEntity().Department);
                editView.BindEmployeeStatus(this.EnumDataSource(typeof(EnumStatus)));
                editView.BindSex(this.EnumDataSource(typeof(EnumGender)));
                this.ChangeDepartmentToPost(editView.DepartmentID);
            }
            base.PreViewLoadData();
        }
        #endregion

        #region 数据操作函数。
        /// <summary>
        /// 
        /// </summary>
        public void ChangeDepartmentToPost(string departmentID)
        {
            IOrgEmployeeEditView editView = this.View as IOrgEmployeeEditView;
            if(editView != null)
                editView.BindPost(new OrgPostEntity().Post(departmentID));
        }
        ///<summary>
		///编辑页面加载数据。
		///</summary>
		///<param name="handler"></param>
        public void LoadEntityData(EventHandler<EntityEventArgs<Domain.OrgEmployee>> handler)
		{
            IOrgEmployeeEditView editView = this.View as IOrgEmployeeEditView;
            if (editView != null)
            {
                Domain.OrgEmployee data = new Domain.OrgEmployee();
                data.EmployeeID = editView.EmployeeID;
                if (this.orgEmployeeEntity.LoadRecord(ref data))
                {
                    this.ChangeDepartmentToPost(data.DepartmentID);
                    handler(this, new EntityEventArgs<Domain.OrgEmployee>(data));
                }
            }
		}
        /// <summary>
        /// 更新用户数据。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateEmployee(Domain.OrgEmployee data)
        {
            if (data != null)
            {
                if (!string.IsNullOrEmpty(data.EmployeePassword))
                    data.PasswordDate = DateTime.Now;
                if (!string.IsNullOrEmpty(data.EmployeePassword2))
                    data.PasswordDate2 = DateTime.Now;
            }
            return this.orgEmployeeEntity.UpdateRecord(data);
        }
        /// <summary>
        /// 批量删除用户数据。
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteEmployee(StringCollection priCollection)
        {
            bool result = false;
            string err = string.Empty;
            IOrgEmployeeListView listView = this.View as IOrgEmployeeListView;
            foreach (string emp in priCollection)
            {
                result = this.orgEmployeeEntity.DeleteEmployee(emp, out err);
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
