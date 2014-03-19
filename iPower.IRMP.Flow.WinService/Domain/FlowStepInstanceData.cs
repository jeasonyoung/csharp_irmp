//================================================================================
// FileName: FlowStepInstanceData.cs
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
	///tblFlowStepInstanceDataӳ���ࡣ
	///</summary>
	[DbTable("tblFlowStepInstanceData")]
	public class FlowStepInstanceData
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public FlowStepInstanceData()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������TaskDataID��
		///</summary>
		[DbField("TaskDataID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	TaskDataID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������StepInstanceID��
		///</summary>
		[DbField("StepInstanceID")]
		public	GUIDEx	StepInstanceID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������DataCategory��
		///</summary>
		[DbField("DataCategory")]
		public	int	DataCategory
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������DataText��
		///</summary>
		[DbField("DataText")]
		public	string	DataText
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
			
		#endregion

	}

}
