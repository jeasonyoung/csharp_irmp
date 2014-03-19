//================================================================================
//  FileName: AuthenticateUserModulesProvider.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/7/5
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
using System.Web.Caching;
using System.Data;
using iPower;
using iPower.Platform;
using iPower.Platform.Security;
namespace iPower.IRMP.Security.Client
{
    /// <summary>
    /// 验证用户模块。
    /// </summary>
    internal class AuthenticateUserModulesProvider
    {
        #region 成员变量，构造函数。
        private System.Web.Caching.Cache cache = null;
        private ISecurityPermissionFactory securityPermissionFacotry = null;
        /// <summary>
        ///构造函数。
        /// </summary>
        public AuthenticateUserModulesProvider(ISecurityPermissionFactory securityPermissionFacotry)
        {
            this.cache = System.Web.HttpContext.Current.Cache;
            this.securityPermissionFacotry = securityPermissionFacotry;
        }
        #endregion

        #region 验证用户模块。
        /// <summary>
        /// 设置页面按钮权限。
        /// </summary>
        /// <param name="security"></param>
        /// <param name="systemID"></param>
        /// <param name="userID"></param>
        public void SetPagePermissions(ISecurity security, GUIDEx systemID, GUIDEx userID)
        {
            lock (this)
            {
                if (security != null && security.SecurityID.IsValid && systemID.IsValid && userID.IsValid)
                {
                    string key = string.Format("SPP_{0}_{1}_{2}", systemID, security.SecurityID, userID);
                    SecurityPermissionCollection permissions = this.cache[key] as SecurityPermissionCollection;
                    if (permissions == null)
                    {
                        permissions = this.securityPermissionFacotry.ModulePermissions(systemID, security.SecurityID, userID);
                        if (permissions != null)
                        {
                            this.cache.Insert(key, permissions, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
                        }
                    }
                    if (permissions == null || permissions.Count == 0)
                    {
                        throw new Exception("用户无权访问本页面，请与管理员联系！");
                    }
                    else
                    {
                        security.VerifyPermissions(permissions);
                    }
                }
            }
        }
        /// <summary>
        /// 验证用户模块。
        /// </summary>
        public ModuleDefineCollection AuthenticateUserModules(ModuleDefineCollection modules, GUIDEx systemID, GUIDEx userID)
        {
            lock (this)
            {
                if (this.securityPermissionFacotry != null && modules != null && systemID.IsValid && userID.IsValid)
                {
                    string key = string.Format("AUM_{0}_{1}_{2:yyyy-MM-dd-HH}", systemID, userID, DateTime.Now);
                    ModuleDefineCollection result = this.cache[key] as ModuleDefineCollection;
                    if (result == null)
                    {
                        result = new ModuleDefineCollection();

                        #region 定义表结构。
                        DataTable dtSource = new DataTable();
                        dtSource.Columns.Add("ModuleID", typeof(System.String));
                        dtSource.Columns.Add("ParentModuleID", typeof(System.String));
                        dtSource.Columns.Add("ModuleName", typeof(System.String));
                        dtSource.Columns.Add("ModuleUri", typeof(System.String));
                        dtSource.Columns.Add("OrderNo", typeof(System.Int32));
                        dtSource.PrimaryKey = new DataColumn[] { dtSource.Columns["ModuleID"] };
                        #endregion

                        //转换为表结构。
                        this.AuthenticateUserModules(ref dtSource, modules);

                        #region 校验权限。
                        if (dtSource != null && dtSource.Rows.Count > 0)
                        {
                            //获取有权限的模块ID。
                            List<GUIDEx> listRightModules = this.AuthenticateUserModules(dtSource, systemID, userID);
                            if (listRightModules != null && listRightModules.Count > 0)
                            {
                                DataTable dtResult = dtSource.Clone();
                                //查找有权限的模块ID到结果数据集。
                                foreach (GUIDEx moduleID in listRightModules)
                                {
                                    DataRow dr = dtSource.Rows.Find(moduleID);
                                    if (dr != null && (dtResult.Rows.Find(moduleID) == null))
                                        dtResult.ImportRow(dr);
                                }
                                //整理结果数据集的结构。
                                DataRow[] parentRows = dtResult.Select("isnull(ParentModuleID,'') <> ''");
                                this.AuthenticateUserModules(ref dtResult, parentRows, dtSource.Copy());

                                #region 转换为对象。
                                DataRow[] drs = dtResult.Select("isnull(ParentModuleID,'') = ''", "OrderNo");
                                if (drs != null)
                                {
                                    foreach (DataRow row in drs)
                                    {
                                        ModuleDefine moduleDefine = new ModuleDefine(Convert.ToString(row["ModuleID"]),
                                                                                     Convert.ToString(row["ModuleName"]),
                                                                                     Convert.ToString(row["ModuleUri"]),
                                                                                     Convert.ToInt32(row["OrderNo"]));

                                        this.AuthenticateUserModules(moduleDefine, dtResult.Select(string.Format("isnull(ParentModuleID,'') = '{0}'", row["ModuleID"]), "OrderNo"), dtResult.Copy());
                                        result.Add(moduleDefine);
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion

                        if (result.Count > 0)
                        {
                            this.cache.Insert(key, result, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                        }
                    }
                    return result;
                }
                return modules;
            }
        }
        /// <summary>
        /// 验证用户模块。
        /// </summary>
        /// <param name="systemID">系统ID。</param>
        /// <param name="userID">用户ID。</param>
        /// <param name="moduleID">模块ID。</param>
        /// <returns></returns>
        private bool AuthenticateUserModules(GUIDEx systemID, GUIDEx userID, GUIDEx moduleID)
        {
            bool result = false;
            if (this.securityPermissionFacotry != null && systemID.IsValid && userID.IsValid && moduleID.IsValid)
            {
                SecurityPermissionCollection securityPermissionCollection = this.securityPermissionFacotry.ModulePermissions(systemID, moduleID, userID);
                if (securityPermissionCollection != null && securityPermissionCollection.Count > 0)
                {
                    SecurityPermission sp = securityPermissionCollection[SecurityPermissionConstants.Query];
                    return sp != null;
                }
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="collection"></param>
        private void AuthenticateUserModules(ref DataTable dt, ModuleDefineCollection collection)
        {
            if (collection != null)
            {
                DataRow dr = null;
                foreach (ModuleDefine d in collection)
                {
                    if (dt.Rows.Find(d.ModuleID) == null)
                    {
                        dr = dt.NewRow();
                        dr["ModuleID"] = d.ModuleID;
                        dr["ParentModuleID"] = (d.Parent == null) ? string.Empty : d.Parent.ModuleID;
                        dr["ModuleName"] = d.ModuleName;
                        dr["ModuleUri"] = d.ModuleUri;
                        dr["OrderNo"] = d.OrderNo;
                        dt.Rows.Add(dr);
                    }
                    if (d.Modules != null && d.Modules.Count > 0)
                        this.AuthenticateUserModules(ref dt, d.Modules);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="systemID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        private List<GUIDEx> AuthenticateUserModules(DataTable dtSource, GUIDEx systemID, GUIDEx userID)
        {
            List<GUIDEx> result = null;
            if (dtSource != null && systemID.IsValid && userID.IsValid)
            {
                result = new List<GUIDEx>();
                GUIDEx moduleID = GUIDEx.Null;
                foreach (DataRow row in dtSource.Rows)
                {
                    moduleID = new GUIDEx(row["ModuleID"]);
                    if (moduleID.IsValid && this.AuthenticateUserModules(systemID, userID, moduleID))
                        result.Add(moduleID);
                }
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtResult"></param>
        /// <param name="parentRows"></param>
        /// <param name="dtSource"></param>
        private void AuthenticateUserModules(ref DataTable dtResult, DataRow[] parentRows, DataTable dtSource)
        {
            if (parentRows != null && parentRows.Length > 0)
            {
                List<DataRow> listParent = new List<DataRow>();
                foreach (DataRow row in parentRows)
                {
                    DataRow[] drs = dtSource.Select(string.Format("ModuleID='{0}'", row["ParentModuleID"]));
                    if (drs != null && drs.Length > 0)
                    {
                        foreach (DataRow r in drs)
                        {
                            if (dtResult.Rows.Find(r["ModuleID"]) == null)
                            {
                                dtResult.ImportRow(r);
                                if (new GUIDEx(r["ParentModuleID"]).IsValid)
                                    listParent.Add(r);
                            }
                        }
                    }
                    else
                    {
                        DataRow dr = dtResult.Rows.Find(row["ModuleID"]);
                        if (dr != null)
                            dr["ParentModuleID"] = string.Empty;
                    }
                }
                if (listParent.Count > 0)
                    this.AuthenticateUserModules(ref dtResult, listParent.ToArray(), dtSource.Copy());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="drs"></param>
        /// <param name="dtResult"></param>
        private void AuthenticateUserModules(ModuleDefine parent, DataRow[] drs, DataTable dtResult)
        {
            if (parent != null && drs != null)
            {
                foreach (DataRow row in drs)
                {
                    ModuleDefine m = new ModuleDefine(Convert.ToString(row["ModuleID"]),
                                                                 Convert.ToString(row["ModuleName"]),
                                                                 Convert.ToString(row["ModuleUri"]),
                                                                 Convert.ToInt32(row["OrderNo"]));

                    this.AuthenticateUserModules(m, dtResult.Select(string.Format("isnull(ParentModuleID,'') = '{0}'", row["ModuleID"]), "OrderNo"), dtResult.Copy());
                    parent.Modules.Add(m);
                }
            }
        }
        #endregion
    }
}
