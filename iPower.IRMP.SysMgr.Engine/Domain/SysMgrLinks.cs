//================================================================================
// FileName: SysMgrLinks.cs
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
	///tblSysMgrLinksӳ���ࡣ
	///</summary>
	[DbTable("tblSysMgrLinks")]
	public class SysMgrLinks
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SysMgrLinks()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������LinkID��
		///</summary>
		[DbField("LinkID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	LinkID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������LinkName��
		///</summary>
		[DbField("LinkName")]
		public	string	LinkName
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������LinkUrl��
		///</summary>
		[DbField("LinkUrl")]
		public	string	LinkUrl
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������LinkTarget��
		///</summary>
		[DbField("LinkTarget")]
		public	int	LinkTarget
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������LinkStatus��
		///</summary>
		[DbField("LinkStatus")]
		public	int	LinkStatus
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ImageUrlID��
		///</summary>
		[DbField("ImageUrlID")]
		public	GUIDEx	ImageUrlID
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
		///��ȡ������Description��
		///</summary>
		[DbField("Description")]
		public	string	Description
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
			
		#endregion

	}

}
