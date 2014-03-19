//================================================================================
//  FileName: WinFlowEngineService.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/23
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

using iPower.WinService;
using iPower.WinService.Jobs;

using iPower.IRMP.Flow.WinService.Domain;
using iPower.IRMP.Flow.WinService.Persistence;
namespace iPower.IRMP.Flow.WinService
{
    /// <summary>
    /// 流程引擎Windows服务。
    /// </summary>
    public class WinFlowEngineService : Job<ModuleConfiguration>, IJobFunction
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public WinFlowEngineService()
        {
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public override string JobName
        {
            get { return "流程引擎Windows服务"; }
        }
        /// <summary>
        /// 
        /// </summary>
        protected override ModuleConfiguration JobConfig
        {
            get { return new ModuleConfiguration(); }
        }

        protected override IJobFunction JobFunction
        {
            get { return this; }
        }

        #region IJobFunction 成员

        public void Run(params string[] args)
        {
            FlowStepInstanceEntity flowStepInstanceEntity = new FlowStepInstanceEntity();
            List<FlowStepInstance> list = flowStepInstanceEntity.LoadFlowStepInstance(EnumInstanceStepStatus.Committed);
            if (list.Count > 0)
            {
                WinFlowEngineServiceCore serviceCore = new WinFlowEngineServiceCore();
                foreach (FlowStepInstance fsi in list)
                {
                    serviceCore.Start(fsi);
                    if (fsi != null)
                    {
                        fsi.EndDate = DateTime.Now;
                        flowStepInstanceEntity.UpdateRecord(fsi);
                    }
                }
            }
        }

        #endregion
    }
}