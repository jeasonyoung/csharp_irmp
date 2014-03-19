
//================================================================================
//  FileName: ModuleConstants.cs
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
    /// 模块常量。
    /// </summary>
    internal static class ModuleConstants
    {
        /// <summary>
        /// 应用系统访问授权模块ID。
        /// </summary>
        public const string AppAuthorization_ModuleID = "AS000000000000000000000000000101";
        /// <summary>
        /// 用户访问授权模块ID。
        /// </summary>
        public const string EmployeeAuthorization_ModuleID = "AS000000000000000000000000000201";

        /// <summary>
        /// 拒绝IP模块ID。
        /// </summary>
        public const string LimitRefusedIPAddr_ModuleID = "AS000000000000000000000000000102";
        /// <summary>
        /// 绑定IP模块ID。
        /// </summary>
        public const string LimitBindIPAddr_ModuleID = "AS000000000000000000000000000202";
        /// <summary>
        /// 指定时间区段模块ID。
        /// </summary>
        public const string LimitSpecifyTimeZone_ModuleID = "AS000000000000000000000000000302";
        /// <summary>
        /// 限制登录模块ID。
        /// </summary>
        public const string LimitLogin_ModuleID = "AS000000000000000000000000000402";
        /// <summary>
        /// 枚举数据维护模块ID。
        /// </summary>
        public const string CommonEnums_ModuleID = "AS000000000000000000000000000104";
        /// <summary>
        /// 资源数据维护模块ID。
        /// </summary>
        public const string Resources_ModuleID = "AS000000000000000000000000000204";
        /// <summary>
        /// 日志数据模块ID。
        /// </summary>
        public const string CommonLog_ModuleID = "AS000000000000000000000000000206";
        /// <summary>
        /// 系统参数设置模块ID
        /// </summary>
        public const string Setting_ModuleID = "AS000000000000000000000000000103";
        /// <summary>
        /// 注册系统部件模块ID
        /// </summary>
        public const string RegWebPartTemplate_ModuleID = "AS000000000000000000000000000203";
        /// <summary>
        /// 系统部件设置模块ID
        /// </summary>
        public const string WebPart_ModuleID = "AS000000000000000000000000000303";
        /// <summary>
        /// 部件位置定义模块ID
        /// </summary>
        public const string WebPartZone_ModuleID = "AS000000000000000000000000000403";
        /// <summary>
        /// 链接管理模块ID
        /// </summary>
        public const string Links_ModuleID = "AS000000000000000000000000030503";
        /// <summary>
        /// 首页设置模块ID
        /// </summary>
        public const string WebPartPersonal_ModuleID = "AS000000000000000000000000020503";
        /// <summary>
        /// 实例化参数设置模块ID
        /// </summary>
        public const string SettingPersonal_ModuleID = "AS000000000000000000000000010503";
        /// <summary>
        /// 单点登录票据模块ID。
        /// </summary>
        public const string SSOTicket_ModuleID = "AS000000000000000000000000000306";

    }
}
