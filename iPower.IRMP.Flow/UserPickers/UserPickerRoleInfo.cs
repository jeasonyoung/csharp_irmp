//================================================================================
//  FileName:UserPickerRoleInfo.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-15 15:49:05
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
    /// 角色信息集合。
    /// </summary>
    public class UserPickerRoleInfoCollection : UserPickerCollection<UserPickerRoleInfo>
    {
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="roleID">角色ID。</param>
        /// <returns></returns>
        public NameValueCollection Find(string roleID)
        {
            NameValueCollection collection = new NameValueCollection();
            UserPickerRoleInfo info = this.Data.Find(new Predicate<UserPickerRoleInfo>(delegate(UserPickerRoleInfo sender)
            {
                return (sender != null) && (string.Equals(sender.RoleID, roleID, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase));
            }));

            if (info != null)
            {
                collection.Add(info.RoleID, info.RoleName);
            }

            return collection;
        }
    }

    /// <summary>
    /// 角色信息。
    /// </summary>
    public class UserPickerRoleInfo
    {
        #region 构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public UserPickerRoleInfo()
        {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="roleID">角色ID。</param>
        /// <param name="roleName">角色名称。</param>
        public UserPickerRoleInfo(string roleID, string roleName)
            : this()
        {
            this.RoleID = roleID;
            this.RoleName = roleName;
        }
        #endregion

        /// <summary>
        /// 获取或设置角色ID。
        /// </summary>
        public string RoleID { get; set; }
        /// <summary>
        /// 获取或设置角色名称。
        /// </summary>
        public string RoleName { get; set; }
    }
}
