//================================================================================
// FileName: SysMgrWebPartPropertyEntity.cs
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
using System.Collections.Specialized;
using System.Text;
using System.Data;
	
using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.DataSource;
using iPower.Platform.Engine.Persistence;
using iPower.IRMP.SysMgr.Engine.Domain;
namespace iPower.IRMP.SysMgr.Engine.Persistence
{
	///<summary>
	///SysMgrWebPartPropertyEntityʵ���ࡣ
	///</summary>
	internal class SysMgrWebPartPropertyEntity: DbModuleEntity<SysMgrWebPartProperty>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public SysMgrWebPartPropertyEntity()
		{

		}
		#endregion

        /// <summary>
        /// ��ȡ�������ԡ�
        /// </summary>
        /// <param name="webPartID"></param>
        /// <returns></returns>
        public List<WebPartProperty> GetWebPartProperty(GUIDEx webPartID)
        {
            const string sql = "exec spSysMgrWebPartPropertyListView '{0}'";
            if (webPartID.IsValid)
            {
                DataTable dtSource = this.DatabaseAccess.ExecuteDataset(string.Format(sql, webPartID)).Tables[0];
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    List<WebPartProperty> list = new List<WebPartProperty>();
                    foreach (DataRow row in dtSource.Rows)
                    {
                        WebPartProperty p = new WebPartProperty();
                        p.PropertyID = new GUIDEx(row["PropertyID"]);
                        p.PropertyName = Convert.ToString(row["PropertyName"]);
                        p.PropertyValue = Convert.ToString(row["PropertyValue"]);
                        p.PropertyDescription = Convert.ToString(row["PropertyDescription"]);
                        list.Add(p);
                    }
                    return list;
                }
            }
            return null;
        }

        /// <summary>
        /// ɾ�����ݡ�
        /// </summary>
        /// <param name="webPartID">����ID��</param>
        /// <returns></returns>
        public bool DeleteRecord(GUIDEx webPartID)
        {
            return base.DeleteRecord(string.Format("WebPartID = '{0}'", webPartID));
        }
	}
    /// <summary>
    /// WebPart�����ࡣ
    /// </summary>
    [Serializable]
    public class WebPartProperty
    {
        /// <summary>
        /// ��ȡ����������ID��
        /// </summary>
        public GUIDEx PropertyID { get; set; }
        /// <summary>
        /// ��ȡ�������������ơ�
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// ��ȡ����������ֵ��
        /// </summary>
        public string PropertyValue { get; set; }
        /// <summary>
        /// ��ȡ������������
        /// </summary>
        public string PropertyDescription { get; set; }

    }

}
