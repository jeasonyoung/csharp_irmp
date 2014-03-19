//================================================================================
// FileName: SysMgrSettingEntity.cs
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
	///SysMgrSettingEntity实体类。
	///</summary>
	internal class SysMgrSettingEntity: DbModuleEntity<SysMgrSetting>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public SysMgrSettingEntity()
		{

		}
		#endregion

        /// <summary>
        /// 列表数据源
        /// </summary>
        /// <param name="SystemName">系统名称</param>
        /// <returns></returns>
        public DataTable ListDataSource(string SystemName)
        {
            const string sql = "exec spSysMgrSettingListView '{0}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, SystemName)).Tables[0].Copy();
        }

        /// <summary>
        /// 删除授权用户。
        /// </summary>
        /// <param name="SettingID"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool DeleteSysMgrSetting(GUIDEx SettingID, out string err)
        {
            const string sql = "exec spSysMgrDeleteSetting '{0}'";
            err = null;
            if (SettingID.IsValid)
            {
                string result = this.DatabaseAccess.ExecuteScalar(string.Format(sql, SettingID)).ToString();
                string[] array = result.Split('|');
                err = array[1];
                return array[0] == "0";
            }
            return false;
        }

        /// <summary>
        ///  Picker部件
        /// </summary>
        /// <param name="SettingSign">配置符号</param>
        /// <returns></returns>
        public IListControlsData SettingPicker(string SettingSign)
        {
            return new ListControlsDataSource("SettingSign", "SettingID", this.GetAllRecord(string.Format("SettingSign like '%{0}%'", SettingSign)));
        }
	}

}
