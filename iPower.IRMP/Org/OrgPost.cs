//================================================================================
//  FileName: IOrgPost.cs
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
    /// 岗位数据接口。
    /// </summary>
    [XmlRoot("OrgPost")]
    [Serializable]
    public class OrgPost
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public OrgPost()
        {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="parentPostID">上级岗位ID。</param>
        /// <param name="postID">岗位ID。</param>
        /// <param name="postName">岗位名称。</param>
        /// <param name="departmentID">所属部门ID。</param>
        /// <param name="rankID">所属岗位级别ID。</param>
        public OrgPost(string parentPostID, string postID, string postName, string departmentID, string rankID)
        {
            this.ParentPostID = parentPostID;
            this.PostID = postID;
            this.PostName = postName;
            this.DepartmentID = departmentID;
            this.RankID = rankID;
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="postID">岗位ID。</param>
        /// <param name="postName">岗位名称。</param>
        /// <param name="departmentID">所属部门ID。</param>
        /// <param name="rankID">所属岗位级别ID。</param>
        public OrgPost(string postID, string postName, string departmentID, string rankID)
        {
            this.PostID = postID;
            this.PostName = postName;
            this.DepartmentID = departmentID;
            this.RankID = rankID;
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="postID">岗位ID。</param>
        /// <param name="postName">岗位名称。</param>
        /// <param name="departmentID">所属部门ID。</param>
        public OrgPost(string postID, string postName, string departmentID)
        {
            this.PostID = postID;
            this.PostName = postName;
            this.DepartmentID = departmentID;
        }
        #endregion

        /// <summary>
        /// 获取或设置岗位ID。
        /// </summary>
        [XmlElement("PostID")]
        public string PostID { get; set; }
        /// <summary>
        /// 获取或设置上级岗位ID。
        /// </summary>
        [XmlElement("ParentPostID")]
        public string ParentPostID { get; set; }
        /// <summary>
        /// 获取或设置所属部门ID。
        /// </summary>
        [XmlAttribute("departmentID")]
        public string DepartmentID { get; set; }
        /// <summary>
        /// 获取或设置所属岗位级别ID。
        /// </summary>
        [XmlAttribute("rankID")]
        public string RankID { get; set; }
        /// <summary>
        /// 获取或设置岗位名称。
        /// </summary>
        [XmlElement("PostName")]
        public string PostName { get; set; }
    }
    /// <summary>
    /// 岗位数据集合接口。
    /// </summary>
    [XmlRoot("OrgPosts")]
    [Serializable]
    public class OrgPostCollection : DataCollection<OrgPost>
    {
        #region 属性。
        /// <summary>
        /// 根据岗位ID获取岗位数据。
        /// </summary>
        /// <param name="postID"></param>
        /// <returns></returns>
        [XmlIgnore]
        public virtual OrgPost this[GUIDEx postID]
        {
            get
            {
                if (!postID.IsValid)
                    return null;
                OrgPost post = this.Items.Find(new Predicate<OrgPost>(delegate(OrgPost data)
                {
                    return (data != null) && (postID == data.PostID);
                }));
                return post;
            }
        }
        #endregion

        #region 函数。
        /// <summary>
        /// 根据岗位级别ID获取岗位数据。
        /// </summary>
        /// <param name="rankID">岗位级别ID。</param>
        /// <returns></returns>
        public virtual OrgPostCollection FindByRank(GUIDEx rankID)
        {
            if (rankID.IsValid)
            {
                List<OrgPost> listOrgPost = this.Items.FindAll(new Predicate<OrgPost>(delegate(OrgPost data)
                {
                    return (data != null) && (data.RankID == rankID);
                }));
                if (listOrgPost != null && listOrgPost.Count > 0)
                {
                    OrgPostCollection collection = new OrgPostCollection();
                    collection.InitAssignment(listOrgPost.GetEnumerator());
                    return collection;
                }
            }
            return null;
        }
        /// <summary>
        /// 根据部门ID获取岗位数据。
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public virtual OrgPostCollection FindByDepartment(GUIDEx departmentID)
        {
            if (departmentID.IsValid)
            {
                List<OrgPost> listOrgPost = this.Items.FindAll(new Predicate<OrgPost>(delegate(OrgPost data)
                {
                    return (data != null) && (data.DepartmentID == departmentID);
                }));
                if (listOrgPost != null && listOrgPost.Count > 0)
                {
                    OrgPostCollection collection = new OrgPostCollection();
                    collection.InitAssignment(listOrgPost.GetEnumerator());
                    return collection;
                }
            }
            return null;
        }
        /// <summary>
        /// 根据上级岗位ID获取岗位数据。
        /// </summary>
        /// <param name="parentPostID"></param>
        /// <returns></returns>
        public virtual OrgPostCollection FindByParent(GUIDEx parentPostID)
        {
            if (parentPostID.IsValid)
            {
                List<OrgPost> listOrgPost = this.Items.FindAll(new Predicate<OrgPost>(delegate(OrgPost data)
                {
                    return (data != null) && (data.ParentPostID == parentPostID);
                }));
                if (listOrgPost != null && listOrgPost.Count > 0)
                {
                    OrgPostCollection collection = new OrgPostCollection();
                    collection.InitAssignment(listOrgPost.GetEnumerator());
                    return collection;
                }
            }
            return null;
        }
        #endregion

        #region  重载。
        /// <summary>
        /// 是否存在。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool Contains(OrgPost item)
        {
            if (item != null)
            {
                return this[item.PostID] != null;
            }
            return false;
        }
        /// <summary>
        /// 排序。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override int Compare(OrgPost x, OrgPost y)
        {
            int result = string.Compare(x.DepartmentID, y.DepartmentID);
            if (result == 0)
            {
                result = string.Compare(x.RankID, y.RankID);
                if (result == 0)
                    result = string.Compare(x.PostName, y.PostName);
            }
            return result;
        }
        #endregion
    }
}
