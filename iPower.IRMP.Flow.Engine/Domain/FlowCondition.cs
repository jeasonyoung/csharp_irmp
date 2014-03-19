//================================================================================
// FileName: FlowCondition.cs
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
	///tblFlowCondition映射类。
	///</summary>
	[DbTable("tblFlowCondition")]
	public class FlowCondition
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public FlowCondition()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置ConditionID。
		///</summary>
		[DbField("ConditionID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ConditionID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置TransitionID。
		///</summary>
		[DbField("TransitionID")]
		public	GUIDEx	TransitionID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ParameterID。
		///</summary>
		[DbField("ParameterID")]
		public	GUIDEx	ParameterID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置CompareValue。
		///</summary>
		[DbField("CompareValue")]
		public	string	CompareValue
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ConditionValue。
		///</summary>
		[DbField("ConditionValue")]
		public	int	ConditionValue
		{
			get;set;

		}
			
		#endregion

	}

}
