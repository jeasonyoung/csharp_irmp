//================================================================================
// FileName: SysMgrWebPartZone.cs
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
using System.Text;
	
using iPower;
using iPower.Data;
using iPower.Data.ORM;
namespace iPower.IRMP.SysMgr.Engine.Domain
{
	///<summary>
	///tblSysMgrWebPartZone映射类。
	///</summary>
	[DbTable("tblSysMgrWebPartZone")]
	public class SysMgrWebPartZone
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrWebPartZone()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置ZoneID。
		///</summary>
		[DbField("ZoneID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ZoneID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ZoneName。
		///</summary>
		[DbField("ZoneName")]
		public	string	ZoneName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置AppAuthID。
		///</summary>
		[DbField("AppAuthID")]
		public	GUIDEx	AppAuthID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ZoneMode。
		///</summary>
		[DbField("ZoneMode")]
		public	int	ZoneMode
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ZoneLength。
		///</summary>
		[DbField("ZoneLength")]
		public	int	ZoneLength
		{
			get;set;

		}
			
		///<summary>
		///获取或设置Description。
		///</summary>
		[DbField("Description")]
		public	string	Description
		{
			get;set;

		}

        /// <summary>
        /// 获取或设置SystemName
        /// </summary>
        public string SystemName
        {
            get;
            set;
        }
		#endregion

	}

}
