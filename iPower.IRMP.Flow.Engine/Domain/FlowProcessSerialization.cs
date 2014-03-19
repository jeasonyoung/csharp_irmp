//================================================================================
// FileName: FlowProcessSerialization.cs
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
	///tblFlowProcessSerialization映射类。
	///</summary>
	[DbTable("tblFlowProcessSerialization")]
	public class FlowProcessSerialization
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public FlowProcessSerialization()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置ProcessID。
		///</summary>
		[DbField("ProcessID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ProcessID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置Serialization。
		///</summary>
		[DbField("Serialization")]
		public	string	Serialization
		{
			get;set;

		}
			
		///<summary>
		///获取或设置Verify。
		///</summary>
		[DbField("Verify")]
		public	string	Verify
		{
			get;set;

		}
			
		#endregion

	}

}
