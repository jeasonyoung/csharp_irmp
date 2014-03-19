//================================================================================
// FileName: OrgRankEntity.cs
// Desc:
// Called by
// Auth: �������ɴ����������Զ����ɡ�
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
    ///OrgRankEntityʵ���ࡣ
    ///</summary>
    internal class OrgRankEntity : DbModuleEntity<Domain.OrgRank>
    {
        #region ��Ա���������캯����
        ListControlsTreeViewDataSource listControlsTreeViewDataSource;
        ///<summary>
        ///���캯��
        ///</summary>
        public OrgRankEntity()
        {

        }
        #endregion

        /// <summary>
        /// ��ȡ�������ݡ�
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
        /// �б����ݡ�
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
        /// ��ȡȫ�����ݡ�
        /// </summary>
        /// <param name="rankID">��λ����ID��</param>
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
