//================================================================================
//  FileName:Parameter.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-09 10:35:53
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
    /// 步骤参数集合。
    /// </summary>
    [Serializable]
    public class ParameterCollection : FlowBaseCollection<Parameter>
    {
        /// <summary>
        /// 索引属性。
        /// </summary>
        /// <param name="parameterID"></param>
        /// <returns></returns>
        public Parameter this[string parameterID]
        {
            get
            {
                if (string.IsNullOrEmpty(parameterID))
                    return null;
                Parameter p = this.Items.Find(new Predicate<Parameter>(delegate(Parameter sender)
                {
                    return (sender != null) && (string.Equals(sender.ParameterID, parameterID, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase));
                }));
                return p;
            }
        }
        /// <summary>
        /// 根据参数名称或者变量名称获取参数对象。
        /// </summary>
        /// <param name="parameterName">参数名称。</param>
        /// <returns>参数对象。</returns>
        public Parameter FindParameter(string parameterName)
        {
            Parameter p = null;
            if (!string.IsNullOrEmpty(parameterName))
            {
                foreach (Parameter r in this.Items)
                {
                    if (string.Equals(r.ParameterName, parameterName, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase))
                    {
                        p = r;
                        break;
                    }
                }
            }
            return p;
        }
    }
    /// <summary>
    /// 步骤参数定义。
    /// </summary>
    [Serializable]
    public class Parameter
    {
        /// <summary>
        /// 获取或设置参数ID。
        /// </summary>
        public string ParameterID { get; set; }
        /// <summary>
        /// 获取或设置参数名称。
        /// </summary>
        public string ParameterName { get; set; }
        /// <summary>
        /// 获取或设置参数类型。
        /// </summary>
        public EnumParameterType ParameterType { get; set; }
        /// <summary>
        /// 获取或设置参数默认值。
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 获取或设置参数描述信息。
        /// </summary>
        public string ParameterDescription { get; set; }
    }

    /// <summary>
    /// 参数实例定义。
    /// </summary>
    [Serializable]
    public class ParamInstance
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="name">参数名称。</param>
        /// <param name="value">参数值。</param>
        public ParamInstance(string name, string value)
        {
            this.ParamName = name;
            this.ParamValue = value;
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        public ParamInstance()
            : this(string.Empty, string.Empty)
        {
        }
        #endregion

        /// <summary>
        /// 获取或设置参数名称。
        /// </summary>
        public string ParamName { get; set; }
        /// <summary>
        /// 获取或设置参数值
        /// </summary>
        public string ParamValue { get; set; }
    }
    /// <summary>
    /// 参数实例集合。
    /// </summary>
    [Serializable]
    public class ParamInstanceCollection : FlowBaseCollection<ParamInstance>
    {
        #region 索引属性。
        /// <summary>
        /// 获取参数值。
        /// </summary>
        /// <param name="paramName">参数名称。</param>
        /// <returns>参数值。</returns>
        public string this[string paramName]
        {
            get
            {
                string result = string.Empty;
                if (!string.IsNullOrEmpty(paramName))
                {
                    ParamInstance p = this.Items.Find(new Predicate<ParamInstance>(delegate(ParamInstance sender)
                    {
                        return (sender != null) && string.Equals(sender.ParamName, paramName, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase);
                    }));

                    if (p != null)
                        result = p.ParamValue;
                }
                return result;
            }
        }
        #endregion
    }
}
