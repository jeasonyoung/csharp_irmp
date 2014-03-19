//================================================================================
//  FileName:UserPickerPostInfo.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-15 16:01:07
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
    /// 岗位信息集合。
    /// </summary>
    public class UserPickerPostInfoCollection : UserPickerCollection<UserPickerPostInfo>
    {
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="postID">岗位ID。</param>
        /// <returns></returns>
        public NameValueCollection Find(string postID)
        {
            NameValueCollection collection = new NameValueCollection();
            UserPickerPostInfo info = this.Data.Find(new Predicate<UserPickerPostInfo>(delegate(UserPickerPostInfo sender)
            {
                return (sender != null) && (string.Equals(sender.PostID, postID, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase));
            }));

            if (info != null)
            {
                collection.Add(info.PostID, info.PostName);
            }

            return collection;
        }
    }

    /// <summary>
    /// 岗位信息。
    /// </summary>
    public class UserPickerPostInfo
    {
        #region 构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public UserPickerPostInfo()
        {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="postID">岗位ID。</param>
        /// <param name="postName">岗位名称。</param>
        public UserPickerPostInfo(string postID, string postName)
            : this()
        {
            this.PostID = postID;
            this.PostName = postName;
        }
        #endregion

        /// <summary>
        /// 获取或设置岗位ID。
        /// </summary>
        public string PostID { get; set; }
        /// <summary>
        /// 获取或设置岗位名称。
        /// </summary>
        public string PostName { get; set; }
    }
}
