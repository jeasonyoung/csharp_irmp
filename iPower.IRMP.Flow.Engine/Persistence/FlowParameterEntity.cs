//================================================================================
// FileName: FlowParameterEntity.cs
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
	///FlowParameterEntityʵ���ࡣ
	///</summary>
	internal class FlowParameterEntity: DbModuleEntity<FlowParameter>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public FlowParameterEntity()
		{

		}
		#endregion

        #region ���ݴ���
        /// <summary>
        /// �󶨲������ݡ�
        /// </summary>
        /// <param name="stepID">����ID��</param>
        /// <returns></returns>
        public IListControlsData BindParameter(GUIDEx stepID)
        {
            StringBuilder filter = new StringBuilder();
            if (stepID.IsValid)
            {
                filter.AppendFormat(" StepID='{0}'", stepID);
            }

            return new ListControlsDataSource("ParameterName", "ParameterID", this.GetAllRecord(filter.ToString()));
        }
        /// <summary>
        /// �б����ݡ�
        /// </summary>
        /// <param name="parameterName">�������ơ�</param>
        /// <param name="processID">����ID��</param>
        /// <param name="stepID">����ID��</param>
        /// <returns></returns>
        public DataTable ListDataSource(string parameterName, string processID, string stepID)
        {
            return this.DatabaseAccess.ExecuteDataset(string.Format("exec spFlowStepListView '{0}','{1}','{2}'", parameterName, processID, stepID)).Tables[0].Copy();
        }
        /// <summary>
        /// ��ȡ���������
        /// </summary>
        /// <param name="stepID">����ID��</param>
        /// <returns>����������ϡ�</returns>
        public ParameterCollection LoadParameterCollection(GUIDEx stepID)
        {
            ParameterCollection collection = new ParameterCollection();
            if (stepID.IsValid)
            {
                DataTable dtSource = this.GetAllRecord(string.Format("StepID='{0}'", stepID));
                if (dtSource != null)
                {
                    List<FlowParameter> list = this.ConvertDataSource(dtSource);
                    if (list != null)
                    {
                        foreach (FlowParameter fp in list)
                        {
                            Parameter p = new Parameter();

                            p.ParameterID = fp.ParameterID;
                            p.ParameterName = fp.ParameterName;
                            p.ParameterType = (EnumParameterType)fp.ParameterType;
                            p.ParameterDescription = fp.ParameterDescription;
                            p.DefaultValue = fp.DefaultValue;

                            collection.Add(p);
                        }
                    }
                }
            }
            return collection;
        }
        #endregion

        #region ���ء�
        public override bool DeleteRecord(StringCollection primaryValues)
        {
            if (primaryValues != null && primaryValues.Count > 0)
            {
                string[] p = new string[primaryValues.Count];
                primaryValues.CopyTo(p, 0);

                const string conditionSql = "select count(*) from tblFlowCondition where ParameterID in ('{0}')";
                const string parameterMapSql = "select count(*) from tblFlowParameterMap where (ParameterID in ('{0}')) or (MapParameterID in ('{0}'))";

                if ((int)this.DatabaseAccess.ExecuteScalar(string.Format(conditionSql, string.Join("','", p))) > 0)
                    throw new Exception("��δɾ���ı�Ǩ����������");
                if ((int)this.DatabaseAccess.ExecuteScalar(string.Format(parameterMapSql, string.Join("','", p))) > 0)
                    throw new Exception("��δɾ���Ĳ���ӳ�䣡");
                
                return base.DeleteRecord(primaryValues);
            }
            return false;
        }
        #endregion
    }

}
