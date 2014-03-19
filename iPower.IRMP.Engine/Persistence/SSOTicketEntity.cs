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
using iPower.IRMP.Engine.Domain;
namespace iPower.IRMP.Engine.Persistence
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
        /// �����û���Ϣ����Ʊ�ݡ�
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public bool DestroyTicket(string userData)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(userData))
            {
                DataTable dtSource = this.GetAllRecord(string.Format("HasValid = 1 and (UserData = '{0}')", userData));
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DateTime dtEnd = DateTime.Now.AddSeconds(-10);
                    foreach (DataRow row in dtSource.Rows)
                    {
                        SSOTicket ticket = this.Assignment(row);
                        if (ticket != null)
                        {
                            ticket.Expiration = dtEnd;
                            result = this.UpdateRecord(ticket);
                        }
                    }
                }
            }
            return result;
        }
	}

}
