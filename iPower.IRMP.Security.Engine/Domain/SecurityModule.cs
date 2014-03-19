//================================================================================
// FileName: SecurityModule.cs
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
	///tblSecurityModule映射类。
	///</summary>
	[DbTable("tblSecurityModule")]
	public class SecurityModule
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public SecurityModule()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置ModuleID。
		///</summary>
		[DbField("ModuleID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ModuleID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ParentModuleID。
		///</summary>
		[DbField("ParentModuleID")]
		public	GUIDEx	ParentModuleID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置SystemID。
		///</summary>
		[DbField("SystemID")]
		public	GUIDEx	SystemID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ModuleName。
		///</summary>
		[DbField("ModuleName")]
		public	string	ModuleName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置OrderNo。
		///</summary>
		[DbField("OrderNo")]
		public	int	OrderNo
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ModuleStatus。
		///</summary>
		[DbField("ModuleStatus")]
		public	int	ModuleStatus
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ModuleDescription。
		///</summary>
		[DbField("ModuleDescription")]
		public	string	ModuleDescription
		{
			get;set;

		}
			
		#endregion

	}

}
