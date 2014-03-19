//================================================================================
//  FileName: IOrgDepartment.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/4/8
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
    /// 部门数据接口。
    /// </summary>
    [XmlRoot("OrgDepartment")]
    [Serializable]
    public class OrgDepartment
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public OrgDepartment()
        {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="parentDepartmentID">上级部门ID。</param>
        /// <param name="departmentID">部门ID。</param>
        /// <param name="departmentName">部门名称。</param>
        /// <param name="order">排序。</param>
        public OrgDepartment(string parentDepartmentID, string departmentID, string departmentName, int order)
        {
            this.ParentDepartmentID = parentDepartmentID;
            this.DepartmentID = departmentID;
            this.DepartmentName = departmentName;
            this.Order = order;
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="departmentID">部门ID。</param>
        /// <param name="departmentName">部门名称。</param>
        /// <param name="order">排序。</param>
        public OrgDepartment(string departmentID, string departmentName, int order)
        {
            this.DepartmentID = departmentID;
            this.DepartmentName = departmentName;
            this.Order = order;
        }
        #endregion

        /// <summary>
        /// 获取部门ID。
        /// </summary>
        [XmlElement("DepartmentID")]
        public string DepartmentID { get; set; }
        /// <summary>
        /// 获取上级部门ID。
        /// </summary>
        [XmlElement("ParentDepartmentID")]
        public string ParentDepartmentID { get; set; }
        /// <summary>
        /// 获取部门名称。
        /// </summary>
        [XmlElement("DepartmentName")]
        public string DepartmentName { get; set; }
        /// <summary>
        /// 获取或设置排序。
        /// </summary>
        [XmlAttribute("order")]
        public int Order { get; set; }
    }
    /// <summary>
    /// 部门数据集合接口。
    /// </summary>
    [XmlRoot("OrgDepartments")]
    [Serializable]
    public class OrgDepartmentCollection : DataCollection<OrgDepartment>
    {
        #region 属性。
        /// <summary>
        /// 根据部门ID查找部门数据。
        /// </summary>
        /// <param name="departmentID">部门ID。</param>
        /// <returns>部门数据。</returns>
        [XmlIgnore]
        public OrgDepartment this[GUIDEx departmentID]
        {
            get
            {
                if (!departmentID.IsValid)
                    return null;
                OrgDepartment result = this.Items.Find(new Predicate<OrgDepartment>(delegate(OrgDepartment data)
                {
                    return (data != null) && (data.DepartmentID == departmentID);
                }));
                return result;
            }
        }
        #endregion

        #region 函数。
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentDepartmentID"></param>
        /// <returns></returns>
        public OrgDepartmentCollection FindByParent(GUIDEx parentDepartmentID)
        {
            if (parentDepartmentID.IsValid)
            {
                List<OrgDepartment> childs = this.Items.FindAll(new Predicate<OrgDepartment>(delegate(OrgDepartment sender)
                {
                    return (sender != null) && (sender.ParentDepartmentID == parentDepartmentID);
                }));
                if (childs != null && childs.Count > 0)
                {
                    OrgDepartmentCollection collection = new OrgDepartmentCollection();
                    collection.InitAssignment(childs.GetEnumerator());
                    return collection;
                }
            }
            return null;
        }
        #endregion

        #region 重载。
        /// <summary>
        /// 排序。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override int Compare(OrgDepartment x, OrgDepartment y)
        {
            int result = x.Order - y.Order;
            if (result == 0)
                return string.Compare(x.DepartmentName, y.DepartmentName);
            return result;
        }
        /// <summary>
        /// 是否存在。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool Contains(OrgDepartment item)
        {
            if (item == null)
                return false;
            return this[item.DepartmentID] != null;
        }
        #endregion
    }
}
