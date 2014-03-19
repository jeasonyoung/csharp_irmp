//================================================================================
// FileName: FlowProcessSerialization.cs
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
	///tblFlowProcessSerializationӳ���ࡣ
	///</summary>
	[DbTable("tblFlowProcessSerialization")]
	public class FlowProcessSerialization
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public FlowProcessSerialization()
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
		///��ȡ������Serialization��
		///</summary>
		[DbField("Serialization")]
		public	string	Serialization
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
			
		#endregion

	}

}
