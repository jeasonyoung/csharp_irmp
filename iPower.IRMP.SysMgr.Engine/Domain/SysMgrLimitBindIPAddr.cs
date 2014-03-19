//================================================================================
// FileName: SysMgrLimitBindIPAddr.cs
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
	///tblSysMgrLimitBindIPAddrӳ���ࡣ
	///</summary>
	[DbTable("tblSysMgrLimitBindIPAddr")]
	public class SysMgrLimitBindIPAddr
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SysMgrLimitBindIPAddr()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������BindID��
		///</summary>
		[DbField("BindID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	BindID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������EmployeeID��
		///</summary>
		[DbField("EmployeeID")]
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
			
		///<summary>
		///��ȡ������BindIPAddr��
		///</summary>
		[DbField("BindIPAddr")]
		public	string	BindIPAddr
		{
			get;set;

		}
			
		#endregion

	}

}
