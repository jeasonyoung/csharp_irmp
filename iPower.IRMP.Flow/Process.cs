//================================================================================
//  FileName:Process.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-09 09:33:19
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
using System.Xml;
using System.Xml.Serialization;
namespace iPower.IRMP.Flow
{
    /// <summary>
    /// 流程定义
    /// </summary>
    [Serializable]
    public class Process
    {
        #region 成员变量，构造函数。
        StepCollection stepCollection;
        TransitionCollection transitionCollection;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public Process()
        {
            this.stepCollection = new StepCollection();
            this.transitionCollection = new TransitionCollection();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置流程ID。
        /// </summary>
        public string ProcessID { get; set; }
        /// <summary>
        /// 获取或设置流程标识。
        /// </summary>
        public string ProcessSign { get; set; }
        /// <summary>
        /// 获取或设置流程名称。
        /// </summary>
        public string ProcessName { get; set; }
        /// <summary>
        /// 获取或设置有效开始时间。
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 获取或设置有效期结束时间。
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 获取或设置流程描述信息。
        /// </summary>
        public string ProcessDescription { get; set; }
        /// <summary>
        /// 获取或设置流程步骤。
        /// </summary>
        public StepCollection Steps
        {
            get { return this.stepCollection; }
            set
            {
                StepCollection data = value;
                if (data != null && data.Count > 0)
                {
                    foreach (Step s in data)
                    {
                        if (!this.stepCollection.Contains(s))
                            this.stepCollection.Add(s);
                    }
                }
            }
        }
        /// <summary>
        /// 获取或设置步骤变迁规则。
        /// </summary>
        public TransitionCollection Transitions
        {
            get { return this.transitionCollection; }
            set
            {
                TransitionCollection data = value;
                if (data != null && data.Count > 0)
                {
                    foreach (Transition t in data)
                    {
                        if (!this.transitionCollection.Contains(t))
                            this.transitionCollection.Add(t);
                    }
                }
            }
        }

        #endregion

        #region 函数。
        /// <summary>
        /// 序列化。
        /// </summary>
        /// <param name="stream"></param>
        public void Serializer(Stream stream)
        {
            Utils.Serializer<Process>(stream, this);
        }
        /// <summary>
        /// 反序列化。
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static Process DeSerializer(Stream stream)
        {
            return Utils.DeSerializer<Process>(stream);
        }
        #endregion
    }
}
