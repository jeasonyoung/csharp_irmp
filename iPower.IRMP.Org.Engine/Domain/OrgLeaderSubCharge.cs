//================================================================================
// FileName: OrgLeaderSubCharge.cs
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
namespace iPower.IRMP.Org.Engine.Domain
{
	///<summary>
	///tblOrgLeaderSubCharge映射类。
	///</summary>
	[DbTable("tblOrgLeaderSubCharge")]
	public class OrgLeaderSubCharge
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public OrgLeaderSubCharge()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置EmployeeID。
		///</summary>
		[DbField("EmployeeID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	EmployeeID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置DepartmentID。
		///</summary>
		[DbField("DepartmentID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	DepartmentID
		{
			get;set;

		}
			
		#endregion

	}

}
