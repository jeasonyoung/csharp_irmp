//================================================================================
// FileName: FlowTransition.cs
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
	///tblFlowTransitionӳ���ࡣ
	///</summary>
	[DbTable("tblFlowTransition")]
	public class FlowTransition
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public FlowTransition()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������TransitionID��
		///</summary>
		[DbField("TransitionID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	TransitionID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ProcessID��
		///</summary>
		[DbField("ProcessID")]
		public	GUIDEx	ProcessID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������FromStepID��
		///</summary>
		[DbField("FromStepID")]
		public	GUIDEx	FromStepID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ToStepID��
		///</summary>
		[DbField("ToStepID")]
		public	GUIDEx	ToStepID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������TransitionRule��
		///</summary>
		[DbField("TransitionRule")]
		public	int	TransitionRule
		{
			get;set;

		}
			
		#endregion

	}

}
