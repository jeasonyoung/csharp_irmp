//================================================================================
// FileName: FlowParameterMap.cs
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
	///tblFlowParameterMapӳ���ࡣ
	///</summary>
	[DbTable("tblFlowParameterMap")]
	public class FlowParameterMap
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public FlowParameterMap()
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
		///��ȡ������ParameterID��
		///</summary>
		[DbField("ParameterID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ParameterID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������MapParameterID��
		///</summary>
		[DbField("MapParameterID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	MapParameterID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������MapMode��
		///</summary>
		[DbField("MapMode")]
		public	int	MapMode
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������AssemblyName��
		///</summary>
		[DbField("AssemblyName")]
		public	string	AssemblyName
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ClassName��
		///</summary>
		[DbField("ClassName")]
		public	string	ClassName
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������EntryName��
		///</summary>
		[DbField("EntryName")]
		public	string	EntryName
		{
			get;set;

		}
			
		#endregion

	}

}
