//================================================================================
// FileName: FlowStepRole.cs
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
namespace iPower.IRMP.Flow.Engine.Domain
{
	///<summary>
	///tblFlowStepRole映射类。
	///</summary>
	[DbTable("tblFlowStepRole")]
	public class FlowStepRole
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public FlowStepRole()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置StepID。
		///</summary>
		[DbField("StepID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	StepID
		{
			get;set;

		}
			
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
		public	string	RoleName
		{
			get;set;

		}
			
		#endregion

	}

}
