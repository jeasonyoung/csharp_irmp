//================================================================================
//  FileName:ModuleConstants.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-14 10:30:35
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

namespace iPower.IRMP.Flow.Engine.Persistence
{
    /// <summary>
    /// 模块常量。
    /// </summary>
    public static class ModuleConstants
    {
        /// <summary>
        /// 流程管理模块ID。
        /// </summary>
        public const string Process_ModuleID = "AF000000000000000000000000000101";
        /// <summary>
        /// 步骤定义模块ID。
        /// </summary>
        public const string Step_ModuleID = "AF000000000000000000000000000201";
        /// <summary>
        /// 参数定义模块ID。
        /// </summary>
        public const string Parameter_ModuleID = "AF000000000000000000000000000301";
        /// <summary>
        /// 变迁规则定义模块ID。
        /// </summary>
        public const string Transition_ModuleID = "AF000000000000000000000000000401";
        /// <summary>
        /// 参数映射模块ID。
        /// </summary>
        public const string ParameterMap_ModuleID = "AF000000000000000000000000000501";
        /// <summary>
        /// 授权管理模块ID。
        /// </summary>
        public const string StepAuthorize_ModuleID = "AF000000000000000000000000000601";
        /// <summary>
        /// 流程实例模块ID。
        /// </summary>
        public const string ProcessInstance_ModuleID = "AF000000000000000000000000000102";
    }
}
