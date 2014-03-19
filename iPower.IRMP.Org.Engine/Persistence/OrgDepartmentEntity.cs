//================================================================================
// FileName: OrgDepartmentEntity.cs
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
using iPower.Data;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.DataSource;
using iPower.Platform.Engine.Persistence;

using iPower.IRMP;
using Domain = iPower.IRMP.Org.Engine.Domain;
namespace iPower.IRMP.Org.Engine.Persistence
{
	///<summary>
	///OrgDepartmentEntityʵ���ࡣ
	///</summary>
    internal class OrgDepartmentEntity : DbModuleEntity<Domain.OrgDepartment>
	{
		#region ��Ա���������캯����
        IListControlsTreeViewData listControlsTreeViewData;
		///<summary>
		///���캯��
		///</summary>
		public OrgDepartmentEntity()
		{

		}
		#endregion

        /// <summary>
        /// ��ȡ������Ϣ���ݡ�
        /// </summary>
        public IListControlsTreeViewData Department
        {
            get
            {
                if (this.listControlsTreeViewData == null)
                    this.listControlsTreeViewData = new ListControlsTreeViewDataSource("DepartmentName", "DepartmentID", "ParentDepartmentID",
                        this.GetAllRecord(string.Format("DepartmentStatus='{0}'", (int)EnumStatus.Start), "DepartmentOrder"));
                return this.listControlsTreeViewData;
            }
        }
         /// <summary>
        /// �б����ݡ�
        /// </summary>
        /// <param name="departmentName"></param>
        /// <param name="paretDepartmentID"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string departmentName, string paretDepartmentID)
        {
            const string sql = "exec spOrgDepartmentListView '{0}','{1}'";

            string strSql = string.Format(sql, departmentName, paretDepartmentID);
            return this.DatabaseAccess.ExecuteDataset(strSql).Tables[0].Copy();
        }
        /// <summary>
        /// ɾ���������ݡ�
        /// </summary>
        /// <param name="departmentID"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool DeleteDepartment(string departmentID, out string err)
        {
            const string sql = "exec spOrgDeleteDepartment '{0}'";
            string strSQL = string.Format(sql, departmentID);
            string result = this.DatabaseAccess.ExecuteScalar(strSQL).ToString();
            string[] array = result.Split('|');
            err = array[1];
            return array[0] == "0";
        }
        /// <summary>
        /// ��ȡ���в������ݡ�
        /// </summary>
        /// <param name="departmentID">����ID��</param>
        /// <returns></returns>
        public OrgDepartmentCollection GetAllDepartment(string departmentID)
        {
            OrgDepartmentCollection collection = new OrgDepartmentCollection();
            DataTable dtSource = null;
            if (string.IsNullOrEmpty(departmentID))
                dtSource = this.GetAllRecord(string.Format("DepartmentStatus='{0}'", (int)EnumStatus.Start));
            else
            {
                dtSource = this.GetAllRecord(string.Format("DepartmentStatus='{0}' and DepartmentID = '{1}'", (int)EnumStatus.Start, departmentID));
                if (dtSource != null && dtSource.Rows.Count == 0)
                {
                    dtSource = this.GetAllRecord(string.Format("DepartmentStatus='{0}' and (DepartmentName like '%{1}%')", (int)EnumStatus.Start, departmentID));
                }
            }

            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                foreach (DataRow row in dtSource.Rows)
                {
                    OrgDepartment dept = new OrgDepartment();
                    dept.DepartmentID = Convert.ToString(row["DepartmentID"]);
                    dept.DepartmentName = Convert.ToString(row["DepartmentName"]);
                    dept.ParentDepartmentID = Convert.ToString(row["ParentDepartmentID"]);
                    dept.Order = Convert.ToInt32(row["DepartmentOrder"]);
                    collection.Add(dept);
                }
            }
            return collection;
        }
	}
}
