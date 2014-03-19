//================================================================================
// FileName: SysMgrSetting.cs
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
	///tblSysMgrSettingӳ���ࡣ
	///</summary>
	[DbTable("tblSysMgrSetting")]
	public class SysMgrSetting
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SysMgrSetting()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������SettingID��
		///</summary>
		[DbField("SettingID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	SettingID
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

        /// <summary>
        /// ��ȡ������SystemName
        /// </summary>        
        public string SystemName
        {
            get;set;
        }
			
		///<summary>
		///��ȡ������SettingType��
		///</summary>
		[DbField("SettingType")]
		public	int	SettingType
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������SettingSign��
		///</summary>
		[DbField("SettingSign")]
		public	string	SettingSign
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������DefaultValue��
		///</summary>
		[DbField("DefaultValue")]
		public	string	DefaultValue
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
			
		#endregion

	}

}
