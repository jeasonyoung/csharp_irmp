//================================================================================
// FileName: FlowInstanceRunError.cs
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
	///tblFlowInstanceRunError映射类。
	///</summary>
	[DbTable("tblFlowInstanceRunError")]
	public class FlowInstanceRunError
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public FlowInstanceRunError()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置ErrorID。
		///</summary>
		[DbField("ErrorID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ErrorID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ProcessInstanceID。
		///</summary>
		[DbField("ProcessInstanceID")]
		public	GUIDEx	ProcessInstanceID
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
		///获取或设置ErrorMessage。
		///</summary>
		[DbField("ErrorMessage")]
		public	string	ErrorMessage
		{
			get;set;

		}
			
		///<summary>
		///获取或设置CreateDate。
		///</summary>
		[DbField("CreateDate")]
		public	DateTime	CreateDate
		{
			get;set;

		}
			
		#endregion

	}

}
