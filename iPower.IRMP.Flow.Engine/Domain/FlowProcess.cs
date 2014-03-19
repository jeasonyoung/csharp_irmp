//================================================================================
// FileName: FlowProcess.cs
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
	///tblFlowProcess映射类。
	///</summary>
	[DbTable("tblFlowProcess")]
	public class FlowProcess
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public FlowProcess()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置ProcessID。
		///</summary>
		[DbField("ProcessID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ProcessID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ProcessSign。
		///</summary>
		[DbField("ProcessSign")]
		public	GUIDEx	ProcessSign
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ProcessName。
		///</summary>
		[DbField("ProcessName")]
		public	string	ProcessName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ProcessStatus。
		///</summary>
		[DbField("ProcessStatus")]
		public	int	ProcessStatus
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
			
		///<summary>
		///获取或设置ProcessDescription。
		///</summary>
		[DbField("ProcessDescription")]
		public	string	ProcessDescription
		{
			get;set;

		}
			
		#endregion

	}

}
