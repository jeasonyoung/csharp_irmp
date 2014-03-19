//================================================================================
// FileName: SysMgrSettingPersonal.cs
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
	///tblSysMgrSettingPersonalӳ���ࡣ
	///</summary>
	[DbTable("tblSysMgrSettingPersonal")]
	public class SysMgrSettingPersonal
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SysMgrSettingPersonal()
		{

		}
		#endregion

		#region ���ԡ�
		///<summary>
		///��ȡ������PersonalSettingID��
		///</summary>
		[DbField("PersonalSettingID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	PersonalSettingID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������SettingID��
		///</summary>
		[DbField("SettingID")]
		public	GUIDEx	SettingID
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
		///��ȡ������SettingValue��
		///</summary>
		[DbField("SettingValue")]
		public	string	SettingValue
		{
			get;set;

		}

        /// <summary>
        /// ��ȡ������SettingSignID
        /// </summary>
        public string SettingSign
        {
            get;set;
        }
		#endregion


	}

}
