//================================================================================
//  FileName:StepRole.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-09 11:01:40
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

namespace iPower.IRMP.Flow
{
    /// <summary>
    /// 流程步骤上的用户(映射角色)集合。
    /// </summary>
    [Serializable]
    public class StepRoleCollection : FlowBaseCollection<StepRole>
    {
    }
    /// <summary>
    /// 流程步骤上的用户(映射角色)
    /// </summary>
    [Serializable]
    public class StepRole
    {
        /// <summary>
        /// 获取或设置角色定义ID。
        /// </summary>
        public string RoleID { get; set; }
        /// <summary>
        /// 获取或设置角色定义名称。
        /// </summary>
        public string RoleName
        {
            get;
            set;
        }
    }
}
