//================================================================================
// FileName: SysMgrRegWebPartTemplate.cs
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
	///tblSysMgrRegWebPartTemplateӳ���ࡣ
	///</summary>
	[DbTable("tblSysMgrRegWebPartTemplate")]
	public class SysMgrRegWebPartTemplate
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SysMgrRegWebPartTemplate()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������WebPartTemplateID��
		///</summary>
		[DbField("WebPartTemplateID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	WebPartTemplateID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������WebPartTemplateName��
		///</summary>
		[DbField("WebPartTemplateName")]
		public	string	WebPartTemplateName
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������WebPartTemplatePath��
		///</summary>
		[DbField("WebPartTemplatePath")]
		public	string	WebPartTemplatePath
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
