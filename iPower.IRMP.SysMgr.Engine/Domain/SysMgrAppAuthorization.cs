//================================================================================
// FileName: SysMgrAppAuthorization.cs
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
	///tblSysMgrAppAuthorization映射类。
	///</summary>
	[DbTable("tblSysMgrAppAuthorization")]
	public class SysMgrAppAuthorization
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrAppAuthorization()
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
		///获取或设置SystemID。
		///</summary>
		[DbField("SystemID")]
		public	GUIDEx	SystemID
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
		///获取或设置AuthPwd。
		///</summary>
		[DbField("AuthPwd")]
		public	string	AuthPwd
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
