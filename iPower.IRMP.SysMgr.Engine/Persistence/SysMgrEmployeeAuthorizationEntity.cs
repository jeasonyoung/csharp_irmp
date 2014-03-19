//================================================================================
// FileName: SysMgrEmployeeAuthorizationEntity.cs
// Desc:
// Called by
// Auth: 本代码由代码生成器自动生成。
// Date:
//================================================================================
// Change History
//================================================================================
// Date  Author  Description
// ----  ------  -----------
//
//================================================================================
// Copyright (C) 2009-2010 Jeason Young Corporation
//================================================================================
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Data;
	
using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.DataSource;
using iPower.Platform.Engine.Persistence;
using iPower.IRMP.SysMgr.Engine.Domain;
namespace iPower.IRMP.SysMgr.Engine.Persistence
{
	///<summary>
	///SysMgrEmployeeAuthorizationEntity实体类。
	///</summary>
	internal class SysMgrEmployeeAuthorizationEntity: DbModuleEntity<SysMgrEmployeeAuthorization>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public SysMgrEmployeeAuthorizationEntity()
		{

		}
		#endregion

        #region 数据操作。
        /// <summary>
        /// 列表数据源。
        /// </summary>
        /// <returns></returns>
        public DataTable ListDataSource(string employeeName)
        {
            const string sql = "exec spSysMgrEmployeeAuthorizationListView '{0}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, employeeName)).Tables[0].Copy();
        }
        /// <summary>
        /// 验证用户授权。
        /// </summary>
        /// <param name="employeeID">用户ID。</param>
        /// <param name="systemID">系统ID。</param>
        /// <param name="clientIP">用户登录IP地址。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>获得授权返回true,否则返回false。</returns>
        public bool UserAuthorizationVerification(GUIDEx employeeID, GUIDEx systemID, string clientIP, out string err)
        {
            err = null;
            const string sql = "exec spSysMgrEmployeeAuthentication '{0}','{1}','{2}'";
            bool result = false;
            if (!employeeID.IsValid)
                err = "用户ID不能为空！";
            else if (!systemID.IsValid)
                err = "系统ID不能为空！";
            else if (string.IsNullOrEmpty(clientIP))
                err = "用户登录IP不能为空！";
            else
            {
                object obj = this.DatabaseAccess.ExecuteScalar(string.Format(sql, employeeID, systemID, clientIP));
                if (obj != null)
                {
                    string[] strResult = obj.ToString().Split('|');
                    if (strResult != null)
                    {
                        err = strResult[1];
                        result = strResult[0] == "0";
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 获取用户姓名。
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public string GetEmployeeName(GUIDEx employeeID)
        {
            return this.DatabaseAccess.ExecuteScalar(string.Format("select top 1 EmployeeName from tblSysMgrEmployeeAuthorization where EmployeeID='{0}'", employeeID)).ToString();
        }
        /// <summary>
        /// 获取全部授权用户。
        /// </summary>
        /// <param name="employeeName"></param>
        /// <returns></returns>
        public DataTable GetAllAuthorizationEmployee(string employeeName)
        {
            const string sql = "exec spSysMgrAuthEmployee '{0}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, employeeName.Trim())).Tables[0].Copy();
        }
        /// <summary>
        /// 获取授权用户系统。
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="isAuthorization"></param>
        /// <returns></returns>
        public IListControlsData GetAuthorizationEmployeeApp(GUIDEx employeeID, bool isAuthorization)
        {
            const string sql = "exec spSysMgrEmployeeAuthorizationApp '{0}',{1}";
            DataTable dtSouce = this.DatabaseAccess.ExecuteDataset(string.Format(sql, employeeID, isAuthorization ? 1 : 0)).Tables[0].Copy();
            return new ListControlsDataSource("SystemName", "AppAuthID", dtSouce);
        }
        /// <summary>
        /// 绑定授权用户。
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public IListControlsData BindAuthorizationEmployees(params string[] employeeID)
        {
            const string sql = "select distinct EmployeeID,EmployeeName from {0}  where (EmployeeID in ('{1}'))";
            DataTable dtSource = this.DatabaseAccess.ExecuteDataset(string.Format(sql, this.TableName, string.Join("','", employeeID))).Tables[0];
            return new ListControlsDataSource("EmployeeName", "EmployeeID", dtSource);
        }
        /// <summary>
        /// 删除授权用户。
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool DeleteAuthorizationEmployee(GUIDEx employeeID, out string err)
        {
            const string sql = "exec spSysMgrDeleteEmployeeAuthorization '{0}'";
            err = null;
            if (employeeID.IsValid)
            {
                string result = this.DatabaseAccess.ExecuteScalar(string.Format(sql, employeeID)).ToString();
                string[] array = result.Split('|');
                err = array[1];
                return array[0] == "0";
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="keepApp"></param>
        /// <returns></returns>
        public bool DeleteEmployeeAuthApp(GUIDEx employeeID,string[] keepApp)
        {
            if (!employeeID.IsValid && keepApp == null && keepApp.Length == 0)
                return false;
            return this.DeleteRecord(string.Format("EmployeeID = '{0}' and (AppAuthID not in ('{1}'))", employeeID, string.Join("','", keepApp)));
        }
        #endregion
    }

}
