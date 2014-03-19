//================================================================================
// FileName: SysMgrWebPartPersonal.cs
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
	///tblSysMgrWebPartPersonal映射类。
	///</summary>
	[DbTable("tblSysMgrWebPartPersonal")]
	public class SysMgrWebPartPersonal
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrWebPartPersonal()
		{

		}
		#endregion

		#region 属性。
		///<summary>
		///获取或设置PersonalWebPartID。
		///</summary>
		[DbField("PersonalWebPartID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	PersonalWebPartID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置WebPartID。
		///</summary>
		[DbField("WebPartID")]
		public	GUIDEx	WebPartID
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
		///获取或设置ZoneID。
		///</summary>
		[DbField("ZoneID")]
		public	GUIDEx	ZoneID
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

        /// <summary>
        /// 显示或设置WebPartName
        /// </summary>
        public string WebPartName
        {
            get;set;
        }
        /// <summary>
        /// 显示或设置ZoneName
        /// </summary>
        public string ZoneName
        {
            get;
            set;
        }
		#endregion
	}

}
