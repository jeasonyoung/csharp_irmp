//================================================================================
//  FileName:StepPost.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-09 11:03:33
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
    /// 流程步骤上的用户(映射岗位)集合。
    /// </summary>
    [Serializable]
    public class StepPostCollection : FlowBaseCollection<StepPost>
    {
    }
    /// <summary>
    /// 流程步骤上的用户(映射岗位)
    /// </summary>
    [Serializable]
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
