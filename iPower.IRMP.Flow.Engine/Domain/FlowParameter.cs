//================================================================================
// FileName: FlowParameter.cs
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
	///tblFlowParameter映射类。
	///</summary>
	[DbTable("tblFlowParameter")]
	public class FlowParameter
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public FlowParameter()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置ParameterID。
		///</summary>
		[DbField("ParameterID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ParameterID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置StepID。
		///</summary>
		[DbField("StepID")]
		public	GUIDEx	StepID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ParameterName。
		///</summary>
		[DbField("ParameterName")]
		public	string	ParameterName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ParameterType。
		///</summary>
		[DbField("ParameterType")]
		public	int	ParameterType
		{
			get;set;

		}
			
		///<summary>
		///获取或设置DefaultValue。
		///</summary>
		[DbField("DefaultValue")]
		public	string	DefaultValue
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ParameterDescription。
		///</summary>
		[DbField("ParameterDescription")]
		public	string	ParameterDescription
		{
			get;set;

		}
			
		#endregion

	}

}
