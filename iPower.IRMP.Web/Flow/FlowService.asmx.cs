//================================================================================
//  FileName:FlowService.asmx.cs
//  Desc:
//
//  Called by
//
//  Auth:iPowerYoung
//  Date:2011-01-05 11:39:57
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
// ----  ------  -----------
//
//================================================================================
//  Copyright (C) 2009-2011 iPower Young Corporation
//================================================================================

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.ComponentModel;

using iPower;
using iPower.IRMP.Flow;
using iPower.IRMP.Flow.Engine.Service;
namespace iPower.IRMP.Flow.Web
{
    /// <summary>
    /// 流程引擎调用接口。
    /// </summary>
    [WebService(Namespace = "http://ipower.org/",
                Name = "流程引擎调用接口",
                Description = "该接口为实现流程的具体业务调用。")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class FlowService : WebService
    {
        #region 成员变量，构造函数。
        IFlowService flowService = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public FlowService()
        {
            this.flowService = new FlowServicePresenter();
        }
        #endregion

        #region 流程调用。
        /// <summary>
        /// 初始化流程实例。
        /// </summary>
        /// <param name="processSign">流程标识。</param>
        /// <param name="processInstanceName">流程实例名称。</param>
        /// <param name="employeeID">进行操作的用户ID。</param>
        /// <param name="employeeName">进行操作的用户名称。</param>
        /// <param name="processInstanceID">初始化流程实例的唯一标识。</param>
        /// <param name="msgError">调用失败时的错误消息。</param>
        /// <returns>实例化成功则返回true,否则返回false。</returns>
        [WebMethod(Description = @"<b>初始化流程实例。</b><br/>
                                    参数：<br/>
                                    processSign：要实例化的流程唯一标识。<br/>
                                    processInstanceName：流程实例名称。<br/>
                                    employeeID：初始化调用的用户ID。<br/>
                                    employeeName：初始化调用的用户姓名。<br/>
                                    processInstanceID：初始化的流程实例ID。<br/>
                                    msgError：调用失败时的错误信息。")]
        public bool InitFlow(string processSign, string processInstanceName, string employeeID, string employeeName,
            out string processInstanceID, out string msgError)
        {
            GUIDEx gProcessInstanceID = GUIDEx.Null;
            bool result = this.flowService.InitFlow(processSign, processInstanceName, employeeID, employeeName, out gProcessInstanceID, out  msgError);
            processInstanceID = gProcessInstanceID;
            return result;
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
        [WebMethod(Description = @"<b>提交任务。</b><br/>
                                   参数：<br/>
                                   processInstanceID:流程实例ID。<br/>
                                   stepSign:当前步骤标识。<br/>
                                   employeeID:进行操作的用户ID。<br/>
                                   employeeName:进行操作的用户名称。<br/>
                                   approvalViews:审批意见。<br/>
                                   taskID:流程任务ID。<br/>
                                   parameters:参数集合，为流程流转提供参数。<br/>
                                   msgError:调用失败时的错误消息。")]
        public bool CommitFlowInstance(string processInstanceID, string stepSign, string employeeID, string employeeName,
            ParamInstanceCollection parameters, string approvalViews, string taskID, out string msgError)
        {
            return this.flowService.CommitFlow(processInstanceID, stepSign, employeeID, employeeName, parameters, approvalViews, taskID, out msgError);
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
        /// <param name="toDoEmployees">推进该流程的相关人员（用户ID,用户名称），为空表示流程定义中的所有相关人员。</param>
        /// <param name="toViewEmployees">流程待阅的相关人员（用户ID,用户名称），为空表示无待阅。</param>
        /// <param name="msgError">调用失败时的错误消息。</param>
        /// <returns>提交任务成功则返回true,否则返回false。</returns>
        [WebMethod(Description = @"<b>提交任务。</b><br/>
                                   参数：<br/>
                                   processInstanceID:流程实例ID。<br/>
                                   stepSign:当前步骤标识。<br/>
                                   employeeID:进行操作的用户ID。<br/>
                                   employeeName:进行操作的用户名称。<br/>
                                   parameters:参数集合，为流程流转提供参数。<br/>
                                   approvalViews:审批意见。<br/>
                                   taskID:流程任务ID。<br/>
                                   toDoEmployees:推进该流程的相关人员（用户ID,用户名称），为空表示流程定义中的所有相关人员。<br/>
                                   toViewEmployees:流程待阅的相关人员（用户ID,用户名称），为空表示无待阅。<br/>
                                   msgError:调用失败时的错误消息。")]
        public bool CommitFlow(string processInstanceID, string stepSign, string employeeID, string employeeName,
             ParamInstanceCollection parameters, string approvalViews, string taskID, string[][] toDoEmployees, string[][] toViewEmployees,
             out string msgError)
        {
            return this.flowService.CommitFlow(processInstanceID, stepSign, employeeID, employeeName, parameters, approvalViews, taskID, toDoEmployees, toViewEmployees, out msgError);
        }

        /// <summary>
        /// 撤销指定的流程实例。
        /// </summary>
        /// <param name="processInstanceID">要进行销毁的流程实例ID。</param>
        /// <param name="employeeID">进行操作的用户ID。</param>
        /// <param name="employeeName">进行操作的用户名称。</param>
        /// <param name="msgError">调用失败时的错误消息。</param>
        /// <returns>撤销成功则返回true,否则返回false。</returns>
        [WebMethod(Description = @"<b>撤销指定的流程实例。</b><br/>
                                   参数：<br/>
                                   processInstanceID:要进行销毁的流程实例ID。<br/>
                                   employeeID:进行操作的用户ID。<br/>
                                   employeeName:进行操作的用户名称。<br/>
                                   msgError:调用失败时的错误消息。")]
        public bool DestroyFlow(string processInstanceID, string employeeID, string employeeName, out string msgError)
        {
            return this.flowService.DestroyFlow(processInstanceID, employeeID, employeeName, out msgError);
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
        [WebMethod(Description = @"<b>对指定的步骤进行授权。</b><br/>
                                   参数：<br/>
                                   processSign:流程标识。<br/>
                                   employeeID:进行授权的用户ID。<br/>
                                   employeeName:进行授权的用户名称。<br/>
                                   stepSigns:进行授权的步骤标识列表。<br/>
                                   toEmployeeID:授权的目标用户ID。<br/>
                                   fromDate:授权开始时间，为2010-01-11 15:00:00格式。<br/>
                                   toDate:授权结束时间，为2010-01-11 15:00:00格式。<br/>
                                   msgError:调用失败时的错误消息。")]
        public bool AuthorizeFlow(string processSign, string employeeID, string employeeName, string[] stepSigns,
            string toEmployeeID, DateTime fromDate, DateTime toDate, out string msgError)
        {
            return this.flowService.AuthorizeFlow(processSign, employeeID, employeeName, stepSigns, toEmployeeID, fromDate, toDate, out msgError);
        }

        /// <summary>
        /// 收回流程的授权。
        /// </summary>
        /// <param name="processSign">流程标识。</param>
        /// <param name="employeeID">进行回收的用户ID，只能回收自己的授权。</param>
        /// <param name="employeeName">进行回收的用户名称。</param>
        /// <param name="stepSigns">要回收的流程授权的步骤标识列表。</param>
        /// <param name="msgError">调用失败时的错误消息。</param>
        /// <returns>回收成功则返回true，否则返回false。</returns>
        [WebMethod(Description = @"<b>收回流程的授权。</b><br/>
                                   参数：<br/>
                                   processSign:流程标识。<br/>
                                   employeeID:进行回收的用户ID，只能回收自己的授权。<br/>
                                   employeeName:进行回收的用户名称。<br/>
                                   stepSigns:要回收的流程授权的步骤标识列表。<br/>
                                   msgError:调用失败时的错误消息。")]
        public bool WithdrawFlow(string processSign, string employeeID, string employeeName, string[] stepSigns, out string msgError)
        {
            return this.flowService.WithdrawFlow(processSign, employeeID, employeeName, stepSigns, out msgError);
        }
        #endregion

        #region 流程履历。
        /// <summary>
        /// 获取流程履历。
        /// </summary>
        /// <param name="processInstanceID">流程实例ID。</param>
        /// <param name="msgError">调用失败时的错误消息。</param>
        /// <returns></returns>
        [WebMethod(Description = @"<b>获取流程履历。</b><br/>
                                   参数：<br/>
                                   processInstanceID:流程实例ID。<br/>
                                   msgError:调用失败时的错误消息。")]
        public ProcessResumesCollection GetProcessResumes(string processInstanceID, out string msgError)
        {
            return this.flowService.GetProcessResumes(processInstanceID, out msgError);
        }
        #endregion
    }
}
