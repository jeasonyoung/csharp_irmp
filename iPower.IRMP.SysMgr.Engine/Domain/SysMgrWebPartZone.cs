//================================================================================
// FileName: SysMgrWebPartZone.cs
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
namespace iPower.IRMP.SysMgr.Engine.Domain
{
	///<summary>
	///tblSysMgrWebPartZoneӳ���ࡣ
	///</summary>
	[DbTable("tblSysMgrWebPartZone")]
	public class SysMgrWebPartZone
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SysMgrWebPartZone()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������ZoneID��
		///</summary>
		[DbField("ZoneID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ZoneID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ZoneName��
		///</summary>
		[DbField("ZoneName")]
		public	string	ZoneName
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������AppAuthID��
		///</summary>
		[DbField("AppAuthID")]
		public	GUIDEx	AppAuthID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ZoneMode��
		///</summary>
		[DbField("ZoneMode")]
		public	int	ZoneMode
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ZoneLength��
		///</summary>
		[DbField("ZoneLength")]
		public	int	ZoneLength
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������Description��
		///</summary>
		[DbField("Description")]
		public	string	Description
		{
			get;set;

		}

        /// <summary>
        /// ��ȡ������SystemName
        /// </summary>
        public string SystemName
        {
            get;
            set;
        }
		#endregion

	}

}
