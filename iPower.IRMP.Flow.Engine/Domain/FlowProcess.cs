//================================================================================
// FileName: FlowProcess.cs
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
	///tblFlowProcessӳ���ࡣ
	///</summary>
	[DbTable("tblFlowProcess")]
	public class FlowProcess
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public FlowProcess()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������ProcessID��
		///</summary>
		[DbField("ProcessID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ProcessID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ProcessSign��
		///</summary>
		[DbField("ProcessSign")]
		public	GUIDEx	ProcessSign
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ProcessName��
		///</summary>
		[DbField("ProcessName")]
		public	string	ProcessName
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ProcessStatus��
		///</summary>
		[DbField("ProcessStatus")]
		public	int	ProcessStatus
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������BeginDate��
		///</summary>
		[DbField("BeginDate")]
		public	DateTime	BeginDate
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������EndDate��
		///</summary>
		[DbField("EndDate")]
		public	DateTime	EndDate
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ProcessDescription��
		///</summary>
		[DbField("ProcessDescription")]
		public	string	ProcessDescription
		{
			get;set;

		}
			
		#endregion

	}

}
