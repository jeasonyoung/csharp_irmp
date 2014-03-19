//================================================================================
//  FileName: IOrgEmployee.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/4/9
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
using System.Text;
using System.Xml.Serialization;
using iPower;
using iPower.Data;
namespace iPower.IRMP.Org
{
    /// <summary>
    /// 用户数据。
    /// </summary>
    [XmlRoot("OrgEmployee")]
    [Serializable]
    public class OrgEmployee
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public OrgEmployee()
        {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <param name="employeeSign">用户标识。</param>
        /// <param name="employeeName">用户名称。</param>
        /// <param name="departmentID">部门ID。</param>
        /// <param name="postID">岗位ID。</param>
        /// <param name="order">排序。</param>
        public OrgEmployee(string employeeID, string employeeSign, string employeeName, string departmentID, string postID, int order)
        {
            this.EmployeeID = employeeID;
            this.EmployeeSign = employeeSign;
            this.EmployeeName = employeeName;
            this.DepartmentID = departmentID;
            this.PostID = postID;
            this.Order = order;
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <param name="employeeSign">用户标识。</param>
        /// <param name="employeeName">用户名称。</param>
        /// <param name="departmentID">部门ID。</param>
        /// <param name="order">排序。</param>
        public OrgEmployee(string employeeID, string employeeSign, string employeeName, string departmentID, int order)
        {
            this.EmployeeID = employeeID;
            this.EmployeeSign = employeeSign;
            this.EmployeeName = employeeName;
            this.DepartmentID = departmentID;
            this.Order = order;
        }
        #endregion

        /// <summary>
        /// 获取或设置用户ID。
        /// </summary>
        [XmlElement("EmployeeID")]
        public string EmployeeID { get; set; }
        /// <summary>
        /// 获取或设置所属部门ID。
        /// </summary>
        [XmlAttribute("DepartmentID")]
        public string DepartmentID { get; set; }
        /// <summary>
        /// 获取或设置所属岗位ID。
        /// </summary>
        [XmlAttribute("PostID")]
        public string PostID { get; set; }
        /// <summary>
        /// 获取或设置用户标识。
        /// </summary>
        [XmlElement("EmployeSign")]
        public string EmployeeSign { get; set; }
        /// <summary>
        /// 获取或设置用户姓名。
        /// </summary>
        [XmlElement("EmployeeName")]
        public string EmployeeName { get; set; }
        /// <summary>
        /// 获取或设置排序。
        /// </summary>
        [XmlAttribute("order")]
        public int Order { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Concat(this.EmployeeName, "[", this.EmployeeSign, "]");
        }
    }
    /// <summary>
    /// 用户数据集合接口。
    /// </summary>
    [XmlRoot("OrgEmployees")]
    [Serializable]
    public class OrgEmployeeCollection : DataCollection<OrgEmployee>
    {
        #region 属性。
        /// <summary>
        /// 根据用户ID查找用户数据。
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <returns></returns>
        [XmlIgnore]
        public virtual OrgEmployee this[GUIDEx employeeID]
        {
            get
            {
                if (!employeeID.IsValid)
                    return null;
                OrgEmployee employee = this.Items.Find(new Predicate<OrgEmployee>(delegate(OrgEmployee data)
                {
                    return (data != null) && (data.EmployeeID == employeeID);
                }));
                return employee;
            }
        }
        #endregion

        #region 重载。
        /// <summary>
        /// 重载。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool Contains(OrgEmployee item)
        {
            if (item != null)
            {
                return this[item.EmployeeID] != null;
            }
            return false;
        }
        /// <summary>
        /// 排序。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override int Compare(OrgEmployee x, OrgEmployee y)
        {
            int result = x.Order - y.Order;
            if (result == 0)
            {
                result = string.Compare(x.EmployeeName, y.EmployeeName);
                if (result == 0)
                    result = string.Compare(x.EmployeeSign, y.EmployeeSign);
            }
            return result;
        }
        #endregion

        #region 函数。
        /// <summary>
        /// 添加元素。
        /// </summary>
        /// <param name="employeeCollection"></param>
        public virtual void Add(OrgEmployeeCollection employeeCollection)
        {
            if (employeeCollection != null && employeeCollection.Count > 0)
            {
                foreach (OrgEmployee item in employeeCollection)
                {
                    this.Add(item);
                }
            }
        }
        /// <summary>
        /// 根据岗位ID查找用户数据。
        /// </summary>
        /// <param name="postID">岗位ID。</param>
        /// <returns></returns>
        public virtual OrgEmployeeCollection FindByPost(string postID)
        {
            if (!string.IsNullOrEmpty(postID))
            {
                List<OrgEmployee> listOrgEmployee = this.Items.FindAll(new Predicate<OrgEmployee>(delegate(OrgEmployee data)
                {
                    return (data != null) && (data.PostID == postID);
                }));
                if (listOrgEmployee != null && listOrgEmployee.Count > 0)
                {
                    OrgEmployeeCollection collection = new OrgEmployeeCollection();
                    collection.InitAssignment(listOrgEmployee.GetEnumerator());
                    return collection;
                }
            }
            return null;
        }
        /// <summary>
        /// 根据部门ID查找用户数据。
        /// </summary>
        /// <param name="departmentID">部门ID。</param>
        /// <returns></returns>
        public virtual OrgEmployeeCollection FindByDepartment(string departmentID)
        {
            if (!string.IsNullOrEmpty(departmentID))
            {
                List<OrgEmployee> listOrgEmployee = this.Items.FindAll(new Predicate<OrgEmployee>(delegate(OrgEmployee data)
                {
                    return (data != null) && (data.DepartmentID == departmentID);
                }));
                if (listOrgEmployee != null && listOrgEmployee.Count > 0)
                {
                    OrgEmployeeCollection collection = new OrgEmployeeCollection();
                    collection.InitAssignment(listOrgEmployee.GetEnumerator());
                    return collection;
                }
            }
            return null;
        }
        /// <summary>
        ///  根据条件模糊检索数据。
        /// </summary>
        /// <param name="employeeName">姓名。</param>
        /// <returns></returns>
        public virtual OrgEmployeeCollection FindByEmployee(string employeeName)
        {
            if (!string.IsNullOrEmpty(employeeName))
            {
                employeeName = employeeName.Trim();
                List<OrgEmployee> listOrgEmployee = this.Items.FindAll(new Predicate<OrgEmployee>(delegate(OrgEmployee data)
                {
                    return (data != null) && ((data.EmployeeName.IndexOf(employeeName) > -1) || (data.EmployeeSign.IndexOf(employeeName) > -1));
                }));
                if (listOrgEmployee != null && listOrgEmployee.Count > 0)
                {
                    OrgEmployeeCollection collection = new OrgEmployeeCollection();
                    collection.InitAssignment(listOrgEmployee.GetEnumerator());
                    return collection;
                }
            }
            return null;
        }
        #endregion
    }
}
