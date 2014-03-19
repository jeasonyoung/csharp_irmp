//================================================================================
// FileName: SSOTicket.cs
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
	///tblSSOTicketӳ���ࡣ
	///</summary>
	[DbTable("tblSSOTicket")]
	public class SSOTicket
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SSOTicket()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������Token��
		///</summary>
		[DbField("Token",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	Token
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������UserData��
		///</summary>
		[DbField("UserData")]
		public	string	UserData
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������IssueDate��
		///</summary>
		[DbField("IssueDate")]
		public	DateTime	IssueDate
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������Expiration��
		///</summary>
		[DbField("Expiration")]
		public	DateTime	Expiration
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������IssueClientIP��
		///</summary>
		[DbField("IssueClientIP")]
		public	string	IssueClientIP
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������RenewalCount��
		///</summary>
		[DbField("RenewalCount")]
		public	int	RenewalCount
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������LastRenewalIP��
		///</summary>
		[DbField("LastRenewalIP")]
		public	string	LastRenewalIP
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������HasValid��
		///</summary>
        public bool HasValid
        {
            get
            {
                return this.Expiration > DateTime.Now;
            }
        }
			
		#endregion

	}

}
