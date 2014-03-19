//================================================================================
// FileName: SysMgrRegWebPartTemplateProperty.cs
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
	///tblSysMgrRegWebPartTemplateProperty映射类。
	///</summary>
	[Serializable]
    [DbTable("tblSysMgrRegWebPartTemplateProperty")]
	public class SysMgrRegWebPartTemplateProperty
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrRegWebPartTemplateProperty()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置TemplatePropertyID。
		///</summary>
		[DbField("TemplatePropertyID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	TemplatePropertyID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置WebPartTemplateID。
		///</summary>
		[DbField("WebPartTemplateID")]
		public	GUIDEx	WebPartTemplateID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置TemplatePropertyName。
		///</summary>
		[DbField("TemplatePropertyName")]
		public	string	TemplatePropertyName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置TemplateDefaultValue。
		///</summary>
		[DbField("TemplateDefaultValue")]
		public	string	TemplateDefaultValue
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
