//================================================================================
// FileName: SysMgrLinksEntity.cs
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
	///SysMgrLinksEntityʵ���ࡣ
	///</summary>
	internal class SysMgrLinksEntity: DbModuleEntity<SysMgrLinks>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public SysMgrLinksEntity()
		{

		}
		#endregion

        /// <summary>
        /// ��ȡ�б�����Դ��
        /// </summary>
        /// <param name="linkName"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string linkName)
        {
            return this.GetAllRecord(string.Format("LinkName like '%{0}%'", linkName), "OrderNo");
        }
	}
}