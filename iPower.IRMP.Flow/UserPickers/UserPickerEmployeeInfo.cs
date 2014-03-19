//================================================================================
//  FileName:UserPickerEmployeeInfo.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-15 14:44:35
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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

using iPower;
namespace Yaesoft.IRMP.Flow.UserPickers
{
    /// <summary>
    /// 用户信息集合。
    /// </summary>
    public class UserPickerEmployeeInfoCollection : UserPickerCollection<UserPickerEmployeeInfo>
    {
        #region 函数。
        /// <summary>
        /// 查找数据。
        /// </summary>
        /// <param name="department">部门名称。</param>
        /// <param name="sex">性别。</param>
        /// <param name="employeeName">用户名字。</param>
        /// <returns>用户信息集合(用户ID，用户姓名)。</returns>
        public NameValueCollection Find(string department, string sex, string employeeName)
        {
            NameValueCollection collection = new NameValueCollection();
            List<UserPickerEmployeeInfo> list = this.Data.FindAll(new Predicate<UserPickerEmployeeInfo>(delegate(UserPickerEmployeeInfo sender)
            {
                if (sender != null)
                {
                    if (!string.IsNullOrEmpty(department) && !string.IsNullOrEmpty(sex) && !string.IsNullOrEmpty(employeeName))
                        return (sender.DepartmentName.IndexOf(department) > -1) && (sender.Sex.IndexOf(sex) > -1) && (sender.EmployeeName.IndexOf(employeeName) > -1);

                    if (!string.IsNullOrEmpty(department) && !string.IsNullOrEmpty(employeeName))
                        return (sender.DepartmentName.IndexOf(department) > -1) && (sender.EmployeeName.IndexOf(employeeName) > -1);

                    if (!string.IsNullOrEmpty(department) && !string.IsNullOrEmpty(sex))
                        return (sender.EmployeeName.IndexOf(department) > -1) && (sender.Sex.IndexOf(sex) > -1);

                    if (!string.IsNullOrEmpty(sex) && !string.IsNullOrEmpty(employeeName))
                        return (sender.Sex.IndexOf(sex) > -1) && (sender.EmployeeName.IndexOf(employeeName) > -1);
                   
                    if (!string.IsNullOrEmpty(employeeName))
                        return (sender.EmployeeName.IndexOf(employeeName) > -1);
                    
                }
                return false;
            }));

            if (list != null)
            {
                foreach (UserPickerEmployeeInfo info in list)
                {
                    collection.Add(info.EmployeeID, info.EmployeeName);
                }
            }

            return collection;
        }
        /// <summary>
        /// 根据用户ID查找用户信息.
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <returns>用户信息集合(用户ID，用户姓名)。</returns>
        public NameValueCollection FindByEmployeeID(string employeeID)
        {
            NameValueCollection collection = new NameValueCollection();
            UserPickerEmployeeInfo info = this.Data.Find(new Predicate<UserPickerEmployeeInfo>(delegate(UserPickerEmployeeInfo sender)
              {
                  return (sender != null) && (string.Equals(sender.EmployeeID, employeeID, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase));
              }));

            if (info != null)
            {
                collection.Add(info.EmployeeID, info.EmployeeName);
            }

            return collection;
        }
        /// <summary>
        /// 根据角色ID查找用户信息。
        /// </summary>
        /// <param name="roleID">角色ID。</param>
        /// <returns>用户信息集合(用户ID，用户姓名)。</returns>
        public NameValueCollection FindByRoleID(string roleID)
        {
            NameValueCollection collection = new NameValueCollection();
            if (!string.IsNullOrEmpty(roleID))
            {
                List<UserPickerEmployeeInfo> list = this.Data.FindAll(new Predicate<UserPickerEmployeeInfo>(delegate(UserPickerEmployeeInfo sender)
                {
                    //return (sender != null) && (string.Equals(sender.RoleID, roleID, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase));
                    return (sender != null) && (sender.RoleID != null) && (Array.BinarySearch<String>(sender.RoleID, roleID) > -1);
                }));
                if (list != null)
                {
                    foreach (UserPickerEmployeeInfo info in list)
                    {
                        collection.Add(info.EmployeeID, info.EmployeeName);
                    }
                }
            }
            return collection;
        }
        /// <summary>
        /// 根据岗位级别ID查找用户信息。
        /// </summary>
        /// <param name="rankID">岗位级别ID。</param>
        /// <returns>用户信息集合(用户ID，用户姓名)。</returns>
        public NameValueCollection FindByRankID(string rankID)
        {
            NameValueCollection collection = new NameValueCollection();
            if (!string.IsNullOrEmpty(rankID))
            {
                List<UserPickerEmployeeInfo> list = this.Data.FindAll(new Predicate<UserPickerEmployeeInfo>(delegate(UserPickerEmployeeInfo sender)
                {
                    //return (sender != null) && (string.Equals(sender.RankID, rankID, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase));
                    return (sender != null) && (sender.RankID != null) && (Array.BinarySearch<String>(sender.RankID, rankID) > -1);
                }));
                if (list != null)
                {
                    foreach (UserPickerEmployeeInfo info in list)
                    {
                        collection.Add(info.EmployeeID, info.EmployeeName);
                    }
                }
            }
            return collection;
        }
        /// <summary>
        /// 根据岗位ID查找用户信息。
        /// </summary>
        /// <param name="postID">岗位ID。</param>
        /// <returns>用户信息集合(用户ID，用户姓名)。</returns>
        public NameValueCollection FindByPostID(string postID)
        {
            NameValueCollection collection = new NameValueCollection();
            if (!string.IsNullOrEmpty(postID))
            {
                List<UserPickerEmployeeInfo> list = this.Data.FindAll(new Predicate<UserPickerEmployeeInfo>(delegate(UserPickerEmployeeInfo sender)
                {
                    //return (sender != null) && (string.Equals(sender.PostID, postID, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase));
                    return (sender != null) && (sender.PostID != null) && (Array.BinarySearch<String>(sender.PostID, postID) > -1);
                }));
                if (list != null)
                {
                    foreach (UserPickerEmployeeInfo info in list)
                    {
                        collection.Add(info.EmployeeID, info.EmployeeName);
                    }
                }
            }
            return collection;
        }
        #endregion
    }
    /// <summary>
    /// 用户信息。
    /// </summary>
    public class UserPickerEmployeeInfo
    {
        #region 构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public UserPickerEmployeeInfo()
        {
            
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="departmentName">部门名称。</param>
        /// <param name="employeeID">用户ID。</param>
        /// <param name="employeeName">用户名称。</param>
        /// <param name="sex">性别。</param>
        public UserPickerEmployeeInfo(string departmentName, string employeeID, string employeeName, string sex)
            : this()
        {
            this.DepartmentName = departmentName;
            this.EmployeeID = employeeID;
            this.EmployeeName = employeeName;
            this.Sex = sex;
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="departmentName">部门名称。</param>
        /// <param name="employeeID">用户ID。</param>
        /// <param name="employeeName">用户名称。</param>
        /// <param name="sex">性别。</param>
        /// <param name="roleID">角色ID。</param>
        /// <param name="rankID">岗位级别ID。</param>
        /// <param name="postID">岗位ID。</param>
        public UserPickerEmployeeInfo(string departmentName, string employeeID, string employeeName, string sex, string[] roleID, string[] rankID, string[] postID):
            this(departmentName, employeeID, employeeName, sex)
        {
            this.RoleID = roleID;
            this.RankID = rankID;
            this.PostID = postID;
        }
        #endregion

        #region 属性。
        /// <summary>
        /// 获取或设置部门名称。
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 获取或设置性别。
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 获取或设置用户ID。
        /// </summary>
        public string EmployeeID { get; set; }
        /// <summary>
        /// 获取或设置用户名称。
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 获取或设置角色ID。
        /// </summary>
        public string[] RoleID { get; set; }
        /// <summary>
        /// 获取或设置岗位级别ID。
        /// </summary>
        public string[] RankID { get; set; }
        /// <summary>
        /// 获取或设置岗位ID。
        /// </summary>
        public string[] PostID { get; set; }
        #endregion
    }
}
