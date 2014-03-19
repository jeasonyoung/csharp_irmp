//================================================================================
// FileName: SysMgrRegWebPartTemplateEntity.cs
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
	///SysMgrRegWebPartTemplateEntityʵ���ࡣ
	///</summary>
	internal class SysMgrRegWebPartTemplateEntity: DbModuleEntity<SysMgrRegWebPartTemplate>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public SysMgrRegWebPartTemplateEntity()
		{

		}
		#endregion

        /// <summary>
        /// �б�����
        /// </summary>
        /// <param name="WebPartTemplateName">��������</param>
        /// <returns></returns>
        public DataTable ListDataSource(string WebPartTemplateName)
        {
            return this.GetAllRecord(string.Format("WebPartTemplateName like '%{0}%'", WebPartTemplateName));
        }
        /// <summary>
        /// ����ɾ������
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
