//================================================================================
// FileName: FlowStepRank.cs
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
	///tblFlowStepRank映射类。
	///</summary>
	[DbTable("tblFlowStepRank")]
	public class FlowStepRank
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public FlowStepRank()
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
		///获取或设置RankID。
		///</summary>
		[DbField("RankID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	RankID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置RankName。
		///</summary>
		[DbField("RankName")]
		public	string	RankName
		{
			get;set;

		}
			
		#endregion

	}

}
