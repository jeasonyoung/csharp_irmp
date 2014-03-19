//================================================================================
// FileName: SecurityRegsiter.cs
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
	///tblSecurityRegsiter映射类。
	///</summary>
	[DbTable("tblSecurityRegsiter")]
	public class SecurityRegsiter
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public SecurityRegsiter()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置SystemID。
		///</summary>
		[DbField("SystemID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	SystemID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ParentSystemID。
		///</summary>
		[DbField("ParentSystemID")]
		public	GUIDEx	ParentSystemID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置SystemSign。
		///</summary>
		[DbField("SystemSign")]
		public	string	SystemSign
		{
			get;set;

		}
			
		///<summary>
		///获取或设置SystemName。
		///</summary>
		[DbField("SystemName")]
		public	string	SystemName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置SystemURL。
		///</summary>
		[DbField("SystemURL")]
		public	string	SystemURL
		{
			get;set;

		}
			
		///<summary>
		///获取或设置SecurityURL。
		///</summary>
		[DbField("SecurityURL")]
		public	string	SecurityURL
		{
			get;set;

		}
			
		///<summary>
		///获取或设置PatchURL。
		///</summary>
		[DbField("PatchURL")]
		public	string	PatchURL
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ModuleConfigURL。
		///</summary>
		[DbField("ModuleConfigURL")]
		public	string	ModuleConfigURL
		{
			get;set;

		}
			
		///<summary>
		///获取或设置SystemType。
		///</summary>
		[DbField("SystemType")]
		public	int	SystemType
		{
			get;set;

		}
			
		///<summary>
		///获取或设置SystemStatus。
		///</summary>
		[DbField("SystemStatus")]
		public	int	SystemStatus
		{
			get;set;

		}
			
		///<summary>
		///获取或设置SystemDescription。
		///</summary>
		[DbField("SystemDescription")]
		public	string	SystemDescription
		{
			get;set;

		}
			
		#endregion

	}

}
