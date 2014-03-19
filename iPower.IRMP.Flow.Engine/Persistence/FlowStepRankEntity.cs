//================================================================================
// FileName: FlowStepRankEntity.cs
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
using iPower.IRMP.Flow.Engine.Domain;
namespace iPower.IRMP.Flow.Engine.Persistence
{
	///<summary>
	///FlowStepRankEntity实体类。
	///</summary>
	internal class FlowStepRankEntity: DbModuleEntity<FlowStepRank>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public FlowStepRankEntity()
		{

		}
		#endregion

        #region 数据处理。
        /// <summary>
        /// 加载数据。
        /// </summary>
        /// <param name="stepID">步骤ID。</param>
        /// <param name="rankID">岗位级别ID。</param>
        /// <param name="rankName">岗位级别名称。</param>
        /// <returns></returns>
        public bool LoadFlowStepRank(string stepID, out string[] rankID, out string[] rankName)
        {
            rankID = rankName = new string[0];
            bool result = false;
            if (!string.IsNullOrEmpty(stepID))
            {
                List<string> listRankID = new List<string>(), listRankName = new List<string>();
                DataTable dtSource = this.GetAllRecord(string.Format("StepID='{0}'", stepID));
                foreach (DataRow row in dtSource.Rows)
                {
                    listRankID.Add(Convert.ToString(row["RankID"]));
                    listRankName.Add(Convert.ToString(row["RankName"]));
                }
                rankID = new string[listRankID.Count];
                listRankID.CopyTo(rankID);

                rankName = new string[listRankName.Count];
                listRankName.CopyTo(rankName);
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 删除并添加。
        /// </summary>
        /// <param name="stepID">步骤ID。</param>
        /// <param name="rankID">岗位级别ID。</param>
        /// <param name="rankName">岗位级别名称。</param>
        /// <returns></returns>
        public bool DeleteAllAndInsert(string stepID, string[] rankID, string[] rankName)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(stepID))
            {
                result = this.DeleteRecord(string.Format("StepID='{0}'", stepID));

                if (rankID != null && rankName != null && (rankID.Length == rankName.Length))
                {
                    for (int i = 0; i < rankID.Length; i++)
                    {
                        string id = rankID[i];
                        if (!string.IsNullOrEmpty(id))
                        {
                            FlowStepRank flowStepRank = new FlowStepRank();
                            flowStepRank.StepID = stepID;
                            flowStepRank.RankID = id;
                            flowStepRank.RankName = rankName[i];

                            result = this.UpdateRecord(flowStepRank);
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 加载数据。
        /// </summary>
        /// <param name="stepID">步骤ID。</param>
        /// <returns></returns>
        public StepRankCollection LoadStepRankCollection(GUIDEx stepID)
        {
            StepRankCollection collection = new StepRankCollection();
            if (stepID.IsValid)
            {
                DataTable dtSource = this.GetAllRecord(string.Format("StepID='{0}'", stepID));
                List<FlowStepRank> list = this.ConvertDataSource(dtSource);
                if (list != null)
                {
                    foreach (FlowStepRank stepRank in list)
                    {
                        StepRank rank = new StepRank();
                        rank.RankID = stepRank.RankID;
                        rank.RankName = stepRank.RankName;
                        collection.Add(rank);
                    }
                }
            }
            return collection;
        }
        #endregion

        #region 重载。
        public override bool DeleteRecord(StringCollection primaryValues)
        {
            if (primaryValues != null && primaryValues.Count > 0)
            {
                string[] p = new string[primaryValues.Count];
                primaryValues.CopyTo(p, 0);

                const string sql = "delete from {0} where StepID in ('{1}')";

                return this.DatabaseAccess.ExecuteNonQuery(string.Format(sql, this.TableName, string.Join("','", p))) > 0;
                //return base.DeleteRecord(primaryValues);
            }
            return false;
        }
        #endregion
    }

}
