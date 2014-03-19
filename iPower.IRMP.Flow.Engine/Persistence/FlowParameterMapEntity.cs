//================================================================================
// FileName: FlowParameterMapEntity.cs
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
	///FlowParameterMapEntityʵ���ࡣ
	///</summary>
	internal class FlowParameterMapEntity: DbModuleEntity<FlowParameterMap>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public FlowParameterMapEntity()
		{

		}
		#endregion

        #region ���ݴ���
        /// <summary>
        /// �б����ݡ�
        /// </summary>
        /// <param name="processID">����ID��</param>
        /// <param name="transitionID">��ǨID��</param>
        /// <param name="parameterName">�������ơ�</param>
        /// <returns></returns>
        public DataTable ListDataSource(GUIDEx processID, GUIDEx transitionID, string parameterName)
        {
            const string sql = "exec spFlowParameterMapListView '{0}','{1}','{2}'";

            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, processID, transitionID, parameterName)).Tables[0].Copy();
        }

        /// <summary>
        ///  �������ݡ�
        /// </summary>
        /// <param name="transitionID">��Ǩ����ID��</param>
        /// <returns></returns>
        public ParameterMapCollection LoadParameterMapCollection(GUIDEx transitionID)
        {
            ParameterMapCollection collection = new ParameterMapCollection();
            if (transitionID.IsValid)
            {
                DataTable dtSource = this.GetAllRecord(string.Format("TransitionID='{0}'", transitionID));
                List<FlowParameterMap> list = this.ConvertDataSource(dtSource);
                if (list != null)
                {
                    foreach (FlowParameterMap fpm in list)
                    {
                        ParameterMap pm = new ParameterMap();
                        pm.ParameterID = fpm.ParameterID;
                        pm.MapParameterID = fpm.MapParameterID;
                        pm.MapMode = (EnumMapMode)fpm.MapMode;
                        pm.AssemblyName = fpm.AssemblyName;
                        pm.ClassName = fpm.ClassName;
                        pm.EntryName = fpm.EntryName;

                        collection.Add(pm);
                    }
                }
            }
            return collection;
        }
        #endregion

        #region ���ء�
        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryValues"></param>
        /// <returns></returns>
        public override bool DeleteRecord(StringCollection primaryValues)
        {
            if (primaryValues != null && primaryValues.Count > 0)
            {
                string[] pri = new string[primaryValues.Count];
                primaryValues.CopyTo(pri, 0);

                string sql = string.Format("delete from {0} where (TransitionID + '_' + ParameterID + '_' + MapParameterID) in ('{1}')",
                                                            this.TableName, string.Join("','", pri));

                return this.DatabaseAccess.ExecuteNonQuery(sql) > 0;
            }
            return false;
        }
        #endregion
    }

}
