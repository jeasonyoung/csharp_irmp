//================================================================================
// FileName: SysMgrWebPartZoneEntity.cs
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
	///SysMgrWebPartZoneEntity实体类。
	///</summary>
	internal class SysMgrWebPartZoneEntity: DbModuleEntity<SysMgrWebPartZone>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public SysMgrWebPartZoneEntity()
		{

		}
		#endregion

        /// <summary>
        /// 部件位置定义列表
        /// </summary>
        /// <param name="ZoneName"></param>
        /// <returns></returns>
        public DataTable ListDataSource(string ZoneName)
        {
            const string sql = "exec spSysMgrWebPartZoneListView '{0}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, ZoneName)).Tables[0].Copy();
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ZoneID"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool DeleteSysMgrWebPartZone(GUIDEx ZoneID, out string err)
        {
            const string sql = "exec spSysMgrDeleteWebPartZone '{0}'";
            err = null;
            if (ZoneID.IsValid)
            {
                string result = this.DatabaseAccess.ExecuteScalar(string.Format(sql, ZoneID)).ToString();
                string[] array = result.Split('|');
                err = array[1];
                return array[0] == "0";
            }
            return false;
        }
        /// <summary>
        /// Picker。
        /// </summary>
        /// <param name="ZoneName"></param>
        /// <returns></returns>
        public IListControlsData WebPartZonePicker(string ZoneName)
        {
            return new ListControlsDataSource("ZoneName", "ZoneID", this.GetAllRecord(string.Format("ZoneName like '%{0}%'", ZoneName)));
        }
	}

}
