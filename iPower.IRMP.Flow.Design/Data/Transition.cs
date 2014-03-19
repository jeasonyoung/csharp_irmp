//================================================================================
//  FileName: Transition.cs
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
    /// 变迁规则枚举。
    /// </summary>
    public enum EnumTransitionRule
    {
        /// <summary>
        /// 与。
        /// </summary>
        AND = 0x00,
        /// <summary>
        /// 或。
        /// </summary>
        OR = 0x01
    }
    /// <summary>
    /// 步骤变迁规则集合。
    /// </summary>
    public class TransitionCollection : WFCollection<Transition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="transitionID"></param>
        /// <returns></returns>
        public Transition this[string transitionID]
        {
            get
            {
                if (string.IsNullOrEmpty(transitionID))
                    return null;
                Transition result = null;
                foreach (Transition t in this.DataCollection)
                {
                    if (t.TransitionID == transitionID)
                    {
                        result = t;
                        break;
                    }
                }
                return result;
            }
        }
        /// <summary>
        /// 查找变迁规则。
        /// </summary>
        /// <param name="fromStepID"></param>
        /// <returns></returns>
        public TransitionCollection FindTransition(string fromStepID)
        {
            TransitionCollection collection = new TransitionCollection();
            if (!string.IsNullOrEmpty(fromStepID))
            {
                foreach (Transition t in this.DataCollection)
                {
                    if (string.Equals(t.FromStepID, fromStepID, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase))
                    {
                        collection.Add(t);
                    }
                }
            }
            return collection;
        }

    }
    /// <summary>
    /// 步骤变迁规则定义。
    /// </summary>
    public class Transition
    {
        /// <summary>
        /// 获取或设置变迁规则ID。
        /// </summary>
        public string TransitionID { get; set; }
        /// <summary>
        /// 获取或设置前驱步骤ID。
        /// </summary>
        public string FromStepID { get; set; }
        /// <summary>
        /// 获取或设置后续步骤ID。
        /// </summary>
        public string ToStepID { get; set; }
        /// <summary>
        /// 获取或设置变迁规则。
        /// </summary>
        public EnumTransitionRule TransitionRule { get; set; }
        /// <summary>
        /// 获取或设置变迁规则条件。
        /// </summary>
        public ConditionCollection Conditions { get; set; }
        /// <summary>
        /// 获取或设置参数映射。
        /// </summary>
        public ParameterMapCollection Maps { get; set; }
    }
}
