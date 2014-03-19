//================================================================================
// FileName: SysMgrSetting.cs
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
	///tblSysMgrSetting映射类。
	///</summary>
	[DbTable("tblSysMgrSetting")]
	public class SysMgrSetting
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrSetting()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置SettingID。
		///</summary>
		[DbField("SettingID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	SettingID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置AppAuthID。
		///</summary>
		[DbField("AppAuthID")]
		public	GUIDEx	AppAuthID
		{
			get;set;

		}

        /// <summary>
        /// 获取或设置SystemName
        /// </summary>        
        public string SystemName
        {
            get;set;
        }
			
		///<summary>
		///获取或设置SettingType。
		///</summary>
		[DbField("SettingType")]
		public	int	SettingType
		{
			get;set;

		}
			
		///<summary>
		///获取或设置SettingSign。
		///</summary>
		[DbField("SettingSign")]
		public	string	SettingSign
		{
			get;set;

		}
			
		///<summary>
		///获取或设置DefaultValue。
		///</summary>
		[DbField("DefaultValue")]
		public	string	DefaultValue
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
			
		#endregion

	}

}
