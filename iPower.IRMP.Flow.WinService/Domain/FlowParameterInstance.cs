//================================================================================
// FileName: FlowParameterInstance.cs
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
namespace iPower.IRMP.Flow.WinService.Domain
{
	///<summary>
	///tblFlowParameterInstance映射类。
	///</summary>
	[DbTable("tblFlowParameterInstance")]
	public class FlowParameterInstance
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public FlowParameterInstance()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置StepInstanceID。
		///</summary>
		[DbField("StepInstanceID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	StepInstanceID
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
		///获取或设置ParameterValue。
		///</summary>
		[DbField("ParameterValue")]
		public	string	ParameterValue
		{
			get;set;

		}
			
		#endregion

	}

}
