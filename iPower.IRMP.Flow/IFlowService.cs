//================================================================================
//  FileName: IFlowAdmin.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/1/11
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
namespace iPower.IRMP.Flow
{
    /// <summary>
    /// 流程操作接口。
    /// </summary>
    public interface IFlowService
    {
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
        bool InitFlow(string processSign, string processInstanceName, GUIDEx employeeID, string employeeName, out GUIDEx processInstanceID, out string msgError);
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
        bool CommitFlow(GUIDEx processInstanceID, string stepSign, GUIDEx employeeID, string employeeName,
            ParamInstanceCollection parameters, string approvalViews, string taskID, out string msgError);

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
        /// <param name="toDoEmployees">推进该流程一下步骤的相关人员（用户ID,用户名称），为空表示流程定义中的所有相关人员。</param>
        /// <param name="toViewEmployees">流程一下步骤待阅的相关人员（用户ID,用户名称），为空表示无待阅。</param>
        /// <param name="msgError">调用失败时的错误消息。</param>
        /// <returns>提交任务成功则返回true,否则返回false。</returns>
        bool CommitFlow(GUIDEx processInstanceID, string stepSign, GUIDEx employeeID, string employeeName,
            ParamInstanceCollection parameters, string approvalViews, string taskID, string[][] toDoEmployees, string[][] toViewEmployees,
            out string msgError);

        /// <summary>
        /// 撤销指定的流程实例。
        /// </summary>
        /// <param name="processInstanceID">要进行销毁的流程实例ID。</param>
        /// <param name="employeeID">进行操作的用户ID。</param>
        /// <param name="employeeName">进行操作的用户名称。</param>
        /// <param name="msgError">调用失败时的错误消息。</param>
        /// <returns>撤销成功则返回true,否则返回false。</returns>
        bool DestroyFlow(GUIDEx processInstanceID, GUIDEx employeeID, string employeeName, out string msgError);

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
        bool AuthorizeFlow(string processSign, GUIDEx employeeID, string employeeName, string[] stepSigns,
            GUIDEx toEmployeeID, DateTime fromDate, DateTime toDate, out string msgError);

        /// <summary>
        /// 收回流程的授权。
        /// </summary>
        /// <param name="processSign">流程标识。</param>
        /// <param name="employeeID">进行回收的用户ID，只能回收自己的授权。</param>
        /// <param name="employeeName">进行回收的用户名称。</param>
        /// <param name="stepSigns">要回收的流程授权的步骤标识列表。</param>
        /// <param name="msgError">调用失败时的错误消息。</param>
        /// <returns>回收成功则返回true，否则返回false。</returns>
        bool WithdrawFlow(string processSign, GUIDEx employeeID, string employeeName, string[] stepSigns, out string msgError);

        /// <summary>
        /// 获取流程履历。
        /// </summary>
        /// <param name="processInstanceID">流程实例ID。</param>
        /// <param name="msgError">调用失败时的错误消息。</param>
        /// <returns></returns>
        ProcessResumesCollection GetProcessResumes(GUIDEx processInstanceID, out string msgError);
    }
}
