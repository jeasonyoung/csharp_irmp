//================================================================================
//  FileName: WinFlowEngineServiceCore.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/24
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

using iPower;

using iPower.IRMP.Flow;
using iPower.IRMP.Org;
using iPower.IRMP.Security;
using iPower.IRMP.Flow.WinService.Domain;
using iPower.IRMP.Flow.WinService.Persistence;
namespace iPower.IRMP.Flow.WinService
{
    /// <summary>
    /// 流程引擎Windows服务核心类。
    /// </summary>
    internal class WinFlowEngineServiceCore
    {
        #region 成员变量，构造函数。
        FlowProcessInstanceEntity flowProcessInstanceEntity = null;
        FlowStepInstanceEntity flowStepInstanceEntity = null;
        FlowParameterInstanceEntity flowParameterInstanceEntity = null;
        FlowStepInstanceDataEntity flowStepInstanceDataEntity = null;
        FlowInstanceRunErrorEntity flowInstanceRunErrorEntity = null;
        FlowInstanceTaskEntity flowInstanceTaskEntity = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="flowStepInstance"></param>
        public WinFlowEngineServiceCore()
        {
            this.flowProcessInstanceEntity = new FlowProcessInstanceEntity();
            this.flowStepInstanceEntity = new FlowStepInstanceEntity();
            this.flowParameterInstanceEntity = new FlowParameterInstanceEntity();
            this.flowStepInstanceDataEntity = new FlowStepInstanceDataEntity();
            this.flowInstanceRunErrorEntity = new FlowInstanceRunErrorEntity();
            this.flowInstanceTaskEntity = new FlowInstanceTaskEntity();
        }
        #endregion

        #region 公开函数。
        /// <summary>
        /// 启动。
        /// </summary>
        public void Start(FlowStepInstance flowStepInstance)
        {
            FlowProcessInstance flowProcessInstance = new FlowProcessInstance();
            flowProcessInstance.ProcessInstanceID = flowStepInstance.ProcessInstanceID;
            if (this.flowProcessInstanceEntity.LoadRecord(ref flowProcessInstance))
            {
                #region 流程处理。
                EnumInstanceProcessStatus status = (EnumInstanceProcessStatus)flowProcessInstance.InstanceProcessStatus;
                FlowInstanceRunError err = null;
                switch (status)
                {
                    case EnumInstanceProcessStatus.Run:
                        {
                            try
                            {
                                Process process = Utils.DeSerializerDatabaseFormart<Process>(flowProcessInstance.ProcessSerialization, flowProcessInstance.Verify);
                                if (process == null)
                                    throw new Exception("流程实例反序列化失败。");
                                this.FlowServiceCore(flowProcessInstance, flowStepInstance, process);

                                string strVerify = null;
                                flowProcessInstance.ProcessSerialization = Utils.SerializerDatabaseFormart<Process>(process, out strVerify);
                                if (!string.IsNullOrEmpty(strVerify) && (flowProcessInstance.Verify != strVerify))
                                {
                                    flowProcessInstance.Verify = strVerify;
                                    this.flowProcessInstanceEntity.UpdateRecord(flowProcessInstance);
                                }
                            }
                            catch (Exception e)
                            {
                                flowStepInstance.InstanceStepStatus = (int)EnumInstanceStepStatus.Error;
                                err = new FlowInstanceRunError();
                                err.ErrorID = GUIDEx.New;
                                err.ProcessInstanceID = flowProcessInstance.ProcessInstanceID;
                                err.StepInstanceID = flowStepInstance.StepInstanceID;
                                err.ErrorMessage = e.Message;
                                err.CreateDate = DateTime.Now;
                                this.flowInstanceRunErrorEntity.UpdateRecord(err);

                                flowProcessInstance.InstanceProcessStatus = (int)EnumInstanceProcessStatus.Error;
                                this.flowProcessInstanceEntity.UpdateRecord(flowProcessInstance);
                            }
                        }
                        break;

                    case EnumInstanceProcessStatus.Complete:
                        {
                            flowStepInstance.InstanceStepStatus = (int)EnumInstanceStepStatus.Error;
                            err = new FlowInstanceRunError();
                            err.ErrorID = GUIDEx.New;
                            err.ProcessInstanceID = flowProcessInstance.ProcessInstanceID;
                            err.StepInstanceID = flowStepInstance.StepInstanceID;
                            err.ErrorMessage = "该流程已经完成结束。";
                            err.CreateDate = DateTime.Now;
                            this.flowInstanceRunErrorEntity.UpdateRecord(err);
                        }
                        break;

                    case EnumInstanceProcessStatus.Error:
                        {
                            flowStepInstance.InstanceStepStatus = (int)EnumInstanceStepStatus.Error;
                        }
                        break;

                    case EnumInstanceProcessStatus.Stop:
                        {
                            flowStepInstance.InstanceStepStatus = (int)EnumInstanceStepStatus.Suspended;
                            err = new FlowInstanceRunError();
                            err.ErrorID = GUIDEx.New;
                            err.ProcessInstanceID = flowProcessInstance.ProcessInstanceID;
                            err.StepInstanceID = flowStepInstance.StepInstanceID;
                            err.ErrorMessage = "该流程已经被暂停。";
                            err.CreateDate = DateTime.Now;
                            this.flowInstanceRunErrorEntity.UpdateRecord(err);
                        }
                        break;
                }
                #endregion
            }
            else
                flowStepInstance.InstanceStepStatus = (int)EnumInstanceStepStatus.Suspended;
        }
        #endregion

