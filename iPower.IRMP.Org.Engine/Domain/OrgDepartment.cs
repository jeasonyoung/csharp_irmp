//================================================================================
// FileName: OrgDepartment.cs
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

using iPower.IRMP;
namespace iPower.IRMP.Org.Engine.Domain
{
	///<summary>
	///tblOrgDepartment映射类。
	///</summary>
    [DbTable("tblOrgDepartment")]
    public class OrgDepartment
    {
        #region 成员变量，构造函数。
        ///<summary>
        ///构造函数。
        ///</summary>
        public OrgDepartment()
        {

        }
        #endregion
        #region 属性。
        ///<summary>
        ///获取或设置DepartmentID。
        ///</summary>
        [DbField("DepartmentID", DbFieldUsage.PrimaryKey)]
        public string DepartmentID
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置ParentDepartmentID。
        ///</summary>
        [DbField("ParentDepartmentID")]
        public string ParentDepartmentID
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置DepartmentSign。
        ///</summary>
        [DbField("DepartmentSign")]
        public string DepartmentSign
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置DepartmentName。
        ///</summary>
        [DbField("DepartmentName")]
        public string DepartmentName
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置DepartmentDescription。
        ///</summary>
        [DbField("DepartmentDescription")]
        public string DepartmentDescription
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置DepartmentOrder。
        ///</summary>
        [DbField("DepartmentOrder")]
        public int DepartmentOrder
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置DepartmentLevel。
        ///</summary>
        [DbField("DepartmentLevel")]
        public int DepartmentLevel
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置DepartmentStatus。
        ///</summary>
        [DbField("DepartmentStatus")]
        public int DepartmentStatus
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置DepartmentAddress。
        ///</summary>
        [DbField("DepartmentAddress")]
        public string DepartmentAddress
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置DepartmentFax。
        ///</summary>
        [DbField("DepartmentFax")]
        public string DepartmentFax
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置DepartmentTel。
        ///</summary>
        [DbField("DepartmentTel")]
        public string DepartmentTel
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置DepartmentLeader。
        ///</summary>
        [DbField("DepartmentLeader")]
        public string DepartmentLeader
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置DepartmentCapability。
        ///</summary>
        [DbField("DepartmentCapability")]
        public int DepartmentCapability
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置DepartmentEx1。
        ///</summary>
        [DbField("DepartmentEx1")]
        public string DepartmentEx1
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置DepartmentEx2。
        ///</summary>
        [DbField("DepartmentEx2")]
        public string DepartmentEx2
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置DepartmentEx3。
        ///</summary>
        [DbField("DepartmentEx3")]
        public string DepartmentEx3
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置DepartmentEx4。
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
