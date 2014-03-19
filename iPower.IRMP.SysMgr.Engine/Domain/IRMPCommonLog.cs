//================================================================================
// FileName: IRMPCommonLog.cs
// Desc:
// Called by
// Auth: �������ɴ����������Զ����ɡ�
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
	///tblIRMPCommonLogӳ���ࡣ
	///</summary>
	[DbTable("tblIRMPCommonLog")]
	public class IRMPCommonLog
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public IRMPCommonLog()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������LogID��
		///</summary>
		[DbField("LogID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	LogID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������SystemID��
		///</summary>
		[DbField("SystemID")]
		public	GUIDEx	SystemID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������SystemName��
		///</summary>
		[DbField("SystemName")]
		public	string	SystemName
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������RelationTable��
		///</summary>
		[DbField("RelationTable")]
		public	string	RelationTable
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������LogContext��
		///</summary>
		[DbField("LogContext")]
		public	string	LogContext
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������CreateDate��
		///</summary>
		[DbField("CreateDate")]
		public	DateTime	CreateDate
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������CreateEmployeeID��
		///</summary>
		[DbField("CreateEmployeeID")]
		public	GUIDEx	CreateEmployeeID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������CreateEmployeeName��
		///</summary>
		[DbField("CreateEmployeeName")]
		public	string	CreateEmployeeName
		{
			get;set;

		}
			
		#endregion

	}

}
