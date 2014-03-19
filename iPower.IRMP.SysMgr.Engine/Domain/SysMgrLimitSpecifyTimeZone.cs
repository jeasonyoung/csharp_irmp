//================================================================================
// FileName: SysMgrLimitSpecifyTimeZone.cs
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
	///tblSysMgrLimitSpecifyTimeZoneӳ���ࡣ
	///</summary>
	[DbTable("tblSysMgrLimitSpecifyTimeZone")]
	public class SysMgrLimitSpecifyTimeZone
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SysMgrLimitSpecifyTimeZone()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������ZoneID��
		///</summary>
		[DbField("ZoneID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ZoneID
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
		///��ȡ������StartTime��
		///</summary>
		[DbField("StartTime")]
		public	DateTime	StartTime
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������EndTime��
		///</summary>
		[DbField("EndTime")]
		public	DateTime	EndTime
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������AuthStatus��
		///</summary>
		[DbField("AuthStatus")]
		public	int	AuthStatus
		{
			get;set;

		}
			
		#endregion

	}

}
