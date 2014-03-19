//================================================================================
//  FileName: CreateDbCommonLogService.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/13
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
using System.Text.RegularExpressions;

using iPower;
using iPower.Platform.Logs;

using iPower.IRMP.Engine.Domain;
using iPower.IRMP.Engine.Persistence;
namespace iPower.IRMP.Engine
{
    /// <summary>
    /// 创建数据库日志服务。
    /// </summary>
    public class CreateDbCommonLogProvider: ICreateDbCommonLog
    {
        #region 成员变量，构造函数。
        IRMPCommonLogEntity iRMPCommonLogEntity = null;
        Regex replaceRegex = new Regex("'", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        /// <summary>
        /// 构造函数。
        /// </summary>
        public CreateDbCommonLogProvider()
        {
            this.iRMPCommonLogEntity = new IRMPCommonLogEntity();
        }
        #endregion

        #region ICreateDbCommonLog 成员
        /// <summary>
        /// 创建日志。
        /// </summary>
        /// <param name="systemID">系统ID。</param>
        /// <param name="systemName">系统名称。</param>
        /// <param name="createEmployeeID">创建日志用户ID。</param>
        /// <param name="createEmployeeName">创建日志用户名称。</param>
        /// <param name="relationTable">关联表名称。</param>
        /// <param name="log">日志内容。</param>
        public void CreateCommonLog(GUIDEx systemID, string systemName, GUIDEx createEmployeeID, string createEmployeeName, string relationTable, string log)
        {
            if (systemID.IsValid && createEmployeeID.IsValid && (!string.IsNullOrEmpty(relationTable) || !string.IsNullOrEmpty(log)))
            {
                IRMPCommonLog data = new IRMPCommonLog();

                data.LogID = GUIDEx.New;

                data.SystemID = systemID;
                data.SystemName = systemName;

                data.CreateEmployeeID = createEmployeeID;
                data.CreateEmployeeName = createEmployeeName;

                data.RelationTable = this.ReplaceContent(relationTable);
                data.LogContext = this.ReplaceContent(log);

                data.CreateDate = DateTime.Now;

                this.iRMPCommonLogEntity.UpdateRecord(data);
            }
        }

        #endregion

        /// <summary>
        /// 替换内容。
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        protected virtual string ReplaceContent(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                content = this.replaceRegex.Replace(content, "\"");
            }
            return content;
        }
    }
}
