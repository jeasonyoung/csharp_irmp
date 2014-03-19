//================================================================================
// FileName: SSOTicket.cs
// Desc:
// Called by
// Auth: 本代码由代码生成器自动生成。
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
	///tblSSOTicket映射类。
	///</summary>
	[DbTable("tblSSOTicket")]
	public class SSOTicket
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public SSOTicket()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置Token。
		///</summary>
		[DbField("Token",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	Token
		{
			get;set;

		}
			
		///<summary>
		///获取或设置UserData。
		///</summary>
		[DbField("UserData")]
		public	string	UserData
		{
			get;set;

		}
			
		///<summary>
		///获取或设置IssueDate。
		///</summary>
		[DbField("IssueDate")]
		public	DateTime	IssueDate
		{
			get;set;

		}
			
		///<summary>
		///获取或设置Expiration。
		///</summary>
		[DbField("Expiration")]
		public	DateTime	Expiration
		{
			get;set;

		}
			
		///<summary>
		///获取或设置IssueClientIP。
		///</summary>
		[DbField("IssueClientIP")]
		public	string	IssueClientIP
		{
			get;set;

		}
			
		///<summary>
		///获取或设置RenewalCount。
		///</summary>
		[DbField("RenewalCount")]
		public	int	RenewalCount
		{
			get;set;

		}
			
		///<summary>
		///获取或设置LastRenewalIP。
		///</summary>
		[DbField("LastRenewalIP")]
		public	string	LastRenewalIP
		{
			get;set;

		}
			
		///<summary>
		///获取或设置HasValid。
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
