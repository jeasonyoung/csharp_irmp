//================================================================================
// FileName: SecurityRole.cs
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
	///tblSecurityRoleӳ���ࡣ
	///</summary>
	[DbTable("tblSecurityRole")]
	public class SecurityRole
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SecurityRole()
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
		///��ȡ������RoleName��
		///</summary>
		[DbField("RoleName")]
		public	GUIDEx	RoleName
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ParentRoleID��
		///</summary>
		[DbField("ParentRoleID")]
		public	GUIDEx	ParentRoleID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������RoleDescription��
		///</summary>
		[DbField("RoleDescription")]
		public	string	RoleDescription
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������RoleStatus��
		///</summary>
		[DbField("RoleStatus")]
		public	int	RoleStatus
		{
			get;set;

		}
			
		#endregion

	}

}
