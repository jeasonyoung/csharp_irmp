//================================================================================
//  FileName: ParameterMap.cs
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
    /// 流程环节之间的参数映射模式，用于上一个环节的参数和下一个环节的参数之间的映射。
    /// </summary>
    public enum EnumMapMode
    {
        /// <summary>
        /// 直接传值模式，0x00的十六进制值。
        /// </summary>
        Value = 0x00,
        /// <summary>
        /// 函数反射模式，0x01的十六进制值。
        /// </summary>
        Reflection = 0x01
    }
    /// <summary>
    /// 步骤参数映射关系集合。
    /// </summary>
    public class ParameterMapCollection : WFCollection<ParameterMap>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterID"></param>
        /// <returns></returns>
        public ParameterMap this[string parameterID]
        {
            get
            {
                if (string.IsNullOrEmpty(parameterID))
                    return null;
                ParameterMap result = null;
                foreach (ParameterMap p in this.DataCollection)
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
    }
    /// <summary>
    /// 步骤参数映射关系。
    /// </summary>
    public class ParameterMap
    {
        /// <summary>
        /// 获取或设置参数ID。
        /// </summary>
        public string ParameterID { get; set; }
        /// <summary>
        /// 获取或设置映射参数ID。
        /// </summary>
        public string MapParameterID { get; set; }
        /// <summary>
        /// 获取或设置映射模式。
        /// </summary>
        public EnumMapMode MapMode { get; set; }
        /// <summary>
        /// 获取或设置映射函数的程序集。
        /// </summary>
        public string AssemblyName { get; set; }
        /// <summary>
        /// 获取或设置映射函数的类名称。
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 获取或设置映射函数入口名称。
        /// </summary>
        public string EntryName { get; set; }
    }
}
