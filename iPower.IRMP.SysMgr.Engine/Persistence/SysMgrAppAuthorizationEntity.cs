//================================================================================
// FileName: SysMgrAppAuthorizationEntity.cs
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
	///SysMgrAppAuthorizationEntity实体类。
	///</summary>
	internal class SysMgrAppAuthorizationEntity: DbModuleEntity<SysMgrAppAuthorization>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public SysMgrAppAuthorizationEntity()
		{

		}
		#endregion

        /// <summary>
        /// 获取列表数据源。
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string appName)
        {
            return this.GetAllRecord(string.Format("SystemName like '%{0}%'", appName), "SystemID");
        }

        /// <summary>
        /// 验证系统授权。
        /// </summary>
        /// <param name="systemID">系统ID。</param>
        /// <param name="authPassword">授权密码。</param>
        /// <param name="err">异常错误信息。</param>
        /// <returns>获得授权返回true,否则返回false。</returns>
        public bool AppAuthorization(GUIDEx systemID, string authPassword, out string err)
        {
            err = null;
            bool result = false;
            if (!systemID.IsValid)
                err = "系统ID不能为空！";
            else if (string.IsNullOrEmpty(authPassword))
                err = "授权密码不能为空！";
            else
            {
                const string sql = "exec spSysMgrAppAuthorizedToVerify '{0}','{1}'";
                object obj = this.DatabaseAccess.ExecuteScalar(string.Format(sql, systemID, authPassword));
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
        /// 删除数据。
        /// </summary>
        /// <param name="appAuthID"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool DeleteRecord(GUIDEx appAuthID, out string err)
        {
            const string sql = "exec spSysMgrDeleteAppAuthorization '{0}'";
            err = null;
            if (appAuthID.IsValid)
            {
                string result = this.DatabaseAccess.ExecuteScalar(string.Format(sql, appAuthID)).ToString();
                string[] array = result.Split('|');
                err = array[1];
                return array[0] == "0";
            }
            return false;
        }
        /// <summary>
        /// Picker授权系统
        /// </summary>
        /// <param name="SystemName">系统名称</param>
        /// <returns></returns>
        public IListControlsData AppAuthorizationPicker(string SystemName)
        {
            return new ListControlsDataSource("SystemName", "AppAuthID", this.GetAllRecord(string.Format("SystemName like '%{0}%'", SystemName)));
        }
	}

}
