//================================================================================
// FileName: FlowTransitionEntity.cs
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
	///FlowTransitionEntityʵ���ࡣ
	///</summary>
	internal class FlowTransitionEntity: DbModuleEntity<FlowTransition>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public FlowTransitionEntity()
		{

		}
		#endregion

        #region ���ݴ���
        /// <summary>
        /// �б����ݡ�
        /// </summary>
        /// <param name="stepName">�������ơ�</param>
        /// <param name="processID">����ID��</param>
        /// <returns></returns>
        public DataTable ListDataSource(string stepName, GUIDEx processID)
        {
            const string sql = "exec spFlowTransitionListView '{0}','{1}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, stepName, processID)).Tables[0].Copy();
        }
        /// <summary>
        /// �󶨱�Ǩ����
        /// </summary>
        /// <returns></returns>
        public IListControlsData BindTransition()
        {
            const string sql = @"select a.TransitionID,
                                b.StepName +'->'+ c.StepName as TransitionName
                                from tblFlowTransition a
                                inner join tblFlowStep b
                                on b.StepID = a.FromStepID
                                inner join tblFlowStep c
                                on c.StepID = a.ToStepID
                                order by b.OrderNo";

            return new ListControlsDataSource("TransitionName", "TransitionID", this.DatabaseAccess.ExecuteDataset(sql));
        }
        /// <summary>
        /// �󶨱�Ǩ����
        /// </summary>
        /// <returns></returns>
        public IListControlsData BindTransition(GUIDEx processID)
        {
            const string sql = @"select a.TransitionID,
                                b.StepName +'->'+ c.StepName as TransitionName
                                from tblFlowTransition a
                                inner join tblFlowStep b
                                on b.StepID = a.FromStepID
                                inner join tblFlowStep c
                                on c.StepID = a.ToStepID
                                where a.ProcessID = '{0}'
                                order by b.OrderNo";
            string strSql = string.Format(sql, processID);

            return new ListControlsDataSource("TransitionName", "TransitionID", this.DatabaseAccess.ExecuteDataset(strSql));
        }
        /// <summary>
        /// ��ǰ��������
        /// </summary>
        /// <param name="transitionID">��ǨID��</param>
        /// <returns></returns>
        public IListControlsData BindFromStepParameterData(GUIDEx transitionID)
        {
            const string sql = @"select ParameterID,ParameterName
                                 from tblFlowParameter 
                                 where StepID in (select FromStepID from tblFlowTransition where TransitionID = '{0}')";

            return new ListControlsDataSource("ParameterName", "ParameterID", this.DatabaseAccess.ExecuteDataset(string.Format(sql, transitionID)));
        }

        /// <summary>
        /// �󶨺���������
        /// </summary>
        /// <param name="transitionID">��ǨID��</param>
        /// <returns></returns>
        public IListControlsData BindToStepParameterData(GUIDEx transitionID)
        {
            const string sql = @"select ParameterID,ParameterName
                                 from tblFlowParameter 
                                 where StepID in (select ToStepID from tblFlowTransition where TransitionID = '{0}')";

            return new ListControlsDataSource("ParameterName", "ParameterID", this.DatabaseAccess.ExecuteDataset(string.Format(sql, transitionID)));
        }

        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="processID">����ID��</param>
        /// <returns></returns>
        public TransitionCollection LoadTransitionCollection(GUIDEx processID)
        {
            TransitionCollection transitionCollection = new TransitionCollection();
            if (processID.IsValid)
            {
                DataTable dtSource = this.GetAllRecord(string.Format("ProcessID='{0}'", processID));
                List<FlowTransition> list = this.ConvertDataSource(dtSource);
                if (list != null)
                {
                    FlowParameterMapEntity flowParameterMapEntity = new FlowParameterMapEntity();
                    FlowConditionEntity flowConditionEntity = new FlowConditionEntity();
                    foreach (FlowTransition ft in list)
                    {
                        Transition t = new Transition();
                        t.TransitionID = ft.TransitionID;
                        t.FromStepID = ft.FromStepID;
                        t.ToStepID = ft.ToStepID;
                        t.TransitionRule = (EnumTransitionRule)ft.TransitionRule;

                        t.Maps = flowParameterMapEntity.LoadParameterMapCollection(t.TransitionID);

                        t.Conditions = flowConditionEntity.LoadConditionCollection(t.TransitionID);

                        transitionCollection.Add(t);
                    }
                }
            }
            return transitionCollection;
        }
        #endregion

        #region ���ء�
        public override bool DeleteRecord(StringCollection primaryValues)
        {
            if (primaryValues != null && primaryValues.Count > 0)
            {
                const string sql = "select count(*) from tblFlowParameterMap where TransitionID in ('{0}')";

                string[] p = new string[primaryValues.Count];
                primaryValues.CopyTo(p, 0);

                if ((int)this.DatabaseAccess.ExecuteScalar(string.Format(sql, string.Join("','", p))) > 0)
                    throw new Exception("��δɾ���Ĳ���ӳ�䣡");

                new FlowConditionEntity().DeleteRecord(primaryValues);
                return base.DeleteRecord(primaryValues);
            }
            return false;
        }
        #endregion
    }

}
