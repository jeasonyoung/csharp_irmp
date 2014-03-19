//================================================================================
// FileName: FlowStep.cs
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
	///tblFlowStepӳ���ࡣ
	///</summary>
	[DbTable("tblFlowStep")]
	public class FlowStep
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public FlowStep()
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
		///��ȡ������StepSign��
		///</summary>
		[DbField("StepSign")]
		public	GUIDEx	StepSign
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������StepName��
		///</summary>
		[DbField("StepName")]
		public	string	StepName
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
		///��ȡ������StepType��
		///</summary>
		[DbField("StepType")]
		public	int	StepType
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������EntryAction��
		///</summary>
		[DbField("EntryAction")]
		public	string	EntryAction
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������EntryQuery��
		///</summary>
		[DbField("EntryQuery")]
		public	string	EntryQuery
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������StepMode��
		///</summary>
		[DbField("StepMode")]
		public	int	StepMode
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������StepDuration��
		///</summary>
		[DbField("StepDuration")]
		public	int	StepDuration
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������StepWarning��
		///</summary>
		[DbField("StepWarning")]
		public	int	StepWarning
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������StepDescription��
		///</summary>
		[DbField("StepDescription")]
		public	string	StepDescription
		{
			get;set;

		}
        /// <summary>
        /// ��ȡ�����������ֶΡ�
        /// </summary>
        [DbField("OrderNo")]
        public int OrderNo
        {
            get;
            set;
        }
		#endregion

	}

}
