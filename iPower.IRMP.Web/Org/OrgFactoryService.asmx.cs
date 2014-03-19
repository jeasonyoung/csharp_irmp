//================================================================================
//  FileName: OrgFactoryProviderService.asmx.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/6/28
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
using System.Web;
using System.Web.Services;

using iPower.IRMP.Org;
using iPower.IRMP.Org.Engine;
namespace iPower.IRMP.Org.Web
{
    /// <summary>
    /// OrgFactoryProviderService。
    /// </summary>
    [WebService(Namespace = "http://ipower.org/",
                Name="OrgFactoryService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class OrgFactoryService : System.Web.Services.WebService, IOrgFactory
    {
        #region 成员变量，构造函数。
        IOrgFactory orgFactory = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public OrgFactoryService()
        {
            this.orgFactory = new OrgFactory();
        }
        #endregion

        #region IOrgFactory 成员
        /// <summary>
        /// 获取所有的部门数据。
        /// </summary>
        /// <param name="departmentID">部门ID。</param>
        /// <returns></returns>
        [WebMethod(Description = "获取所有的部门数据。")]
        public OrgDepartmentCollection GetAllDepartment(string departmentID)
        {
            return this.orgFactory.GetAllDepartment(departmentID);
        }
        /// <summary>
        /// 获取用户下的部门数据。
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <returns></returns>
        [WebMethod(Description = "获取用户下的部门数据。")]
        public OrgDepartmentCollection GetSubCharge(string employeeID)
        {
            return this.orgFactory.GetSubCharge(employeeID);
        }
        /// <summary>
        /// 获取所有的岗位级别数据。
        /// </summary>
        /// <param name="rankID">岗位级别ID。</param>
        /// <returns></returns>
        [WebMethod(Description = "获取所有的岗位级别数据。")]
        public OrgRankCollection GetAllRank(string rankID)
        {
            return this.orgFactory.GetAllRank(rankID);
        }
        /// <summary>
        /// 获取所有的岗位数据。
        /// </summary>
        /// <param name="postID">岗位ID。</param>
        /// <returns></returns>
        [WebMethod(Description = "获取所有的岗位数据。")]
        public OrgPostCollection GetAllPost(string postID)
        {
            return this.orgFactory.GetAllPost(postID);
        }
        /// <summary>
        /// 获取所有的用户数据。
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <returns></returns>
        [WebMethod(Description = "获取所有的用户数据。")]
        public OrgEmployeeCollection GetAllEmployee(string employeeID)
        {
            return this.orgFactory.GetAllEmployee(employeeID);
        }

        #endregion
    }
}
