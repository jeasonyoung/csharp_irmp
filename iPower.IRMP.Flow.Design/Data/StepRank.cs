//================================================================================
//  FileName: StepRank.cs
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
    /// 流程步骤上的用户(映射岗位级别)集合。
    /// </summary>
    public class StepRankCollection : WFCollection<StepRank>
    {
    }
    /// <summary>
    /// 流程步骤上的用户(映射岗位级别)
    /// </summary>
    public class StepRank
    {
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
