//================================================================================
// FileName: OrgRankEntity.cs
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

using iPower.IRMP;
using Domain = iPower.IRMP.Org.Engine.Domain;
namespace iPower.IRMP.Org.Engine.Persistence
{
    ///<summary>
    ///OrgRankEntity实体类。
    ///</summary>
    internal class OrgRankEntity : DbModuleEntity<Domain.OrgRank>
    {
        #region 成员变量，构造函数。
        ListControlsTreeViewDataSource listControlsTreeViewDataSource;
        ///<summary>
        ///构造函数
        ///</summary>
        public OrgRankEntity()
        {

        }
        #endregion

        /// <summary>
        /// 获取树形数据。
        /// </summary>
        public IListControlsTreeViewData RankData
        {
            get
            {
                if (this.listControlsTreeViewDataSource == null)
                    this.listControlsTreeViewDataSource = new ListControlsTreeViewDataSource("RankName", "RankID", "ParentRankID", this.GetAllRecord());
                return this.listControlsTreeViewDataSource;
            }
        }

        /// <summary>
        /// 列表数据。
        /// </summary>
        /// <param name="rankName"></param>
        /// <param name="parentRankID"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string rankName, string parentRankID)
        {
            const string sql = "exec spOrgRankListView '{0}','{1}'";
            string strSQL = string.Format(sql, rankName, parentRankID);
            return this.DatabaseAccess.ExecuteDataset(strSQL).Tables[0].Copy();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rankID"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool DeleteRank(string rankID, out string err)
        {
            const string sql = "exec spOrgDeleteRank '{0}'";
            string result = this.DatabaseAccess.ExecuteScalar(string.Format(sql, rankID)).ToString();
            string[] array = result.Split('|');
            err = array[1];

            return array[0] == "0";
        }

        /// <summary>
        /// 获取全部数据。
        /// </summary>
        /// <param name="rankID">岗位级别ID。</param>
        /// <returns></returns>
        public OrgRankCollection GetAllOrgRank(string rankID)
        {
            OrgRankCollection collection = new OrgRankCollection();
            DataTable dtSource = null;
            if (string.IsNullOrEmpty(rankID))
                dtSource = this.GetAllRecord();
            else
            {
                dtSource = this.GetAllRecord(string.Format("RankID = '{0}'", rankID));
                if (dtSource != null && dtSource.Rows.Count == 0)
                    dtSource = this.GetAllRecord(string.Format("RankName like '%{0}%'", rankID));
            }
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                foreach (DataRow row in dtSource.Rows)
                {
                    OrgRank rank = new OrgRank();
                    rank.RankID = Convert.ToString(row["RankID"]);
                    rank.RankName = Convert.ToString(row["RankName"]);
                    rank.ParentRankID = Convert.ToString(row["ParentRankID"]);
                    collection.Add(rank);
                }
            }
            return collection;
        }
    }
}
