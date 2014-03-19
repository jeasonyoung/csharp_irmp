//================================================================================
// FileName: SecurityRoleRightEntity.cs
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
using iPower.IRMP.Security.Engine.Domain;
namespace iPower.IRMP.Security.Engine.Persistence
{
	///<summary>
	///SecurityRoleRightEntity实体类。
	///</summary>
	internal class SecurityRoleRightEntity: DbModuleEntity<SecurityRoleRight>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public SecurityRoleRightEntity()
		{

		}
		#endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public IListControlsTreeViewData GetRoleSystemAllRightData(string roleID)
        {
            const string sql = "exec spSecurityRoleSystemAllRight '{0}'";
            DataTable dtSource = this.DatabaseAccess.ExecuteDataset(string.Format(sql, roleID)).Tables[0].Copy();

            return new ListControlsTreeViewDataSource("RSRName", "RSRID", "ParentRSRID", "RSRName", dtSource);
        }
        /// <summary>
        /// 获取角色下的所有权限。
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public StringCollection GetRoleRight(string roleID)
        {
            StringCollection collection = new StringCollection();
            DataTable dtSource = this.GetAllRecord(string.Format("RoleID='{0}'", roleID));
            foreach (DataRow row in dtSource.Rows)
            {
                collection.Add(Convert.ToString(row["RightID"]));
            }
            return collection;
        }

        /// <summary>
        /// 获取列表数据。
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="moduleRightName"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string roleName, string moduleRightName)
        {
            const string sql = "exec spSecurityRoleRightListView '{0}','{1}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, roleName, moduleRightName)).Tables[0].Copy();
        }
        /// <summary>
        /// 删除。
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public new bool DeleteRecord(string roleID)
        {
            return base.DeleteRecord(string.Format("RoleID='{0}'", roleID));
        }

	}

}
