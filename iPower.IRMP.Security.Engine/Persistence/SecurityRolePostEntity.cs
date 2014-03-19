//================================================================================
// FileName: SecurityRolePostEntity.cs
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
	///SecurityRolePostEntity实体类。
	///</summary>
	internal class SecurityRolePostEntity: DbModuleEntity<SecurityRolePost>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public SecurityRolePostEntity()
		{

		}
		#endregion

        /// <summary>
        /// 获取角色下的岗位。
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<GUIDEx> GetAllPost(GUIDEx roleID)
        {
            List<GUIDEx> list = new List<GUIDEx>();
            if (roleID.IsValid)
            {
                DataTable dtSource = this.GetAllRecord(string.Format("RoleID='{0}'", roleID));
                foreach (DataRow row in dtSource.Rows)
                {
                    list.Add(new GUIDEx(row["PostID"]));
                }
            }
            return list;
        }

        /// <summary>
        /// 获取角色下的岗位ID。
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public StringCollection GetPost(GUIDEx roleID)
        {
            StringCollection collection = new StringCollection();
            if (roleID.IsValid)
            {
                DataTable dtSource = this.GetAllRecord(string.Format("RoleID='{0}'", roleID));
                foreach (DataRow row in dtSource.Rows)
                {
                    collection.Add(Convert.ToString(row["PostID"]));
                }
            }
            return collection;
        }

        /// <summary>
        /// 获取列表数据。
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="postName"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string roleName, string postName)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("RoleID", typeof(string));
            dtResult.Columns.Add("RoleName", typeof(string));
            dtResult.Columns.Add("PostNames", typeof(string));

            const string sql = "exec spSecurityRolePostListView '{0}','{1}'";
            DataTable dtSource = this.DatabaseAccess.ExecuteDataset(string.Format(sql, roleName, postName)).Tables[0];
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                string strRoleID = null, strRoleName = null, strPosts = string.Empty;
                string oldStrRoleID = null;
                foreach (DataRow row in dtSource.Rows)
                {
                    strRoleID = Convert.ToString(row["RoleID"]);
                    if (!string.IsNullOrEmpty(oldStrRoleID) && (oldStrRoleID != strRoleID))
                    {
                        DataRow dr = dtResult.NewRow();
                        dr["RoleID"] = oldStrRoleID;
                        dr["RoleName"] = strRoleName;
                        dr["PostNames"] = strPosts;
                        dtResult.Rows.Add(dr);
                        strPosts = string.Empty;
                    }
                    strRoleName = Convert.ToString(row["RoleName"]);

                    if (!string.IsNullOrEmpty(strPosts))
                        strPosts += ",";

                    strPosts += Convert.ToString(row["PostName"]);

                    oldStrRoleID = strRoleID;
                }

                if (!string.IsNullOrEmpty(oldStrRoleID))
                {
                    DataRow dr = dtResult.NewRow();
                    dr["RoleID"] = oldStrRoleID;
                    dr["RoleName"] = strRoleName;
                    dr["PostNames"] = strPosts;
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
        /// <param name="posts"></param>
        /// <returns></returns>
        public bool UpdateRolePost(GUIDEx roleID, StringCollection posts)
        {
            bool result = false;
            if (roleID.IsValid)
            {
                const string del_sql = "delete from {0} where RoleID='{1}'";
                result = this.DatabaseAccess.ExecuteNonQuery(string.Format(del_sql, this.TableName, roleID)) > 0;
                if (posts != null && posts.Count > 0)
                {
                    IOrgFactory factory = this.ModuleConfig.OrgFactory;
                    if (factory != null)
                    {
                        SecurityRolePost data = new SecurityRolePost();
                        foreach (string pid in posts)
                        {
                            OrgPostCollection postCollection = factory.GetAllPost(pid);
                            if (postCollection != null && postCollection.Count > 0)
                            {
                                data.RoleID = roleID;
                                data.PostID = postCollection[0].PostID;
                                data.PostName = postCollection[0].PostName;
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
        public bool BatchDeleteRolePost(StringCollection roles)
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
