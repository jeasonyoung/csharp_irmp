//================================================================================
//  FileName: StepEmployee.cs
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
    /// <summary>
    /// 流程步骤上的用户(映射用户)集合。
    /// </summary>
    public class StepEmployeeCollection : WFCollection<StepEmployee>
    {
    }
    /// <summary>
    /// 流程步骤上的用户(映射用户)
    /// </summary>
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
