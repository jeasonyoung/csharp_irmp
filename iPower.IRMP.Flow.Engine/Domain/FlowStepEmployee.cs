//================================================================================
// FileName: FlowStepEmployee.cs
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
	///tblFlowStepEmployeeӳ���ࡣ
	///</summary>
	[DbTable("tblFlowStepEmployee")]
	public class FlowStepEmployee
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public FlowStepEmployee()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������StepID��
		///</summary>
		[DbField("StepID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	StepID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������EmployeeID��
		///</summary>
		[DbField("EmployeeID",DbFieldUsage.PrimaryKey)]
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
			
		#endregion

	}

}
