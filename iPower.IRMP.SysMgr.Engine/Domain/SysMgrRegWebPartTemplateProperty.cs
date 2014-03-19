//================================================================================
// FileName: SysMgrRegWebPartTemplateProperty.cs
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
	///tblSysMgrRegWebPartTemplatePropertyӳ���ࡣ
	///</summary>
	[Serializable]
    [DbTable("tblSysMgrRegWebPartTemplateProperty")]
	public class SysMgrRegWebPartTemplateProperty
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SysMgrRegWebPartTemplateProperty()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������TemplatePropertyID��
		///</summary>
		[DbField("TemplatePropertyID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	TemplatePropertyID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������WebPartTemplateID��
		///</summary>
		[DbField("WebPartTemplateID")]
		public	GUIDEx	WebPartTemplateID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������TemplatePropertyName��
		///</summary>
		[DbField("TemplatePropertyName")]
		public	string	TemplatePropertyName
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������TemplateDefaultValue��
		///</summary>
		[DbField("TemplateDefaultValue")]
		public	string	TemplateDefaultValue
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
