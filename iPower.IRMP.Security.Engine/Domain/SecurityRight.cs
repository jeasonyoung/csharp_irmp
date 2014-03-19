//================================================================================
// FileName: SecurityRight.cs
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
	///tblSecurityRightӳ���ࡣ
	///</summary>
	[DbTable("tblSecurityRight")]
	public class SecurityRight
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SecurityRight()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������RightID��
		///</summary>
		[DbField("RightID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	RightID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ModuleID��
		///</summary>
		[DbField("ModuleID")]
		public	GUIDEx	ModuleID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ActionID��
		///</summary>
		[DbField("ActionID")]
		public	GUIDEx	ActionID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������RightName��
		///</summary>
		[DbField("RightName")]
		public	string	RightName
		{
			get;set;

		}
			
		#endregion

        /// <summary>
        /// ��ȡ����������ϵͳID��
        /// </summary>
        public GUIDEx SystemID { get; set; }
	}

}
