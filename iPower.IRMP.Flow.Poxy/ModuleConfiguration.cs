//================================================================================
//  FileName: ModuleConfiguration.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/7/13
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
using iPower.Logs;
using iPower.Configuration;
namespace iPower.IRMP.Flow.Poxy
{
    /// <summary>
    /// 配置键名。
    /// </summary>
    internal static class ModuleConfigurationKeys
    {
        /// <summary>
        /// 流程服务URL键名。
        /// </summary>
        public const string FlowServiceURLKey = "FlowServiceURL";
        /// <summary>
        /// 流程任务URL键名。
        /// </summary>
        public const string TaskServiceURLKey = "TaskServiceURL";
        /// <summary>
        /// 日志文件头键名。
        /// </summary>
        public const string LogFileHeadKey = "iPower.Logs.FileHead";
        /// <summary>
        /// 日志文件生成规则(Year-年，Month-月，Week-周，Date-日， Hour-时)键名。
        /// </summary>
        public const string LogFileRuleKey = "iPower.Logs.LogFileRule";
    }
    /// <summary>
    /// 模块配置。
    /// </summary>
    internal class ModuleConfiguration : iPowerConfiguration, ILogFileHead
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public ModuleConfiguration()
            : base("FlowPoxy")
        {
        }
        #endregion

        #region 静态实例。
        static ModuleConfiguration m_config;
        static object synchronizationObject = new object();
        /// <summary>
        /// 获取静态实例。
        /// </summary>
        public static ModuleConfiguration ModuleConfig
        {
            get
            {
                lock (synchronizationObject)
                {
                    if (m_config == null)
                        m_config = new ModuleConfiguration();
                    return m_config;
                }
            }
        }
        #endregion

        #region 流程URL。
        /// <summary>
        /// 获取流程服务URL。
        /// </summary>
        public string FlowServiceURL
        {
            get
            {
                string url = this[ModuleConfigurationKeys.FlowServiceURLKey];
                if (string.IsNullOrEmpty(url))
                    throw new ArgumentNullException("流程服务URL不能为空！");
                return url;
            }
        }
        /// <summary>
        /// 获取流程任务URL。
        /// </summary>
        public string TaskServiceURL
        {
            get
            {
                string url = this[ModuleConfigurationKeys.TaskServiceURLKey];
                if (string.IsNullOrEmpty(url))
                    throw new ArgumentNullException("流程任务URL不能为空！");
                return url;
            }
        }
        #endregion

        #region ILogFileHead 成员
        /// <summary>
        /// 获取日志文件头。
        /// </summary>
        public string LogFileHead
        {
            get { return this[ModuleConfigurationKeys.LogFileHeadKey]; }
        }
        /// <summary>
        /// 获取日志文件生成规则(Year-年，Month-月，Week-周，Date-日， Hour-时)。
        /// </summary>
        public EnumLogFileRule LogFileRule
        {
            get
            {
                try
                {
                    string strRule = this[ModuleConfigurationKeys.LogFileRuleKey];
                    if (!string.IsNullOrEmpty(strRule))
                        return (EnumLogFileRule)Enum.Parse(typeof(EnumLogFileRule), strRule);
                    return EnumLogFileRule.Week;
                }
                catch (Exception)
                {
                    return EnumLogFileRule.Week;
                }
            }
        }

        #endregion
    }
}
