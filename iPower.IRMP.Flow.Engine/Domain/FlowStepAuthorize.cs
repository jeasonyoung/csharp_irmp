//================================================================================
// FileName: FlowStepAuthorize.cs
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
	///tblFlowStepAuthorize映射类。
	///</summary>
	[DbTable("tblFlowStepAuthorize")]
	public class FlowStepAuthorize
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public FlowStepAuthorize()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置AuthorizeID。
		///</summary>
		[DbField("AuthorizeID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	AuthorizeID
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
		///获取或设置EmployeeID。
		///</summary>
		[DbField("EmployeeID")]
		public	GUIDEx	EmployeeID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置EmployeeName。
		///</summary>
		[DbField("EmployeeName")]
		public	string	EmployeeName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置TargetEmployeeID。
		///</summary>
		[DbField("TargetEmployeeID")]
		public	GUIDEx	TargetEmployeeID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置TargetEmployeeName。
		///</summary>
		[DbField("TargetEmployeeName")]
		public	string	TargetEmployeeName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置BeginDate。
		///</summary>
		[DbField("BeginDate")]
		public	DateTime	BeginDate
		{
			get;set;

		}
			
		///<summary>
		///获取或设置EndDate。
		///</summary>
		[DbField("EndDate")]
		public	DateTime	EndDate
		{
			get;set;

		}
			
		#endregion

	}

}
