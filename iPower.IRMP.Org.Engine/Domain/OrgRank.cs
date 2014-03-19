//================================================================================
// FileName: OrgRank.cs
// Desc:
// Called by
// Auth: �������ɴ����������Զ����ɡ�
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
	///tblOrgRankӳ���ࡣ
	///</summary>
	[DbTable("tblOrgRank")]
	public class OrgRank
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public OrgRank()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������RankID��
		///</summary>
		[DbField("RankID",DbFieldUsage.PrimaryKey)]
		public	string	RankID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ParentRankID��
		///</summary>
		[DbField("ParentRankID")]
        public string ParentRankID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������RankName��
		///</summary>
		[DbField("RankName")]
		public	string	RankName
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������RankDescription��
		///</summary>
		[DbField("RankDescription")]
		public	string	RankDescription
		{
			get;set;

		}
			
		#endregion

	}

}
