//================================================================================
// FileName: IRMPCommonLog.cs
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
namespace iPower.IRMP.SysMgr.Engine.Domain
{
	///<summary>
	///tblIRMPCommonLog映射类。
	///</summary>
	[DbTable("tblIRMPCommonLog")]
	public class IRMPCommonLog
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public IRMPCommonLog()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置LogID。
		///</summary>
		[DbField("LogID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	LogID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置SystemID。
		///</summary>
		[DbField("SystemID")]
		public	GUIDEx	SystemID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置SystemName。
		///</summary>
		[DbField("SystemName")]
		public	string	SystemName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置RelationTable。
		///</summary>
		[DbField("RelationTable")]
		public	string	RelationTable
		{
			get;set;

		}
			
		///<summary>
		///获取或设置LogContext。
		///</summary>
		[DbField("LogContext")]
		public	string	LogContext
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
			
		///<summary>
		///获取或设置CreateEmployeeID。
		///</summary>
		[DbField("CreateEmployeeID")]
		public	GUIDEx	CreateEmployeeID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置CreateEmployeeName。
		///</summary>
		[DbField("CreateEmployeeName")]
		public	string	CreateEmployeeName
		{
			get;set;

		}
			
		#endregion

	}

}
