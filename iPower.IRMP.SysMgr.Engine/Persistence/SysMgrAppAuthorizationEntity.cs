//================================================================================
// FileName: SysMgrAppAuthorizationEntity.cs
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
	///SysMgrAppAuthorizationEntityʵ���ࡣ
	///</summary>
	internal class SysMgrAppAuthorizationEntity: DbModuleEntity<SysMgrAppAuthorization>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public SysMgrAppAuthorizationEntity()
		{

		}
		#endregion

        /// <summary>
        /// ��ȡ�б�����Դ��
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string appName)
        {
            return this.GetAllRecord(string.Format("SystemName like '%{0}%'", appName), "SystemID");
        }

        /// <summary>
        /// ��֤ϵͳ��Ȩ��
        /// </summary>
        /// <param name="systemID">ϵͳID��</param>
        /// <param name="authPassword">��Ȩ���롣</param>
        /// <param name="err">�쳣������Ϣ��</param>
        /// <returns>�����Ȩ����true,���򷵻�false��</returns>
        public bool AppAuthorization(GUIDEx systemID, string authPassword, out string err)
        {
            err = null;
            bool result = false;
            if (!systemID.IsValid)
                err = "ϵͳID����Ϊ�գ�";
            else if (string.IsNullOrEmpty(authPassword))
                err = "��Ȩ���벻��Ϊ�գ�";
            else
            {
                const string sql = "exec spSysMgrAppAuthorizedToVerify '{0}','{1}'";
                object obj = this.DatabaseAccess.ExecuteScalar(string.Format(sql, systemID, authPassword));
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
        /// ɾ�����ݡ�
        /// </summary>
        /// <param name="appAuthID"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool DeleteRecord(GUIDEx appAuthID, out string err)
        {
            const string sql = "exec spSysMgrDeleteAppAuthorization '{0}'";
            err = null;
            if (appAuthID.IsValid)
            {
                string result = this.DatabaseAccess.ExecuteScalar(string.Format(sql, appAuthID)).ToString();
                string[] array = result.Split('|');
                err = array[1];
                return array[0] == "0";
            }
            return false;
        }
        /// <summary>
        /// Picker��Ȩϵͳ
        /// </summary>
        /// <param name="SystemName">ϵͳ����</param>
        /// <returns></returns>
        public IListControlsData AppAuthorizationPicker(string SystemName)
        {
            return new ListControlsDataSource("SystemName", "AppAuthID", this.GetAllRecord(string.Format("SystemName like '%{0}%'", SystemName)));
        }
	}

}
