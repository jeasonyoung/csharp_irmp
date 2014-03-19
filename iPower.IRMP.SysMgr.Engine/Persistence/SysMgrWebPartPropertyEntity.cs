//================================================================================
// FileName: SysMgrWebPartPropertyEntity.cs
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
	///SysMgrWebPartPropertyEntity实体类。
	///</summary>
	internal class SysMgrWebPartPropertyEntity: DbModuleEntity<SysMgrWebPartProperty>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public SysMgrWebPartPropertyEntity()
		{

		}
		#endregion

        /// <summary>
        /// 获取部件属性。
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
        /// 删除数据。
        /// </summary>
        /// <param name="webPartID">部件ID。</param>
        /// <returns></returns>
        public bool DeleteRecord(GUIDEx webPartID)
        {
            return base.DeleteRecord(string.Format("WebPartID = '{0}'", webPartID));
        }
	}
    /// <summary>
    /// WebPart属性类。
    /// </summary>
    [Serializable]
    public class WebPartProperty
    {
        /// <summary>
        /// 获取或设置属性ID。
        /// </summary>
        public GUIDEx PropertyID { get; set; }
        /// <summary>
        /// 获取或设置属性名称。
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// 获取或设置属性值。
        /// </summary>
        public string PropertyValue { get; set; }
        /// <summary>
        /// 获取或设置描述。
        /// </summary>
        public string PropertyDescription { get; set; }

    }

}
