//================================================================================
// FileName: SecurityRoleRight.cs
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
namespace iPower.IRMP.Security.Engine.Domain
{
	///<summary>
	///tblSecurityRoleRight映射类。
	///</summary>
	[DbTable("tblSecurityRoleRight")]
	public class SecurityRoleRight
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public SecurityRoleRight()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置RoleID。
		///</summary>
		[DbField("RoleID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	RoleID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置RightID。
		///</summary>
		[DbField("RightID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	RightID
		{
			get;set;

		}
			
		#endregion

	}

}
