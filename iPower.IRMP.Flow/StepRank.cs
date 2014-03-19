//================================================================================
//  FileName:StepRank.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-09 11:05:11
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
    /// 流程步骤上的用户(映射岗位级别)集合。
    /// </summary>
    [Serializable]
    public class StepRankCollection : FlowBaseCollection<StepRank>
    {
    }
    /// <summary>
    /// 流程步骤上的用户(映射岗位级别)
    /// </summary>
    [Serializable]
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
