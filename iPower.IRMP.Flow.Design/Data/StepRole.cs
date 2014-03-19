//================================================================================
//  FileName: StepRole.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/5/24
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

namespace iPower.IRMP.Flow.Design.Data
{
    /// 流程步骤上的用户(映射角色)集合。
    /// </summary>
    public class StepRoleCollection :WFCollection<StepRole>
    {
    }
    /// <summary>
    /// 流程步骤上的用户(映射角色)
    /// </summary>
     public class StepRole
    {
        /// <summary>
        /// 获取或设置角色定义ID。
        /// </summary>
         public string RoleID
         {
             get;
             set;
         }
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
