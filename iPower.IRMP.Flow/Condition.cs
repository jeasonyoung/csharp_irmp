//================================================================================
//  FileName:Condition.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-09 10:53:23
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
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace iPower.IRMP.Flow
{
    /// <summary>
    ///  变迁规则条件集合。
    /// </summary>
    [Serializable]
    public class ConditionCollection : FlowBaseCollection<Condition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conditionID"></param>
        /// <returns></returns>
        public Condition this[string conditionID]
        {
            get
            {
                if (string.IsNullOrEmpty(conditionID))
                    return null;

                Condition c = this.Items.Find(new Predicate<Condition>(delegate(Condition sender)
                {
                    return (sender != null) && (string.Equals(sender.ConditionID, conditionID, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase));
                }));
                return c;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterID"></param>
        /// <returns></returns>
        public Condition FindCondition(string parameterID)
        {
            if (string.IsNullOrEmpty(parameterID))
                return null;

            Condition c = this.Items.Find(new Predicate<Condition>(delegate(Condition sender)
            {
                return (sender != null) && (string.Equals(sender.ParameterID, parameterID, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase));
            }));
            return c;

        }
    }
    /// <summary>
    /// 变迁规则条件类。
    /// </summary>
    [Serializable]
    public class Condition
    {
        /// <summary>
        /// 获取或设置条件ID。
        /// </summary>
        public string ConditionID { get; set; }
        /// <summary>
        /// 获取或设置参数ID。
        /// </summary>
        public string ParameterID { get; set; }
        /// <summary>
        /// 获取或设置比较的值。
        /// </summary>
        public string CompareValue { get; set; }
        /// <summary>
        /// 获取或设置比较结果。
        /// </summary>
        public EnumCompareSign ConditionValue { get; set; }
    }
}
