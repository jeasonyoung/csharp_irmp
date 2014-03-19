//================================================================================
// FileName: SecurityRoleRight.cs
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
namespace iPower.IRMP.Security.Engine.Domain
{
	///<summary>
	///tblSecurityRoleRightӳ���ࡣ
	///</summary>
	[DbTable("tblSecurityRoleRight")]
	public class SecurityRoleRight
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SecurityRoleRight()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������RoleID��
		///</summary>
		[DbField("RoleID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	RoleID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������RightID��
		///</summary>
		[DbField("RightID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	RightID
		{
			get;set;

		}
			
		#endregion

	}

}
