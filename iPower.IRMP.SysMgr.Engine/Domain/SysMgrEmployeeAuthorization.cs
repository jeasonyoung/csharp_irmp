//================================================================================
// FileName: SysMgrEmployeeAuthorization.cs
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
	///tblSysMgrEmployeeAuthorization映射类。
	///</summary>
	[DbTable("tblSysMgrEmployeeAuthorization")]
	public class SysMgrEmployeeAuthorization
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrEmployeeAuthorization()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置AppAuthID。
		///</summary>
		[DbField("AppAuthID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	AppAuthID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置EmployeeID。
		///</summary>
		[DbField("EmployeeID",DbFieldUsage.PrimaryKey)]
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
			
		#endregion

	}

}
