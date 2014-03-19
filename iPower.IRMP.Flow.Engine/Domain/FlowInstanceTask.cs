//================================================================================
// FileName: FlowInstanceTask.cs
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
	///tblFlowInstanceTask映射类。
	///</summary>
	[DbTable("tblFlowInstanceTask")]
	public class FlowInstanceTask
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public FlowInstanceTask()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置TaskID。
		///</summary>
		[DbField("TaskID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	TaskID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置StepInstanceID。
		///</summary>
		[DbField("StepInstanceID")]
		public	GUIDEx	StepInstanceID
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
		///获取或设置AuthorizeEmployeeID。
		///</summary>
		[DbField("AuthorizeEmployeeID")]
		public	GUIDEx	AuthorizeEmployeeID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置AuthorizeEmployeeName。
		///</summary>
		[DbField("AuthorizeEmployeeName")]
		public	string	AuthorizeEmployeeName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置DoEmployeeID。
		///</summary>
		[DbField("DoEmployeeID")]
		public	GUIDEx	DoEmployeeID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置DoEmployeeName。
		///</summary>
		[DbField("DoEmployeeName")]
		public	string	DoEmployeeName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置TaskCategory。
		///</summary>
		[DbField("TaskCategory")]
		public	int	TaskCategory
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
		[DbField("EndDate", DbFieldUsage.EmptyOrNullNotUpdate)]
		public	DateTime	EndDate
		{
			get;set;

		}
			
		///<summary>
		///获取或设置BeginMode。
		///</summary>
		[DbField("BeginMode")]
		public	int	BeginMode
		{
			get;set;

		}
			
		///<summary>
		///获取或设置EndMode。
		///</summary>
		[DbField("EndMode")]
		public	int	EndMode
		{
			get;set;

		}
			
		///<summary>
		///获取或设置URL。
		///</summary>
		[DbField("URL")]
		public	string	URL
		{
			get;set;

		}
			
		///<summary>
		///获取或设置TaskDescription。
		///</summary>
		[DbField("TaskDescription")]
		public	string	TaskDescription
		{
			get;set;

		}
			
		#endregion

	}

}
