//================================================================================
// FileName: SecurityRightEntity.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Data;
	
using iPower;
using iPower.Data;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Security;
using iPower.Platform.Engine.DataSource;
using iPower.Platform.Engine.Persistence;
using iPower.IRMP.Org;
using iPower.IRMP.Security;
using iPower.IRMP.Security.Engine.Domain;
namespace iPower.IRMP.Security.Engine.Persistence
{
	///<summary>
	///SecurityRightEntityʵ���ࡣ
	///</summary>
    internal class SecurityRightEntity : DbModuleEntity<SecurityRight>
    {
        #region ��Ա���������캯����
        ///<summary>
        ///���캯��
        ///</summary>
        public SecurityRightEntity()
        {

        }
        #endregion

        /// <summary>
        /// �б����ݡ�
        /// </summary>
        /// <param name="systemID"></param>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string systemID, string moduleName)
        {
            const string sql = "exec spSecurityRightListView '{0}','{1}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, systemID, moduleName)).Tables[0].Copy();
        }
        /// <summary>
        /// ɾ�����ݡ�
        /// </summary>
        /// <param name="rightID"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool DeleteRight(string rightID, out string err)
        {
            err = null;
            bool result = false;
            const string query = "select count(*) from tblSecurityRoleRight where RightID='{0}'";
            int r = (int)this.DatabaseAccess.ExecuteScalar(string.Format(query, rightID));
            if (r > 0)
                err = "��Ȩ���Ѿ��ڽ�ɫȨ����ʹ�ã����Ƚ���ɾ����";
            else
                result = this.DeleteRecord(new string[] { rightID });

            return result;
        }
        /// <summary>
        ///���������ݡ�
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool UpdateRecord(SecurityRight entity, out string err)
        {
            err = null;
            bool result = false;
            const string query = "select count(*) from {0} where ModuleID='{1}' and ActionID='{2}'";
            if (entity != null)
            {
                int r = (int)this.DatabaseAccess.ExecuteScalar(string.Format(query, this.TableName, entity.ModuleID, entity.ActionID));
                if (r > 0)
                    err = "��ģ��ı�Ԫ�����Ѿ����ڣ�";
                else
                    result = this.UpdateRecord(entity);
            }
            return result;
        }

        #region ģ��Ȩ�޴���
        static Hashtable CacheModulePermissions = Hashtable.Synchronized(new Hashtable());
        /// <summary>
        /// ��ȡȨ�ޡ�
        /// </summary>
        /// <param name="systemID">ϵͳID��</param>
        /// <param name="moduleID">ģ��ID��</param>
        /// <param name="employeeID">�û�ID��</param>
        /// <returns>ģ��Ȩ�޼��ϡ�</returns>
        public SecurityPermissionCollection GetModulePermissions(GUIDEx systemID, GUIDEx moduleID, GUIDEx employeeID)
        {
            const string sql = "exec spSecurityModulePermissions '{0}','{1}','{2}','{3}','{4}','{5}'";
            SecurityPermissionCollection collection = new SecurityPermissionCollection();
            if (systemID.IsValid && moduleID.IsValid && employeeID.IsValid)
            {
                string key = string.Format("GMP_EMPLOYEEID_{0}", employeeID);
                string[] emps = CacheModulePermissions[key] as string[];
                #region �û���Ϣ��
                if (emps == null)
                {
                    emps = new string[3];
                    lock (this)
                    {
                        IOrgFactory facotry = this.ModuleConfig.OrgFactory;
                        if (facotry != null)
                        {
                            string rankID = string.Empty;
                            OrgEmployeeCollection employees = facotry.GetAllEmployee(employeeID);
                            if (employees != null && employees.Count > 0)
                            {
                                OrgPostCollection posts = facotry.GetAllPost(employees[0].PostID);
                                if (posts != null && posts.Count > 0 && !string.IsNullOrEmpty(posts[0].RankID))
                                {
                                    rankID = posts[0].RankID;
                                }

                                emps = new string[] { employees[0].DepartmentID, rankID, employees[0].PostID };
                                CacheModulePermissions[key] = emps;
                            }
                        }
                    }
                }
                #endregion

                string str_sql = string.Format(sql, systemID, moduleID, employeeID, emps[0], emps[1], emps[2]);
                DataTable dtSource = this.DatabaseAccess.ExecuteDataset(str_sql).Tables[0];
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    collection.InitAssignment(dtSource);
                }
            }
            return collection;
        }
        #endregion
    }
}