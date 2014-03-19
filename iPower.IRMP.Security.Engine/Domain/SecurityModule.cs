//================================================================================
// FileName: SecurityModule.cs
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
	///tblSecurityModuleӳ���ࡣ
	///</summary>
	[DbTable("tblSecurityModule")]
	public class SecurityModule
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SecurityModule()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������ModuleID��
		///</summary>
		[DbField("ModuleID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ModuleID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ParentModuleID��
		///</summary>
		[DbField("ParentModuleID")]
		public	GUIDEx	ParentModuleID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������SystemID��
		///</summary>
		[DbField("SystemID")]
		public	GUIDEx	SystemID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ModuleName��
		///</summary>
		[DbField("ModuleName")]
		public	string	ModuleName
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
			
		///<summary>
		///��ȡ������ModuleStatus��
		///</summary>
		[DbField("ModuleStatus")]
		public	int	ModuleStatus
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ModuleDescription��
		///</summary>
		[DbField("ModuleDescription")]
		public	string	ModuleDescription
		{
			get;set;

		}
			
		#endregion

	}

}
