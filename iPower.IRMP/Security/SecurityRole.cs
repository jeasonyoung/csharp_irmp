//================================================================================
//  FileName: SecurityRole.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/27
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

using iPower;
using iPower.Data;
namespace iPower.IRMP.Security
{
    /// <summary>
    /// 安全角色数据类。
    /// </summary>
    [Serializable]
    public class SecurityRole
    {
        /// <summary>
        /// 获取或设置角色ID。
        /// </summary>
        public string RoleID { get; set; }
        /// <summary>
        /// 获取或设置角色名称。
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 获取或设置上级角色ID。
        /// </summary>
        public string ParentRoleID { get; set; }
    }
    /// <summary>
    /// 安全角色数据集合。
    /// </summary>
    [Serializable]
    public class SecurityRoleCollection : DataCollection<SecurityRole>
    {
        #region 属性。
        /// <summary>
        /// 安全角色数据类。
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public SecurityRole this[GUIDEx roleID]
        {
            get
            {
                if (!string.IsNullOrEmpty(roleID))
                {
                    SecurityRole role = this.Items.Find(new Predicate<SecurityRole>(delegate(SecurityRole sender)
                    {
                        return (sender != null) && (sender.RoleID == roleID);
                    }));
                    return role;
                }
                return null;
            }
        }
        #endregion

        #region 重载。
        /// <summary>
        /// 是否存在。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool Contains(SecurityRole item)
        {
            if (item != null)
            {
                return this[item.RoleID] != null;
            }
            return false;
        }
        /// <summary>
        /// 排序。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override int Compare(SecurityRole x, SecurityRole y)
        {
            int result = string.Compare(x.ParentRoleID, y.ParentRoleID);
            if (result == 0)
                result = string.Compare(x.RoleName, y.RoleName);
            return result;
        }
        #endregion
    }
}
