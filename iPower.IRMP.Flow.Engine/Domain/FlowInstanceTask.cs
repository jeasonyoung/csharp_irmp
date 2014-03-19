//================================================================================
// FileName: FlowInstanceTask.cs
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
namespace iPower.IRMP.Flow.Engine.Domain
{
	///<summary>
	///tblFlowInstanceTaskӳ���ࡣ
	///</summary>
	[DbTable("tblFlowInstanceTask")]
	public class FlowInstanceTask
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public FlowInstanceTask()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������TaskID��
		///</summary>
		[DbField("TaskID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	TaskID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������StepInstanceID��
		///</summary>
		[DbField("StepInstanceID")]
		public	GUIDEx	StepInstanceID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������EmployeeID��
		///</summary>
		[DbField("EmployeeID")]
		public	GUIDEx	EmployeeID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������EmployeeName��
		///</summary>
		[DbField("EmployeeName")]
		public	string	EmployeeName
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������AuthorizeEmployeeID��
		///</summary>
		[DbField("AuthorizeEmployeeID")]
		public	GUIDEx	AuthorizeEmployeeID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������AuthorizeEmployeeName��
		///</summary>
		[DbField("AuthorizeEmployeeName")]
		public	string	AuthorizeEmployeeName
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������DoEmployeeID��
		///</summary>
		[DbField("DoEmployeeID")]
		public	GUIDEx	DoEmployeeID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������DoEmployeeName��
		///</summary>
		[DbField("DoEmployeeName")]
		public	string	DoEmployeeName
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������TaskCategory��
		///</summary>
		[DbField("TaskCategory")]
		public	int	TaskCategory
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������BeginDate��
		///</summary>
		[DbField("BeginDate")]
		public	DateTime	BeginDate
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������EndDate��
		///</summary>
		[DbField("EndDate", DbFieldUsage.EmptyOrNullNotUpdate)]
		public	DateTime	EndDate
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������BeginMode��
		///</summary>
		[DbField("BeginMode")]
		public	int	BeginMode
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������EndMode��
		///</summary>
		[DbField("EndMode")]
		public	int	EndMode
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������URL��
		///</summary>
		[DbField("URL")]
		public	string	URL
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������TaskDescription��
		///</summary>
		[DbField("TaskDescription")]
		public	string	TaskDescription
		{
			get;set;

		}
			
		#endregion

	}

}
