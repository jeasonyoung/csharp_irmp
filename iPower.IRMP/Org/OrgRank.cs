//================================================================================
//  FileName: IOrgRank.cs
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
    /// 岗位级别。
    /// </summary>
    [XmlRoot("OrgRank")]
    [Serializable]
    public class OrgRank
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public OrgRank()
        {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="parentRankID">上级岗位级别。</param>
        /// <param name="rankID">岗位级别ID。</param>
        /// <param name="rankName">岗位级别名称。</param>
        public OrgRank(string parentRankID, string rankID, string rankName)
        {
            this.ParentRankID = parentRankID;
            this.RankID = rankID;
            this.RankName = rankName;
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="rankID">岗位级别ID。</param>
        /// <param name="rankName">岗位级别名称。</param>
        public OrgRank(string rankID, string rankName)
        {
            this.RankID = rankID;
            this.RankName = rankName;
        }
        #endregion

        /// <summary>
        /// 获取或设置上级岗位级别ID。
        /// </summary>
        [XmlElement("ParentRankID")]
        public string ParentRankID { get; set; }
        /// <summary>
        /// 获取或设置岗位级别ID。
        /// </summary>
        [XmlElement("RankID")]
        public string RankID { get; set; }
        /// <summary>
        /// 获取或设置岗位级别名称。
        /// </summary>
        [XmlElement("RankName")]
        public string RankName { get; set; }
    }
    /// <summary>
    /// 岗位级别集合接口。
    /// </summary>
    public class OrgRankCollection : DataCollection<OrgRank>
    {
        #region 属性。
        /// <summary>
        /// 根据岗位级别ID获取数据。
        /// </summary>
        /// <param name="rankID"></param>
        /// <returns></returns>
        public virtual OrgRank this[GUIDEx rankID]
        {
            get
            {
                if (!rankID.IsValid)
                    return null;
                OrgRank result = this.Items.Find(new Predicate<OrgRank>(delegate(OrgRank data)
                {
                    return (data != null) && (data.RankID == rankID);
                }));
                return result;
            }
        }
        #endregion

        #region 函数。
        /// <summary>
        /// 根据上级岗位级别ID查找。
        /// </summary>
        /// <param name="parentRankID"></param>
        /// <returns></returns>
        public OrgRankCollection FindByParent(GUIDEx parentRankID)
        {
            if (parentRankID.IsValid)
            {
                List<OrgRank> listOrgRank = this.Items.FindAll(new Predicate<OrgRank>(delegate(OrgRank sender)
                {
                    return (sender != null) && (sender.ParentRankID == parentRankID);
                }));
                if (listOrgRank != null && listOrgRank.Count > 0)
                {
                    OrgRankCollection collection = new OrgRankCollection();
                    collection.InitAssignment(listOrgRank.GetEnumerator());
                    return collection;
                }
            }
            return null;
        }
        #endregion

        #region 重载。
        /// <summary>
        /// 是否存在。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool Contains(OrgRank item)
        {
            if (item != null)
            {
                return this[item.RankID] != null;
            }
            return false;
        }
        /// <summary>
        /// 排序。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override int Compare(OrgRank x, OrgRank y)
        {
            int result = string.Compare(x.ParentRankID, y.ParentRankID);
            if (result == 0)
                result = string.Compare(x.RankName, y.RankName);
            return result;
        }
        #endregion
    }
}
