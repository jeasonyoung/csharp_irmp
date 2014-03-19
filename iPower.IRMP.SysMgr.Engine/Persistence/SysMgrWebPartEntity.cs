//================================================================================
// FileName: SysMgrWebPartEntity.cs
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
	///SysMgrWebPartEntityʵ���ࡣ
	///</summary>
	internal class SysMgrWebPartEntity: DbModuleEntity<SysMgrWebPart>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public SysMgrWebPartEntity()
		{

        }
        #endregion

        /// <summary>
        /// �б�����Դ
        /// </summary>
        /// <param name="WebPartName">��������</param>
        /// <returns></returns>
        public DataTable ListDataSource(string WebPartName)
        {
            const string sql = "exec spSysMgrWebPartListView '{0}'";
            return this.DatabaseAccess.ExecuteDataset(string.Format(sql,WebPartName)).Tables[0].Copy();
        }
        /// <summary>
        /// ����ɾ��ϵͳ��������
        /// </summary>
        /// <param name="WebPartID"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool DeleteWebPart(GUIDEx WebPartID, out string err)
        {
            const string sql = "exec spSysMgrDeleteWebPart '{0}'";
            err = null;
            if (WebPartID.IsValid)
            {
                //new SysMgrWebPartPropertyEntity().DeleteRecord(WebPartID);
                string result = this.DatabaseAccess.ExecuteScalar(string.Format(sql, WebPartID)).ToString();
                string[] array = result.Split('|');
                err = array[1];
                return array[0] == "0";
            }
            return false;
        }
        /// <summary>
        /// PickerWebPart
        /// </summary>
        /// <param name="WebPartName"></param>
        /// <returns></returns>
        public IListControlsData WebPartPicker(string WebPartName)
        {
            return new ListControlsDataSource("WebPartName", "WebPartID", this.GetAllRecord(string.Format("WebPartName like '%{0}%'",WebPartName)));
        }
	}

}
