//================================================================================
// FileName: SysMgrSettingPersonal.cs
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
	///tblSysMgrSettingPersonal映射类。
	///</summary>
	[DbTable("tblSysMgrSettingPersonal")]
	public class SysMgrSettingPersonal
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrSettingPersonal()
		{

		}
		#endregion

		#region 属性。
		///<summary>
		///获取或设置PersonalSettingID。
		///</summary>
		[DbField("PersonalSettingID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	PersonalSettingID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置SettingID。
		///</summary>
		[DbField("SettingID")]
		public	GUIDEx	SettingID
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
		///获取或设置SettingValue。
		///</summary>
		[DbField("SettingValue")]
		public	string	SettingValue
		{
			get;set;

		}

        /// <summary>
        /// 获取或设置SettingSignID
        /// </summary>
        public string SettingSign
        {
            get;set;
        }
		#endregion


	}

}
