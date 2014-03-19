//================================================================================
// FileName: CommonEnumsEntity.cs
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
	///CommonEnumsEntityʵ���ࡣ
	///</summary>
	internal class CommonEnumsEntity: DbModuleEntity<CommonEnums>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public CommonEnumsEntity()
		{

		}
		#endregion

        /// <summary>
        /// ɾ�����ݡ�
        /// </summary>
        /// <param name="fullEnumNameMember"></param>
        /// <returns></returns>
        public new bool DeleteRecord(string fullEnumNameMember)
        {
            if (!string.IsNullOrEmpty(fullEnumNameMember))
                return base.DeleteRecord(string.Format("EnumName + '.' + Member = '{0}'", fullEnumNameMember));
            return false;
        }
	}

}
