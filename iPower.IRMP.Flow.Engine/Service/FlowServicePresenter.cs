//================================================================================
//  FileName:FlowServicePresenter.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2011-01-05 16:07:14
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
// ----  ------  -----------
//
//================================================================================
//  Copyright (C) 2009-2011 Jeason Young Corporation
//================================================================================

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Data;

using iPower;
using iPower.Data;
using iPower.Utility;
using iPower.Cryptography;

using iPower.IRMP.Flow;
using iPower.IRMP.Flow.Engine.Domain;
using iPower.IRMP.Flow.Engine.Persistence;

namespace iPower.IRMP.Flow.Engine.Service
{
    /// <summary>
    /// 流程服务接口行为类。
    /// </summary>
    public class FlowServicePresenter: IFlowService
    {
        #region 成员变量，构造函数。
        FlowProcessEntity flowProcessEntity = null;
        FlowProcessInstanceEntity flowProcessInstanceEntity = null;
        FlowStepInstanceEntity flowStepInstanceEntity = null;
        FlowStepInstanceDataEntity flowStepInstanceDataEntity = null;
        FlowInstanceRunErrorEntity flowInstanceRunErrorEntity = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public FlowServicePresenter()
        {
            this.flowProcessEntity = new FlowProcessEntity();
            this.flowProcessInstanceEntity = new FlowProcessInstanceEntity();
            this.flowStepInstanceEntity = new FlowStepInstanceEntity();
            this.flowStepInstanceDataEntity = new FlowStepInstanceDataEntity();
            this.flowInstanceRunErrorEntity = new FlowInstanceRunErrorEntity();
        }
        #endregion

