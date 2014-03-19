//================================================================================
// FileName: FlowConditionEntity.cs
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
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.DataSource;
using iPower.Platform.Engine.Persistence;
using iPower.IRMP.Flow.Engine.Domain;
namespace iPower.IRMP.Flow.Engine.Persistence
{
	///<summary>
	///FlowConditionEntityʵ���ࡣ
	///</summary>
	internal class FlowConditionEntity: DbModuleEntity<FlowCondition>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public FlowConditionEntity()
		{

		}
		#endregion

        #region ���ݴ���
        /// <summary>
        /// �б����ݡ�
        /// </summary>
        /// <param name="transitionID">��ǨID��</param>
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
        /// �������ݡ�
        /// </summary>
        /// <param name="transitionID">��ǨID��</param>
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

        #region ���ء�
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
