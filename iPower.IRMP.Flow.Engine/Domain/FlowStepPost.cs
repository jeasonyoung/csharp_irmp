//================================================================================
// FileName: FlowStepPost.cs
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
	///tblFlowStepPost映射类。
	///</summary>
	[DbTable("tblFlowStepPost")]
	public class FlowStepPost
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public FlowStepPost()
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
		///获取或设置PostID。
		///</summary>
		[DbField("PostID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	PostID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置PostName。
		///</summary>
		[DbField("PostName")]
		public	string	PostName
		{
			get;set;

		}
			
		#endregion

	}

}
