//================================================================================
// FileName: SSOTicketEntity.cs
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
	///SSOTicketEntityʵ���ࡣ
	///</summary>
	internal class SSOTicketEntity: DbModuleEntity<SSOTicket>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public SSOTicketEntity()
		{

		}
		#endregion

        /// <summary>
        /// ��ȡ�б����ݡ�
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string userData)
        {
            return this.GetAllRecord(string.Format("UserData like '%{0}%'", userData), "HasValid desc,IssueDate desc");
        }
	}

}
