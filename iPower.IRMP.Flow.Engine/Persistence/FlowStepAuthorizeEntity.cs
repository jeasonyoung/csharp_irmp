//================================================================================
// FileName: FlowStepAuthorizeEntity.cs
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
	///FlowStepAuthorizeEntity实体类。
	///</summary>
	internal class FlowStepAuthorizeEntity: DbModuleEntity<FlowStepAuthorize>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public FlowStepAuthorizeEntity()
		{

		}
		#endregion

        #region 数据操作。
        /// <summary>
        /// 列表数据。
        /// </summary>
        /// <param name="processID">流程ID。</param>
        /// <param name="stepName">步骤名称。</param>
        /// <param name="employeeID">用户ID。</param>
        /// <param name="validDate">授权日期。</param>
        /// <returns></returns>
        public DataTable ListDataSource(string processID, string stepName, string employeeID, string validDate)
        {
            const string sql = "exec spFlowStepAuthorizeListView '{0}','{1}','{2}','{3}'";

            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, processID, stepName, employeeID, validDate)).Tables[0].Copy();
        }
        /// <summary>
        /// 加载步骤授权。
        /// </summary>
        /// <param name="stepID">步骤ID。</param>
        /// <returns></returns>
        public StepAuthorizeCollection LoadStepAuthorizeCollection(GUIDEx stepID)
        {
            StepAuthorizeCollection collection = new StepAuthorizeCollection();
            if (stepID.IsValid)
            {
                DataTable dtSource = this.GetAllRecord(string.Format("StepID='{0}'", stepID));
                List<FlowStepAuthorize> list = this.ConvertDataSource(dtSource);
                if (list != null)
                {
                    foreach (FlowStepAuthorize authorize in list)
                    {
                        StepAuthorize stepAuthorize = new StepAuthorize();

                        stepAuthorize.AuthorizeID = authorize.AuthorizeID;
                        stepAuthorize.EmployeeID = authorize.EmployeeID;
                        stepAuthorize.EmployeeName = authorize.EmployeeName;
                        stepAuthorize.TargetEmployeeID = authorize.TargetEmployeeID;
                        stepAuthorize.TargetEmployeeName = authorize.TargetEmployeeName;
                        stepAuthorize.BeginDate = authorize.BeginDate;
                        stepAuthorize.EndDate = authorize.EndDate;

                        collection.Add(stepAuthorize);
                    }
                }
            }
            return collection;
        }
        /// <summary>
        /// 删除数据。
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public bool DeleteFlowStepAuthorize(GUIDEx employeeID, GUIDEx stepID)
        {
            return this.DeleteRecord(string.Format("StepID='{0}' and EmployeeID = '{1}'", stepID, employeeID));
        }
        /// <summary>
        /// 删除数据。
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteRecord(List<FlowStepAuthorize> list)
        {
            if (list != null && list.Count > 0)
            {
                const string sql = "delete from {0} where AuthorizeID in ('{1}')";
                List<string> listAuthorize = new List<string>();
                foreach (FlowStepAuthorize fsa in list)
                {
                    listAuthorize.Add(fsa.AuthorizeID);
                }

                string[] strAuthorizes = new string[listAuthorize.Count];
                listAuthorize.CopyTo(strAuthorizes);
                string strSQL = string.Format(sql, this.TableName, string.Join("','", strAuthorizes));

                return this.DatabaseAccess.ExecuteNonQuery(strSQL) > 0;
            }
            return false;
        }
        #endregion
    }

}
