//================================================================================
//  FileName:Transition.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-09 10:45:17
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

namespace iPower.IRMP.Flow
{
    /// <summary>
    /// 步骤变迁规则集合。
    /// </summary>
    [Serializable]
    public class TransitionCollection : FlowBaseCollection<Transition>
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
                Transition t = this.Items.Find(new Predicate<Transition>(delegate(Transition sender)
                {
                    return (sender != null) && (string.Equals(sender.TransitionID, transitionID, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase));
                }));
                return t;
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
                foreach (Transition t in this.Items)
                {
                    if (string.Equals(t.FromStepID, fromStepID, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase))
                    {
                        collection.Add(t);
                    }
                }
            }
            return collection;
        }
        /// <summary>
        ///  查找变迁规则。
        /// </summary>
        /// <param name="toStepID"></param>
        /// <returns></returns>
        public TransitionCollection FindToStepTransition(string toStepID)
        {
            TransitionCollection collection = new TransitionCollection();
            if (!string.IsNullOrEmpty(toStepID))
            {
                foreach (Transition t in this.Items)
                {
                    if (string.Equals(t.ToStepID, toStepID, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase))
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
    [Serializable]
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
        /// 获取或设置是否已执行完成。
        /// </summary>
        public bool IsComplete { get; set; }
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
