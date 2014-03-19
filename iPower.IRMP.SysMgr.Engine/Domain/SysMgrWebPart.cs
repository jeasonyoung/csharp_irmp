//================================================================================
// FileName: SysMgrWebPart.cs
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
	///tblSysMgrWebPartӳ���ࡣ
	///</summary>
	[DbTable("tblSysMgrWebPart")]
	public class SysMgrWebPart
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SysMgrWebPart()
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
		///��ȡ������WebPartName��
		///</summary>
		[DbField("WebPartName")]
		public	string	WebPartName
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
        /// <summary>
        /// 
        /// </summary>
        public string WebPartTemplateName
        {
            get;
            set;
        }
			
		///<summary>
		///��ȡ������DataAssemblyName��
		///</summary>
		[DbField("DataAssemblyName")]
		public	string	DataAssemblyName
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������DataClassName��
		///</summary>
		[DbField("DataClassName")]
		public	string	DataClassName
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������WebPartStatus��
		///</summary>
		[DbField("WebPartStatus")]
		public	int	WebPartStatus
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
