//================================================================================
// FileName: SysMgrRegWebPartTemplatePropertyEntity.cs
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
	///SysMgrRegWebPartTemplatePropertyEntity实体类。
	///</summary>
	internal class SysMgrRegWebPartTemplatePropertyEntity: DbModuleEntity<SysMgrRegWebPartTemplateProperty>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public SysMgrRegWebPartTemplatePropertyEntity()
		{

		}
		#endregion

        /// <summary>
        /// 获取模板ID下的模板属性数据。
        /// </summary>
        /// <param name="webPartTemplateID">模板ID。</param>
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
        /// 删除数据。
        /// </summary>
        /// <param name="webPartTemplateID">模板ID。</param>
        /// <returns></returns>
        public bool DeleteRecord(GUIDEx webPartTemplateID)
        {
            return base.DeleteRecord(string.Format("WebPartTemplateID = '{0}' and (TemplatePropertyID not in (select TemplatePropertyID from tblSysMgrWebPartProperty))", webPartTemplateID));
        }
	}

}
