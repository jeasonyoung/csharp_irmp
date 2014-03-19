//================================================================================
//  FileName: ParamInstanceCollection.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/7/13
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
using System.Text;

using iPower.Data;
namespace iPower.IRMP.Flow.Poxy
{
    /// <summary>
    /// 参数实例集合。
    /// </summary>
    [Serializable]
    public class ParamInstanceCollection : DataCollection<ParamInstance>
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
