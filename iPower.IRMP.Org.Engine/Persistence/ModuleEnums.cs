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

namespace iPower.IRMP.Org.Engine.Persistence
{
    /// <summary>
    /// 状态枚举。
    /// </summary>
    public enum EnumStatus
    {
        /// <summary>
        /// 启用。
        /// </summary>
        Start = 1,
        /// <summary>
        /// 禁用。
        /// </summary>
        Stop = 0
    }
    /// <summary>
    /// 性别。
    /// </summary>
    public enum EnumGender
    {
        /// <summary>
        /// 未知。
        /// </summary>
        None = 0,
        /// <summary>
        /// 男性。
        /// </summary>
        Male = 1,
        /// <summary>
        /// 女性。
        /// </summary>
        Female = 2
    }
}
