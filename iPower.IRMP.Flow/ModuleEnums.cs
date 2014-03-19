//================================================================================
//  FileName:ModuleEnums.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-09 09:58:30
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
namespace iPower.IRMP.Flow
{
    #region 流程状态
    /// <summary>
    /// 流程状态。
    /// </summary>
    [Serializable]
    public enum EnumProcessStatus
    {
        /// <summary>
        /// 禁用。
        /// </summary>
        Stop = 0x00,
        /// <summary>
        /// 启用。
        /// </summary>
        Start = 0x01
    }
    #endregion

    #region 流程实例状态。
    /// <summary>
    /// 流程实例状态。
    /// </summary>
    [Serializable]
    public enum EnumInstanceProcessStatus
    {
        /// <summary>
        /// 暂停。
        /// </summary>
        Stop = 0x00,
        /// <summary>
        /// 运行。
        /// </summary>
        Run = 0x01,
        /// <summary>
        /// 完成。
        /// </summary>
        Complete = 0x02,
        /// <summary>
        /// 错误。
        /// </summary>
        Error = 0x03
    }
    /// <summary>
    /// 流程实例步骤运行状态。
    /// </summary>
    [Serializable]
    public enum EnumInstanceStepStatus
    {
        /// <summary>
        /// 提交。
        /// </summary>
        Committed = 0x00,
        /// <summary>
        /// 挂起。
        /// </summary>
        Suspended = 0x01,
        /// <summary>
        /// 完成。
        /// </summary>
        Complete = 0x02,
        /// <summary>
        /// 错误。
        /// </summary>
        Error = 0x03
    }
    #endregion

    #region 步骤类型。
    /// <summary>
    /// 步骤类型枚举。
    /// </summary>
    [Serializable]
    public enum EnumStepType
    {
        /// <summary>
        /// 开始步骤。
        /// </summary>
        First = 0x01,
        /// <summary>
        /// 中间步骤。
        /// </summary>
        Middle = 0x02,
        /// <summary>
        /// 终结步骤。
        /// </summary>
        Last = 0x04
    }
    #endregion

    #region 步骤模式，分支汇聚。
    /// <summary>
    /// 步骤模式枚举。
    /// </summary>
    [Serializable]
    public enum EnumStepMode
    {
        /// <summary>
        /// 与分支。
        /// </summary>
        SplitAND = 0x01,
        /// <summary>
        /// 或分支。
        /// </summary>
        SplitOR  = 0x02,
        /// <summary>
        /// 与汇聚。
        /// </summary>
        JoinAND  = 0x04,
        /// <summary>
        /// 或汇聚。
        /// </summary>
        JoinOR   = 0x08
    }
    #endregion

    #region 步骤通知消息类型。
    /// <summary>
    /// 步骤通知消息类型。
    /// </summary>
    [Flags]
    [Serializable]
    public enum EnumStepWarning
    {
        /// <summary>
        /// 未定义。
        /// </summary>
        None = 0x00,
        /// <summary>
        /// 短信。
        /// </summary>
        SMS = 0x01,
        /// <summary>
        /// 邮件。
        /// </summary>
        Email = 0x02,
        /// <summary>
        /// 站内消息。
        /// </summary>
        IAMS  = 0x04
    }
    #endregion

    #region 流程参数类型。
    /// <summary>
    /// 流程中各种操作参数的类型定义。
    /// </summary>
    [Serializable]
    public enum EnumParameterType
    {
        /// <summary>
        /// 字符串类型。
        /// </summary>
        String = 0x00,
        /// <summary>
        /// 整型数据。
        /// </summary>
        Int32  = 0x01
    }
    #endregion

    #region 变迁规则。
    /// <summary>
    /// 变迁规则枚举。
    /// </summary>
    [Serializable]
    public enum EnumTransitionRule
    {
        /// <summary>
        /// 与。
        /// </summary>
        AND = 0x00,
        /// <summary>
        /// 或。
        /// </summary>
        OR  = 0x01
    }
    #endregion

    #region IComparable的比较结果类型
    /// <summary>
    /// 比较操作符类别。
    /// </summary>
    [Serializable]
    public enum EnumCompareSign
    {
        /// <summary>
        /// 不可比较，0x00的十六进制值。
        /// </summary>
        None = 0x00,
        /// <summary>
        /// 大于，0x01的十六进制值。
        /// </summary>
        GT = 0x01,
        /// <summary>
        /// 等于，0x02的十六进制值。
        /// </summary>
        EQ = 0x02,
        /// <summary>
        /// 大于等于，0x03的十六进制值。
        /// </summary>
        GTEQ = 0x03,
        /// <summary>
        /// 小于，0x04的十六进制值。
        /// </summary>
        LT = 0x04,
        /// <summary>
        /// 不等于，0x04的十六进制值。
        /// </summary>
        NEQ = 0x05,
        /// <summary>
        /// 小于等于，0x06的十六进制值。
        /// </summary>
        LTEQ = 0x06

    }
    #endregion

    #region 参数映射模式
    /// <summary>
    /// 流程环节之间的参数映射模式，用于上一个环节的参数和下一个环节的参数之间的映射。
    /// </summary>
    [Serializable]
    public enum EnumMapMode
    {
        /// <summary>
        /// 直接传值模式，0x00的十六进制值。
        /// </summary>
        Value = 0x00,
        /// <summary>
        /// 函数反射模式，0x01的十六进制值。
        /// </summary>
        Reflection = 0x01
    }
    #endregion

    #region 数据类型。
    /// <summary>
    /// 数据类型枚举。
    /// </summary>
    public enum EnumDataCategory
    {
        /// <summary>
        /// 附加数据。
        /// </summary>
        AddData = 0x00,
        /// <summary>
        /// 流程履历数据。
        /// </summary>
        FlowData = 0x01
    }
    #endregion

    #region 任务类别。
    /// <summary>
    /// 任务类别枚举。
    /// </summary>
    public enum EnumTaskCategory
    {
        /// <summary>
        /// 待办。
        /// </summary>
        Pending = 1,
        /// <summary>
        /// 待阅。
        /// </summary>
        BeRead = 2

    }
    #endregion

    #region 任务进入模式。
    /// <summary>
    /// 任务进入模式。
    /// </summary>
    public enum EnumTaskBeginMode
    {
        /// <summary>
        /// 未进入
        /// </summary>
        None = 0,
        /// <summary>
        /// 正常进入。
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 授权进入。
        /// </summary>
        Authorize = 2
    }
    #endregion

    #region 任务离开模式。
    /// <summary>
    /// 任务离开模式。
    /// </summary>
    public enum EnumTaskEndMode
    {
        /// <summary>
        /// 未处理。
        /// </summary>
        None = 0,
        /// <summary>
        /// 正常离开。
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 超时处理。
        /// </summary>
        TimeOut = 2,
        /// <summary>
        /// 任务强占。
        /// </summary>
        TaskSeizure = 3
    }
    #endregion
}
