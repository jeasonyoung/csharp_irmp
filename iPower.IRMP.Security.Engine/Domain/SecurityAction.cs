//================================================================================
// FileName: SecurityAction.cs
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
	///系统权限元操作类。
	///</summary>
    [Serializable]
    [DbTable("tblSecurityAction")]
	public class SecurityAction
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数。
		///</summary>
		public SecurityAction()
		{

		}
		#endregion
		#region 属性。
		///<summary>
		///获取或设置ActionID。
		///</summary>
        [XmlElement("ActionID", DataType = "string", Type = typeof(string))]
        [DbField("ActionID",DbFieldUsage.PrimaryKey)]
		public	GUIDEx	ActionID
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ActionSign。
		///</summary>
		[DbField("ActionSign")]
		public	string	ActionSign
		{
			get;set;

		}
        /// <summary>
        /// 获取或设置ActionType。
        /// </summary>
        [DbField("ActionType")]
        public int ActionType
        {
            get;
            set;
        }
			
		///<summary>
		///获取或设置ActionName。
		///</summary>
		[DbField("ActionName")]
		public	string	ActionName
		{
			get;set;

		}
			
		///<summary>
		///获取或设置ActionDescription。
		///</summary>
		[DbField("ActionDescription")]
		public	string	ActionDescription
		{
			get;set;

		}
			
		#endregion

	}

    /// <summary>
    /// 系统权限元操作集合类。
    /// </summary>
    [Serializable]
    [XmlRoot("SecurityActionCollection")]
    public class SecurityActionCollection : Collection<SecurityAction>
    {
        #region 成员变量，构造函数。
        /// <summary>
        /// 构造函数。
        /// </summary>
        public SecurityActionCollection()
        {
        }
        #endregion

        #region 索引。
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

        #region 序列化函数。
        /// <summary>
        /// 序列化当前对象。
        /// </summary>
        /// <param name="output">输出流。</param>
        public void Serializer(Stream output)
        {
            if (output != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SecurityActionCollection));
                serializer.Serialize(output, this);
            }
        }
        /// <summary>
        /// 反序列化。
        /// </summary>
        /// <param name="input">数据输入流。</param>
        /// <returns> 菜单模块工厂类。</returns>
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