        #region IFlowService 成员
        // <summary>
        /// 初始化流程实例。
        /// </summary>
        /// <param name="processSign">流程标识。</param>
        /// <param name="processInstanceName">流程实例名称。</param>
        /// <param name="employeeID">进行操作的用户ID。</param>
        /// <param name="employeeName">进行操作的用户名称。</param>
        /// <param name="processInstanceID">初始化流程实例的唯一标识。</param>
        /// <param name="msgError">调用失败时的错误消息。</param>
        /// <returns>实例化成功则返回true,否则返回false。</returns>
        public bool InitFlow(string processSign, string processInstanceName, GUIDEx employeeID, string employeeName, out GUIDEx processInstanceID, out string msgError)
        {
            lock (this)
            {
                bool result = false;
                msgError = processInstanceID = null;
                try
                {
                    #region 验证参数。
                    if (string.IsNullOrEmpty(processSign))
                    {
                        msgError = "流程标识为空。";
                        return result;
                    }
                    if (string.IsNullOrEmpty(processInstanceName))
                    {
                        msgError = "流程实例名称为空。";
                        return result;
                    }
                    #endregion

                    #region 获取流程定义。
                    GUIDEx processID = this.flowProcessEntity.FindProcessID(processSign);
                    if (!processID.IsValid)
                    {
                        msgError = "流程标识不正确。";
                        return result;
                    }
                    Process p = ModuleUtils.CreateProcess(processID);
                    if (p == null)
                    {
                        msgError = "流程定义序列化失败。";
                        return result;
                    }
                    string strVerify = null;
                    string processSerialization = Utils.SerializerDatabaseFormart<Process>(p, out strVerify);
                    #endregion
                                        
                    FlowProcessInstance flowProcessInstance = new FlowProcessInstance();
                    flowProcessInstance.ProcessID = processID;
                    flowProcessInstance.ProcessName = p.ProcessName;
                    flowProcessInstance.ProcessInstanceID = processInstanceID = GUIDEx.New;
                    flowProcessInstance.ProcessInstanceName = processInstanceName;
                    flowProcessInstance.ProcessSerialization = processSerialization;
                    flowProcessInstance.Verify = strVerify;
                    flowProcessInstance.CreateDate = DateTime.Now;
                    flowProcessInstance.EndDate = null;
                    flowProcessInstance.FromEmployeeID = employeeID;
                    flowProcessInstance.FromEmployeeName = employeeName;
                    flowProcessInstance.InstanceProcessStatus = (int)EnumInstanceProcessStatus.Run;
                    result = this.flowProcessInstanceEntity.UpdateRecord(flowProcessInstance);
                }
                catch (Exception e)
                {
                    msgError = e.Message;
                    if (processInstanceID.IsValid)
                    {
                        FlowInstanceRunError err = new FlowInstanceRunError();
                        err.ErrorID = GUIDEx.New;
                        err.ProcessInstanceID = processInstanceID;
                        err.StepInstanceID = GUIDEx.Null;
                        err.ErrorMessage = msgError;
                        this.flowInstanceRunErrorEntity.UpdateRecord(err);
                    }
                }
                return result;
            }
        }
        /// <summary>
        /// 提交任务。
        /// </summary>
        /// <param name="processInstanceID">流程实例ID。</param>
        /// <param name="stepSign">当前步骤标识。</param>
        /// <param name="employeeID">进行操作的用户ID。</param>
        /// <param name="employeeName">进行操作的用户名称。</param>
        /// <param name="parameters">参数集合，为流程流转提供参数。</param>
        /// <param name="approvalViews">审批意见。</param>
        /// <param name="taskID">流程任务ID。</param>
        /// <param name="msgError">调用失败时的错误消息。</param>
        /// <returns>提交任务成功则返回true,否则返回false。</returns>
        public bool CommitFlow(GUIDEx processInstanceID, string stepSign, GUIDEx employeeID, string employeeName, ParamInstanceCollection parameters, string approvalViews,string taskID, out string msgError)
        {
            return this.CommitFlow(processInstanceID, stepSign, employeeID, employeeName, parameters, approvalViews, taskID,null, null,  out msgError);
        }
        /// <summary>
        /// 提交任务。
        /// </summary>
        /// <param name="processInstanceID">流程实例ID。</param>
        /// <param name="stepSign">当前步骤标识。</param>
        /// <param name="employeeID">进行操作的用户ID。</param>
        /// <param name="employeeName">进行操作的用户名称。</param>
        /// <param name="parameters">参数集合，为流程流转提供参数。</param>
        /// <param name="approvalViews">审批意见。</param>
        /// <param name="toDoEmployees">推进该流程一下步骤的相关人员（用户ID,用户名称），为空表示流程定义中的所有相关人员。</param>
        /// <param name="toViewEmployees">流程一下步骤待阅的相关人员（用户ID,用户名称），为空表示无待阅。</param>
        /// <param name="taskID">流程任务ID。</param>
        /// <param name="msgError">调用失败时的错误消息。</param>
        /// <returns>提交任务成功则返回true,否则返回false。</returns>
        public bool CommitFlow(GUIDEx processInstanceID, string stepSign, GUIDEx employeeID, string employeeName, ParamInstanceCollection parameters, string approvalViews,string taskID, string[][] toDoEmployees, string[][] toViewEmployees,  out string msgError)
        {
            lock (this)
            {
                msgError = null;
                bool result = false;
                GUIDEx stepInstanceID = GUIDEx.Null;
                try
                {
                    #region 验证参数。
                    if (string.IsNullOrEmpty(processInstanceID))
                    {
                        msgError = "流程实例ID为空。";
                        return result;
                    }
                    if (string.IsNullOrEmpty(stepSign))
                    {
                        msgError = "当前步骤标识为空。";
                        return result;
                    }
                    #endregion

                    #region 获取流程实例。
                    FlowProcessInstance flowProcessInstance = new FlowProcessInstance();
                    flowProcessInstance.ProcessInstanceID = processInstanceID;
                    if (false == (result = this.flowProcessInstanceEntity.LoadRecord(ref flowProcessInstance)))
                    {
                        msgError = "流程实例ID不正确。";
                        return result;
                    }
                    EnumInstanceProcessStatus ips = (EnumInstanceProcessStatus)flowProcessInstance.InstanceProcessStatus;
                    if (ips == EnumInstanceProcessStatus.Complete)
                    {
                        msgError = string.Format("该流程实例[{0}]已经完成。", flowProcessInstance.ProcessInstanceName);
                        return result;
                    }
                    if (ips == EnumInstanceProcessStatus.Stop)
                    {
                        msgError = string.Format("该流程实例[{0}]已经被暂停。", flowProcessInstance.ProcessInstanceName);
                        return result;
                    }
                    if (ips != EnumInstanceProcessStatus.Run)
                    {
                        msgError = string.Format("该流程实例[{0}]未在运行状态。", flowProcessInstance.ProcessInstanceName);
                        return result;
                    }
                    Process p = Utils.DeSerializerDatabaseFormart<Process>(flowProcessInstance.ProcessSerialization);
                    if (p == null)
                    {
                        msgError = string.Format("该流程实例[{0}]反序列化失败,无法加载流程定义。", flowProcessInstance.ProcessInstanceName);
                        return result;
                    }
                    #endregion

                    #region 流程步骤处理。
                    Step step = p.Steps.FindStep(stepSign);
                    if (step == null)
                    {
                        msgError = string.Format("该流程实例[{0}]的步骤[{1}]不存在。", flowProcessInstance.ProcessInstanceName, stepSign);
                        return result;
                    }
                    FlowStepInstance flowStepInstance = new FlowStepInstance();
                    flowStepInstance.StepInstanceID = stepInstanceID = GUIDEx.New;
                    flowStepInstance.StepID = step.StepID;
                    flowStepInstance.StepName = step.StepName;
                    flowStepInstance.ProcessInstanceID = flowProcessInstance.ProcessInstanceID;
                    flowStepInstance.CreateDate = DateTime.Now;
                    flowStepInstance.EndDate = null;
                    flowStepInstance.FromEmployeeID = employeeID;
                    flowStepInstance.FromEmployeeName = employeeName;
                    flowStepInstance.InstanceStepStatus = (int)EnumInstanceStepStatus.Suspended;
                    IDBAccess dbAccess = this.flowStepInstanceEntity.DatabaseAccess;
                    try
                    {
                        //启动事务。
                        dbAccess.BeginTransaction();
                        if (result = this.flowStepInstanceEntity.UpdateRecord(flowStepInstance))
                        {
                            #region 参数处理。
                            bool pResult = true;
                            if (parameters != null && parameters.Count > 0 && step.Parameters != null && step.Parameters.Count > 0)
                            {
                                FlowParameterInstance flowParameterInstance = new FlowParameterInstance();
                                FlowParameterInstanceEntity flowParameterInstanceEntity = new FlowParameterInstanceEntity();
                                flowParameterInstanceEntity.DatabaseAccess = dbAccess;
                                foreach (ParamInstance pi in parameters)
                                {
                                    Parameter parameter = step.Parameters.FindParameter(pi.ParamName);
                                    if (parameter == null)
                                    {
                                        msgError = string.Format("该流程实例[{0}]的步骤[{1}]中参数[{2}]不存在。", flowProcessInstance.ProcessInstanceName, stepSign, pi.ParamName);
                                        pResult = false;
                                        break;
                                    }
                                    flowParameterInstance.StepInstanceID = flowStepInstance.StepInstanceID;
                                    flowParameterInstance.ParameterID = parameter.ParameterID;
                                    flowParameterInstance.ParameterName = parameter.ParameterName;
                                    flowParameterInstance.ParameterValue = pi.ParamValue;
                                    result = flowParameterInstanceEntity.UpdateRecord(flowParameterInstance);
                                }
                                if (!pResult)
                                    flowParameterInstanceEntity.DeleteRecord(flowStepInstance.StepInstanceID);
                            }
                            if (!pResult && !string.IsNullOrEmpty(msgError))
                            {
                                this.flowStepInstanceEntity.DeleteRecord(flowStepInstance);
                                stepInstanceID = GUIDEx.Null;
                                throw new Exception(msgError);
                            }
                            #endregion
                        }

                        #region 记录审批意见。
                        if (result && !string.IsNullOrEmpty(approvalViews))
                        {
                            FlowStepInstanceData flowStepInstanceData = new FlowStepInstanceData();
                            flowStepInstanceData.TaskDataID = string.IsNullOrEmpty(taskID) ? GUIDEx.New : new GUIDEx(taskID);
                            flowStepInstanceData.StepInstanceID = flowStepInstance.StepInstanceID;
                            flowStepInstanceData.CreateDate = DateTime.Now;
                            flowStepInstanceData.DataCategory = (int)EnumDataCategory.FlowData;
                            flowStepInstanceData.DataText = approvalViews;

                            this.flowStepInstanceDataEntity.DatabaseAccess = dbAccess;
                            result = this.flowStepInstanceDataEntity.UpdateRecord(flowStepInstanceData);
                        }
                        #endregion

                        #region toDoEmployees
                        if (result && toDoEmployees != null && toDoEmployees.Length > 0)
                        {
                            StringBuilder builder = new StringBuilder("toDoEmployees:{");
                            bool bFlag = false;
                            foreach (string[] ep in toDoEmployees)
                            {
                                if (ep != null && ep.Length == 2)
                                {
                                    bFlag = true;
                                    builder.Append(string.Join(":", ep));
                                    builder.Append(";");
                                }
                            }
                            if (bFlag)
                                builder.Remove(builder.Length - 1, 1);
                            builder.Append("}");

                            FlowStepInstanceData flowStepInstanceData = new FlowStepInstanceData();
                            flowStepInstanceData.TaskDataID = GUIDEx.New;
                            flowStepInstanceData.StepInstanceID = flowStepInstance.StepInstanceID;
                            flowStepInstanceData.CreateDate = DateTime.Now;
                            flowStepInstanceData.DataCategory = (int)EnumDataCategory.AddData;
                            flowStepInstanceData.DataText = builder.ToString();
                            
                            flowStepInstanceDataEntity.DatabaseAccess = dbAccess;
                            result = flowStepInstanceDataEntity.UpdateRecord(flowStepInstanceData);
                        }
                        #endregion

                        #region toViewEmployees
                        if (result && toViewEmployees != null && toViewEmployees.Length > 0)
                        {
                            StringBuilder builder = new StringBuilder("toViewEmployees:{");
                            bool bFlag = false;
                            foreach (string[] ve in toViewEmployees)
                            {
                                if (ve != null && ve.Length == 2)
                                {
                                    bFlag = true;
                                    builder.Append(string.Join(":", ve));
                                    builder.Append(";");
                                }
                            }
                            if (bFlag)
                                builder.Remove(builder.Length - 1, 1);
                            builder.Append("}");

                            FlowStepInstanceData flowStepInstanceData = new FlowStepInstanceData();
                            flowStepInstanceData.TaskDataID = GUIDEx.New;
                            flowStepInstanceData.StepInstanceID = flowStepInstance.StepInstanceID;
                            flowStepInstanceData.CreateDate = DateTime.Now;
                            flowStepInstanceData.DataCategory = (int)EnumDataCategory.AddData;
                            flowStepInstanceData.DataText = builder.ToString();

                            flowStepInstanceDataEntity.DatabaseAccess = dbAccess;
                            result = flowStepInstanceDataEntity.UpdateRecord(flowStepInstanceData);
                        }
                        #endregion

                        #region 流程任务处理。
                        if (!string.IsNullOrEmpty(taskID))
                        {
                            FlowInstanceTaskEntity flowInstanceTaskEntity = new FlowInstanceTaskEntity();
                            FlowInstanceTask flowInstanceTask = new FlowInstanceTask();
                            flowInstanceTask.TaskID = taskID;
                            if (flowInstanceTaskEntity.LoadRecord(ref flowInstanceTask))
                            {
                                if (flowInstanceTask.EndMode == (int)EnumTaskEndMode.None)
                                {
                                    flowInstanceTask.DoEmployeeID = employeeID;
                                    flowInstanceTask.DoEmployeeName = employeeName;
                                    flowInstanceTask.EndDate = DateTime.Now;
                                    flowInstanceTask.EndMode = (int)EnumTaskEndMode.Normal;
                                    flowInstanceTask.BeginMode = (int)((employeeID == flowInstanceTask.EmployeeID) ? EnumTaskBeginMode.Normal : EnumTaskBeginMode.Authorize);
                                    result = flowInstanceTaskEntity.UpdateRecord(flowInstanceTask);
                                }
                            }
                        }
                        #endregion

                        flowStepInstance.InstanceStepStatus = (int)EnumInstanceStepStatus.Committed;
                        result = this.flowStepInstanceEntity.UpdateRecord(flowStepInstance);
                        //提交事务。
                        result = dbAccess.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        result = false;
                        dbAccess.RollbackTransaction();
                        throw ex;
                    }
                    #endregion
                }
                catch (Exception e)
                {
                    msgError = e.Message;
                    if (processInstanceID.IsValid)
                    {
                        FlowInstanceRunError err = new FlowInstanceRunError();
                        err.ErrorID = GUIDEx.New;
                        err.ProcessInstanceID = processInstanceID;
                        err.StepInstanceID = stepInstanceID;
                        err.ErrorMessage = msgError;
                        err.CreateDate = DateTime.Now;
                        this.flowInstanceRunErrorEntity.UpdateRecord(err);
                    }
                }
                return result;
            }
        }
        /// <summary>
        /// 撤销指定的流程实例。
        /// </summary>
        /// <param name="processInstanceID">要进行销毁的流程实例ID。</param>
        /// <param name="employeeID">进行操作的用户ID。</param>
        /// <param name="employeeName">进行操作的用户名称。</param>
        /// <param name="msgError">调用失败时的错误消息。</param>
        /// <returns>撤销成功则返回true,否则返回false。</returns>
        public bool DestroyFlow(GUIDEx processInstanceID, GUIDEx employeeID, string employeeName, out string msgError)
        {
            lock (this)
            {
                msgError = null;
                bool result = false;
                try
                {
                    #region 验证参数。
                    if (string.IsNullOrEmpty(processInstanceID))
                    {
                        msgError = "流程实例ID为空。";
                        return result;
                    }
                    #endregion

                    #region 获取流程实例。
                    FlowProcessInstance flowProcessInstance = new FlowProcessInstance();
                    flowProcessInstance.ProcessInstanceID = processInstanceID;
                    if (false == (result = this.flowProcessInstanceEntity.LoadRecord(ref flowProcessInstance)))
                    {
                        msgError = "流程实例ID不正确。";
                        return result;
                    }
                    flowProcessInstance.InstanceProcessStatus = (int)EnumInstanceProcessStatus.Stop;
                    result = this.flowProcessInstanceEntity.UpdateRecord(flowProcessInstance);
                    #endregion
                }
                catch (Exception e)
                {
                    msgError = e.Message;
                }
                return result;
            }
        }
        /// <summary>
        /// 对指定的步骤进行授权。
        /// </summary>
        /// <param name="processSign">流程标识。</param>
        /// <param name="employeeID">进行授权的用户ID。</param>
        /// <param name="employeeName">进行授权的用户名称。</param>
        /// <param name="stepSigns">进行授权的步骤标识列表。</param>
        /// <param name="toEmployeeID">授权的目标用户ID。</param>
        /// <param name="fromDate">授权开始时间，为2010-01-11 15:00:00格式。</param>
        /// <param name="toDate">授权结束时间，为2010-01-11 15:00:00格式。</param>
        /// <param name="msgError">调用失败时的错误消息。</param>
        /// <returns>如果授权成功则返回true，否则返回false。</returns>
        public bool AuthorizeFlow(string processSign, GUIDEx employeeID, string employeeName, string[] stepSigns, GUIDEx toEmployeeID, DateTime fromDate, DateTime toDate, out string msgError)
        {
            lock (this)
            {
                msgError = null;
                bool result = false;
                try
                {
                    #region 验证参数。
                    if (string.IsNullOrEmpty(processSign))
                    {
                        msgError = "流程标识为空。";
                        return result;
                    }
                    if (!employeeID.IsValid)
                    {
                        msgError = "进行授权的用户ID为空。";
                        return result;
                    }
                    if (string.IsNullOrEmpty(employeeName))
                    {
                        msgError = "进行授权的用户名称为空。";
                        return result;
                    }
                    if (stepSigns == null || stepSigns.Length == 0)
                    {
                        msgError = "进行授权的步骤标识列表不存在或为空。";
                        return result;
                    }
                    if (!toEmployeeID.IsValid)
                    {
                        msgError = "授权的目标用户ID为空。";
                        return result;
                    }
                    #endregion

                    #region 获取流程定义。
                    GUIDEx processID = this.flowProcessEntity.FindProcessID(processSign);
                    if (!processID.IsValid)
                    {
                        msgError = "流程标识不正确。";
                        return result;
                    }
                    #endregion

                    #region 流程步骤定义。
                    FlowStepEntity flowStepEntity = new FlowStepEntity();
                    List<FlowStep> flowStepList = flowStepEntity.FindFlowStep(processID, stepSigns);
                    if (flowStepList == null || flowStepList.Count == 0)
                    {
                        msgError = "进行授权的步骤标识列表不存在或为空。";
                        return result;
                    }
                    #endregion

                    #region 流程步骤的授权。
                    FlowStepAuthorize flowStepAuthorize = new FlowStepAuthorize();
                    FlowStepAuthorizeEntity flowStepAuthorizeEntity = new FlowStepAuthorizeEntity();
                    foreach (FlowStep fs in flowStepList)
                    {
                        flowStepAuthorize.AuthorizeID = GUIDEx.New;
                        flowStepAuthorize.BeginDate = fromDate;
                        flowStepAuthorize.EndDate = toDate;
                        flowStepAuthorize.EmployeeID = employeeID;
                        flowStepAuthorize.StepID = fs.StepID;
                        flowStepAuthorize.TargetEmployeeID = toEmployeeID;

                        result = flowStepAuthorizeEntity.UpdateRecord(flowStepAuthorize);
                    }
                    #endregion
                }
                catch (Exception e)
                {
                    msgError = e.Message;
                }
                return result;
            }
        }
        /// <summary>
        /// 收回流程的授权。
        /// </summary>
        /// <param name="processSign">流程标识。</param>
        /// <param name="employeeID">进行回收的用户ID，只能回收自己的授权。</param>
        /// <param name="employeeName">进行回收的用户名称。</param>
        /// <param name="authorizeID">要回收的流程授权的步骤标识列表。</param>
        /// <param name="msgError">调用失败时的错误消息。</param>
        /// <returns>回收成功则返回true，否则返回false。</returns>
        public bool WithdrawFlow(string processSign, GUIDEx employeeID, string employeeName, string[] stepSigns, out string msgError)
        {
            lock (this)
            {
                msgError = null;
                bool result = false;
                try
                {
                    #region 验证参数。
                    if (string.IsNullOrEmpty(processSign))
                    {
                        msgError = "流程标识为空。";
                        return result;
                    }
                    if (!employeeID.IsValid)
                    {
                        msgError = "用户ID为空。";
                        return result;
                    }
                    if (stepSigns == null || stepSigns.Length == 0)
                    {
                        msgError = "要回收的流程授权的步骤标识列表为空。";
                        return result;
                    }
                    #endregion

                    #region 获取流程定义。
                    GUIDEx processID = this.flowProcessEntity.FindProcessID(processSign);
                    if (!processID.IsValid)
                    {
                        msgError = "流程标识不正确。";
                        return result;
                    }
                    #endregion

                    #region 流程步骤定义。
                    FlowStepEntity flowStepEntity = new FlowStepEntity();
                    List<FlowStep> flowStepList = flowStepEntity.FindFlowStep(processID, stepSigns);
                    if (flowStepList == null || flowStepList.Count == 0)
                    {
                        msgError = "进行授权的步骤标识列表不存在或为空。";
                        return result;
                    }
                    #endregion

                    #region 流程步骤授权取消。
                    FlowStepAuthorizeEntity flowStepAuthorizeEntity = new FlowStepAuthorizeEntity();
                    foreach (FlowStep fs in flowStepList)
                    {
                        result = flowStepAuthorizeEntity.DeleteFlowStepAuthorize(employeeID, fs.StepID);
                    }
                    #endregion
                }
                catch (Exception e)
                {
                    msgError = e.Message;
                }
                return result;
            }
        }

        /// <summary>
        /// 获取流程履历。
        /// </summary>
        /// <param name="processInstanceID">流程实例ID。</param>
        /// <param name="msgError">调用失败时的错误消息。</param>
        /// <returns></returns>
        public ProcessResumesCollection GetProcessResumes(GUIDEx processInstanceID, out string msgError)
        {
            ProcessResumesCollection collection = new ProcessResumesCollection();
            msgError = null;
            if (processInstanceID.IsValid)
            {
                try
                {
                    DataTable dtSource = this.flowStepInstanceDataEntity.GetProcessResumes(processInstanceID);
                    if (dtSource != null)
                    {
                        collection.InitAssignment(dtSource);
                    }
                }
                catch (Exception e)
                {
                    msgError = e.Message;
                }
            }
            return collection;
        }

        #endregion
    }
}
