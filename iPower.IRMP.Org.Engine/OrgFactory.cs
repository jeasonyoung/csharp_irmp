//================================================================================
//  FileName: OrgFacotry.cs
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

using iPower.IRMP.Org;
using iPower.IRMP.Org.Engine.Persistence;
namespace iPower.IRMP.Org.Engine
{
    /// <summary>
    /// 组织架构工厂类。
    /// </summary>
    public class OrgFactory : IOrgFactory
    {
        #region 成员变量，构造函数。
        OrgDepartmentEntity orgDepartmentEntity;
        OrgRankEntity orgRankEntity;
        OrgPostEntity orgPostEntity;
        OrgEmployeeEntity orgEmployeeEntity;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public OrgFactory()
        {
            this.orgDepartmentEntity = new OrgDepartmentEntity();
            this.orgRankEntity = new OrgRankEntity();
            this.orgPostEntity = new OrgPostEntity();
            this.orgEmployeeEntity = new OrgEmployeeEntity();
        }
        #endregion

        #region IOrgFacotry 成员
        /// <summary>
        /// 获取所有的部门数据。
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        public OrgDepartmentCollection GetAllDepartment(string departmentID)
        {
            return this.orgDepartmentEntity.GetAllDepartment(departmentID);
        }
        /// <summary>
        /// 获取用户下的部门数据。
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <returns></returns>
        public OrgDepartmentCollection GetSubCharge(string employeeID)
        {
            return this.orgEmployeeEntity.GetSubCharge(employeeID);
        }
        /// <summary>
        /// 获取所有的岗位级别数据。
        /// </summary>
        /// <param name="rankID">岗位级别ID。</param>
        /// <returns></returns>
        public OrgRankCollection GetAllRank(string rankID)
        {
            return this.orgRankEntity.GetAllOrgRank(rankID);
        }
        /// <summary>
        /// 获取所有的岗位数据。
        /// </summary>
        /// <param name="postID">岗位ID。</param>
        /// <returns></returns>
        public OrgPostCollection GetAllPost(string postID)
        {
            return this.orgPostEntity.GetAllPost(postID);
        }
        /// <summary>
        /// 获取所有的用户数据。
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <returns></returns>
        public OrgEmployeeCollection GetAllEmployee(string employeeID)
        {
            return this.orgEmployeeEntity.GetAllEmployee(employeeID);
        }
        #endregion
    }
}
