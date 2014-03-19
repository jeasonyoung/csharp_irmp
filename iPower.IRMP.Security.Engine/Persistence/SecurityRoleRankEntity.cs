//================================================================================
// FileName: SecurityRoleRankEntity.cs
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

using iPower.IRMP.Org;
using iPower.IRMP.Security.Engine.Domain;
namespace iPower.IRMP.Security.Engine.Persistence
{
	///<summary>
	///SecurityRoleRankEntity实体类。
	///</summary>
	internal class SecurityRoleRankEntity: DbModuleEntity<SecurityRoleRank>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public SecurityRoleRankEntity()
		{

		}
		#endregion

        /// <summary>
        /// 获取角色下的岗位级别ID。
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<GUIDEx> GetAllRank(GUIDEx roleID)
        {
            List<GUIDEx> list = new List<GUIDEx>();
            if (roleID.IsValid)
            {
                DataTable dtSource = this.GetAllRecord(string.Format("RoleID='{0}'", roleID));
                foreach (DataRow row in dtSource.Rows)
                {
                    list.Add(new GUIDEx(row["RankID"]));
                }
            }
            return list;
        }

        /// <summary>
        /// 获取角色下的岗位级别。
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public StringCollection GetRank(GUIDEx roleID)
        {
            StringCollection collection = new StringCollection();
            if (roleID.IsValid)
            {
                DataTable dtSource = this.GetAllRecord(string.Format("RoleID='{0}'", roleID));
                foreach (DataRow row in dtSource.Rows)
                {
                    collection.Add(Convert.ToString(row["RankID"]));
                }
            }
            return collection;
        }
        /// <summary>
        /// 获取列表数据。
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="rankName"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string roleName, string rankName)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("RoleID", typeof(string));
            dtResult.Columns.Add("RoleName", typeof(string));
            dtResult.Columns.Add("RankNames", typeof(string));

            const string sql = "exec spSecurityRoleRankListView '{0}','{1}'";
            DataTable dtSource = this.DatabaseAccess.ExecuteDataset(string.Format(sql, roleName, rankName)).Tables[0];
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                string strRoleID = null, strRoleName = null, strRanks = string.Empty;
                string oldStrRoleID = null;
                foreach (DataRow row in dtSource.Rows)
                {
                    strRoleID = Convert.ToString(row["RoleID"]);
                    if (!string.IsNullOrEmpty(oldStrRoleID) && (oldStrRoleID != strRoleID))
                    {
                        DataRow dr = dtResult.NewRow();
                        dr["RoleID"] = oldStrRoleID;
                        dr["RoleName"] = strRoleName;
                        dr["RankNames"] = strRanks;
                        dtResult.Rows.Add(dr);
                        strRanks = string.Empty;
                    }
                    strRoleName = Convert.ToString(row["RoleName"]);
                    if (!string.IsNullOrEmpty(strRanks))
                        strRanks += ",";
                    strRanks += Convert.ToString(row["RankName"]);
                    oldStrRoleID = strRoleID;
                }
                if (!string.IsNullOrEmpty(oldStrRoleID))
                {
                    DataRow dr = dtResult.NewRow();
                    dr["RoleID"] = oldStrRoleID;
                    dr["RoleName"] = strRoleName;
                    dr["RankNames"] = strRanks;
                    dtResult.Rows.Add(dr);
                }
                dtResult.AcceptChanges();
            }
            return dtResult.Copy();
        }
        /// <summary>
        /// 更新角色岗位级别。
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="rankIDs"></param>
        /// <returns></returns>
        public bool UpdateRoleRank(GUIDEx roleID, StringCollection rankIDs)
        {
            bool result = false;
            if (roleID.IsValid)
            {
                const string del_sql = "delete from {0} where RoleID='{1}'";
                result = this.DatabaseAccess.ExecuteNonQuery(string.Format(del_sql, this.TableName, roleID)) > 0;
                if (rankIDs != null && rankIDs.Count > 0)
                {
                    IOrgFactory facotry = this.ModuleConfig.OrgFactory;
                    if (facotry != null)
                    {
                        foreach (string rid in rankIDs)
                        {
                            OrgRankCollection ranks = facotry.GetAllRank(rid);
                            if (ranks != null && ranks.Count > 0)
                            {
                                SecurityRoleRank data = new SecurityRoleRank();
                                data.RoleID = roleID;
                                data.RankID = ranks[0].RankID;
                                data.RankName = ranks[0].RankName;
                                result = this.UpdateRecord(data);
                            }
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 批量删除。
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool BatchDeleteRoleRank(StringCollection roles)
        {
            const string del_sql = "delete from {0} where RoleID in ('{1}')";
            if (roles != null && roles.Count > 0)
            {
                string[] roleIDs = new string[roles.Count];
                roles.CopyTo(roleIDs, 0);
                return this.DatabaseAccess.ExecuteNonQuery(string.Format(del_sql, this.TableName, string.Join("','", roleIDs))) > 0;
            }
            return false;
        }
	}

}
