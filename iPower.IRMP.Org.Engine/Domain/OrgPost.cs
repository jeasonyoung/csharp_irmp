//================================================================================
// FileName: OrgPost.cs
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
	///tblOrgPost映射类。
	///</summary>
    [DbTable("tblOrgPost")]
    public class OrgPost
    {
        #region 成员变量，构造函数。
        ///<summary>
        ///构造函数。
        ///</summary>
        public OrgPost()
        {

        }
        #endregion
        #region 属性。
        ///<summary>
        ///获取或设置PostID。
        ///</summary>
        [DbField("PostID", DbFieldUsage.PrimaryKey)]
        public string PostID
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置ParentPostID。
        ///</summary>
        [DbField("ParentPostID")]
        public string ParentPostID
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
        ///获取或设置RankID。
        ///</summary>
        [DbField("RankID")]
        public string RankID
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置PostSign。
        ///</summary>
        [DbField("PostSign")]
        public string PostSign
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置PostName。
        ///</summary>
        [DbField("PostName")]
        public string PostName
        {
            get;
            set;

        }

        ///<summary>
        ///获取或设置PostDescription。
        ///</summary>
        [DbField("PostDescription")]
        public string PostDescription
        {
            get;
            set;

        }

        #endregion

    }

}
