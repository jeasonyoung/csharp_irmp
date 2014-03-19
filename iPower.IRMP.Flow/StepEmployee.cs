//================================================================================
//  FileName:StepEmployee.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-09 11:06:42
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
    /// 流程步骤上的用户(映射用户)集合。
    /// </summary>
    [Serializable]
    public class StepEmployeeCollection : FlowBaseCollection<StepEmployee>
    {
    }
    /// <summary>
    /// 流程步骤上的用户(映射用户)
    /// </summary>
    [Serializable]
    public class StepEmployee
    {
        /// <summary>
        /// 获取或设置用户ID。
        /// </summary>
        public string EmployeeID { get; set; }
        /// <summary>
        /// 获取或设置用户名称。
        /// </summary>
        public string EmployeeName { get; set; }
    }
}
