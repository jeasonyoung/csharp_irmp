//================================================================================
// FileName: SysMgrEmployeeAuthorization.cs
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
namespace iPower.IRMP.SysMgr.Engine.Domain
{
	///<summary>
	///tblSysMgrEmployeeAuthorizationӳ���ࡣ
	///</summary>
	[DbTable("tblSysMgrEmployeeAuthorization")]
	public class SysMgrEmployeeAuthorization
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SysMgrEmployeeAuthorization()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������AppAuthID��
		///</summary>
		[DbField("AppAuthID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	AppAuthID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������EmployeeID��
		///</summary>
		[DbField("EmployeeID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	EmployeeID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������EmployeeName��
		///</summary>
		[DbField("EmployeeName")]
		public	string	EmployeeName
		{
			get;set;

		}
			
		#endregion

	}

}
