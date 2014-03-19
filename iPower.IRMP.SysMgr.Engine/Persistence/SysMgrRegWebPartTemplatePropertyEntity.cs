//================================================================================
// FileName: SysMgrRegWebPartTemplatePropertyEntity.cs
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
	///SysMgrRegWebPartTemplatePropertyEntityʵ���ࡣ
	///</summary>
	internal class SysMgrRegWebPartTemplatePropertyEntity: DbModuleEntity<SysMgrRegWebPartTemplateProperty>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public SysMgrRegWebPartTemplatePropertyEntity()
		{

		}
		#endregion

        /// <summary>
        /// ��ȡģ��ID�µ�ģ���������ݡ�
        /// </summary>
        /// <param name="webPartTemplateID">ģ��ID��</param>
        /// <returns></returns>
        public List<SysMgrRegWebPartTemplateProperty> GetAllRecord(GUIDEx webPartTemplateID)
        {
            if (webPartTemplateID.IsValid)
            {
                DataTable dtSource = this.GetAllRecord(string.Format("WebPartTemplateID = '{0}'", webPartTemplateID), "TemplatePropertyName");
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    List<SysMgrRegWebPartTemplateProperty> list = new List<SysMgrRegWebPartTemplateProperty>();
                    foreach (DataRow row in dtSource.Rows)
                    {
                        list.Add(this.Assignment(row));
                    }
                    return list;
                }
            }
            return null;
        }
        /// <summary>
        /// ɾ�����ݡ�
        /// </summary>
        /// <param name="webPartTemplateID">ģ��ID��</param>
        /// <returns></returns>
        public bool DeleteRecord(GUIDEx webPartTemplateID)
        {
            return base.DeleteRecord(string.Format("WebPartTemplateID = '{0}' and (TemplatePropertyID not in (select TemplatePropertyID from tblSysMgrWebPartProperty))", webPartTemplateID));
        }
	}

}
