//================================================================================
// FileName: SecurityRolePost.cs
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
	///tblSecurityRolePostӳ���ࡣ
	///</summary>
	[DbTable("tblSecurityRolePost")]
	public class SecurityRolePost
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SecurityRolePost()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������RoleID��
		///</summary>
		[DbField("RoleID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	RoleID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������PostID��
		///</summary>
		[DbField("PostID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	PostID
		{
			get;set;

		}
        /// <summary>
        /// 
        /// </summary>
        [DbField("PostName")]
        public string PostName
        {
            get;
            set;
        }
		#endregion

	}

}
