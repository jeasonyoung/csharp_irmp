//================================================================================
//  FileName:ParameterMap.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-09 14:28:26
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
    /// 步骤参数映射关系集合。
    /// </summary>
    public class ParameterMapCollection : FlowBaseCollection<ParameterMap>
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
                ParameterMap p = this.Items.Find(new Predicate<ParameterMap>(delegate(ParameterMap sender)
                {
                    return (sender != null) && (string.Equals(sender.ParameterID, parameterID, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase));
                }));
                return p;
            }
        }
    }
    /// <summary>
    /// 步骤参数映射关系。
    /// </summary>
    [Serializable]
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
