//================================================================================
// FileName: SysMgrRegWebPartTemplate.cs
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
	///tblSysMgrRegWebPartTemplate映射类。
	///</summary>
	[DbTable("tblSysMgrRegWebPartTemplate")]
	public class SysMgrRegWebPartTemplate
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrRegWebPartTemplate()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置WebPartTemplateID。
		///</summary>
		[DbField("WebPartTemplateID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	WebPartTemplateID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置WebPartTemplateName。
		///</summary>
		[DbField("WebPartTemplateName")]
		public	string	WebPartTemplateName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置WebPartTemplatePath。
		///</summary>
		[DbField("WebPartTemplatePath")]
		public	string	WebPartTemplatePath
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
