//================================================================================
// FileName: SecurityAction.cs
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
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using System.Xml.Serialization;

using iPower;
using iPower.Data;
using iPower.Data.ORM;
namespace iPower.IRMP.Security.Engine.Domain
{
	///<summary>
	///ϵͳȨ��Ԫ�����ࡣ
	///</summary>
    [Serializable]
    [DbTable("tblSecurityAction")]
	public class SecurityAction
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public SecurityAction()
		{

		}
		#endregion
		#region ���ԡ�
		///<summary>
		///��ȡ������ActionID��
		///</summary>
        [XmlElement("ActionID", DataType = "string", Type = typeof(string))]
        [DbField("ActionID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ActionID
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ActionSign��
		///</summary>
		[DbField("ActionSign")]
		public	string	ActionSign
		{
			get;set;

		}
        /// <summary>
        /// ��ȡ������ActionType��
        /// </summary>
        [DbField("ActionType")]
        public int ActionType
        {
            get;
            set;
        }
			
		///<summary>
		///��ȡ������ActionName��
		///</summary>
		[DbField("ActionName")]
		public	string	ActionName
		{
			get;set;

		}
			
		///<summary>
		///��ȡ������ActionDescription��
		///</summary>
		[DbField("ActionDescription")]
		public	string	ActionDescription
		{
			get;set;

		}
			
		#endregion

	}

    /// <summary>
    /// ϵͳȨ��Ԫ���������ࡣ
    /// </summary>
    [Serializable]
    [XmlRoot("SecurityActionCollection")]
    public class SecurityActionCollection : Collection<SecurityAction>
    {
        #region ��Ա���������캯����
        /// <summary>
        /// ���캯����
        /// </summary>
        public SecurityActionCollection()
        {
        }
        #endregion

        #region ������
        /// <summary>
        ///
        /// </summary>
        /// <param name="actionID"></param>
        /// <returns></returns>
        public SecurityAction this[string actionID]
        {
            get
            {
                SecurityAction data = null;
                if (!string.IsNullOrEmpty(actionID))
                {
                    foreach (SecurityAction d in this.Items)
                    {
                        if (string.Equals(d.ActionID,actionID, StringComparison.OrdinalIgnoreCase))
                        {
                            data = d;
                            break;
                        }
                    }
                }
                return data;
            }
        }
        #endregion

        #region ���л�������
        /// <summary>
        /// ���л���ǰ����
        /// </summary>
        /// <param name="output">�������</param>
        public void Serializer(Stream output)
        {
            if (output != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SecurityActionCollection));
                serializer.Serialize(output, this);
            }
        }
        /// <summary>
        /// �����л���
        /// </summary>
        /// <param name="input">������������</param>
        /// <returns> �˵�ģ�鹤���ࡣ</returns>
        public static SecurityActionCollection DeSerializer(Stream input)
        {
            SecurityActionCollection f = null;
            if (input != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SecurityActionCollection));
                f = serializer.Deserialize(input) as SecurityActionCollection;
            }
            return f;
        }
        #endregion
    }
}
