//================================================================================
// FileName: OrgDepartment.cs
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

using iPower.IRMP;
namespace iPower.IRMP.Org.Engine.Domain
{
	///<summary>
	///tblOrgDepartmentӳ���ࡣ
	///</summary>
    [DbTable("tblOrgDepartment")]
    public class OrgDepartment
    {
        #region ��Ա���������캯����
        ///<summary>
        ///���캯����
        ///</summary>
        public OrgDepartment()
        {

        }
        #endregion
        #region ���ԡ�
        ///<summary>
        ///��ȡ������DepartmentID��
        ///</summary>
        [DbField("DepartmentID", DbFieldUsage.PrimaryKey)]
        public string DepartmentID
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������ParentDepartmentID��
        ///</summary>
        [DbField("ParentDepartmentID")]
        public string ParentDepartmentID
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������DepartmentSign��
        ///</summary>
        [DbField("DepartmentSign")]
        public string DepartmentSign
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������DepartmentName��
        ///</summary>
        [DbField("DepartmentName")]
        public string DepartmentName
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������DepartmentDescription��
        ///</summary>
        [DbField("DepartmentDescription")]
        public string DepartmentDescription
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������DepartmentOrder��
        ///</summary>
        [DbField("DepartmentOrder")]
        public int DepartmentOrder
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������DepartmentLevel��
        ///</summary>
        [DbField("DepartmentLevel")]
        public int DepartmentLevel
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������DepartmentStatus��
        ///</summary>
        [DbField("DepartmentStatus")]
        public int DepartmentStatus
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������DepartmentAddress��
        ///</summary>
        [DbField("DepartmentAddress")]
        public string DepartmentAddress
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������DepartmentFax��
        ///</summary>
        [DbField("DepartmentFax")]
        public string DepartmentFax
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������DepartmentTel��
        ///</summary>
        [DbField("DepartmentTel")]
        public string DepartmentTel
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������DepartmentLeader��
        ///</summary>
        [DbField("DepartmentLeader")]
        public string DepartmentLeader
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������DepartmentCapability��
        ///</summary>
        [DbField("DepartmentCapability")]
        public int DepartmentCapability
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������DepartmentEx1��
        ///</summary>
        [DbField("DepartmentEx1")]
        public string DepartmentEx1
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������DepartmentEx2��
        ///</summary>
        [DbField("DepartmentEx2")]
        public string DepartmentEx2
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������DepartmentEx3��
        ///</summary>
        [DbField("DepartmentEx3")]
        public string DepartmentEx3
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������DepartmentEx4��
        ///</summary>
        [DbField("DepartmentEx4")]
        public string DepartmentEx4
        {
            get;
            set;

        }

        #endregion

    }

}
