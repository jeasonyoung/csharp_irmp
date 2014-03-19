//================================================================================
//  FileName: OrgFactory.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/7/12
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

using iPower.Logs;
using Org = iPower.IRMP.Org;
namespace iPower.IRMP.Org.Poxy
{
    /// <summary>
    /// 组织架构工厂类。
    /// </summary>
    public class OrgFactory : IOrgFactory
    {
        #region 成员变量，构造函数。
        LogContainer log = null;
        ModuleConfiguration config = null;
        OrgFactoryServicePoxy poxy = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public OrgFactory()
        {
            this.config = ModuleConfiguration.ModuleConfig;
            this.log = new LogContainer(this.config);
            this.poxy = new OrgFactoryServicePoxy();
            this.poxy.Url = this.config.OrgFactoryServiceURL;
        }
        #endregion

        #region IOrgFactory 成员
        /// <summary>
        ///  获取所有的部门数据。
        /// </summary>
        /// <param name="departmentID">部门ID。</param>
        /// <returns></returns>
        public OrgDepartmentCollection GetAllDepartment(string departmentID)
        {
            try
            {
                OrgDepartmentCollection collection = new OrgDepartmentCollection();
                OrgDepartment[] departments = this.poxy.GetAllDepartment(departmentID);
                if (departments != null && departments.Length > 0)
                {
                    foreach (OrgDepartment d in departments)
                    {
                        Org.OrgDepartment item = new Org.OrgDepartment();
                        item.DepartmentID = d.DepartmentID;
                        item.DepartmentName = d.DepartmentName;
                        item.ParentDepartmentID = d.ParentDepartmentID;
                        item.Order = d.order;

                        collection.Add(item);
                    }
                }
                return collection;
            }
            catch (Exception e)
            {
                this.log.CreateErrorLog(e.Message);
                throw e;
            }
        }
        /// <summary>
        /// 获取用户下的部门数据。
        /// </summary>
        /// <param name="employeeID">部门ID。</param>
        /// <returns></returns>
        public OrgDepartmentCollection GetSubCharge(string employeeID)
        {
            try
            {
                OrgDepartmentCollection collection = new OrgDepartmentCollection();
                OrgDepartment[] departments = this.poxy.GetSubCharge(employeeID);
                if (departments != null && departments.Length > 0)
                {
                    foreach (OrgDepartment d in departments)
                    {
                        Org.OrgDepartment item = new Org.OrgDepartment();
                        item.DepartmentID = d.DepartmentID;
                        item.DepartmentName = d.DepartmentName;
                        item.ParentDepartmentID = d.ParentDepartmentID;
                        item.Order = d.order;

                        collection.Add(item);
                    }
                }
                return collection;
            }
            catch (Exception e)
            {
                this.log.CreateErrorLog(e.Message);
                throw e;
            }
        }
        /// <summary>
        ///  获取所有的岗位级别数据。
        /// </summary>
        /// <param name="rankID">岗位级别ID。</param>
        /// <returns></returns>
        public OrgRankCollection GetAllRank(string rankID)
        {
            try
            {
                OrgRankCollection collection = new OrgRankCollection();
                OrgRank[] ranks = this.poxy.GetAllRank(rankID);
                if (ranks != null && ranks.Length > 0)
                {
                    foreach (OrgRank r in ranks)
                    {
                        Org.OrgRank item = new Org.OrgRank();
                        item.ParentRankID = r.ParentRankID;
                        item.RankID = r.RankID;
                        item.RankName = r.RankName;
                        collection.Add(item);
                    }
                }
                return collection;
            }
            catch (Exception e)
            {
                this.log.CreateErrorLog(e.Message);
                throw e;
            }
        }
        /// <summary>
        /// 获取所有的岗位数据。
        /// </summary>
        /// <param name="postID">岗位ID。</param>
        /// <returns></returns>
        public OrgPostCollection GetAllPost(string postID)
        {
            try
            {
                OrgPostCollection collection = new OrgPostCollection();
                OrgPost[] posts = this.poxy.GetAllPost(postID);
                if (posts != null && posts.Length > 0)
                {
                    foreach (OrgPost p in posts)
                    {
                        Org.OrgPost item = new Org.OrgPost();
                        item.ParentPostID = p.ParentPostID;
                        item.PostID = p.PostID;
                        item.PostName = p.PostName;
                        item.DepartmentID = p.departmentID;
                        item.RankID = p.rankID;
                        collection.Add(item);
                    }
                }
                return collection;
            }
            catch (Exception e)
            {
                this.log.CreateErrorLog(e.Message);
                throw e;
            }
        }
        /// <summary>
        /// 获取所有的用户数据。
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <returns></returns>
        public OrgEmployeeCollection GetAllEmployee(string employeeID)
        {
            try
            {
                OrgEmployeeCollection collection = new OrgEmployeeCollection();
                OrgEmployee[] employees = this.poxy.GetAllEmployee(employeeID);
                if (employees != null && employees.Length > 0)
                {
                    foreach (OrgEmployee e in employees)
                    {
                        Org.OrgEmployee item = new Org.OrgEmployee();
                        item.EmployeeID = e.EmployeeID;
                        item.EmployeeSign = e.EmployeSign;
                        item.EmployeeName = e.EmployeeName;
                        item.Order = e.order;
                        item.DepartmentID = e.DepartmentID;
                        item.PostID = e.PostID;
                        collection.Add(item);
                    }
                }
                return collection;
            }
            catch (Exception e)
            {
                this.log.CreateErrorLog(e.Message);
                throw e;
            }
        }

        #endregion
    }
}
