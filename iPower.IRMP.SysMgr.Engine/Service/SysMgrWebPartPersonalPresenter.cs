//================================================================================
// FileName: SysMgrWebPartPersonalPresenter.cs
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
using iPower.Platform.Engine.Service;
using iPower.Platform.Engine.DataSource;
using iPower.IRMP.SysMgr.Engine.Domain;
using iPower.IRMP.SysMgr.Engine.Persistence;
namespace iPower.IRMP.SysMgr.Engine.Service
{
	///<summary>
	/// ISysMgrWebPartPersonalView�ӿڡ�
	///</summary>
	public interface ISysMgrWebPartPersonalView: IModuleView
	{
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="msg"></param>
        void ShowMessage(string msg);
	}
    /// <summary>
    /// �б����ӿ�
    /// </summary>
    public interface ISysMgrWebPartPersonalListView : ISysMgrWebPartPersonalView
    {
        /// <summary>
        /// ��������
        /// </summary>
        string WebPartName { get; }
    }
    /// <summary>
    /// �༭����ӿ�
    /// </summary>
    public interface ISysMgrWebPartPersonalEditView : ISysMgrWebPartPersonalView
    {
        /// <summary>
        /// 
        /// </summary>
        GUIDEx PersonalWebPartID { get; }
    }
	///<summary>
	/// SysMgrWebPartPersonalPresenter��Ϊ�ࡣ
	///</summary>
	public class SysMgrWebPartPersonalPresenter: ModulePresenter<ISysMgrWebPartPersonalView>
	{
		#region ��Ա���������캯����
        SysMgrWebPartPersonalEntity sysMgrWebPartPersonalEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SysMgrWebPartPersonalPresenter(ISysMgrWebPartPersonalView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.WebPartPersonal_ModuleID;
            this.sysMgrWebPartPersonalEntity = new SysMgrWebPartPersonalEntity();
            this.sysMgrWebPartPersonalEntity.DbEntityDataChangeLogEvent += this.CreateCommonLog;
		}
		#endregion

		#region ���ݲ���������
        /// <summary>
        /// ��ҳ�б�����Դ
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                ISysMgrWebPartPersonalListView ListView = this.View as ISysMgrWebPartPersonalListView;
                if (ListView != null)
                {
                    DataTable dtSource = this.sysMgrWebPartPersonalEntity.ListDataSource(ListView.WebPartName);
                    if (dtSource != null)
                    {
                        string strEmployeeName = string.Empty;
                        dtSource.Columns.Add("WebPartStatusName");
                        foreach (DataRow row in dtSource.Rows)
                        {
                            row["WebPartStatusName"] = this.GetEnumMemberName(typeof(EnumWebPartStatus), Convert.ToInt32(row["WebPartStatus"]));
                            strEmployeeName = Convert.ToString(row["EmployeeName"]);
                            if(string.IsNullOrEmpty(strEmployeeName))
                                row["EmployeeName"] = "[ȫ��]";
                        }
                        return dtSource;
                    }
                }
                return null;
            }
        }
		///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<SysMgrWebPartPersonal>> handler)
		{
            ISysMgrWebPartPersonalEditView editview = this.View as ISysMgrWebPartPersonalEditView;
            if (editview != null && editview.PersonalWebPartID.IsValid)
            {
                SysMgrWebPartPersonal data = new SysMgrWebPartPersonal();
                data.PersonalWebPartID = editview.PersonalWebPartID;
                if (this.sysMgrWebPartPersonalEntity.LoadRecord(ref data))
                {
                    SysMgrWebPart sysMgrWebPart = new SysMgrWebPart();
                    sysMgrWebPart.WebPartID = data.WebPartID;
                    if (new SysMgrWebPartEntity().LoadRecord(ref sysMgrWebPart))
                    {
                        data.WebPartName = sysMgrWebPart.WebPartName;
                    }
                    SysMgrWebPartZone sysMgrWebPartZone = new SysMgrWebPartZone();
                    sysMgrWebPartZone.ZoneID = data.ZoneID;
                    if (new SysMgrWebPartZoneEntity().LoadRecord(ref sysMgrWebPartZone))
                    {
                        data.ZoneName = sysMgrWebPartZone.ZoneName;
                    }
                    handler(this, new EntityEventArgs<SysMgrWebPartPersonal>(data));
                }
            }
		}

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateSysMgrWebPartPersonal(SysMgrWebPartPersonal data)
        {
            if (data != null)
            {
                try
                {
                    return this.sysMgrWebPartPersonalEntity.UpdateRecord(data);
                }
                catch (Exception e)
                {
                    this.View.ShowMessage(e.Message);
                }
            }
            return false;
        }
        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteSysMgrWebPartPersonal(StringCollection priCollection)
        {
            bool result = false;
            if (priCollection != null && priCollection.Count > 0)
            {
                try
                {
                    return this.sysMgrWebPartPersonalEntity.DeleteRecord(priCollection);
                }
                catch (Exception e)
                {
                    this.View.ShowMessage(e.Message);
                }
            }
            return result;
        }
		#endregion

	}

}
