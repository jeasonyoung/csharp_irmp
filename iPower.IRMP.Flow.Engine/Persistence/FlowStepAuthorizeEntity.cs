//================================================================================
// FileName: FlowStepAuthorizeEntity.cs
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
	///FlowStepAuthorizeEntityʵ���ࡣ
	///</summary>
	internal class FlowStepAuthorizeEntity: DbModuleEntity<FlowStepAuthorize>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public FlowStepAuthorizeEntity()
		{

		}
		#endregion

        #region ���ݲ�����
        /// <summary>
        /// �б����ݡ�
        /// </summary>
        /// <param name="processID">����ID��</param>
        /// <param name="stepName">�������ơ�</param>
        /// <param name="employeeID">�û�ID��</param>
        /// <param name="validDate">��Ȩ���ڡ�</param>
        /// <returns></returns>
        public DataTable ListDataSource(string processID, string stepName, string employeeID, string validDate)
        {
            const string sql = "exec spFlowStepAuthorizeListView '{0}','{1}','{2}','{3}'";

            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, processID, stepName, employeeID, validDate)).Tables[0].Copy();
        }
        /// <summary>
        /// ���ز�����Ȩ��
        /// </summary>
        /// <param name="stepID">����ID��</param>
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
        /// ɾ�����ݡ�
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public bool DeleteFlowStepAuthorize(GUIDEx employeeID, GUIDEx stepID)
        {
            return this.DeleteRecord(string.Format("StepID='{0}' and EmployeeID = '{1}'", stepID, employeeID));
        }
        /// <summary>
        /// ɾ�����ݡ�
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
