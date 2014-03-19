//================================================================================
// FileName: SysMgrWebPart.cs
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
	///tblSysMgrWebPart映射类。
	///</summary>
	[DbTable("tblSysMgrWebPart")]
	public class SysMgrWebPart
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public SysMgrWebPart()
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
		///获取或设置WebPartName。
		///</summary>
		[DbField("WebPartName")]
		public	string	WebPartName
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
        /// <summary>
        /// 
        /// </summary>
        public string WebPartTemplateName
        {
            get;
            set;
        }
			
		///<summary>
		///获取或设置DataAssemblyName。
		///</summary>
		[DbField("DataAssemblyName")]
		public	string	DataAssemblyName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置DataClassName。
		///</summary>
		[DbField("DataClassName")]
		public	string	DataClassName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置WebPartStatus。
		///</summary>
		[DbField("WebPartStatus")]
		public	int	WebPartStatus
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
