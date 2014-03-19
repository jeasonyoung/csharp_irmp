//================================================================================
// FileName: SysMgrSettingEntity.cs
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
	///SysMgrSettingEntityʵ���ࡣ
	///</summary>
	internal class SysMgrSettingEntity: DbModuleEntity<SysMgrSetting>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public SysMgrSettingEntity()
		{

		}
		#endregion

        /// <summary>
        /// �б�����Դ
        /// </summary>
        /// <param name="SystemName">ϵͳ����</param>
        /// <returns></returns>
        public DataTable ListDataSource(string SystemName)
        {
            const string sql = "exec spSysMgrSettingListView '{0}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql, SystemName)).Tables[0].Copy();
        }

        /// <summary>
        /// ɾ����Ȩ�û���
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
        ///  Picker����
        /// </summary>
        /// <param name="SettingSign">���÷���</param>
        /// <returns></returns>
        public IListControlsData SettingPicker(string SettingSign)
        {
            return new ListControlsDataSource("SettingSign", "SettingID", this.GetAllRecord(string.Format("SettingSign like '%{0}%'", SettingSign)));
        }
	}

}
