//================================================================================
//  FileName:UserPickerPresenter.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-15 10:20:16
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
// ----  ------  -----------
//
//================================================================================
//  Copyright (C) 2009-2010 Jeason Young Corporation
//================================================================================

using System;
using System.Collections.Generic;
using System.Text;

using iPower.Platform.Engine.DataSource;

using iPower.IRMP.Flow.Engine.Persistence;
namespace iPower.IRMP.Flow.Engine.Service
{
    /// <summary>
    /// 
    /// </summary>
    public enum EnumUserPickerType
    {
        /// <summary>
        /// 
        /// </summary>
        Employee = 0,
        /// <summary>
        /// 
        /// </summary>
        Role = 1,
        /// <summary>
        /// 
        /// </summary>
        Rank = 2,
        /// <summary>
        /// 
        /// </summary>
        Post = 3
    }
    /// <summary>
    /// 用户界面接口。
    /// </summary>
    public interface IUserPickerView : IModuleView
    {
        /// <summary>
        /// 获取部门名称。
        /// </summary>
        string DepartmentName { get; }
        /// <summary>
        /// 获取用户名称。
        /// </summary>
        string EmployeeName { get; }
        /// <summary>
        /// 获取用户性别名称。
        /// </summary>
        string EmployeeSexName { get; }
        /// <summary>
        /// 获取值。
        /// </summary>
        string[] Values { get; }
        /// <summary>
        /// 获取是否多选。
        /// </summary>
        bool MultiSelect { get; }
        /// <summary>
        /// 获取类型。
        /// </summary>
        EnumUserPickerType PickerType { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        void DisplayPanel(EnumUserPickerType type, IListControlsData data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        void DisplayEmployeePanel(IListControlsData data);
    }
    /// <summary>
    /// 用户处理。
    /// </summary>
    public class UserPickerPresenter : ModulePresenter<IUserPickerView>
    {
        #region 成员变量，构造函数。
        UserPickerEntity userPickerEntity = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="view"></param>
        public UserPickerPresenter(IUserPickerView view)
            : base(view)
        {
            this.userPickerEntity = new UserPickerEntity();
        }
        #endregion

        #region 重载。
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            IUserPickerView pickerView = this.View as IUserPickerView;
            if (pickerView != null)
            {
                IListControlsData data = null;
                switch (pickerView.PickerType)
                {
                    case EnumUserPickerType.Employee:
                        data = this.userPickerEntity.FindByEmployeeID(pickerView.Values);
                        break;
                    case EnumUserPickerType.Role:
                        data = this.userPickerEntity.BindRoles();
                        break;
                    case EnumUserPickerType.Rank:
                        data = this.userPickerEntity.BindRanks();
                        break;
                    case EnumUserPickerType.Post:
                        data = this.userPickerEntity.BindPosts();
                        break;
                }
                pickerView.DisplayPanel(pickerView.PickerType, data);
            }
        }
        #endregion

        public void SearchEmployee()
        {
            IUserPickerView pickerView = this.View as IUserPickerView;
            if ((pickerView != null) && (pickerView.PickerType == EnumUserPickerType.Employee))
            {
                IListControlsData data = this.userPickerEntity.BindEmployees(pickerView.DepartmentName, pickerView.EmployeeSexName, pickerView.EmployeeName, pickerView.Values);
                if (data != null)
                    pickerView.DisplayEmployeePanel(data);
            }
        }
    }
}
