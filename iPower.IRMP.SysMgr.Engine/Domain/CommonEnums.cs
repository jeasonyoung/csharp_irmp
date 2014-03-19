//================================================================================
// FileName: CommonEnums.cs
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
	///tblCommonEnumsӳ���ࡣ
	///</summary>
	[DbTable("tblCommonEnums")]
	public class CommonEnums
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public CommonEnums()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������EnumName��
		///</summary>
		[DbField("EnumName",DbFieldUsage.PrimaryKey)]
		public	string	FullEnumName
		{
			get;set;

		}
        /// <summary>
        /// ��ȡö�����ơ�
        /// </summary>
        public string EnumName
        {
            get
            {
                string fullName = this.FullEnumName;
                if (!string.IsNullOrEmpty(fullName))
                {
                    string[] arr = fullName.Split('.');
                    if (arr != null && arr.Length > 0)
                        return arr[arr.Length - 1];
                }
                return string.Empty;
            }
        }
			
		///<summary>
		///��ȡ������Member��
		///</summary>
		[DbField("Member",DbFieldUsage.PrimaryKey)]
		public	string	Member
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������MemberName��
		///</summary>
		[DbField("MemberName")]
		public	string	MemberName
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������IntValue��
		///</summary>
		[DbField("IntValue")]
		public	int	IntValue
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������OrderNo��
		///</summary>
		[DbField("OrderNo")]
		public	int	OrderNo
		{
			get;set;

		}
			
		#endregion

	}

}
