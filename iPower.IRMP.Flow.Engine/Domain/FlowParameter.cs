//================================================================================
// FileName: FlowParameter.cs
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
	///tblFlowParameterӳ���ࡣ
	///</summary>
	[DbTable("tblFlowParameter")]
	public class FlowParameter
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public FlowParameter()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������ParameterID��
		///</summary>
		[DbField("ParameterID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ParameterID
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
		///��ȡ������ParameterName��
		///</summary>
		[DbField("ParameterName")]
		public	string	ParameterName
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ParameterType��
		///</summary>
		[DbField("ParameterType")]
		public	int	ParameterType
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������DefaultValue��
		///</summary>
		[DbField("DefaultValue")]
		public	string	DefaultValue
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ParameterDescription��
		///</summary>
		[DbField("ParameterDescription")]
		public	string	ParameterDescription
		{
			get;set;

		}
			
		#endregion

	}

}
