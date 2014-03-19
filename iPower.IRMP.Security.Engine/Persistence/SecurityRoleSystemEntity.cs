//================================================================================
// FileName: SecurityRoleSystemEntity.cs
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
	///SecurityRoleSystemEntityʵ���ࡣ
	///</summary>
	internal class SecurityRoleSystemEntity: DbModuleEntity<SecurityRoleSystem>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public SecurityRoleSystemEntity()
		{

		}
		#endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public StringCollection LoadSystemData(string roleID)
        {
            StringCollection collection = new StringCollection();
            DataTable dtSource = this.GetAllRecord(string.Format("RoleID='{0}'", roleID));
            if (dtSource != null)
            {
                foreach (DataRow row in dtSource.Rows)
                {
                    collection.Add(Convert.ToString(row["SystemID"]));
                }
            }
            return collection;
        }
	}

}
