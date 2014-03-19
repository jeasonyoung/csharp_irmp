//================================================================================
// FileName: FlowStepInstance.cs
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
	///tblFlowStepInstanceӳ���ࡣ
	///</summary>
	[DbTable("tblFlowStepInstance")]
	public class FlowStepInstance
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public FlowStepInstance()
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
		///��ȡ������ProcessInstanceID��
		///</summary>
		[DbField("ProcessInstanceID")]
		public	GUIDEx	ProcessInstanceID
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
        /// <summary>
        /// ��ȡ������StepName��
        /// </summary>
        [DbField("StepName")]
        public string StepName
        {
            get;
            set;
        }
			
		///<summary>
		///��ȡ������FromEmployeeID��
		///</summary>
		[DbField("FromEmployeeID")]
		public	GUIDEx	FromEmployeeID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������FromEmployeeName��
		///</summary>
		[DbField("FromEmployeeName")]
		public	string	FromEmployeeName
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
			
		///<summary>
		///��ȡ������EndDate��
		///</summary>
        [DbField("EndDate", DbFieldUsage.EmptyOrNullNotUpdate)]
        public DateTime? EndDate
        {
            get;
            set;

        }
			
		///<summary>
		///��ȡ������InstanceStepStatus��
		///</summary>
		[DbField("InstanceStepStatus")]
		public	int	InstanceStepStatus
		{
			get;set;

		}
			
		#endregion

	}

}
