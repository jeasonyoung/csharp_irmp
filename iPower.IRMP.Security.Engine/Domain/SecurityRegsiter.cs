//================================================================================
// FileName: SecurityRegsiter.cs
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
	///tblSecurityRegsiterӳ���ࡣ
	///</summary>
	[DbTable("tblSecurityRegsiter")]
	public class SecurityRegsiter
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SecurityRegsiter()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������SystemID��
		///</summary>
		[DbField("SystemID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	SystemID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ParentSystemID��
		///</summary>
		[DbField("ParentSystemID")]
		public	GUIDEx	ParentSystemID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������SystemSign��
		///</summary>
		[DbField("SystemSign")]
		public	string	SystemSign
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������SystemName��
		///</summary>
		[DbField("SystemName")]
		public	string	SystemName
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������SystemURL��
		///</summary>
		[DbField("SystemURL")]
		public	string	SystemURL
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������SecurityURL��
		///</summary>
		[DbField("SecurityURL")]
		public	string	SecurityURL
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������PatchURL��
		///</summary>
		[DbField("PatchURL")]
		public	string	PatchURL
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ModuleConfigURL��
		///</summary>
		[DbField("ModuleConfigURL")]
		public	string	ModuleConfigURL
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������SystemType��
		///</summary>
		[DbField("SystemType")]
		public	int	SystemType
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������SystemStatus��
		///</summary>
		[DbField("SystemStatus")]
		public	int	SystemStatus
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������SystemDescription��
		///</summary>
		[DbField("SystemDescription")]
		public	string	SystemDescription
		{
			get;set;

		}
			
		#endregion

	}

}
