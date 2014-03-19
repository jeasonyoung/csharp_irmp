//================================================================================
//  FileName: OrgEmployeePickerPresenter.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/3/21
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
//  ----    ------  -----------------
//
//================================================================================
//  Copyright (C) 2004-2009 Jeason Young Corporation
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
using iPower.IRMP.Org.Engine.Domain;
using iPower.IRMP.Org.Engine.Persistence;
namespace iPower.IRMP.Org.Engine.Service
{
    /// <summary>
    /// 选择用户视图接口。
    /// </summary>
    public interface IEmployeePicker : IModuleView
    {
        /// <summary>
        /// 获取是否多选。
        /// </summary>
        bool MultiSelect { get; }
        /// <summary>
        /// 获取数据值。
        /// </summary>
        string[] Values { get; }
        /// <summary>
        /// 获取部门名称。
        /// </summary>
        string DepartmentName { get; }
        /// <summary>
        /// 获取用户名称。
        /// </summary>
        string EmployeeName { get; }
        /// <summary>
        /// 获取性别。
        /// </summary>
        string Gender { get; }
        /// <summary>
        /// 绑定性别。
        /// </summary>
        /// <param name="data"></param>
        void BindGender(IListControlsData data);
        /// <summary>
        /// 显示用户信息。
        /// </summary>
        /// <param name="data"></param>
        void DisplayEmployeePanel(IListControlsData data);
        /// <summary>
        /// 显示查询结果。
        /// </summary>
        /// <param name="data"></param>
        void SearchEmployeeResult(IListControlsData data);
    }

    /// <summary>
    /// 选择用户行为类。
    /// </summary>
    public class OrgEmployeePickerPresenter : ModulePresenter<IEmployeePicker>
    {
        #region 成员变量，构造函数。
        OrgEmployeeEntity orgEmployeeEntity = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="view"></param>
        public OrgEmployeePickerPresenter(IEmployeePicker view)
            : base(view)
        {
            this.orgEmployeeEntity = new OrgEmployeeEntity();
        }
        #endregion

        #region 重载。
        /// <summary>
        /// 
        /// </summary>
        protected override void PreViewLoadData()
        {
            IEmployeePicker picker = this.View as IEmployeePicker;
            if (picker != null)
            {
                this.View.CurrentModuleTitle = string.Format("选择用户({0}选)", picker.MultiSelect ? "多" : "单");
                picker.BindGender(this.EnumDataSource(typeof(EnumGender)));

                if (picker.Values != null)
                    picker.DisplayEmployeePanel(this.orgEmployeeEntity.Employee(picker.Values));
            }
            base.PreViewLoadData();
        }
        #endregion

        #region 数据处理。
        /// <summary>
        /// 搜索用户。
        /// </summary>
        public void SearchEmployee()
        {
            IEmployeePicker picker = this.View as IEmployeePicker;
            if (picker != null)
            {
                picker.SearchEmployeeResult(this.orgEmployeeEntity.Employee(picker.DepartmentName,
                                                                            picker.EmployeeName,
                                                                            picker.Gender));
            }
        }
        #endregion

    }
}
