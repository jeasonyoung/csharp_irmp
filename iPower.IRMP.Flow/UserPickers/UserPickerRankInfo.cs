//================================================================================
//  FileName:UserPickerRankInfo.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-15 15:55:35
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
using System.Collections.Specialized;
using System.Text;

namespace Yaesoft.IRMP.Flow.UserPickers
{
    /// <summary>
    /// 岗位级别信息集合。
    /// </summary>
    public class UserPickerRankInfoCollection : UserPickerCollection<UserPickerRankInfo>
    {
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="rankID">岗位级别ID。</param>
        /// <returns></returns>
        public NameValueCollection Find(string rankID)
        {
            NameValueCollection collection = new NameValueCollection();
            UserPickerRankInfo info = this.Data.Find(new Predicate<UserPickerRankInfo>(delegate(UserPickerRankInfo sender)
            {
                return (sender != null) && (string.Equals(sender.RankID, rankID, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase));
            }));

            if (info != null)
            {
                collection.Add(info.RankID, info.RankName);
            }

            return collection;
        }
    }

    /// <summary>
    /// 岗位级别信息。
    /// </summary>
    public class UserPickerRankInfo
    {
        #region 构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public UserPickerRankInfo()
        {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="rankID">岗位级别ID。</param>
        /// <param name="rankName">岗位级别名称。</param>
        public UserPickerRankInfo(string rankID, string rankName)
            : this()
        {
        }
        #endregion

        /// <summary>
        /// 获取或设置岗位级别ID。
        /// </summary>
        public string RankID { get; set; }
        /// <summary>
        /// 获取或设置岗位级别名称。
        /// </summary>
        public string RankName { get; set; }
    }
}
