//================================================================================
// FileName: FlowCondition.cs
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
	///tblFlowConditionӳ���ࡣ
	///</summary>
	[DbTable("tblFlowCondition")]
	public class FlowCondition
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public FlowCondition()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������ConditionID��
		///</summary>
		[DbField("ConditionID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ConditionID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������TransitionID��
		///</summary>
		[DbField("TransitionID")]
		public	GUIDEx	TransitionID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ParameterID��
		///</summary>
		[DbField("ParameterID")]
		public	GUIDEx	ParameterID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������CompareValue��
		///</summary>
		[DbField("CompareValue")]
		public	string	CompareValue
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ConditionValue��
		///</summary>
		[DbField("ConditionValue")]
		public	int	ConditionValue
		{
			get;set;

		}
			
		#endregion

	}

}
