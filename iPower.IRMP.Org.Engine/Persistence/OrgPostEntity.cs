//================================================================================
// FileName: OrgPostEntity.cs
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
using iPower.Data;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.DataSource;
using iPower.Platform.Engine.Persistence;
using iPower.IRMP.Org;
using Domain = iPower.IRMP.Org.Engine.Domain;
namespace iPower.IRMP.Org.Engine.Persistence
{
    ///<summary>
    ///OrgPostEntity实体类。
    ///</summary>
    internal class OrgPostEntity : DbModuleEntity<Domain.OrgPost>
    {
        #region 成员变量，构造函数。
        ///<summary>
        ///构造函数
        ///</summary>
        public OrgPostEntity()
        {

        }
        #endregion

        /// <summary>
        /// 获取岗位树形数据。
        /// </summary>
        public ListControlsTreeViewDataSource PostData(string departmentID)
        {
            DataTable dtSource = this.GetAllRecord();
            if (!string.IsNullOrEmpty(departmentID))
                dtSource = this.GetAllRecord(string.Format("DepartmentID='{0}'", departmentID));
            return new ListControlsTreeViewDataSource("PostName", "PostID", "ParentPostID", dtSource);
        }
        /// <summary>
        /// 根据部门获取岗位数据。
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public ListControlsTreeViewDataSource Post(GUIDEx departmentID)
        {
            DataTable dtSource = this.GetAllRecord();
            if (departmentID.IsValid)
                dtSource = this.GetAllRecord(string.Format("DepartmentID='{0}'", departmentID));
            return new ListControlsTreeViewDataSource("PostName", "PostID", "ParentPostID", dtSource);
        }
        /// <summary>
        /// 获取部门下的岗位。
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public ListControlsTreeViewDataSource DepartmentPost(GUIDEx departmentID)
        {
            const string sql = "exec spOrgDepartmentPost '{0}'";

            DataTable dtSource = this.DatabaseAccess.ExecuteDataset(string.Format(sql, departmentID)).Tables[0].Copy();

            return new ListControlsTreeViewDataSource("FieldName", "FieldID", "ParentFieldID", dtSource);
        }

        /// <summary>
        /// 列表数据源。
        /// </summary>
        /// <param name="departmentName"></param>
        /// <param name="rankID"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string departmentName, string rankID)
        {
            const string sql = "exec spOrgPostListView '{0}','{1}'";

            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, departmentName, rankID)).Tables[0].Copy();
        }
        /// <summary>
        /// 删除岗位。
        /// </summary>
        /// <param name="postID"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool DeletePost(string postID, out string err)
        {
            const string sql = "exec spOrgDeletePost '{0}'";
            string result = this.DatabaseAccess.ExecuteScalar(string.Format(sql, postID)).ToString();
            string[] array = result.Split('|');
            err = array[1];

            return array[0] == "0";
        }

        /// <summary>
        /// 获取全部岗位集合。
        /// </summary>
        /// <param name="postID">岗位ID。</param>
        /// <returns></returns>
        public OrgPostCollection GetAllPost(string postID)
        {
            OrgPostCollection collection = new OrgPostCollection();
            DataTable dtSource = null;
            if (string.IsNullOrEmpty(postID))
                dtSource = this.GetAllRecord();
            else
            {
                dtSource = this.GetAllRecord(string.Format("PostID = '{0}'", postID));
                if(dtSource != null && dtSource.Rows.Count == 0)
                    dtSource = this.GetAllRecord(string.Format("PostName like '%{0}%'", postID));
            }
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                foreach (DataRow row in dtSource.Rows)
                {
                    OrgPost item = new OrgPost();
                    item.PostID = Convert.ToString(row["PostID"]);
                    item.PostName = Convert.ToString(row["PostName"]);
                    item.ParentPostID = Convert.ToString(row["ParentPostID"]);

                    item.DepartmentID = Convert.ToString(row["DepartmentID"]);
                    item.RankID = Convert.ToString(row["RankID"]);

                    collection.Add(item);
                }
            }
            return collection;
        }
    }

}