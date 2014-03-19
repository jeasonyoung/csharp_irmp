//================================================================================
// FileName: OrgEmployee.cs
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
using iPower.Cryptography;

using iPower.IRMP;
namespace iPower.IRMP.Org.Engine.Domain
{
	///<summary>
	///tblOrgEmployeeӳ���ࡣ
	///</summary>
    [DbTable("tblOrgEmployee")]
    public class OrgEmployee
    {
        #region ��Ա���������캯����
        string password1;
        ///<summary>
        ///���캯����
        ///</summary>
        public OrgEmployee()
        {

        }
        #endregion
        #region ���ԡ�
        ///<summary>
        ///��ȡ������EmployeeID��
        ///</summary>
        [DbField("EmployeeID", DbFieldUsage.PrimaryKey)]
        public string EmployeeID
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������DepartmentID��
        ///</summary>
        [DbField("DepartmentID")]
        public string DepartmentID
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������PostID��
        ///</summary>
        [DbField("PostID")]
        public string PostID
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������EmployeeSign��
        ///</summary>
        [DbField("EmployeeSign")]
        public string EmployeeSign
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������EmployeeName��
        ///</summary>
        [DbField("EmployeeName")]
        public string EmployeeName
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������NickName��
        ///</summary>
        [DbField("NickName")]
        public string NickName
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������EmployeePassword��
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
        ///��ȡ������EmployeePassword2��
        ///</summary>
        [DbField("EmployeePassword2", DbFieldUsage.EmptyOrNullNotUpdate)]
        public string EmployeePassword2
        {
            get;
            set;
        }

        ///<summary>
        ///��ȡ������PasswordDate��
        ///</summary>
        [DbField("PasswordDate", DbFieldUsage.EmptyOrNullNotUpdate)]
        public DateTime PasswordDate
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������PasswordDate2��
        ///</summary>
        [DbField("PasswordDate2", DbFieldUsage.EmptyOrNullNotUpdate)]
        public DateTime PasswordDate2
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������PasswordHistory��
        ///</summary>
        [DbField("PasswordHistory")]
        public string PasswordHistory
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������Gender��
        ///</summary>
        [DbField("Gender")]
        public int Gender
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������Birthday��
        ///</summary>
        [DbField("Birthday")]
        public string Birthday
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������Nation��
        ///</summary>
        [DbField("Nation")]
        public string Nation
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������IdentityCard��
        ///</summary>
        [DbField("IdentityCard")]
        public string IdentityCard
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������MSNNO��
        ///</summary>
        [DbField("MSNNO")]
        public string MSNNO
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������QQNO��
        ///</summary>
        [DbField("QQNO")]
        public string QQNO
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������EmployeeDescription��
        ///</summary>
        [DbField("EmployeeDescription")]
        public string EmployeeDescription
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������EmployeeStatus��
        ///</summary>
        [DbField("EmployeeStatus")]
        public int EmployeeStatus
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������CardID��
        ///</summary>
        [DbField("CardID")]
        public string CardID
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������Email��
        ///</summary>
        [DbField("Email")]
        public string Email
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������MobileNo��
        ///</summary>
        [DbField("MobileNo")]
        public string MobileNo
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������WorkTelNo��
        ///</summary>
        [DbField("WorkTelNo")]
        public string WorkTelNo
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������Address��
        ///</summary>
        [DbField("Address")]
        public string Address
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������OrderNo��
        ///</summary>
        [DbField("OrderNo")]
        public int OrderNo
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������EmployeeEx1��
        ///</summary>
        [DbField("EmployeeEx1")]
        public string EmployeeEx1
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������EmployeeEx2��
        ///</summary>
        [DbField("EmployeeEx2")]
        public string EmployeeEx2
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������EmployeeEx3��
        ///</summary>
        [DbField("EmployeeEx3")]
        public string EmployeeEx3
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������EmployeeEx4��
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
