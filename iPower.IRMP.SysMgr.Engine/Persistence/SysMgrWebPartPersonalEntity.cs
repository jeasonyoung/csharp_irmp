//================================================================================
// FileName: SysMgrWebPartPersonalEntity.cs
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
	///SysMgrWebPartPersonalEntityʵ���ࡣ
	///</summary>
    internal class SysMgrWebPartPersonalEntity : DbModuleEntity<SysMgrWebPartPersonal>
    {
        #region ��Ա���������캯����
        ///<summary>
        ///���캯��
        ///</summary>
        public SysMgrWebPartPersonalEntity()
        {

        }
        #endregion

        /// <summary>
        /// ��ҳ�����б�
        /// </summary>
        /// <param name="WebPartName">��������</param>
        /// <returns></returns>
        public DataTable ListDataSource(string WebPartName)
        {
            const string sql = "exec spSysMgrWebPartPersonalListView '{0}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, WebPartName)).Tables[0].Copy();
        }
    }

}
