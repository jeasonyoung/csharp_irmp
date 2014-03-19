//================================================================================
//  FileName: StepPost.cs
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
    /// 流程步骤上的用户(映射岗位)集合。
    /// </summary>
    public class StepPostCollection : WFCollection<StepPost>
    {
    }
    /// <summary>
    /// 流程步骤上的用户(映射岗位)
    /// </summary>
    public class StepPost
    {
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
