//================================================================================
//  FileName: ModuleEnums.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/5/27
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

namespace iPower.IRMP.SysMgr.Engine.Persistence
{
    /// <summary>
    /// 授权状态枚举。
    /// </summary>
    public enum EnumAuthStatus
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
    /// 配置类型枚举。
    /// </summary>
    public enum EnumSettingType
    {
        /// <summary>
        /// 系统。
        /// </summary>
        System = 0x00,
        /// <summary>
        /// 个性化。
        /// </summary>
        Personal = 0x01
    }
    /// <summary>
    /// 部件模板状态枚举。
    /// </summary>
    public enum EnumWebPartStatus 
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
    /// 显示位置定义枚举
    /// </summary>
    public enum EnumZoneMode
    {
        /// <summary>
        /// 左
        /// </summary>
        Left = 0x00,
        /// <summary>
        /// 中
        /// </summary>
        Middle = 0x01,
        /// <summary>
        /// 右
        /// </summary>
        Right = 0x02,
    }
    /// <summary>
    /// 链接方式枚举
    /// </summary>
    public enum EnumLinkTarget
    {
        /// <summary>
        /// blank
        /// </summary>
        Blank = 0x00,
        /// <summary>
        /// self
        /// </summary>
        Self = 0x01,
    }
    /// <summary>
    /// 链接状态枚举
    /// </summary>
    public enum EnumLinkStatus
    {
        /// <summary>
        /// 停用
        /// </summary>
        Disable=0x00,
        /// <summary>
        /// 启用
        /// </summary>
        Enabled = 0x01,
        /// <summary>
        /// 申请
        /// </summary>
        Application = 0x02,
    }
}
