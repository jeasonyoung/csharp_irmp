//================================================================================
// FileName: CommonEnumsEntity.cs
// Desc:
// Called by
// Auth: 本代码由代码生成器自动生成。
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
	///CommonEnumsEntity实体类。
	///</summary>
	internal class CommonEnumsEntity: DbModuleEntity<CommonEnums>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public CommonEnumsEntity()
		{

		}
		#endregion

        /// <summary>
        /// 删除数据。
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
