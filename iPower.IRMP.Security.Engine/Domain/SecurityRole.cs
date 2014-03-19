//================================================================================
// FileName: SecurityRole.cs
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
	///tblSecurityRole映射类。
	///</summary>
	[DbTable("tblSecurityRole")]
	public class SecurityRole
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public SecurityRole()
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
		///获取或设置RoleName。
		///</summary>
		[DbField("RoleName")]
		public	GUIDEx	RoleName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ParentRoleID。
		///</summary>
		[DbField("ParentRoleID")]
		public	GUIDEx	ParentRoleID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置RoleDescription。
		///</summary>
		[DbField("RoleDescription")]
		public	string	RoleDescription
		{
			get;set;

		}
			
		///<summary>
		///获取或设置RoleStatus。
		///</summary>
		[DbField("RoleStatus")]
		public	int	RoleStatus
		{
			get;set;

		}
			
		#endregion

	}

}
