//================================================================================
// FileName: FlowStepRank.cs
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
	///tblFlowStepRankӳ���ࡣ
	///</summary>
	[DbTable("tblFlowStepRank")]
	public class FlowStepRank
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public FlowStepRank()
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
		///��ȡ������RankID��
		///</summary>
		[DbField("RankID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	RankID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������RankName��
		///</summary>
		[DbField("RankName")]
		public	string	RankName
		{
			get;set;

		}
			
		#endregion

	}

}
