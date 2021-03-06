﻿//================================================================================
//  FileName: IOrgFacotry.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/4/9
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

using iPower;
using iPower.Data;
namespace iPower.IRMP.Org
{
    /// <summary>
    /// 组织架构工厂接口。
    /// </summary>
    public interface IOrgFactory
    {
        /// <summary>
        /// 获取所有的部门数据。
        /// </summary>
        /// <param name="departmentID">部门ID。</param>
        /// <returns></returns>
        OrgDepartmentCollection GetAllDepartment(string departmentID);
        /// <summary>
        /// 获取用户下的部门数据。
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <returns></returns>
        OrgDepartmentCollection GetSubCharge(string employeeID);
        /// <summary>
        /// 获取所有的岗位级别数据。
        /// </summary>
        /// <param name="rankID">岗位级别ID。</param>
        /// <returns></returns>
        OrgRankCollection GetAllRank(string rankID);
        /// <summary>
        /// 获取所有的岗位数据。
        /// </summary>
        /// <param name="postID">岗位ID。</param>
        /// <returns></returns>
        OrgPostCollection GetAllPost(string postID);
        /// <summary>
        /// 获取所有的用户数据。
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <returns></returns>
        OrgEmployeeCollection GetAllEmployee(string employeeID);
    }
}
