//================================================================================
// FileName: FlowParameterEntity.cs
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
	///FlowParameterEntity实体类。
	///</summary>
	internal class FlowParameterEntity: DbModuleEntity<FlowParameter>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public FlowParameterEntity()
		{

		}
		#endregion

        #region 数据处理。
        /// <summary>
        /// 绑定参数数据。
        /// </summary>
        /// <param name="stepID">步骤ID。</param>
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
        /// 列表数据。
        /// </summary>
        /// <param name="parameterName">参数名称。</param>
        /// <param name="processID">流程ID。</param>
        /// <param name="stepID">步骤ID。</param>
        /// <returns></returns>
        public DataTable ListDataSource(string parameterName, string processID, string stepID)
        {
            return this.DatabaseAccess.ExecuteDataset(string.Format("exec spFlowStepListView '{0}','{1}','{2}'", parameterName, processID, stepID)).Tables[0].Copy();
        }
        /// <summary>
        /// 获取步骤参数。
        /// </summary>
        /// <param name="stepID">步骤ID。</param>
        /// <returns>步骤参数集合。</returns>
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

        #region 重载。
        public override bool DeleteRecord(StringCollection primaryValues)
        {
            if (primaryValues != null && primaryValues.Count > 0)
            {
                string[] p = new string[primaryValues.Count];
                primaryValues.CopyTo(p, 0);

                const string conditionSql = "select count(*) from tblFlowCondition where ParameterID in ('{0}')";
                const string parameterMapSql = "select count(*) from tblFlowParameterMap where (ParameterID in ('{0}')) or (MapParameterID in ('{0}'))";

                if ((int)this.DatabaseAccess.ExecuteScalar(string.Format(conditionSql, string.Join("','", p))) > 0)
                    throw new Exception("有未删除的变迁规则条件！");
                if ((int)this.DatabaseAccess.ExecuteScalar(string.Format(parameterMapSql, string.Join("','", p))) > 0)
                    throw new Exception("有未删除的参数映射！");
                
                return base.DeleteRecord(primaryValues);
            }
            return false;
        }
        #endregion
    }

}
