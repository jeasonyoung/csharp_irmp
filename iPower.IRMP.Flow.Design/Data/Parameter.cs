//================================================================================
//  FileName: Parameter.cs
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
    /// 流程中各种操作参数的类型定义。
    /// </summary>
    public enum EnumParameterType
    {
        /// <summary>
        /// 字符串类型。
        /// </summary>
        String = 0x00,
        /// <summary>
        /// 整型数据。
        /// </summary>
        Int32 = 0x01
    }
    /// <summary>
    /// 步骤参数定义。
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// 获取或设置参数ID。
        /// </summary>
        public string ParameterID
        {
            get;
            set;
        }
        /// <summary>
        /// 获取或设置参数名称。
        /// </summary>
        public string ParameterName
        {
            get;
            set;
        }
        /// <summary>
        /// 获取或设置变量名称。
        /// </summary>
        public string ParameterVariable
        {
            get;
            set;
        }
        /// <summary>
        /// 获取或设置参数类型。
        /// </summary>
        public EnumParameterType ParameterType
        {
            get;
            set;
        }
        /// <summary>
        /// 获取或设置参数默认值。
        /// </summary>
        public string DefaultValue
        {
            get;
            set;
        }
        /// <summary>
        /// 获取或设置参数描述信息。
        /// </summary>
        public string ParameterDescription
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 步骤参数集合。
    /// </summary>
    public class ParameterCollection : WFCollection<Parameter>
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
                Parameter result = null;
                foreach (Parameter p in this.DataCollection)
                {
                    if (p.ParameterID == parameterID)
                    {
                        result = p;
                        break;
                    }
                }
                return result;
            }
        }
        /// <summary>
        /// 根据参数名称或者变量名称获取参数对象。
        /// </summary>
        /// <param name="parameterNameOrVariable">参数名称或者变量名称。</param>
        /// <returns>参数对象。</returns>
        public Parameter FindParameter(string parameterNameOrVariable)
        {
            Parameter p = null;
            if (!string.IsNullOrEmpty(parameterNameOrVariable))
            {
                foreach (Parameter r in this.DataCollection)
                {
                    if (string.Equals(r.ParameterName, parameterNameOrVariable, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase)
                        || string.Equals(r.ParameterVariable, parameterNameOrVariable, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase))
                    {
                        p = r;
                        break;
                    }
                }
            }
            return p;
        }
    }
}
