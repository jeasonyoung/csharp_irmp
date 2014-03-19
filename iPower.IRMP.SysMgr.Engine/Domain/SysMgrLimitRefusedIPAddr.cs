//================================================================================
// FileName: SysMgrLimitRefusedIPAddr.cs
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
	///tblSysMgrLimitRefusedIPAddrӳ���ࡣ
	///</summary>
	[DbTable("tblSysMgrLimitRefusedIPAddr")]
	public class SysMgrLimitRefusedIPAddr
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SysMgrLimitRefusedIPAddr()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������RefusedID��
		///</summary>
		[DbField("RefusedID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	RefusedID
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
		///��ȡ������RefusedIPAddr��
		///</summary>
		[DbField("RefusedIPAddr")]
		public	string	RefusedIPAddr
		{
			get;set;

		}
			
		#endregion

	}

}
