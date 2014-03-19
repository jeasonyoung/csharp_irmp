//================================================================================
// FileName: OrgEmployee.cs
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
using iPower.Cryptography;

using iPower.IRMP;
namespace iPower.IRMP.Org.Engine.Domain
{
	///<summary>
	///tblOrgEmployee映射类。
	///</summary>
    [DbTable("tblOrgEmployee")]
    public class OrgEmployee
    {
        #region 成员变量，构造函数。
        string password1;
        ///<summary>
        ///构造函数。
        ///</summary>
        public OrgEmployee()
        {

        }
        #endregion
        #region 属性。
        ///<summary>
        ///获取或设置EmployeeID。
        ///</summary>
        [DbField("EmployeeID", DbFieldUsage.PrimaryKey)]
        public string EmployeeID
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置DepartmentID。
        ///</summary>
        [DbField("DepartmentID")]
        public string DepartmentID
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置PostID。
        ///</summary>
        [DbField("PostID")]
        public string PostID
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置EmployeeSign。
        ///</summary>
        [DbField("EmployeeSign")]
        public string EmployeeSign
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置EmployeeName。
        ///</summary>
        [DbField("EmployeeName")]
        public string EmployeeName
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置NickName。
        ///</summary>
        [DbField("NickName")]
        public string NickName
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置EmployeePassword。
        ///</summary>
        [DbField("EmployeePassword", DbFieldUsage.EmptyOrNullNotUpdate)]
        public string EmployeePassword
        {
            get { return this.password1; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.password1 = HashCrypto.Hash(value, "md5");
                }
            }
        }

        ///<summary>
        ///获取或设置EmployeePassword2。
        ///</summary>
        [DbField("EmployeePassword2", DbFieldUsage.EmptyOrNullNotUpdate)]
        public string EmployeePassword2
        {
            get;
            set;
        }

        ///<summary>
        ///获取或设置PasswordDate。
        ///</summary>
        [DbField("PasswordDate", DbFieldUsage.EmptyOrNullNotUpdate)]
        public DateTime PasswordDate
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置PasswordDate2。
        ///</summary>
        [DbField("PasswordDate2", DbFieldUsage.EmptyOrNullNotUpdate)]
        public DateTime PasswordDate2
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置PasswordHistory。
        ///</summary>
        [DbField("PasswordHistory")]
        public string PasswordHistory
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置Gender。
        ///</summary>
        [DbField("Gender")]
        public int Gender
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置Birthday。
        ///</summary>
        [DbField("Birthday")]
        public string Birthday
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置Nation。
        ///</summary>
        [DbField("Nation")]
        public string Nation
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置IdentityCard。
        ///</summary>
        [DbField("IdentityCard")]
        public string IdentityCard
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置MSNNO。
        ///</summary>
        [DbField("MSNNO")]
        public string MSNNO
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置QQNO。
        ///</summary>
        [DbField("QQNO")]
        public string QQNO
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置EmployeeDescription。
        ///</summary>
        [DbField("EmployeeDescription")]
        public string EmployeeDescription
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置EmployeeStatus。
        ///</summary>
        [DbField("EmployeeStatus")]
        public int EmployeeStatus
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置CardID。
        ///</summary>
        [DbField("CardID")]
        public string CardID
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置Email。
        ///</summary>
        [DbField("Email")]
        public string Email
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置MobileNo。
        ///</summary>
        [DbField("MobileNo")]
        public string MobileNo
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置WorkTelNo。
        ///</summary>
        [DbField("WorkTelNo")]
        public string WorkTelNo
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置Address。
        ///</summary>
        [DbField("Address")]
        public string Address
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置OrderNo。
        ///</summary>
        [DbField("OrderNo")]
        public int OrderNo
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置EmployeeEx1。
        ///</summary>
        [DbField("EmployeeEx1")]
        public string EmployeeEx1
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置EmployeeEx2。
        ///</summary>
        [DbField("EmployeeEx2")]
        public string EmployeeEx2
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置EmployeeEx3。
        ///</summary>
        [DbField("EmployeeEx3")]
        public string EmployeeEx3
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置EmployeeEx4。
        ///</summary>
        [DbField("EmployeeEx4")]
        public string EmployeeEx4
        {
            get;
            set;

        }

        #endregion

    }

}
