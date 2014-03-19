//================================================================================
// FileName: SysMgrWebPartZonePresenter.cs
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
	/// ISysMgrWebPartZoneView�ӿڡ�
	///</summary>
	public interface ISysMgrWebPartZoneView: IModuleView
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
	public interface ISysMgrWebPartZoneListView : ISysMgrWebPartZoneView
    {
        /// <summary>
        /// ����λ������
        /// </summary>
        string ZoneName { get; }
    }
    /// <summary>
    /// ����Ӧ��ϵͳ
    /// </summary>
    public interface ISysMgrWebPartZonePickerView : ISysMgrWebPartZoneView
    {
        /// <summary>
        /// ��ȡ����Ӧ��ϵͳID��
        /// </summary>
        GUIDEx ZoneID { get; }
        /// <summary>
        /// ��ȡ����Ӧ��ϵͳ���ơ�
        /// </summary>
        string ZoneName { get; }
        /// <summary>
        /// �����ݡ�
        /// </summary>
        /// <param name="data"></param>
        void BindWebPartZone(IListControlsData data);
    }
    /// <summary>
    /// �༭����ӿ�
    /// </summary>
    public interface ISysMgrWebPartZoneEditView : ISysMgrWebPartZoneView
    {
        /// <summary>
        /// ����ID
        /// </summary>
        GUIDEx ZoneID { get; }

        /// <summary>
        /// AppAuthID
        /// </summary>
        string AppAuthID { get; }

        /// <summary>
        /// ��ʾģʽ
        /// </summary>
        /// <param name="data"></param>
        void BindZoneMode(IListControlsData data);
    }
	///<summary>
	/// SysMgrWebPartZonePresenter��Ϊ�ࡣ
	///</summary>
	public class SysMgrWebPartZonePresenter: ModulePresenter<ISysMgrWebPartZoneView>
	{
		#region ��Ա���������캯����
        SysMgrWebPartZoneEntity sysMgrWebPartZoneEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SysMgrWebPartZonePresenter(ISysMgrWebPartZoneView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.WebPartZone_ModuleID;
            this.sysMgrWebPartZoneEntity = new SysMgrWebPartZoneEntity();
            this.sysMgrWebPartZoneEntity.DbEntityDataChangeLogEvent += this.CreateCommonLog;
		}
		#endregion

		#region ���ݲ���������
        /// <summary>
        /// �б�����Դ
        /// </summary>
        public DataTable ListDataSource
        {
            get
            {
                ISysMgrWebPartZoneListView ListView = this.View as ISysMgrWebPartZoneListView;
                if (ListView != null)
                {
                    DataTable dtSource = this.sysMgrWebPartZoneEntity.ListDataSource(ListView.ZoneName);
                    dtSource.Columns.Add("ZoneModeName");
                    string strSystemName = string.Empty;
                    foreach (DataRow row in dtSource.Rows)
                    {
                        row["ZoneModeName"] = this.GetEnumMemberName(typeof(EnumZoneMode), Convert.ToInt32(row["ZoneMode"]));
                        strSystemName = Convert.ToString(row["SystemName"]);
                        if (string.IsNullOrEmpty(strSystemName))
                            row["SystemName"] = "[ȫ��]";
                    }
                    return dtSource;
                }
                return null;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            ISysMgrWebPartZoneEditView editview = this.View as ISysMgrWebPartZoneEditView;
            if (editview != null)
            {
                editview.BindZoneMode(this.EnumDataSource(typeof(EnumZoneMode)));
                return;
            }
            ISysMgrWebPartZonePickerView pickerView = this.View as ISysMgrWebPartZonePickerView;
            if (pickerView != null && pickerView.ZoneID.IsValid)
            {
                SysMgrWebPartZone data = new SysMgrWebPartZone();
                data.ZoneID = pickerView.ZoneID;
                if (this.sysMgrWebPartZoneEntity.LoadRecord(ref data))
                {
                    List<SysMgrWebPartZone> list = new List<SysMgrWebPartZone>();
                    list.Add(data);
                    pickerView.BindWebPartZone(new ListControlsDataSource("ZoneName", "ZoneID", list));
                }
            }
        }
		///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<SysMgrWebPartZone>> handler)
		{
            ISysMgrWebPartZoneEditView editview = this.View as ISysMgrWebPartZoneEditView;
            if (editview != null && editview.ZoneID.IsValid)
            {
                SysMgrWebPartZone data = new SysMgrWebPartZone();
                data.ZoneID = editview.ZoneID;
                if (this.sysMgrWebPartZoneEntity.LoadRecord(ref data))
                {
                    SysMgrAppAuthorization sysMgrAppAuthorization = new SysMgrAppAuthorization();
                    sysMgrAppAuthorization.AppAuthID = data.AppAuthID;
                    if (new SysMgrAppAuthorizationEntity().LoadRecord(ref sysMgrAppAuthorization))
                    {
                        data.SystemName = sysMgrAppAuthorization.SystemName;
                    }
                    handler(this, new EntityEventArgs<SysMgrWebPartZone>(data));
                }
            }
		}
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateSysMgrWebPartZone(SysMgrWebPartZone data)
        {
            ISysMgrWebPartZoneEditView editview = this.View as ISysMgrWebPartZoneEditView;
            if (editview != null && data != null)
            {
                try
                {
                    return this.sysMgrWebPartZoneEntity.UpdateRecord(data);
                }
                catch (Exception e)
                {
                    editview.ShowMessage(e.Message);
                }
            }
            return false;
        }

		#endregion

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteSysMgrWebPartZone(StringCollection priCollection)
        {
            bool result = false;
            if (priCollection != null && priCollection.Count > 0)
            {
                string err = null;
                foreach (string p in priCollection)
                {
                    result = this.sysMgrWebPartZoneEntity.DeleteSysMgrWebPartZone(p, out err);
                    if (!result && !string.IsNullOrEmpty(err))
                    {
                        this.View.ShowMessage(err);
                        break;
                    }
                }
            }
            return result;
        }
        #region Picker����
        /// <summary>
        /// Picker������
        /// </summary>
        public void PickerSearch()
        {
            ISysMgrWebPartZonePickerView pickerView = this.View as ISysMgrWebPartZonePickerView;
            if (pickerView != null)
            {
                pickerView.BindWebPartZone((this.sysMgrWebPartZoneEntity.WebPartZonePicker(pickerView.ZoneName)));

            }
        }
        #endregion
	}

}
