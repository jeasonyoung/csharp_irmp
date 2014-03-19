//================================================================================
// FileName: SecurityRight.cs
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
namespace iPower.IRMP.Security.Engine.Domain
{
	///<summary>
	///tblSecurityRight映射类。
	///</summary>
	[DbTable("tblSecurityRight")]
	public class SecurityRight
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public SecurityRight()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置RightID。
		///</summary>
		[DbField("RightID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	RightID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ModuleID。
		///</summary>
		[DbField("ModuleID")]
		public	GUIDEx	ModuleID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ActionID。
		///</summary>
		[DbField("ActionID")]
		public	GUIDEx	ActionID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置RightName。
		///</summary>
		[DbField("RightName")]
		public	string	RightName
		{
			get;set;

		}
			
		#endregion

        /// <summary>
        /// 获取或设置所属系统ID。
        /// </summary>
        public GUIDEx SystemID { get; set; }
	}

}
