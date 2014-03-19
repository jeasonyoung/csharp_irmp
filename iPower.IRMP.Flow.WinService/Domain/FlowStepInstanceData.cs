//================================================================================
// FileName: FlowStepInstanceData.cs
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
	///tblFlowStepInstanceData映射类。
	///</summary>
	[DbTable("tblFlowStepInstanceData")]
	public class FlowStepInstanceData
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public FlowStepInstanceData()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置TaskDataID。
		///</summary>
		[DbField("TaskDataID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	TaskDataID
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
		///获取或设置DataCategory。
		///</summary>
		[DbField("DataCategory")]
		public	int	DataCategory
		{
			get;set;

		}
			
		///<summary>
		///获取或设置DataText。
		///</summary>
		[DbField("DataText")]
		public	string	DataText
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
