//================================================================================
// FileName: SysMgrLinks.cs
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
	///tblSysMgrLinks映射类。
	///</summary>
	[DbTable("tblSysMgrLinks")]
	public class SysMgrLinks
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrLinks()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置LinkID。
		///</summary>
		[DbField("LinkID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	LinkID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置LinkName。
		///</summary>
		[DbField("LinkName")]
		public	string	LinkName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置LinkUrl。
		///</summary>
		[DbField("LinkUrl")]
		public	string	LinkUrl
		{
			get;set;

		}
			
		///<summary>
		///获取或设置LinkTarget。
		///</summary>
		[DbField("LinkTarget")]
		public	int	LinkTarget
		{
			get;set;

		}
			
		///<summary>
		///获取或设置LinkStatus。
		///</summary>
		[DbField("LinkStatus")]
		public	int	LinkStatus
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ImageUrlID。
		///</summary>
		[DbField("ImageUrlID")]
		public	GUIDEx	ImageUrlID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置EmployeeID。
		///</summary>
		[DbField("EmployeeID")]
		public	GUIDEx	EmployeeID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置EmployeeName。
		///</summary>
		[DbField("EmployeeName")]
		public	string	EmployeeName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置Description。
		///</summary>
		[DbField("Description")]
		public	string	Description
		{
			get;set;

		}
			
		///<summary>
		///获取或设置OrderNo。
		///</summary>
		[DbField("OrderNo")]
		public	int	OrderNo
		{
			get;set;

		}
			
		#endregion

	}

}
