//================================================================================
//  FileName: StepAuthorize.cs
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
    /// 流程步骤的授权集合。
    /// </summary>
    public class StepAuthorizeCollection : WFCollection<StepAuthorize>
    {
    }
    /// <summary>
    /// 流程步骤的授权(针对自己拥有权限的步骤授权)
    /// </summary>
    public class StepAuthorize
    {
        /// <summary>
        /// 获取或设置授权ID。
        /// </summary>
        public string AuthorizeID { get; set; }
        /// <summary>
        /// 获取或设置授权用户ID。
        /// </summary>
        public string EmployeeID { get; set; }
        /// <summary>
        /// 获取或设置授权用户名称。
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 获取或设置被授权用户ID。
        /// </summary>
        public string TargetEmployeeID { get; set; }
        /// <summary>
        /// 获取或设置被授权用户名称。
        /// </summary>
        public string TargetEmployeeName { get; set; }
        /// <summary>
        /// 获取或设置授权生效开始时间。
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 获取或设置授权生效结束时间。
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
