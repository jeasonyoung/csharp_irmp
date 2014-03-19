//================================================================================
// FileName: FlowStepInstanceEntity.cs
// Desc:
// Called by
// Auth: 本代码由代码生成器自动生成。
// Date:
//================================================================================
// Change History
//================================================================================
// Date  Author  Description
// ----  ------  -----------
//
//================================================================================
// Copyright (C) 2009-2010 Jeason Young Corporation
//================================================================================
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Data;

using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.DataSource;
using iPower.Platform.Engine.Persistence;
using iPower.IRMP.Flow.Engine.Domain;
namespace iPower.IRMP.Flow.Engine.Persistence
{
	///<summary>
	///FlowStepInstanceEntity实体类。
	///</summary>
    internal class FlowStepInstanceEntity : DbModuleEntity<FlowStepInstance>
    {
        #region 成员变量，构造函数。
        ///<summary>
        ///构造函数
        ///</summary>
        public FlowStepInstanceEntity()
        {

        }
        #endregion

        #region 数据处理。
        /// <summary>
        /// 数据源。
        /// </summary>
        /// <param name="processInstanceID"></param>
        /// <returns></returns>
        public DataTable ListDataSource(GUIDEx processInstanceID)
        {
            if (processInstanceID.IsValid)
                return this.GetAllRecord(string.Format("ProcessInstanceID = '{0}'", processInstanceID), "CreateDate desc");
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stepInstanceID"></param>
        /// <param name="instanceName"></param>
        /// <param name="stepInstanceName"></param>
        public bool GetInstanceStepName(GUIDEx stepInstanceID, out string instanceName, out string stepInstanceName)
        {
            instanceName = stepInstanceName = string.Empty;
            bool result = false;
            if (stepInstanceID.IsValid)
            {
                FlowProcessInstanceEntity flowProcessInstanceEntity = new FlowProcessInstanceEntity();
                FlowStepInstance flowStepInstance = new FlowStepInstance();
                flowStepInstance.StepInstanceID = stepInstanceID;
                if (result = this.LoadRecord(ref flowStepInstance))
                {
                    stepInstanceName = flowStepInstance.StepName;

                    FlowProcessInstance flowProcessInstance = new FlowProcessInstance();
                    flowProcessInstance.ProcessInstanceID = flowStepInstance.ProcessInstanceID;
                    if (result = flowProcessInstanceEntity.LoadRecord(ref flowProcessInstance))
                    {
                        instanceName = flowProcessInstance.ProcessInstanceName;
                        Process p = Utils.DeSerializerDatabaseFormart<Process>(flowProcessInstance.ProcessSerialization, flowProcessInstance.Verify);
                        if (p != null && p.Transitions != null)
                        {
                            foreach (Transition t in p.Transitions)
                            {
                                if (t.IsComplete && (t.FromStepID == flowStepInstance.StepID))
                                {
                                    Step s = p.Steps[t.ToStepID];
                                    if (s != null)
                                        stepInstanceName = s.StepName;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取已经完成的步骤ID集合。
        /// </summary>
        /// <param name="processInstanceID"></param>
        /// <returns></returns>
        protected List<GUIDEx> GetInstanceStepStatusStepID(GUIDEx processInstanceID, EnumInstanceStepStatus stepStatus)
        {
            const string sql = "select distinct StepID from {0} where ProcessInstanceID = '{1}' and InstanceStepStatus = '{2}'";
            List<GUIDEx> list = new List<GUIDEx>();
            DataTable dtSource = this.DatabaseAccess.ExecuteDataset(string.Format(sql, this.TableName, processInstanceID, (int)stepStatus)).Tables[0];
            if (dtSource != null)
            {
                foreach (DataRow row in dtSource.Rows)
                {
                    list.Add(new GUIDEx(row[0]));
                }
            }
            return list;
        }
        /// <summary>
        /// 获取已经完成的步骤ID集合。
        /// </summary>
        /// <param name="processInstanceID"></param>
        /// <returns></returns>
        public List<GUIDEx> GetCompleteStepID(GUIDEx processInstanceID)
        {
            return this.GetInstanceStepStatusStepID(processInstanceID, EnumInstanceStepStatus.Complete);
        }
        #endregion
    }

}
