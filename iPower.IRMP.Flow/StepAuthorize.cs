//================================================================================
//  FileName:StepAuthorize.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-09 11:08:48
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
    /// 流程步骤的授权集合。
    /// </summary>
    [Serializable]
    public class StepAuthorizeCollection : FlowBaseCollection<StepAuthorize>
    {
        /// <summary>
        /// 查找授权用户ID。
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <param name="dtCurrent"></param>
        /// <returns></returns>
        public Dictionary<string,string> FindTargetEmployeeID(string EmployeeID, DateTime dtCurrent)
        {
            Dictionary<string, string> result = null;
            if (!string.IsNullOrEmpty(EmployeeID))
            {
                result = new Dictionary<string, string>();
                foreach (StepAuthorize authorize in this.Items)
                {
                    if ((authorize.EmployeeID == EmployeeID) && (dtCurrent >= authorize.BeginDate && dtCurrent <= authorize.EndDate))
                    {
                        result.Add(authorize.TargetEmployeeID, authorize.TargetEmployeeName);
                    }
                }
            }
            return result;
        }
    }
    /// <summary>
    /// 流程步骤的授权(针对自己拥有权限的步骤授权)
    /// </summary>
    [Serializable]
    public class StepAuthorize
    {
        /// <summary>
        /// 获取或设置授权ID。
        /// </summary>
        public string AuthorizeID { get; set; }
        /// <summary>
        /// 获取或设置授权用户ID。
        /// </summary>
        public string EmployeeID { get; set; }
        /// <summary>
        /// 获取或设置授权用户名称。
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 获取或设置被授权用户ID。
        /// </summary>
        public string TargetEmployeeID { get; set; }
        /// <summary>
        /// 获取或设置被授权用户名称。
        /// </summary>
        public string TargetEmployeeName { get; set; }
        /// <summary>
        /// 获取或设置授权生效开始时间。
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 获取或设置授权生效结束时间。
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
