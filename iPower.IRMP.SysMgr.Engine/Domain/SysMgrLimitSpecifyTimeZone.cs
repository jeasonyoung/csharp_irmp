//================================================================================
// FileName: SysMgrLimitSpecifyTimeZone.cs
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
	///tblSysMgrLimitSpecifyTimeZone映射类。
	///</summary>
	[DbTable("tblSysMgrLimitSpecifyTimeZone")]
	public class SysMgrLimitSpecifyTimeZone
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrLimitSpecifyTimeZone()
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
		///获取或设置EmployeeID。
		///</summary>
		[DbField("EmployeeID")]
		public	GUIDEx	EmployeeID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置EmployeeName。
		///</summary>
		[DbField("EmployeeName")]
		public	string	EmployeeName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置StartTime。
		///</summary>
		[DbField("StartTime")]
		public	DateTime	StartTime
		{
			get;set;

		}
			
		///<summary>
		///获取或设置EndTime。
		///</summary>
		[DbField("EndTime")]
		public	DateTime	EndTime
		{
			get;set;

		}
			
		///<summary>
		///获取或设置AuthStatus。
		///</summary>
		[DbField("AuthStatus")]
		public	int	AuthStatus
		{
			get;set;

		}
			
		#endregion

	}

}
