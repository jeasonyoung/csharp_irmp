//================================================================================
// FileName: FlowStepPost.cs
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
	///tblFlowStepPostӳ���ࡣ
	///</summary>
	[DbTable("tblFlowStepPost")]
	public class FlowStepPost
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public FlowStepPost()
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
		///��ȡ������PostID��
		///</summary>
		[DbField("PostID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	PostID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������PostName��
		///</summary>
		[DbField("PostName")]
		public	string	PostName
		{
			get;set;

		}
			
		#endregion

	}

}
