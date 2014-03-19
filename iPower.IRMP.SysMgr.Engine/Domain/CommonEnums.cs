//================================================================================
// FileName: CommonEnums.cs
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
	///tblCommonEnums映射类。
	///</summary>
	[DbTable("tblCommonEnums")]
	public class CommonEnums
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public CommonEnums()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置EnumName。
		///</summary>
		[DbField("EnumName",DbFieldUsage.PrimaryKey)]
		public	string	FullEnumName
		{
			get;set;

		}
        /// <summary>
        /// 获取枚举名称。
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
		///获取或设置Member。
		///</summary>
		[DbField("Member",DbFieldUsage.PrimaryKey)]
		public	string	Member
		{
			get;set;

		}
			
		///<summary>
		///获取或设置MemberName。
		///</summary>
		[DbField("MemberName")]
		public	string	MemberName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置IntValue。
		///</summary>
		[DbField("IntValue")]
		public	int	IntValue
		{
			get;set;

		}
			
		///<summary>
		///获取或设置OrderNo。
		///</summary>
		[DbField("OrderNo")]
		public	int	OrderNo
		{
			get;set;

		}
			
		#endregion

	}

}
