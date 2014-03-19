//================================================================================
//  FileName: ModuleEnums.cs
//  Desc:枚举类文件。
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/2/24
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

namespace iPower.IRMP.Security.Engine.Persistence
{
    /// <summary>
    /// 系统类型枚举。
    /// </summary>
    [Flags]
    public enum EnumSystemType
    {
        /// <summary>
        /// 平台模块。
        /// </summary>
        PlatformModule = 0x01,
        /// <summary>
        /// 应用系统。
        /// </summary>
        WebApplication = 0x02,
        /// <summary>
        /// 信息展示前台。
        /// </summary>
        InfoShowFront = 0x04,
        /// <summary>
        /// CS应用系统。
        /// </summary>
        ClientServerApp = 0x08
    }
    /// <summary>
    /// 系统状态。
    /// </summary>
    public enum EnumSystemStatus
    {
        /// <summary>
        /// 停用。
        /// </summary>
        Stop = 0x00,
        /// <summary>
        /// 启用。
        /// </summary>
        Start = 0x01
    }
    /// <summary>
    /// 元操作类型。
    /// </summary>
    public enum EnumActionType
    {
        /// <summary>
        /// 基本。
        /// </summary>
        Basic = 0x00,
        /// <summary>
        /// 扩展。
        /// </summary>
        Extend = 0x01
    }
}