        #region 辅助函数。
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flowProcessInstance">流程实例。</param>
        /// <param name="flowStepInstance">步骤实例。</param>
        /// <param name="process">流程定义。</param>
        void FlowServiceCore(FlowProcessInstance flowProcessInstance, FlowStepInstance flowStepInstance, Process process)
        {
            Step step = process.Steps[flowStepInstance.StepID];
            if (step == null)
                throw new Exception(string.Format("流程定义中没有找到步骤[步骤ID：{0}]", flowStepInstance.StepID));
            else if (step.StepType == EnumStepType.Last)//终结步骤。
            {
                flowProcessInstance.EndDate = flowStepInstance.EndDate = DateTime.Now;

                flowStepInstance.InstanceStepStatus = (int)EnumInstanceStepStatus.Complete;
                flowProcessInstance.InstanceProcessStatus = (int)EnumInstanceProcessStatus.Complete;

                this.flowProcessInstanceEntity.UpdateRecord(flowProcessInstance);
                return;
            }
            EnumStepMode stepMode = step.StepMode;
            TransitionCollection transitionCollection = null;
            if (stepMode == EnumStepMode.JoinAND)//与汇聚处理。
            {
                transitionCollection = process.Transitions.FindToStepTransition(step.StepID);
                if (transitionCollection == null || transitionCollection.Count == 0)
                    throw new Exception("流程定义中没有找到变迁规则。");
                bool bResult = true;
                foreach (Transition t in transitionCollection)
                {
                    bResult &= this.CoreConditionsCalculated(process, t, step, flowStepInstance.StepInstanceID, false);
                }
                if (!bResult)
                {
                    flowStepInstance.InstanceStepStatus = (int)EnumInstanceStepStatus.Suspended;
                    return;
                }
            }
            //非与汇聚处理。
            transitionCollection = process.Transitions.FindTransition(step.StepID);
            if (transitionCollection == null || transitionCollection.Count == 0)
                throw new Exception("流程定义中没有找到变迁规则。");
            foreach (Transition t in transitionCollection)
            {
                this.CoreConditionsCalculated(process, t, step, flowStepInstance.StepInstanceID, true);
            }
            flowStepInstance.InstanceStepStatus = (int)EnumInstanceStepStatus.Complete;
        }
        bool CoreConditionsCalculated(Process process,Transition t,Step step, GUIDEx stepInstanceID, bool toWriteTask)
        {
            bool result = false;
            if (process != null && t != null && stepInstanceID.IsValid)
            {
                #region 条件计算。
                EnumTransitionRule rule = t.TransitionRule;
                ConditionCollection collection = t.Conditions;
                if (collection != null && collection.Count > 0)
                {
                    switch (rule)
                    {
                        case EnumTransitionRule.AND:
                            bool bFlag = true;
                            foreach (Condition c in collection)
                            {
                                FlowParameterInstance flowParameterInstance = new FlowParameterInstance();
                                flowParameterInstance.StepInstanceID = stepInstanceID;
                                flowParameterInstance.ParameterID = c.ParameterID;
                                if (bFlag &= this.flowParameterInstanceEntity.LoadRecord(ref flowParameterInstance))
                                {
                                    bFlag &= this.ConditionCompareSign(flowParameterInstance.ParameterValue, c.CompareValue, c.ConditionValue);
                                }
                                if (!bFlag)
                                    break;
                            }
                            result = bFlag;
                            break;
                        case EnumTransitionRule.OR:
                            foreach (Condition c in collection)
                            {
                                FlowParameterInstance flowParameterInstance = new FlowParameterInstance();
                                flowParameterInstance.StepInstanceID = stepInstanceID;
                                flowParameterInstance.ParameterID = c.ParameterID;
                                if (this.flowParameterInstanceEntity.LoadRecord(ref flowParameterInstance))
                                {
                                    if (result = this.ConditionCompareSign(flowParameterInstance.ParameterValue, c.CompareValue, c.ConditionValue))
                                        break;
                                }
                                else
                                    throw new Exception(string.Format("参数未传递！[(stepInstanceID:{0},ParameterID:{1},)不存在!]", stepInstanceID, c.ParameterID));
                            }
                            break;
                    }
                }
                else
                    result = true;
                #endregion

                t.IsComplete = result;

                #region 添加任务。
                if (toWriteTask && result)
                {
                    Step toStep = process.Steps[t.ToStepID];
                    
                    if (toStep != null)
                    {
                        if ((toStep.StepType != EnumStepType.Last) && string.IsNullOrEmpty(toStep.EntryAction))
                            throw new Exception("未设置该步骤的入口方法或URL！");

                        StringBuilder query = new StringBuilder();
                        #region 参数相关。
                        List<string> toMapParameters = new List<string>();
                        //参数映射。
                        foreach (Parameter p in step.Parameters)
                        {
                            ParameterMap map = t.Maps[p.ParameterID];
                            if (map != null)
                            {
                                FlowParameterInstance flowParameterInstance = new FlowParameterInstance();
                                flowParameterInstance.StepInstanceID = stepInstanceID;
                                flowParameterInstance.ParameterID = p.ParameterID;
                                if (this.flowParameterInstanceEntity.LoadRecord(ref flowParameterInstance))
                                {
                                   Parameter mapP = toStep.Parameters[map.MapParameterID];
                                   if (mapP != null)
                                   {
                                       toMapParameters.Add(mapP.ParameterID);
                                       //传值。
                                       if (map.MapMode == EnumMapMode.Value)
                                           query.AppendFormat("&{0}={1}", mapP.ParameterName, Uri.EscapeDataString(flowParameterInstance.ParameterValue));
                                   }
                                }
                            }
                        }
                        //查找未映射的参数。
                        foreach (Parameter p in toStep.Parameters)
                        {
                            if (!toMapParameters.Contains(p.ParameterID) && !string.IsNullOrEmpty(p.DefaultValue))
                            {
                                query.AppendFormat("&{0}={1}", p.ParameterName, Uri.EscapeDataString(p.DefaultValue));
                            }
                        }
                        #endregion

                        #region 待办。
                        if (!string.IsNullOrEmpty(toStep.EntryAction))
                        {
                            //待办。
                            query.AppendFormat("&TaskCategory={0}", (int)EnumTaskCategory.Pending);
                            Dictionary<GUIDEx, string> emps = this.flowStepInstanceDataEntity.GetStepInstanceToDoEmployees(stepInstanceID);
                            if (emps == null || emps.Count == 0)
                            {
                                emps = new Dictionary<GUIDEx, string>();
                                #region StepEmployees。
                                if (toStep.StepEmployees != null && toStep.StepEmployees.Count > 0)
                                {
                                    foreach (StepEmployee se in toStep.StepEmployees)
                                    {
                                        if (!emps.ContainsKey(se.EmployeeID))
                                            emps.Add(se.EmployeeID, se.EmployeeName);
                                    }
                                }
                                #endregion

                                IOrgFactory orgFactory = new ModuleConfiguration().OrgFactory;
                                if (orgFactory != null)
                                { 
                                    List<GUIDEx> listPostID = new List<GUIDEx>();
                                    #region StepRanks。
                                    if (toStep.StepRanks != null && toStep.StepRanks.Count > 0)
                                    {
                                        //OrgPostCollection orgPostCollection = orgFactory.GetAllPost();
                                        //foreach (StepRank sr in toStep.StepRanks)
                                        //{
                                        //    OrgPostCollection postCollection = orgPostCollection.FindByRank(sr.RankID);
                                        //    if (postCollection != null && postCollection.Count > 0)
                                        //    {
                                        //        foreach (OrgPost p in postCollection)
                                        //        {
                                        //            if (!listPostID.Contains(p.PostID))
                                        //                listPostID.Add(p.PostID);
                                        //        }
                                        //    }
                                        //}
                                    }
                                    #endregion

                                    #region StepPost。
                                    if (toStep.StepPosts != null && toStep.StepPosts.Count > 0)
                                    {
                                        foreach (StepPost sp in toStep.StepPosts)
                                        {
                                            if (!listPostID.Contains(sp.PostID))
                                                listPostID.Add(sp.PostID);
                                        }
                                    }
                                    if (listPostID.Count > 0)
                                    {
                                        //OrgEmployeeCollection orgEmployeeCollection = orgFactory.GetAllEmployee();
                                        //foreach (GUIDEx p in listPostID)
                                        //{
                                        //    OrgEmployeeCollection employeeCollection = orgEmployeeCollection.FindByPost(p.Value);
                                        //    if (employeeCollection != null && employeeCollection.Count > 0)
                                        //    {
                                        //        foreach (OrgEmployee employee in employeeCollection)
                                        //        {
                                        //            if (!emps.ContainsKey(employee.EmployeeID))
                                        //                emps.Add(employee.EmployeeID, employee.EmployeeName);
                                        //        }
                                        //    }
                                        //}
                                    }
                                    #endregion
                                }
                                #region StepRoles。
                                if (toStep.StepRoles != null && toStep.StepRoles.Count > 0)
                                {
                                    ISecurityFactory securityFactory = new ModuleConfiguration().SecurityFactory;
                                    if (securityFactory != null)
                                    {
                                        foreach (StepRole sp in toStep.StepRoles)
                                        {
                                           OrgEmployeeCollection employeeCollection = securityFactory.GetAllEmployeeByRole(sp.RoleID);
                                           if (employeeCollection != null && employeeCollection.Count > 0)
                                           {
                                               foreach (OrgEmployee employee in employeeCollection)
                                               {
                                                   if (!emps.ContainsKey(employee.EmployeeID))
                                                       emps.Add(employee.EmployeeID, employee.EmployeeName);
                                               }
                                           }
                                        }
                                    }
                                }
                                #endregion
                            }
                            #region 处理用户
                            if (emps != null && emps.Count > 0)
                            {
                                foreach (KeyValuePair<GUIDEx, string> kv in emps)
                                {
                                    if (kv.Key.IsValid)
                                    {
                                        Dictionary<string, string> authEmps = toStep.StepAuthorizes.FindTargetEmployeeID(kv.Key, DateTime.Now);
                                        if (authEmps != null && authEmps.Count > 0)
                                        {
                                            #region 授权。
                                            foreach (KeyValuePair<string, string> kvp in authEmps)
                                            {
                                                FlowInstanceTask task = new FlowInstanceTask();
                                                task.TaskID = GUIDEx.New;
                                                task.StepInstanceID = stepInstanceID;
                                                task.EmployeeID = kv.Key;
                                                task.EmployeeName = kv.Value;
                                                task.AuthorizeEmployeeID = kvp.Key;
                                                task.AuthorizeEmployeeName = kvp.Value;
                                                task.DoEmployeeID = GUIDEx.Null;
                                                task.DoEmployeeName = string.Empty;
                                                task.TaskCategory = (int)EnumTaskCategory.Pending;
                                                task.BeginDate = DateTime.Now;
                                                task.BeginMode = (int)EnumTaskBeginMode.None;
                                                task.URL = string.Format("{0}{1}TaskID={2}{3}", 
                                                                        toStep.EntryAction,
                                                                        toStep.EntryAction.IndexOf('?') > -1 ? "&" : "?", 
                                                                        task.TaskID, 
                                                                        query.ToString());
                                                result = this.flowInstanceTaskEntity.UpdateRecord(task);
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            #region 非授权。
                                            FlowInstanceTask task = new FlowInstanceTask();
                                            task.TaskID = GUIDEx.New;
                                            task.StepInstanceID = stepInstanceID;
                                            task.EmployeeID = kv.Key;
                                            task.EmployeeName = kv.Value;
                                            task.AuthorizeEmployeeID = task.DoEmployeeID = GUIDEx.Null;
                                            task.AuthorizeEmployeeName = task.DoEmployeeName = string.Empty;
                                            task.TaskCategory = (int)EnumTaskCategory.Pending;
                                            task.BeginDate = DateTime.Now;
                                            task.BeginMode = (int)EnumTaskBeginMode.None;
                                            task.URL = string.Format("{0}{1}TaskID={2}{3}",
                                                                        toStep.EntryAction,
                                                                        toStep.EntryAction.IndexOf('?') > -1 ? "&" : "?",
                                                                        task.TaskID,
                                                                        query.ToString());
                                            result = this.flowInstanceTaskEntity.UpdateRecord(task);
                                            #endregion
                                        }
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception("未配置任务推送的用户信息！");
                            }
                            #endregion
                        }
                        #endregion

                        #region 待阅。
                        if (!string.IsNullOrEmpty(toStep.EntryQuery))
                        {
                            //待阅。
                            query.AppendFormat("&TaskCategory={0}", (int)EnumTaskCategory.BeRead);
                            Dictionary<GUIDEx, string> emps = this.flowStepInstanceDataEntity.GetStepInstanceToViewEmployees(stepInstanceID);
                            if (emps != null && emps.Count > 0)
                            {
                                foreach (KeyValuePair<GUIDEx, string> kv in emps)
                                {
                                    if (kv.Key.IsValid)
                                    {
                                        Dictionary<string, string> authEmps = toStep.StepAuthorizes.FindTargetEmployeeID(kv.Key, DateTime.Now);
                                        if (authEmps != null && authEmps.Count > 0)
                                        {
                                            #region 授权。
                                            foreach (KeyValuePair<string, string> kvp in authEmps)
                                            {
                                                FlowInstanceTask task = new FlowInstanceTask();
                                                task.TaskID = GUIDEx.New;
                                                task.StepInstanceID = stepInstanceID;
                                                task.EmployeeID = kv.Key;
                                                task.EmployeeName = kv.Value;
                                                task.AuthorizeEmployeeID = kvp.Key;
                                                task.AuthorizeEmployeeName = kvp.Value;
                                                task.DoEmployeeID = GUIDEx.Null;
                                                task.DoEmployeeName = string.Empty;
                                                task.TaskCategory = (int)EnumTaskCategory.BeRead;
                                                task.BeginDate = DateTime.Now;
                                                task.BeginMode = (int)EnumTaskBeginMode.None;
                                                task.URL = string.Format("{0}{1}TaskID={2}{3}",
                                                                         toStep.EntryQuery,
                                                                         toStep.EntryQuery.IndexOf('?') > -1 ? "&" : "?",
                                                                         task.TaskID,
                                                                         query.ToString());
                                                result = this.flowInstanceTaskEntity.UpdateRecord(task);
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            #region 非授权。
                                            FlowInstanceTask task = new FlowInstanceTask();
                                            task.TaskID = GUIDEx.New;
                                            task.StepInstanceID = stepInstanceID;
                                            task.EmployeeID = kv.Key;
                                            task.EmployeeName = kv.Value;
                                            task.AuthorizeEmployeeID = task.DoEmployeeID = GUIDEx.Null;
                                            task.AuthorizeEmployeeName = task.DoEmployeeName = string.Empty;
                                            task.TaskCategory = (int)EnumTaskCategory.BeRead;
                                            task.BeginDate = DateTime.Now;
                                            task.BeginMode = (int)EnumTaskBeginMode.None;
                                            task.URL = string.Format("{0}{1}TaskID={2}{3}",
                                                                        toStep.EntryQuery,
                                                                        toStep.EntryQuery.IndexOf('?') > -1 ? "&" : "?",
                                                                        task.TaskID,
                                                                        query.ToString());
                                            result = this.flowInstanceTaskEntity.UpdateRecord(task);
                                            #endregion
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
                #endregion
            }
            return result;
        }
        bool ConditionCompareSign(string paramDbValue, string resultValue, EnumCompareSign compareSign)
        {
            bool result = false;
            int intParamDbValue = 0, intResultValue = 0;
            switch (compareSign)
            {
                case EnumCompareSign.EQ://等于。
                    result = (paramDbValue == resultValue);
                    break;
                case EnumCompareSign.NEQ://不等于。
                    result = (paramDbValue != resultValue);
                    break;
                case EnumCompareSign.GT://大于。
                    try
                    {
                        intParamDbValue = int.Parse(paramDbValue);
                    }
                    catch (Exception) { }
                    try
                    {
                        intResultValue = int.Parse(resultValue);
                    }
                    catch (Exception) { }
                    result = (intParamDbValue > intResultValue);
                    break;
                case EnumCompareSign.GTEQ://大于等于。
                    try
                    {
                        intParamDbValue = int.Parse(paramDbValue);
                    }
                    catch (Exception) { }
                    try
                    {
                        intResultValue = int.Parse(resultValue);
                    }
                    catch (Exception) { }
                    result = (intParamDbValue >= intResultValue);
                    break;
                case EnumCompareSign.LT://小于。
                    try
                    {
                        intParamDbValue = int.Parse(paramDbValue);
                    }
                    catch (Exception) { }
                    try
                    {
                        intResultValue = int.Parse(resultValue);
                    }
                    catch (Exception) { }
                    result = (intParamDbValue < intResultValue);
                    break;
                case EnumCompareSign.LTEQ://小于等于。
                    try
                    {
                        intParamDbValue = int.Parse(paramDbValue);
                    }
                    catch (Exception) { }
                    try
                    {
                        intResultValue = int.Parse(resultValue);
                    }
                    catch (Exception) { }
                    result = (intParamDbValue <= intResultValue);
                    break;
                default:
                    break;
            }
            return result;
        }
        #endregion
    }
}
