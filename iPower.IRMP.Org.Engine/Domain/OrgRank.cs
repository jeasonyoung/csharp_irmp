//================================================================================
// FileName: OrgRank.cs
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

using iPower.IRMP;
namespace iPower.IRMP.Org.Engine.Domain
{
	///<summary>
	///tblOrgRank映射类。
	///</summary>
	[DbTable("tblOrgRank")]
	public class OrgRank
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public OrgRank()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置RankID。
		///</summary>
		[DbField("RankID",DbFieldUsage.PrimaryKey)]
		public	string	RankID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ParentRankID。
		///</summary>
		[DbField("ParentRankID")]
        public string ParentRankID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置RankName。
		///</summary>
		[DbField("RankName")]
		public	string	RankName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置RankDescription。
		///</summary>
		[DbField("RankDescription")]
		public	string	RankDescription
		{
			get;set;

		}
			
		#endregion

	}

}
