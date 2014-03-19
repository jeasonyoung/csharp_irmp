//================================================================================
//  FileName: ProcessResumesCollection.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/8/18
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
using iPower.Data;
namespace iPower.IRMP.Flow.Poxy
{
    /// <summary>
    /// 流程履历数据集合。
    /// </summary>
    [Serializable]
    public class ProcessResumesCollection  : DataCollection<ProcessResumes>
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public ProcessResumesCollection()
        {
        }
        #endregion

        #region 重载。
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override int Compare(ProcessResumes x, ProcessResumes y)
        {
            return (int)(y.ApprovalDate - x.ApprovalDate).TotalSeconds;
        }
        #endregion
    }
}
