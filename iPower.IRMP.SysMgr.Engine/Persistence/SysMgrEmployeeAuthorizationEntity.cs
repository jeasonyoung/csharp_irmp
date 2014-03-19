//================================================================================
// FileName: SysMgrEmployeeAuthorizationEntity.cs
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
using iPower.IRMP.SysMgr.Engine.Domain;
namespace iPower.IRMP.SysMgr.Engine.Persistence
{
	///<summary>
	///SysMgrEmployeeAuthorizationEntityʵ���ࡣ
	///</summary>
	internal class SysMgrEmployeeAuthorizationEntity: DbModuleEntity<SysMgrEmployeeAuthorization>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public SysMgrEmployeeAuthorizationEntity()
		{

		}
		#endregion

        #region ���ݲ�����
        /// <summary>
        /// �б�����Դ��
        /// </summary>
        /// <returns></returns>
        public DataTable ListDataSource(string employeeName)
        {
            const string sql = "exec spSysMgrEmployeeAuthorizationListView '{0}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, employeeName)).Tables[0].Copy();
        }
        /// <summary>
        /// ��֤�û���Ȩ��
        /// </summary>
        /// <param name="employeeID">�û�ID��</param>
        /// <param name="systemID">ϵͳID��</param>
        /// <param name="clientIP">�û���¼IP��ַ��</param>
        /// <param name="err">�쳣������Ϣ��</param>
        /// <returns>�����Ȩ����true,���򷵻�false��</returns>
        public bool UserAuthorizationVerification(GUIDEx employeeID, GUIDEx systemID, string clientIP, out string err)
        {
            err = null;
            const string sql = "exec spSysMgrEmployeeAuthentication '{0}','{1}','{2}'";
            bool result = false;
            if (!employeeID.IsValid)
                err = "�û�ID����Ϊ�գ�";
            else if (!systemID.IsValid)
                err = "ϵͳID����Ϊ�գ�";
            else if (string.IsNullOrEmpty(clientIP))
                err = "�û���¼IP����Ϊ�գ�";
            else
            {
                object obj = this.DatabaseAccess.ExecuteScalar(string.Format(sql, employeeID, systemID, clientIP));
                if (obj != null)
                {
                    string[] strResult = obj.ToString().Split('|');
                    if (strResult != null)
                    {
                        err = strResult[1];
                        result = strResult[0] == "0";
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// ��ȡ�û�������
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public string GetEmployeeName(GUIDEx employeeID)
        {
            return this.DatabaseAccess.ExecuteScalar(string.Format("select top 1 EmployeeName from tblSysMgrEmployeeAuthorization where EmployeeID='{0}'", employeeID)).ToString();
        }
        /// <summary>
        /// ��ȡȫ����Ȩ�û���
        /// </summary>
        /// <param name="employeeName"></param>
        /// <returns></returns>
        public DataTable GetAllAuthorizationEmployee(string employeeName)
        {
            const string sql = "exec spSysMgrAuthEmployee '{0}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, employeeName.Trim())).Tables[0].Copy();
        }
        /// <summary>
        /// ��ȡ��Ȩ�û�ϵͳ��
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="isAuthorization"></param>
        /// <returns></returns>
        public IListControlsData GetAuthorizationEmployeeApp(GUIDEx employeeID, bool isAuthorization)
        {
            const string sql = "exec spSysMgrEmployeeAuthorizationApp '{0}',{1}";
            DataTable dtSouce = this.DatabaseAccess.ExecuteDataset(string.Format(sql, employeeID, isAuthorization ? 1 : 0)).Tables[0].Copy();
            return new ListControlsDataSource("SystemName", "AppAuthID", dtSouce);
        }
        /// <summary>
        /// ����Ȩ�û���
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public IListControlsData BindAuthorizationEmployees(params string[] employeeID)
        {
            const string sql = "select distinct EmployeeID,EmployeeName from {0}  where (EmployeeID in ('{1}'))";
            DataTable dtSource = this.DatabaseAccess.ExecuteDataset(string.Format(sql, this.TableName, string.Join("','", employeeID))).Tables[0];
            return new ListControlsDataSource("EmployeeName", "EmployeeID", dtSource);
        }
        /// <summary>
        /// ɾ����Ȩ�û���
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool DeleteAuthorizationEmployee(GUIDEx employeeID, out string err)
        {
            const string sql = "exec spSysMgrDeleteEmployeeAuthorization '{0}'";
            err = null;
            if (employeeID.IsValid)
            {
                string result = this.DatabaseAccess.ExecuteScalar(string.Format(sql, employeeID)).ToString();
                string[] array = result.Split('|');
                err = array[1];
                return array[0] == "0";
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="keepApp"></param>
        /// <returns></returns>
        public bool DeleteEmployeeAuthApp(GUIDEx employeeID,string[] keepApp)
        {
            if (!employeeID.IsValid && keepApp == null && keepApp.Length == 0)
                return false;
            return this.DeleteRecord(string.Format("EmployeeID = '{0}' and (AppAuthID not in ('{1}'))", employeeID, string.Join("','", keepApp)));
        }
        #endregion
    }

}
