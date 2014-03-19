//================================================================================
// FileName: SecurityRoleEntity.cs
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
using iPower.IRMP.Security.Engine.Domain;
namespace iPower.IRMP.Security.Engine.Persistence
{
	///<summary>
	///SecurityRoleEntityʵ���ࡣ
	///</summary>
    internal class SecurityRoleEntity : DbModuleEntity<iPower.IRMP.Security.Engine.Domain.SecurityRole>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public SecurityRoleEntity()
		{

		}
		#endregion

        /// <summary>
        /// ��ȡ��ɫ���ݡ�
        /// </summary>
        public IListControlsTreeViewData Role
        {
            get
            {
                return new ListControlsTreeViewDataSource("RoleName", "RoleID", "ParentRoleID", 
                    this.GetAllRecord(string.Format("RoleStatus='{0}'", (int)EnumSystemStatus.Start)));
            }
        }
        /// <summary>
        /// ���ݽ�ɫ���ƻ�ȡ��ɫ���ݡ�
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public IListControlsData GetRole(string roleName)
        {
            return new ListControlsDataSource("RoleName", "RoleID",
                this.GetAllRecord(string.Format("(RoleStatus='{0}') and (RoleName like '%{1}%')", (int)EnumSystemStatus.Start, roleName)));
        }

        /// <summary>
        /// �б�����Դ��
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string roleName)
        {
            const string sql = "exec spSecurityRoleListView '{0}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, roleName)).Tables[0].Copy();
        }
        /// <summary>
        /// ���½�ɫ��
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sysCollection"></param>
        /// <returns></returns>
        public bool UpdateRole(iPower.IRMP.Security.Engine.Domain.SecurityRole data, StringCollection sysCollection)
        {
            bool result = false;
            if (data != null)
            {
                const string del = "delete from tblSecurityRoleSystem where RoleID='{0}'";
                this.DatabaseAccess.ExecuteNonQuery(string.Format(del, data.RoleID));
                result = this.UpdateRecord(data);
                if (result)
                {
                    SecurityRoleSystem sys = null;
                    SecurityRoleSystemEntity sysEntity = new SecurityRoleSystemEntity();
                    foreach (string s in sysCollection)
                    {
                        sys = new SecurityRoleSystem();
                        sys.RoleID = data.RoleID;
                        sys.SystemID = s;
                        result = sysEntity.UpdateRecord(sys);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// ɾ����ɫ��
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool DeleteRole(string roleID, out string err)
        {
            const string sql = "exec spSecurityDeleteRole '{0}'";
            string result = this.DatabaseAccess.ExecuteScalar(string.Format(sql, roleID)).ToString();
            string[] array = result.Split('|');
            err = array[1];

            return array[0] == "0";
        }

        /// <summary>
        /// ��ȡ�û���ɫ��
        /// </summary>
        /// <param name="systemID"></param>
        /// <param name="employeeID"></param>
        /// <param name="departmentID"></param>
        /// <param name="rankID"></param>
        /// <param name="postID"></param>
        /// <returns></returns>
        public DataTable GetEmployeeRoles(string systemID, string employeeID, string departmentID, string rankID, string postID)
        {
            const string sql = "select RoleID,RoleName,ParentRoleID from dbo.fnSecurityGetEmployeeRoles('{0}','{1}','{2}','{3}','{4}')";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, systemID, employeeID, departmentID, rankID, postID)).Tables[0].Copy();
        }
	}

}
