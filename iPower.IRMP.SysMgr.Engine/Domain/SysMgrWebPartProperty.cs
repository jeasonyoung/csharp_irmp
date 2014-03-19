//================================================================================
// FileName: SysMgrWebPartProperty.cs
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
	///tblSysMgrWebPartPropertyӳ���ࡣ
	///</summary>
	[DbTable("tblSysMgrWebPartProperty")]
	public class SysMgrWebPartProperty
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SysMgrWebPartProperty()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������WebPartID��
		///</summary>
		[DbField("WebPartID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	WebPartID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������TemplatePropertyID��
		///</summary>
		[DbField("TemplatePropertyID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	TemplatePropertyID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������PropertyValue��
		///</summary>
		[DbField("PropertyValue")]
		public	string	PropertyValue
		{
			get;set;

		}
			
		#endregion

	}

}
