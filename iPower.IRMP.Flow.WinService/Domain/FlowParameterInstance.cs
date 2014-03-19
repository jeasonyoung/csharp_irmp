//================================================================================
// FileName: FlowParameterInstance.cs
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
namespace iPower.IRMP.Flow.WinService.Domain
{
	///<summary>
	///tblFlowParameterInstanceӳ���ࡣ
	///</summary>
	[DbTable("tblFlowParameterInstance")]
	public class FlowParameterInstance
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public FlowParameterInstance()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������StepInstanceID��
		///</summary>
		[DbField("StepInstanceID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	StepInstanceID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ParameterID��
		///</summary>
		[DbField("ParameterID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ParameterID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ParameterValue��
		///</summary>
		[DbField("ParameterValue")]
		public	string	ParameterValue
		{
			get;set;

		}
			
		#endregion

	}

}
