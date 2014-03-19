//================================================================================
// FileName: SysMgrWebPartProperty.cs
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
	///tblSysMgrWebPartProperty映射类。
	///</summary>
	[DbTable("tblSysMgrWebPartProperty")]
	public class SysMgrWebPartProperty
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrWebPartProperty()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置WebPartID。
		///</summary>
		[DbField("WebPartID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	WebPartID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置TemplatePropertyID。
		///</summary>
		[DbField("TemplatePropertyID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	TemplatePropertyID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置PropertyValue。
		///</summary>
		[DbField("PropertyValue")]
		public	string	PropertyValue
		{
			get;set;

		}
			
		#endregion

	}

}
