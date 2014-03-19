//================================================================================
// FileName: FlowProcessInstance.cs
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
	///tblFlowProcessInstanceӳ���ࡣ
	///</summary>
	[DbTable("tblFlowProcessInstance")]
	public class FlowProcessInstance
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public FlowProcessInstance()
		{

		}
		#endregion
		#region ���ԡ�
       
		///<summary>
		///��ȡ������ProcessInstanceID��
		///</summary>
		[DbField("ProcessInstanceID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ProcessInstanceID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ProcessInstanceName��
		///</summary>
		[DbField("ProcessInstanceName")]
		public	string	ProcessInstanceName
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
        /// <summary>
        /// ��ȡ�������������ơ�
        /// </summary>
        [DbField("ProcessName")]
        public string ProcessName
        {
            get;
            set;
        }
		///<summary>
		///��ȡ������ProcessSerialization��
		///</summary>
		[DbField("ProcessSerialization")]
		public	string	ProcessSerialization
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������Verify��
		///</summary>
		[DbField("Verify")]
		public	string	Verify
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������CreateDate��
		///</summary>
		[DbField("CreateDate")]
		public	DateTime CreateDate
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������EndDate��
		///</summary>
		[DbField("EndDate", DbFieldUsage.EmptyOrNullNotUpdate)]
		public	DateTime ? EndDate
		{
			get;set;

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
		///��ȡ������InstanceProcessStatus��
		///</summary>
		[DbField("InstanceProcessStatus")]
		public	int	InstanceProcessStatus
		{
			get;set;

		}
			
		#endregion

	}

}
