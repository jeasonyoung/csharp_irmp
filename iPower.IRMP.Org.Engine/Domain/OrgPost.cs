//================================================================================
// FileName: OrgPost.cs
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
	///tblOrgPostӳ���ࡣ
	///</summary>
    [DbTable("tblOrgPost")]
    public class OrgPost
    {
        #region ��Ա���������캯����
        ///<summary>
        ///���캯����
        ///</summary>
        public OrgPost()
        {

        }
        #endregion
        #region ���ԡ�
        ///<summary>
        ///��ȡ������PostID��
        ///</summary>
        [DbField("PostID", DbFieldUsage.PrimaryKey)]
        public string PostID
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������ParentPostID��
        ///</summary>
        [DbField("ParentPostID")]
        public string ParentPostID
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
        ///��ȡ������RankID��
        ///</summary>
        [DbField("RankID")]
        public string RankID
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������PostSign��
        ///</summary>
        [DbField("PostSign")]
        public string PostSign
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������PostName��
        ///</summary>
        [DbField("PostName")]
        public string PostName
        {
            get;
            set;

        }

        ///<summary>
        ///��ȡ������PostDescription��
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
