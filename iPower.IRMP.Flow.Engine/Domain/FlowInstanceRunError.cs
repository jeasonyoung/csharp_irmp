//================================================================================
// FileName: FlowInstanceRunError.cs
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
	///tblFlowInstanceRunErrorӳ���ࡣ
	///</summary>
	[DbTable("tblFlowInstanceRunError")]
	public class FlowInstanceRunError
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public FlowInstanceRunError()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������ErrorID��
		///</summary>
		[DbField("ErrorID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ErrorID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ProcessInstanceID��
		///</summary>
		[DbField("ProcessInstanceID")]
		public	GUIDEx	ProcessInstanceID
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
		///��ȡ������ErrorMessage��
		///</summary>
		[DbField("ErrorMessage")]
		public	string	ErrorMessage
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
			
		#endregion

	}

}
