//================================================================================
//  FileName: Condition.cs
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
    /// 比较操作符类别。
    /// </summary>
    public enum EnumCompareSign
    {
        /// <summary>
        /// 不可比较，0x00的十六进制值。
        /// </summary>
        None = 0x00,
        /// <summary>
        /// 大于，0x01的十六进制值。
        /// </summary>
        GT = 0x01,
        /// <summary>
        /// 等于，0x02的十六进制值。
        /// </summary>
        EQ = 0x02,
        /// <summary>
        /// 大于等于，0x03的十六进制值。
        /// </summary>
        GTEQ = 0x03,
        /// <summary>
        /// 小于，0x04的十六进制值。
        /// </summary>
        LT = 0x04,
        /// <summary>
        /// 不等于，0x04的十六进制值。
        /// </summary>
        NEQ = 0x05,
        /// <summary>
        /// 小于等于，0x06的十六进制值。
        /// </summary>
        LTEQ = 0x06
    }
    /// <summary>
    ///  变迁规则条件集合。
    /// </summary>
    public class ConditionCollection : WFCollection<Condition>
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
                Condition result = null;
                foreach (Condition c in this.DataCollection)
                {
                    if (c.ConditionID == conditionID)
                    {
                        result = c;
                        break;
                    }
                }
                return result;
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
            Condition result = null;
            foreach (Condition c in this.DataCollection)
            {
                if (c.ParameterID == parameterID)
                {
                    result = c;
                    break;
                }
            }
            return result;

        }
    }
    /// <summary>
    /// 变迁规则条件类。
    /// </summary>
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
