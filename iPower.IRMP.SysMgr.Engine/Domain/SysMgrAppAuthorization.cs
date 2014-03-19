//================================================================================
// FileName: SysMgrAppAuthorization.cs
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
	///tblSysMgrAppAuthorizationӳ���ࡣ
	///</summary>
	[DbTable("tblSysMgrAppAuthorization")]
	public class SysMgrAppAuthorization
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SysMgrAppAuthorization()
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
		///��ȡ������SystemID��
		///</summary>
		[DbField("SystemID")]
		public	GUIDEx	SystemID
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
		///��ȡ������AuthPwd��
		///</summary>
		[DbField("AuthPwd")]
		public	string	AuthPwd
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
