//================================================================================
// FileName: SysMgrWebPartPersonal.cs
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
	///tblSysMgrWebPartPersonalӳ���ࡣ
	///</summary>
	[DbTable("tblSysMgrWebPartPersonal")]
	public class SysMgrWebPartPersonal
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SysMgrWebPartPersonal()
		{

		}
		#endregion

		#region ���ԡ�
		///<summary>
		///��ȡ������PersonalWebPartID��
		///</summary>
		[DbField("PersonalWebPartID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	PersonalWebPartID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������WebPartID��
		///</summary>
		[DbField("WebPartID")]
		public	GUIDEx	WebPartID
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
		///��ȡ������ZoneID��
		///</summary>
		[DbField("ZoneID")]
		public	GUIDEx	ZoneID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������OrderNo��
		///</summary>
		[DbField("OrderNo")]
		public	int	OrderNo
		{
			get;set;

		}

        /// <summary>
        /// ��ʾ������WebPartName
        /// </summary>
        public string WebPartName
        {
            get;set;
        }
        /// <summary>
        /// ��ʾ������ZoneName
        /// </summary>
        public string ZoneName
        {
            get;
            set;
        }
		#endregion
	}

}
