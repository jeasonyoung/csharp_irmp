//================================================================================
// FileName: FlowTransition.cs
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
	///tblFlowTransition映射类。
	///</summary>
	[DbTable("tblFlowTransition")]
	public class FlowTransition
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public FlowTransition()
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
		///获取或设置ProcessID。
		///</summary>
		[DbField("ProcessID")]
		public	GUIDEx	ProcessID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置FromStepID。
		///</summary>
		[DbField("FromStepID")]
		public	GUIDEx	FromStepID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ToStepID。
		///</summary>
		[DbField("ToStepID")]
		public	GUIDEx	ToStepID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置TransitionRule。
		///</summary>
		[DbField("TransitionRule")]
		public	int	TransitionRule
		{
			get;set;

		}
			
		#endregion

	}

}
