//================================================================================
// FileName: OrgLeaderSubCharge.cs
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
namespace iPower.IRMP.Org.Engine.Domain
{
	///<summary>
	///tblOrgLeaderSubChargeӳ���ࡣ
	///</summary>
	[DbTable("tblOrgLeaderSubCharge")]
	public class OrgLeaderSubCharge
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public OrgLeaderSubCharge()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������EmployeeID��
		///</summary>
		[DbField("EmployeeID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	EmployeeID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������DepartmentID��
		///</summary>
		[DbField("DepartmentID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	DepartmentID
		{
			get;set;

		}
			
		#endregion

	}

}
