//================================================================================
// FileName: FlowStepEntity.cs
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
	///FlowStepEntityʵ���ࡣ
	///</summary>
	internal class FlowStepEntity: DbModuleEntity<FlowStep>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public FlowStepEntity()
		{

		}
		#endregion

        #region ���ݴ���
        /// <summary>
        /// �б����ݡ�
        /// </summary>
        /// <param name="processID">����ID��</param>
        /// <param name="stepName">�������ơ�</param>
        /// <returns></returns>
        public DataTable ListDataSource(string processID, string stepName)
        {
            StringBuilder filter = new StringBuilder();
            if (!string.IsNullOrEmpty(processID))
                 filter.AppendFormat(" ProcessID = '{0}' ", processID);
             if (!string.IsNullOrEmpty(stepName))
            {
                if (filter.Length > 0)
                    filter.Append(" and ");
                filter.AppendFormat("((StepName like '%{0}%') or (StepSign like '%{0}%'))  ", stepName);
            }
            FlowProcessEntity flowProcessEntity = new FlowProcessEntity();
            DataTable dtSource = this.GetAllRecord(filter.ToString(), "StepType asc, OrderNo asc");
            dtSource.Columns.Add("ProcessName");
            foreach (DataRow row in dtSource.Rows)
            {
                row["ProcessName"] = flowProcessEntity.FindProcessName(Convert.ToString(row["ProcessID"]));
            }
            return dtSource.Copy();
        }
        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="stepSigns"></param>
        /// <returns></returns>
        public List<FlowStep> FindFlowStep(GUIDEx processID, string[] stepSigns)
        {
            List<FlowStep> list = new List<FlowStep>();
            DataTable dtSource = null;
            if (stepSigns != null && stepSigns.Length > 0)
                dtSource = this.GetAllRecord(string.Format("ProcessID='{0}' and (StepSign in ('{1}'))", processID, string.Join("','", stepSigns)), "OrderNo asc");
            else
                dtSource = this.GetAllRecord(string.Format("ProcessID='{0}'", processID), "OrderNo asc");
            if (dtSource != null)
            {
                foreach (DataRow row in dtSource.Rows)
                {
                    list.Add(this.Assignment(row));
                }
            }
            return list;
        }
        /// <summary>
        ///  �󶨲������ݡ�
        /// </summary>
        /// <param name="processID">����ID��</param>
        /// <returns></returns>
        public IListControlsData BindStep(string processID)
        {
            DataTable dtSource = null;
            if (!string.IsNullOrEmpty(processID))
                dtSource = this.GetAllRecord(string.Format("ProcessID='{0}'", processID), "OrderNo asc");
            else
                dtSource = this.GetAllRecord("", "OrderNo asc");

            return new ListControlsDataSource("StepName", "StepID", dtSource);
        }
        /// <summary>
        /// �󶨲������ݡ�
        /// </summary>
        /// <param name="processID">����ID��</param>
        /// <param name="ingoreStepID">�ų��Ĳ���ID��</param>
        /// <returns></returns>
        public IListControlsData BindStep(GUIDEx processID,GUIDEx ingoreStepID)
        {
            StringBuilder filter = new StringBuilder();
            if (processID.IsValid)
                filter.AppendFormat(" (ProcessID = '{0}') ", processID);
            if (ingoreStepID.IsValid)
            {
                if (filter.Length > 0)
                    filter.Append(" and ");
                filter.AppendFormat(" (StepID <> '{0}') ", ingoreStepID);
            }

            return new ListControlsDataSource("StepName", "StepID", this.GetAllRecord(filter.ToString(), "OrderNo asc"));
        }
        /// <summary>
        /// ���ݲ���ID��ȡ����ID��
        /// </summary>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public GUIDEx FindProcessID(string stepID)
        {
            return new GUIDEx(this.DatabaseAccess.ExecuteScalar(string.Format("select ProcessID from {0} where StepID='{1}'", this.TableName, stepID)));
        }
        /// <summary>
        /// ��ȡ���輯�ϡ�
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        public StepCollection LoadStepCollection(GUIDEx processID)
        {
            StepCollection stepCollection = new StepCollection();
            if (processID.IsValid)
            {
                DataTable dtSource = this.GetAllRecord(string.Format("ProcessID='{0}'", processID), "StepType asc, OrderNo asc");
                if (dtSource != null)
                {
                    List<FlowStep> list = this.ConvertDataSource(dtSource);
                    if (list != null && list.Count > 0)
                    {
                        FlowParameterEntity flowParameterEntity = new FlowParameterEntity();
                        FlowStepAuthorizeEntity flowStepAuthorizeEntity = new FlowStepAuthorizeEntity();
                        FlowStepRoleEntity flowStepRoleEntity = new FlowStepRoleEntity();
                        FlowStepRankEntity flowStepRankEntity = new FlowStepRankEntity();
                        FlowStepPostEntity flowStepPostEntity = new FlowStepPostEntity();
                        FlowStepEmployeeEntity flowStepEmployeeEntity = new FlowStepEmployeeEntity();

                        foreach (FlowStep flowStep in list)
                        {
                            Step s = new Step();
                            s.StepID = flowStep.StepID;
                            s.StepName = flowStep.StepName;
                            s.StepSign = flowStep.StepSign;
                            s.StepType = (EnumStepType)flowStep.StepType;
                            s.EntryAction = flowStep.EntryAction;
                            s.EntryQuery = flowStep.EntryQuery;
                            s.StepDuration = flowStep.StepDuration;
                            s.StepDescription = flowStep.StepDescription;
                            s.StepMode = (EnumStepMode)flowStep.StepMode;
                            s.StepWarning = (EnumStepWarning)flowStep.StepWarning;
                            s.OrderNo = flowStep.OrderNo;

                            s.Parameters = flowParameterEntity.LoadParameterCollection(s.StepID);
                            s.StepAuthorizes = flowStepAuthorizeEntity.LoadStepAuthorizeCollection(s.StepID);
                            s.StepRoles = flowStepRoleEntity.LoadStepRoleCollection(s.StepID);
                            s.StepRanks = flowStepRankEntity.LoadStepRankCollection(s.StepID);
                            s.StepPosts = flowStepPostEntity.LoadStepPostCollection(s.StepID);
                            s.StepEmployees = flowStepEmployeeEntity.LoadStepEmployeeCollection(s.StepID);

                            stepCollection.Add(s);
                        }
                    }
                }
            }
            return stepCollection;
        }
        #endregion

        #region ���ء�
        /// <summary>
        /// ����ɾ�����ݡ�
        /// </summary>
        /// <param name="primaryValues"></param>
        /// <returns></returns>
        public override bool DeleteRecord(StringCollection primaryValues)
        {
            if (primaryValues != null && primaryValues.Count > 0)
            {
                string[] p = new string[primaryValues.Count];
                primaryValues.CopyTo(p, 0);

                const string parameterSql = "select count(*) from tblFlowParameter where StepID in ('{0}')";
                const string transitionSql = "select count(*) from tblFlowTransition where (FromStepID in ('{0}')) or (ToStepID in ('{0}'))";
                const string authorizeSql = "select count(*) from tblFlowStepAuthorize where StepID in ('{0}')";

                if ((int)this.DatabaseAccess.ExecuteScalar(string.Format(parameterSql, string.Join("','", p))) > 0)
                    throw new Exception("��δɾ���Ĳ�����");

                if ((int)this.DatabaseAccess.ExecuteScalar(string.Format(transitionSql, string.Join("','", p))) > 0)
                    throw new Exception("��δɾ���ı�Ǩ����");

                if ((int)this.DatabaseAccess.ExecuteScalar(string.Format(authorizeSql, string.Join("','", p))) > 0)
                    throw new Exception("��δɾ������Ȩ��");

                //Employee
                new FlowStepEmployeeEntity().DeleteRecord(primaryValues);
                //Role
                new FlowStepRoleEntity().DeleteRecord(primaryValues);
                //Rank
                new FlowStepRankEntity().DeleteRecord(primaryValues);
                //Post
                new FlowStepPostEntity().DeleteRecord(primaryValues);

                return base.DeleteRecord(primaryValues);
            }
            return false;
        }
        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool UpdateRecord(FlowStep entity)
        {
            if (entity != null)
            {
                const string stepTypeSql = "select count(*) from {0} where ProcessID='{1}' and StepType={2}";
                const string strSql = "select count(*) from {0} where ProcessID='{1}' and StepID='{2}'";
                EnumStepType stepType = (EnumStepType)Enum.Parse(typeof(EnumStepType), entity.StepType.ToString());
                if (stepType != EnumStepType.Middle)
                {
                    if ((int)this.DatabaseAccess.ExecuteScalar(string.Format(strSql, this.TableName, entity.ProcessID, entity.StepID)) == 0)
                    {
                        if ((int)this.DatabaseAccess.ExecuteScalar(string.Format(stepTypeSql, this.TableName, entity.ProcessID, (int)stepType)) > 0)
                            throw new Exception(string.Format("��ͬһ��������ֻ����һ��{0}��", stepType == EnumStepType.First ? "��ʼ����" : "��������"));
                    }
                }
            }
            return base.UpdateRecord(entity);
        }
        #endregion
    }

}
