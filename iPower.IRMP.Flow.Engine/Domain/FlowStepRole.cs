//================================================================================
// FileName: FlowStepRole.cs
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
	///tblFlowStepRoleӳ���ࡣ
	///</summary>
	[DbTable("tblFlowStepRole")]
	public class FlowStepRole
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public FlowStepRole()
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
		///��ȡ������RoleID��
		///</summary>
		[DbField("RoleID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	RoleID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������RoleName��
		///</summary>
		[DbField("RoleName")]
		public	string	RoleName
		{
			get;set;

		}
			
		#endregion

	}

}
