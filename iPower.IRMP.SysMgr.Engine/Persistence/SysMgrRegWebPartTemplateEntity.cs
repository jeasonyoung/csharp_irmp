//================================================================================
// FileName: SysMgrRegWebPartTemplateEntity.cs
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
	///SysMgrRegWebPartTemplateEntity实体类。
	///</summary>
	internal class SysMgrRegWebPartTemplateEntity: DbModuleEntity<SysMgrRegWebPartTemplate>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public SysMgrRegWebPartTemplateEntity()
		{

		}
		#endregion

        /// <summary>
        /// 列表数据
        /// </summary>
        /// <param name="WebPartTemplateName">部件名称</param>
        /// <returns></returns>
        public DataTable ListDataSource(string WebPartTemplateName)
        {
            return this.GetAllRecord(string.Format("WebPartTemplateName like '%{0}%'", WebPartTemplateName));
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="WebPartTemplateID">ID</param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool DeleteSysMgrRegWebPartTemplate(GUIDEx WebPartTemplateID, out string err)
        {
            const string sql = "exec spSysMgrDeleteRegWebPartTemplate '{0}'";
            err = null;
            if (WebPartTemplateID.IsValid)
            {
                string result = this.DatabaseAccess.ExecuteScalar(string.Format(sql, WebPartTemplateID)).ToString();
                string[] array = result.Split('|');
                err = array[1];
                return array[0] == "0";
            }
            return false;
        }
        /// <summary>
        /// WebPartTemplatePicker
        /// </summary>
        /// <param name="WebPartTemplateName"></param>
        /// <returns></returns>
        public IListControlsData WebPartTemplatePicker(string WebPartTemplateName)
        {
            return new ListControlsDataSource("WebPartTemplateName", "WebPartTemplateID", this.GetAllRecord(string.Format( " WebPartTemplateName like '%{0}%'", WebPartTemplateName)));
        }
	}

}
