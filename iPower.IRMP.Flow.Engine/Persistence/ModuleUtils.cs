//================================================================================
//  FileName:ModuleUtils.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-31 17:17:20
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
using System.IO;

using iPower;
using iPower.Utility;
using iPower.Cryptography;

using iPower.IRMP.Flow;
using iPower.IRMP.Flow.Engine.Domain;
namespace iPower.IRMP.Flow.Engine.Persistence
{
    /// <summary>
    /// 模块工具类。
    /// </summary>
    internal static class ModuleUtils
    {
        #region 成员变量。
        static object SynchronizedObject = new object();
        #endregion

        #region 从数据库创建Process类。
        /// <summary>
        /// 从数据库创建Process类。
        /// </summary>
        /// <param name="processID">流程ID。</param>
        /// <returns>流程类。</returns>
        public static Process CreateProcess(GUIDEx processID)
        {
            lock (ModuleUtils.SynchronizedObject)
            {
                Process process = null;
                if (processID.IsValid)
                {
                    #region 流程信息。
                    FlowProcessEntity flowProcessEntity = new FlowProcessEntity();
                    FlowStepEntity flowStepEntity = new FlowStepEntity();
                    FlowTransitionEntity flowTransitionEntity = new FlowTransitionEntity();

                    FlowProcess p = new FlowProcess();
                    p.ProcessID = processID;
                    if (flowProcessEntity.LoadRecord(ref p))
                    {
                        process = new Process();
                        process.ProcessID = p.ProcessID;
                        process.ProcessName = p.ProcessName;
                        process.ProcessSign = p.ProcessSign;
                        process.BeginDate = p.BeginDate;
                        process.EndDate = p.EndDate;
                        process.ProcessDescription = p.ProcessDescription;

                        #region 步骤信息。
                        process.Steps = flowStepEntity.LoadStepCollection(process.ProcessID);
                        #endregion

                        #region 变迁规则。
                        process.Transitions = flowTransitionEntity.LoadTransitionCollection(process.ProcessID);
                        #endregion
                    }
                    #endregion
                }
                return process;
            }
        }
        #endregion
    }
}
