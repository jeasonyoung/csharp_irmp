//================================================================================
// FileName: FlowParameterMap.cs
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
	///tblFlowParameterMap映射类。
	///</summary>
	[DbTable("tblFlowParameterMap")]
	public class FlowParameterMap
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public FlowParameterMap()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置TransitionID。
		///</summary>
		[DbField("TransitionID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	TransitionID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ParameterID。
		///</summary>
		[DbField("ParameterID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ParameterID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置MapParameterID。
		///</summary>
		[DbField("MapParameterID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	MapParameterID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置MapMode。
		///</summary>
		[DbField("MapMode")]
		public	int	MapMode
		{
			get;set;

		}
			
		///<summary>
		///获取或设置AssemblyName。
		///</summary>
		[DbField("AssemblyName")]
		public	string	AssemblyName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ClassName。
		///</summary>
		[DbField("ClassName")]
		public	string	ClassName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置EntryName。
		///</summary>
		[DbField("EntryName")]
		public	string	EntryName
		{
			get;set;

		}
			
		#endregion

	}

}
