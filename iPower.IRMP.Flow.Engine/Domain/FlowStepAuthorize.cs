//================================================================================
// FileName: FlowStepAuthorize.cs
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
	///tblFlowStepAuthorizeӳ���ࡣ
	///</summary>
	[DbTable("tblFlowStepAuthorize")]
	public class FlowStepAuthorize
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public FlowStepAuthorize()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������AuthorizeID��
		///</summary>
		[DbField("AuthorizeID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	AuthorizeID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������StepID��
		///</summary>
		[DbField("StepID")]
		public	GUIDEx	StepID
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
		///��ȡ������TargetEmployeeID��
		///</summary>
		[DbField("TargetEmployeeID")]
		public	GUIDEx	TargetEmployeeID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������TargetEmployeeName��
		///</summary>
		[DbField("TargetEmployeeName")]
		public	string	TargetEmployeeName
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
		[DbField("EndDate")]
		public	DateTime	EndDate
		{
			get;set;

		}
			
		#endregion

	}

}
