//================================================================================
//  FileName: ProcessResumes.cs
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
namespace iPower.IRMP.Flow
{
    /// <summary>
    /// 流程履历。
    /// </summary>
    [Serializable]
    public class ProcessResumes
    {
        /// <summary>
        /// 获取或设置步骤实例ID。
        /// </summary>
        public string StepInstanceID
        {
            get;
            set;
        }
        /// <summary>
        /// 获取或设置步骤实例名称。
        /// </summary>
        public string StepInstanceName
        {
            get;
            set;
        }
        /// <summary>
        /// 获取或设置审批人ID。
        /// </summary>
        public string DoEmployeeID
        {
            get;
            set;
        }
        /// <summary>
        /// 获取或设置审批人名称。
        /// </summary>
        public string DoEmployeeName
        {
            get;
            set;
        }
        /// <summary>
        /// 获取或设置审批日期。
        /// </summary>
        public DateTime ApprovalDate
        {
            get;
            set;
        }
        /// <summary>
        /// 获取或设置审批意见。
        /// </summary>
        public string ApprovalViews
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 流程履历集合。
    /// </summary>
    [Serializable]
    public class ProcessResumesCollection : DataCollection<ProcessResumes>
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
