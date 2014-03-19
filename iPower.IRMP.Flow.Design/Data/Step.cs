//================================================================================
//  FileName: Step.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/5/24
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
namespace iPower.IRMP.Flow.Design.Data
{
    /// <summary>
    /// 步骤类型枚举。
    /// </summary>
    public enum EnumStepType
    {
        /// <summary>
        /// 开始步骤。
        /// </summary>
        First = 0x01,
        /// <summary>
        /// 中间步骤。
        /// </summary>
        Middle = 0x02,
        /// <summary>
        /// 终结步骤。
        /// </summary>
        Last = 0x04
    }
    /// <summary>
    /// 步骤模式枚举。
    /// </summary>
    public enum EnumStepMode
    {
        /// <summary>
        /// 与分支。
        /// </summary>
        SplitAND = 0x01,
        /// <summary>
        /// 或分支。
        /// </summary>
        SplitOR = 0x02,
        /// <summary>
        /// 与汇聚。
        /// </summary>
        JoinAND = 0x04,
        /// <summary>
        /// 或汇聚。
        /// </summary>
        JoinOR = 0x08
    }
    /// <summary>
    /// 步骤通知消息类型。
    /// </summary>
    [Flags]
    public enum EnumStepWarning
    {
        /// <summary>
        /// 未定义。
        /// </summary>
        None = 0x00,
        /// <summary>
        /// 短信。
        /// </summary>
        SMS = 0x01,
        /// <summary>
        /// 邮件。
        /// </summary>
        Email = 0x02,
        /// <summary>
        /// 站内消息。
        /// </summary>
        IAMS = 0x04
    }
    /// <summary>
    /// 流程步骤集合。
    /// </summary>
    public class StepCollection : WFCollection<Step>
    {
        /// <summary>
        /// 根据步骤ID查找步骤对象。
        /// </summary>
        /// <param name="stepID">步骤ID。</param>
        /// <returns>步骤对象。</returns>
        public Step this[string stepID]
        {
            get
            {
                if (string.IsNullOrEmpty(stepID))
                    return null;
                Step result = null;
                foreach (Step s in this.DataCollection)
                {
                    if (s.StepID == stepID)
                    {
                        result = s;
                        break;
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// 根据步骤类型查找步骤对象。
        /// </summary>
        /// <param name="stepType">步骤类型。</param>
        /// <returns>步骤对象数组。</returns>
        public Step[] this[EnumStepType stepType]
        {
            get
            {
                List<Step> list = new List<Step>();

                foreach (Step s in this.DataCollection)
                {
                    if (s.StepType == stepType)
                        list.Add(s);
                }

                Step[] steps = new Step[list.Count];
                if (list.Count > 0)
                    list.CopyTo(steps);
                return steps;
            }
        }

        /// <summary>
        /// 根据步骤标识查找步骤对象。
        /// </summary>
        /// <param name="stepSign">步骤标识。</param>
        /// <returns>步骤对象。</returns>
        public Step FindStep(string stepSign)
        {
            Step result = null;
            if (!string.IsNullOrEmpty(stepSign))
            {
                foreach (Step s in this.DataCollection)
                {
                    if (s.StepSign == stepSign)
                    {
                        result = s;
                        break;
                    }
                }
            }
            return result;
        }
    }
    /// <summary>
    /// 流程步骤定义。
    /// </summary>
    public class Step
    {
        #region 成员变量，构造函数。
        ParameterCollection parameterCollection;
        StepRoleCollection stepRoleCollection;
        StepPostCollection stepPostCollection;
        StepRankCollection stepRankCollection;
        StepEmployeeCollection stepEmployeeCollection;
        StepAuthorizeCollection stepAuthorizeCollection;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public Step()
        {
            this.parameterCollection = new ParameterCollection();
            this.stepRoleCollection = new StepRoleCollection();
            this.stepPostCollection = new StepPostCollection();
            this.stepRankCollection = new StepRankCollection();
            this.stepEmployeeCollection = new StepEmployeeCollection();
            this.stepAuthorizeCollection = new StepAuthorizeCollection();
        }
        #endregion

        /// <summary>
        /// 获取或设置步骤ID。
        /// </summary>
        public string StepID { get; set; }
        /// <summary>
        /// 获取或设置步骤标识。
        /// </summary>
        public string StepSign { get; set; }
        /// <summary>
        /// 获取或设置步骤名称。
        /// </summary>
        public string StepName { get; set; }
        /// <summary>
        /// 获取或设置步骤类型。
        /// </summary>
        public EnumStepType StepType { get; set; }
        /// <summary>
        /// 获取或设置该步骤的运行入口方法或者URL。
        /// </summary>
        public string EntryAction { get; set; }
        /// <summary>
        /// 获取或设置该步骤的查看入口方法或者URL。
        /// </summary>
        public string EntryQuery { get; set; }
        /// <summary>
        /// 获取或设置步骤模式。
        /// </summary>
        public EnumStepMode StepMode { get; set; }
        /// <summary>
        /// 获取或设置步骤变迁周期，以秒为单位。
        /// </summary>
        public int StepDuration { get; set; }
        /// <summary>
        /// 获取或设置步骤通知消息类型。
        /// </summary>
        public EnumStepWarning StepWarning { get; set; }
        /// <summary>
        /// 获取或设置步骤描述信息。
        /// </summary>
        public string StepDescription { get; set; }

        /// <summary>
        /// 获取或设置步骤参数集合。
        /// </summary>
        public ParameterCollection Parameters
        {
            get { return this.parameterCollection; }
            set
            {
                ParameterCollection collection = value;
                if (collection != null && collection.Count > 0)
                {
                    foreach (Parameter p in collection)
                    {
                        if (!this.parameterCollection.Contains(p))
                            this.parameterCollection.Add(p);
                    }
                }
            }
        }
        /// <summary>
        /// 获取或设置步骤上的用户(映射角色)集合。
        /// </summary>
        public StepRoleCollection StepRoles
        {
            get { return this.stepRoleCollection; }
            set
            {
                StepRoleCollection collection = value;
                if (collection != null && collection.Count > 0)
                {
                    foreach (StepRole r in collection)
                    {
                        if (!this.stepRoleCollection.Contains(r))
                            this.stepRoleCollection.Add(r);
                    }
                }
            }
        }
        /// <summary>
        /// 获取或设置步骤上的用户(映射岗位)集合。
        /// </summary>
        public StepPostCollection StepPosts
        {
            get { return this.stepPostCollection; }
            set
            {
                StepPostCollection collection = value;
                if (collection != null && collection.Count > 0)
                {
                    foreach (StepPost p in collection)
                    {
                        if (!this.stepPostCollection.Contains(p))
                            this.stepPostCollection.Add(p);
                    }
                }
            }
        }
        /// <summary>
        /// 获取或设置步骤上的用户(映射岗位级别)集合。
        /// </summary>
        public StepRankCollection StepRanks
        {
            get { return this.stepRankCollection; }
            set
            {
                StepRankCollection collection = value;
                if (collection != null && collection.Count > 0)
                {
                    foreach (StepRank rank in collection)
                    {
                        if (!this.stepRankCollection.Contains(rank))
                            this.stepRankCollection.Add(rank);
                    }
                }
            }
        }
        /// <summary>
        /// 获取或设置步骤上的用户(映射用户)集合。
        /// </summary>
        public StepEmployeeCollection StepEmployees
        {
            get { return this.stepEmployeeCollection; }
            set
            {
                StepEmployeeCollection collection = value;
                if (collection != null && collection.Count > 0)
                {
                    foreach (StepEmployee e in collection)
                    {
                        if (!this.stepEmployeeCollection.Contains(e))
                            this.stepEmployeeCollection.Add(e);
                    }
                }
            }
        }
        /// <summary>
        /// 获取或设置步骤的授权(针对自己拥有权限的步骤授权)集合。
        /// </summary>
        public StepAuthorizeCollection StepAuthorizes
        {
            get { return this.stepAuthorizeCollection; }
            set
            {
                StepAuthorizeCollection collection = value;
                if (collection != null && collection.Count > 0)
                {
                    foreach (StepAuthorize a in collection)
                    {
                        if (!this.stepAuthorizeCollection.Contains(a))
                            this.stepAuthorizeCollection.Add(a);
                    }
                }
            }
        }
    }
}
