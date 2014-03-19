//================================================================================
// FileName: SecurityRoleDepartment.cs
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
	///tblSecurityRoleDepartmentӳ���ࡣ
	///</summary>
	[DbTable("tblSecurityRoleDepartment")]
	public class SecurityRoleDepartment
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SecurityRoleDepartment()
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
		///��ȡ������DepartmentID��
		///</summary>
		[DbField("DepartmentID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	DepartmentID
		{
			get;set;

		}
        /// <summary>
        /// 
        /// </summary>
        [DbField("DepartmentName")]
        public string DepartmentName
        {
            get;
            set;
        }
		#endregion

	}

}
