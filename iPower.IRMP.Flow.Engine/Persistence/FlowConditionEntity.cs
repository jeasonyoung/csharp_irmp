//================================================================================
// FileName: FlowConditionEntity.cs
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
	///FlowConditionEntity实体类。
	///</summary>
	internal class FlowConditionEntity: DbModuleEntity<FlowCondition>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public FlowConditionEntity()
		{

		}
		#endregion

        #region 数据处理。
        /// <summary>
        /// 列表数据。
        /// </summary>
        /// <param name="transitionID">变迁ID。</param>
        /// <returns></returns>
        public DataTable ListDataSource(GUIDEx transitionID)
        {
            const string sql = @"select a.ConditionID,a.ParameterID, b.ParameterName,a.ConditionValue, a.CompareValue
                                from tblFlowCondition a
                                inner join tblFlowParameter b
                                on b.ParameterID = a.ParameterID
                                where TransitionID = '{0}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, transitionID)).Tables[0].Copy();
        }

        /// <summary>
        /// 加载数据。
        /// </summary>
        /// <param name="transitionID">变迁ID。</param>
        /// <returns></returns>
        public ConditionCollection LoadConditionCollection(GUIDEx transitionID)
        {
            ConditionCollection collection = new ConditionCollection();
            if (transitionID.IsValid)
            {
                DataTable dtSource = this.GetAllRecord(string.Format("TransitionID='{0}'", transitionID));
                List<FlowCondition> list = this.ConvertDataSource(dtSource);
                if (list != null)
                {
                    foreach (FlowCondition fc in list)
                    {
                        Condition c = new Condition();
                        c.ConditionID = fc.ConditionID;
                        c.ParameterID = fc.ParameterID;
                        c.ConditionValue = (EnumCompareSign)fc.ConditionValue;
                        c.CompareValue = fc.CompareValue;

                        collection.Add(c);
                    }
                }
            }
            return collection;
        }
        #endregion

        #region 重载。
        public override bool DeleteRecord(StringCollection primaryValues)
        {
            const string sql = "delete from {0} where TransitionID in ('{1}')";
            if (primaryValues != null && primaryValues.Count > 0)
            {
                string[] p = new string[primaryValues.Count];
                primaryValues.CopyTo(p, 0);

                return this.DatabaseAccess.ExecuteNonQuery( string.Format(sql, this.TableName, string.Join("','", p))) > 0;
                //return base.DeleteRecord(primaryValues);
            }
            return false;
        }
        #endregion
    }

}
